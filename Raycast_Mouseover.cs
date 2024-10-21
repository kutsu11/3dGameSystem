using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Raycast_Mouseover : MonoBehaviour
{

    public DoorObj doorobj;

    public IronDoorObj ironDoorObj;

    public SuittiEvent suittiEvent;

    public PlayerMove playerMove;

    public StageManager stageManager;

    public HusumaObj husumaobj;

    public TanaObj tanaObj;

    public TextController textController;

    public CountDown countDown;

    public Playerhand playerhand;

    Answer answer;

    [SerializeField] GameObject pickuptext;
    [SerializeField] GameObject usetext;
    [SerializeField] GameObject talktext;
    [SerializeField] GameObject Searchimage;

    public GameObject selectObj1;
    public GameObject selectObj2;
    public GameObject selectObj3;
    public GameObject selectObj4;

    public int stageNum;

    public int previousStageNum;

    // Start is called before the first frame update
    public bool outlineinfo = false;

    public bool doorCheck = false;

    [SerializeField] Item.KindOfItem item;

    public GameObject item1;

    public GameObject sun;

    public GameObject playerSubLight;

    GameObject closheir;

    public bool flash = false;

    public GameObject flashLight;

    public bool yes;

    float rayHitTime = 0.1f;
    float elapsedTime = 0f;
    GameObject previousHitObject;

    public bool GetOutLineInfo()
    {
        return outlineinfo;
    }
    void Start()
    {
        closheir = GameObject.Find("closheir");
        answer = GameObject.Find("AnswerManager").GetComponent<Answer>();
    }

    // Update is called once per frame
    void Update()
    {
        //メインカメラ上のマウスポインタのある位置からrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        float distance = 1.2f;

        int layerMask = ~(1 << LayerMask.NameToLayer("notRay"));


        //もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (hit.collider.gameObject != previousHitObject)
            {
                previousHitObject = hit.collider.gameObject;
                elapsedTime = 0f; // 新しいオブジェクトに当たったので時間をリセット
            }

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= rayHitTime)
            {
                item1 = hit.collider.gameObject;
                elapsedTime = 0f;
            }

            //Rayが当たったオブジェクトのtagがDoorで左クリックしたら
            if (item1 != null && item1.tag == "Door")
            {
                UseImage();

                if (Input.GetMouseButtonDown(0))
                {
                    doorobj = item1.GetComponent<DoorObj>();
                    doorobj.DoorChenge();
                }
                
            }

            if(item1 != null && item1.tag == "Door Rock")
            {
                UseImage();

                if (Input.GetMouseButtonDown(0))
                {
                    doorobj = item1.GetComponent<DoorObj>();
                    doorobj.DoorRock();
                }
            }

            if (item1 != null && item1.tag == "IronDoor")
            {
                UseImage();

                if (Input.GetMouseButtonDown(0))
                {
                    ironDoorObj = item1.GetComponent<IronDoorObj>();
                    ironDoorObj.StartCoroutine("IronDoorChenge");
                }

            }

            //Rayが当たったオブジェクトのtagがitemだったら
            if (item1 != null && item1.tag == "item")
            {
                //outlineinfo = true;
                item1.layer = 13;
                outlineinfo = false;
                Pickupview();


                if (Input.GetMouseButtonDown(0))
                {

                    item1.GetComponent<Obtainable>().Obtain(item1);

                    SoundManager.Instance.PlaySE(SESoundData.SE.pickup);
                }
            }
            else
            {
                outlineinfo = true;
                Pickupviewoff();
            }

            if (item1 != null && item1.tag == "Search")
            {
                Searchview();
                item1.GetComponent<Test1>().sendtalklist();
            }
            else
            {
                Searchviewoff();
            }

            if(item1 != null && item1.tag == "Peper")
            {
                UseImage();
                item1.GetComponent<PeperItem>().SendString();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<PeperItem>().StartCoroutine("PeperCanvasView");
                }
            }

            //Rayが当たったオブジェクトのtagがsuittiだったら
            if (item1 != null && item1.tag == "suitti")
            {
                Useview();
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    suittiEvent = item1.GetComponent<SuittiEvent>();
                    suittiEvent.Lightchenge();
                }
            }
            else
            {
                Useviewoff();
            }


            if (item1 != null && item1.tag == "HandleObj" && playerhand.HandNowObj == null)
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<HandIvent>().GetObj();
                }
            }

            //ここからフラグイベント用
            if (item1 != null && item1.tag == "apateDoor")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<YourScript>().OnConditionChanged();
                    item1.GetComponent<Clicks>().Click();
                }
            }

            if(item1 != null && item1.tag == "mailBox")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<Clicks>().Click();
                    playerhand.ByeObj();
                }
            }

            if(item1 != null && item1.tag == "gakkaimen" && playerhand.HandNowObj != null)
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    playerhand.HandNowObj.GetComponent<YourScript>().OnConditionChanged();
                    playerhand.ByeObj();
                    textController.StartCoroutine("TextFinishFade");
                }
            }
            
            if(item1 != null && item1.tag == "curry")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<YourScript>().OnConditionChanged();
                    item1.GetComponent<Clicks>().Click();
                }
            }

            //ここまでフラグイベント用

            //Rayが当たったオブジェクトのtagがCharacterだったら
            if (item1 != null && item1.tag == "Character")
            {
                Talkview();
                item1.GetComponent<Test1>().sendtalklist();
            }
            else
            {
                Talkviewoff();
            }

            if (item1 != null && item1.tag == "stageDoor")
            {
                UseImage();
                item1.GetComponent<Test1>().sendtalklist();
                if (yes)
                {
;                   item1.GetComponent<StageManager>().StartCoroutine("NextStage");
                    yes = false;

                }

            }

            if(item1 != null && item1.tag == "MovieObj")
            {
                UseImage();
                item1.GetComponent<Test1>().sendtalklist();
                if (yes)
                {
                    item1.GetComponent<MoviePush>().StartCoroutine("PlayMovie");

                    yes = false;
                }
            }

            if(item1 != null && item1.tag == "Bed")
            {
                UseImage();
                item1.GetComponent<Test1>().sendtalklist();
                if (yes)
                {
                    item1.GetComponent<StoryManager>().StartCoroutine("BedDaycount");

                    yes = false;
                }
            }

            if (item1 != null && item1.tag == "ChoiceObj")
            {
                UseImage();
                item1.GetComponent<Test1>().sendtalklist();

                if (Input.GetMouseButtonDown(0))
                {
                    DefaultObj();
                    countDown.CountDownRecet();
                    switch (item1.name)
                    {
                        case "interphone":
                            Debug.Log("interphoneがおされた");
                            Flags.Instance.interphoneFlag = true;
                            break;

                        default:
                            break;
                    }
                }
            }

            if(item1 != null && item1.tag == "lastObj")
            {
                UseImage();
                item1.GetComponent<Test1>().sendtalklist();
                if (yes)
                {
                    item1.GetComponent<LastObj>().StartCoroutine("TitleLoadScene");

                    yes = false;
                }
            }

            if (item1 != null && item1.tag == "Husuma")
            {
                UseImage();

                if (Input.GetMouseButtonDown(0))
                {
                    husumaobj = item1.GetComponent<HusumaObj>();
                    husumaobj.HusumaChenge();
                }
                
            }

            if(item1 != null && item1.tag == "Tana")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    tanaObj = item1.GetComponent<TanaObj>();
                    tanaObj.TanaChenge();
                }
            }

            if(item1 != null && item1.tag == "Storycol")
            {
                item1.GetComponent<ColliderEvent>().Startcollider();
            }

            if(item1 != null && item1.tag == "FlashLight")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    SoundManager.Instance.PlaySE(SESoundData.SE.pickup);
                    item1.GetComponent<FlashLight>().Flash();
                }
   
            }

            if(item1 != null && item1.tag == "rockkey")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<Item_consumption>().itemLost();
                    item1.GetComponent<Rockkey>().KeyOpenSound();
                }
            }

            if(item1 != null && item1.tag == "butudan")
            {
                UseImage();
                if (Input.GetMouseButtonDown(0))
                {
                    item1.GetComponent<Butudan>().StartCoroutine("ChangeObj");
                }
            }

            //if(item1)

            if (item1 != null && item1.tag != "butudan" && item1.tag != "lastObj" && item1.tag != "gakkaimen" && item1.tag != "curry" && item1.tag != "Peper" && item1.tag != "mailBox" && item1.tag != "apateDoor" && item1.tag != "HandleObj" && item1.tag != "Bed" && item1.tag != "MovieObj" && item1.tag != "ChoiceObj" && item1.tag != "suitti" && item1.tag != "Tana" && item1.tag != "Husuma" && item1.tag != "Door" && item1.tag != "stageDoor" && item1.tag != "IronDoor" && item1.tag != "FlashLight" && item1.tag != "rockkey")
            {
                UseImageoff();
            }



        }
        else
        {
            elapsedTime = 0f;
            if (!answer.answerCanvas.activeSelf)
            {
                item1 = null;
            }
            previousHitObject = null;
            Pickupviewoff();
            Useviewoff();
            Talkviewoff();
            Searchviewoff();
            UseImageoff();
        }

        if (flash)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(flashLight.GetComponent<Light>().enabled == false)
                {
                    SoundManager.Instance.PlaySE(SESoundData.SE.flashLight);
                    flashLight.GetComponent<Light>().enabled = true;
                }
                else
                {
                    SoundManager.Instance.PlaySE(SESoundData.SE.flashLight);
                    flashLight.GetComponent<Light>().enabled = false;
                }
            }
        }

        //ステージによってBGMを変える
        if (stageNum != previousStageNum)
        {
            previousStageNum = stageNum;

            switch (stageNum)
            {
                case 1:
                    sun.SetActive(true);
                    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.soto);
                    playerSubLight.SetActive(true);
                    break;

                case 2:
                    sun.SetActive(false);
                    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.room);
                    playerSubLight.SetActive(true);
                    break;

                case 3:
                    sun.SetActive(false);
                    SoundManager.Instance.StopBGM();
                    playerSubLight.SetActive(false);
                    break;
                case 4:
                    sun.SetActive(false);
                    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.gakkai);
                    playerSubLight.SetActive(false);
                    break;

                case 5:
                    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.soto);
                    break;
            }
        }

    }

    public void DefaultObj()
    {
        selectObj1.layer = 0;
        selectObj1.tag = "Untagged";
        selectObj1 = null;

        selectObj2.layer = 0;
        selectObj2.tag = "Untagged";
        selectObj2 = null;

        if (selectObj3 != null)
        {
            selectObj3.layer = 0;
            selectObj3.tag = "Untagged";
            selectObj3 = null;
        }
        if(selectObj4 != null)
        {
            selectObj4.layer = 0;
            selectObj4.tag = "Untagged";
            selectObj4 = null;
        }

    }

    public void Pickupview()
    {
        if (!pickuptext.activeSelf)
        {
            pickuptext.SetActive(true);
        }
    }

    public void Pickupviewoff()
    {
        if (pickuptext.activeSelf)
        {
            pickuptext.SetActive(false);
        }
    }

    public void UseImage()
    {
        Color color = new Color(1f, 0.41f, 0f, 0.7f);
        closheir.GetComponent<Image>().color = color;
    }

    public void UseImageoff()
    {
        Color color = new Color(1f, 1f, 1f, 0.05f);
        closheir.GetComponent<Image>().color = color;
    }

    public void Useview()
    {
        if (!usetext.activeSelf)
        {
            usetext.SetActive(true);
        }
    }

    public void Useviewoff()
    {
        if (usetext.activeSelf)
        {
            usetext.SetActive(false);
        }
    }

    public void Talkview()
    {
        if (!talktext.activeSelf)
        {
            talktext.SetActive(true);
        }
    }

    public void Talkviewoff()
    {
        if (talktext.activeSelf)
        {
            talktext.SetActive(false);
        }
    }

    public void Searchview()
    {
        if (!Searchimage.activeSelf)
        {
            Searchimage.SetActive(true);
        }
    }

    public void Searchviewoff()
    {
        if (Searchimage.activeSelf)
        {
            Searchimage.SetActive(false);
        }
    }
}