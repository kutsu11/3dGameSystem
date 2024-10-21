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

    public string[] sentences; // ���͂��i�[����
    [SerializeField] TextMeshProUGUI uiText;   // uiText�ւ̎Q��
    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1�����̕\���ɂ����鎞��

    public int currentSentenceNum = 0; //���ݕ\�����Ă��镶�͔ԍ�
    public string currentSentence = string.Empty;  // ���݂̕�����
    public float timeUntilDisplay = 0;     // �\���ɂ����鎞��
    private float timeBeganDisplay = 1;         // ������̕\�����J�n��������
    private int lastUpdateCharCount = -1;       // �\�����̕�����

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
            // ���͂̕\������ / ������
            if (IsDisplayComplete() && GetItem1().CompareTag("Character") || GetItem1().CompareTag("Search") || GetItem1().CompareTag("MovieObj") || GetItem1().CompareTag("Bed") || GetItem1().CompareTag("stageDoor"))
            {
                //�Ō�̕��͂ł͂Ȃ� & �{�^���������ꂽ
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
                else if (talkNow && !answer.answerCanvas.activeSelf && currentSentenceNum == sentences.Length && Input.GetMouseButtonDown(0))//�Ŋ��̕��͂ŉE�N���b�N�����ꍇ
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
                //�{�^���������ꂽ
                if (Input.GetMouseButtonDown(0))
                {
                    timeUntilDisplay = 0; //��1
                }
            }

            //�\������镶�������v�Z
            int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
            //�\������镶�������\�����Ă��镶�����ƈႤ
            if (displayCharCount != lastUpdateCharCount)
            {
                uiText.text = currentSentence.Substring(0, displayCharCount);
                //�\�����Ă��镶�����̍X�V
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

            //�\������镶�������v�Z
            int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
            //�\������镶�������\�����Ă��镶�����ƈႤ
            if (displayCharCount != lastUpdateCharCount)
            {
                uiText.text = currentSentence.Substring(0, displayCharCount);
                //�\�����Ă��镶�����̍X�V
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

    // ���̕��͂��Z�b�g����
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
        return Time.time > timeBeganDisplay + timeUntilDisplay; //��2
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