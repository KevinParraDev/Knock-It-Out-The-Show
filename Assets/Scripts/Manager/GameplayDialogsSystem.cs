using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayDialogsSystem : MonoBehaviour
{
    [SerializeField] private float typingTime = 0.025f;
    [SerializeField] private float typingTimeLong = 1f;
    [SerializeField] private float dialogueDuration = 3f;

    //[SerializeField] private Animator zapataAnim;

    //[SerializeField] private AudioClip zapatasVoice;
    [SerializeField] private int charsToPlaySound;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField, TextArea(4, 6)] private string[] cakeHitLines;
    [SerializeField, TextArea(4, 6)] private string[] playerOnWaterLines;
    [SerializeField, TextArea(4, 6)] private string[] goalAchieveLines;
    [SerializeField, TextArea(4, 6)] private string[] loserAchieveLines;


    private AudioSource audioSource;

    private bool didDialogueStart;
    private int lineIndex;
    private bool nextLine = false;
    public bool isTalking;

    private void Awake()
    {
        EventManager.OnPlayerHit += RequestCakeHitDialogue;
        EventManager.PlayerOnWater += RequestOnWaterDialogue;
        EventManager.CheckpointAchieve += RequestGoalAchieveDialogue;
        EventManager.LoserAction += RequestLoserAchieveDialogue;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHit -= RequestCakeHitDialogue;
        EventManager.PlayerOnWater -= RequestOnWaterDialogue;
        EventManager.CheckpointAchieve -= RequestGoalAchieveDialogue;
        EventManager.LoserAction -= RequestLoserAchieveDialogue;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTalking = false;
        //StartDialogue();
    }

    private void StartDialogue()
    {
        isTalking = true;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    //private void NextDialogueLine()
    //{
    //    lineIndex++;
    //    if (lineIndex < dialogueLines.Length)
    //    {
    //        StartCoroutine(ShowLine());
    //    }
    //    else
    //    {
    //        didDialogueStart = false;
    //        dialoguePanel.SetActive(false);
    //        zapataTalking = false;
    //    }
    //}

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        //zapataAnim.SetBool("Talking", true);

        int dialogueToShowIndex = Random.Range(0, dialogueLines.Length);

        foreach (char ch in dialogueLines[dialogueToShowIndex])
        {
            if (ch == '►')
            {
                yield return new WaitForSeconds(typingTimeLong);
            }
            else if (ch == '↕')
            {
                nextLine = true;
                //zapataAnim.SetBool("Talking", false);
            }
            else
            {
                dialogueText.text += ch;

                //if (charIndex % charsToPlaySound == 0)
                //{
                //    AudioManager.Instance.PlaySound2D("WizardSpeak");
                //}

                charIndex++;
                yield return new WaitForSeconds(typingTime);
            }
        }

        StartCoroutine(CloseDialogue());
    }

    private IEnumerator CloseDialogue()
    {

        yield return new WaitForSeconds(dialogueDuration);
        dialoguePanel.SetActive(false);
        isTalking = false;

    }

    public void RequestCakeHitDialogue()
    {
        if (!isTalking)
        {
            dialogueLines = cakeHitLines;
            StartDialogue();
        }
    }

    public void RequestOnWaterDialogue()
    {
        if (!isTalking)
        {
            dialogueLines = playerOnWaterLines;
            StartDialogue();
        }
    }

    public void RequestGoalAchieveDialogue()
    {
        if (!isTalking)
        {
            dialogueLines = goalAchieveLines;
            StartDialogue();
        }
    }

    public void RequestLoserAchieveDialogue()
    {
        if (!isTalking)
        {
            dialogueLines = loserAchieveLines;
            StartDialogue();
        }
    }
}
