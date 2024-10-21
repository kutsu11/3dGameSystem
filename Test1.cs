using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    public string[] talklist2; // 最終的にこれに入れる

    public string currentname; // このスクリプトアタッチしたキャラの名前
    public TextController textController;

    public string[] jptalk;

    public string[] entalk;

    public string jpname;

    public string enname;

    public int previousDayNum;

    public bool NotCharacter;

    public bool DeleteCheck;

    private void Start()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        //コライダーに当たったらテキストを表示
        if (NotCharacter)
        {
            if (other.CompareTag("Player"))
            {
                sendtalklist();
                textController.movieFlag = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (NotCharacter)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                sendtalklist();
                textController.movieFlag = true;
                if (DeleteCheck)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if(GameManager.Instance.dayNum != previousDayNum)
        {
            previousDayNum = GameManager.Instance.dayNum;
            SelectTalk();
        }

        if(GameManager.Instance.language != null)
        {
            SelectNama();
        }
    }



    public void sendtalklist()
    {
        textController.ReceiveStringArray(talklist2, currentname);
    }

    public void SelectNama()
    {
        if(GameManager.Instance.language == "jp")
        {
            currentname = jpname;
        }
        else if(GameManager.Instance.language == "en")
        {
            currentname = enname;
        }
    }

    public void SelectTalk()
    {
        if (GameManager.Instance.language == "jp")
        {
            talklist2 = new string[jptalk.Length];
            Array.Copy(jptalk, talklist2, jptalk.Length);
        }
        else if (GameManager.Instance.language == "en")
        {
            talklist2 = new string[entalk.Length];
            Array.Copy(entalk, talklist2, entalk.Length);
        }
    }
}