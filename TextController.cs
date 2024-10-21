using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    Answer answer;

    public Raycast_Mouseover raycast_Mouseover;

    public PlayerMove playerMove;

    public string[] sentences; // 文章を格納する
    [SerializeField] TextMeshProUGUI uiText;   // uiTextへの参照
    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1文字の表示にかける時間

    public int currentSentenceNum = 0; //現在表示している文章番号
    public string currentSentence = string.Empty;  // 現在の文字列
    public float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数

    public GameObject TextPanel;

    public bool has_answer = false;

    public bool movieFlag = false;

    public bool talkNow;

    [NonSerialized] public bool Textfade;
    [NonSerialized] public GameObject ActiveObj;

    void Start()
    {
        answer = GameObject.Find("AnswerManager").GetComponent<Answer>();
    }

    void Update()
    {
        if (GetItem1() != null)
        {
            // 文章の表示完了 / 未完了
            if (IsDisplayComplete() && GetItem1().CompareTag("Character") || GetItem1().CompareTag("Search") || GetItem1().CompareTag("MovieObj") || GetItem1().CompareTag("Bed") || GetItem1().CompareTag("stageDoor"))
            {
                //最後の文章ではない & ボタンが押された
                if (!answer.playerWall.activeSelf && sentences!= null && currentSentenceNum < sentences.Length && Input.GetMouseButtonDown(0) && !answer.answerCanvas.activeSelf)
                {
                    playerMove.SetState(PlayerMove.State.Talk);
                    SetNextSentence();
                    TextPanel.SetActive(true);
                    talkNow = true;

                    if (GetItem1().CompareTag("lastObj") || GetItem1().CompareTag("MovieObj") || GetItem1().CompareTag("Bed") || GetItem1().CompareTag("stageDoor"))
                    {
                        has_answer = true;
                    }

                }
                else if (talkNow && !answer.answerCanvas.activeSelf && currentSentenceNum == sentences.Length && Input.GetMouseButtonDown(0))//最期の文章で右クリックした場合
                {
                    if (has_answer)
                    {
                        answer.AnswerCanvasOn();
                    }
                    else
                    {
                        RestCurent();
                    }

                }
            }
            else
            {
                //ボタンが押された
                if (Input.GetMouseButtonDown(0))
                {
                    timeUntilDisplay = 0; //※1
                }
            }

            //表示される文字数を計算
            int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
            //表示される文字数が表示している文字数と違う
            if (displayCharCount != lastUpdateCharCount)
            {
                uiText.text = currentSentence.Substring(0, displayCharCount);
                //表示している文字数の更新
                lastUpdateCharCount = displayCharCount;
            }
        }

        if (answer.answerCanvas.activeSelf)
        {

        }

        if (movieFlag)
        {
            if (IsDisplayComplete())
            {
                if(currentSentenceNum < sentences.Length && Input.GetMouseButtonDown(0) || currentSentenceNum == 0)
                {
                    playerMove.SetState(PlayerMove.State.Talk);
                    SetNextSentence();
                    TextPanel.SetActive(true);
                }
                else if(currentSentenceNum == sentences.Length && Input.GetMouseButtonDown(0))
                {
                    playerMove.SetState(PlayerMove.State.Normal);
                    TextPanel.SetActive(false);
                    currentSentenceNum = 0;
                    sentences = null;
                    uiText.text = null;
                    if (Textfade)
                    {
                        StartCoroutine("TextFinishFade");
                    }
                    movieFlag = false;
                }
            }

            //表示される文字数を計算
            int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
            //表示される文字数が表示している文字数と違う
            if (displayCharCount != lastUpdateCharCount)
            {
                uiText.text = currentSentence.Substring(0, displayCharCount);
                //表示している文字数の更新
                lastUpdateCharCount = displayCharCount;
            }
        }
    }

    public IEnumerator TextFinishFade()
    {
        playerMove.SetState(PlayerMove.State.Talk);
        FadeManager.Instance.fadeOut();

        yield return new WaitForSeconds(2);

        GameManager.Instance.ChangeYScale();

        FadeManager.Instance.fadeIn();
        playerMove.SetState(PlayerMove.State.Normal);
        Textfade = false;
    }


    private GameObject GetItem1()
    {
        if (raycast_Mouseover.item1 != null)
        {
            return raycast_Mouseover.item1;
        }
        else
        {
            return null;
        }
    }

    // 次の文章をセットする
    public void SetNextSentence()
    {
        currentSentence = sentences[currentSentenceNum];
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay; //※2
    }

    public void ReceiveStringArray(string[] stringArray, string name)
    {
        sentences = stringArray;
        nameText.text = name;
    }

    public void RestCurent()
    {
        timeUntilDisplay = 0;
        currentSentenceNum = 0;
        nameText.text = null;
        uiText.text = null;
        sentences = null;
        has_answer = false;
        talkNow = false;
        playerMove.SetState(PlayerMove.State.Normal);
        TextPanel.SetActive(false);
    }


}