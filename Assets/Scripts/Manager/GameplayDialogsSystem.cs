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


    private AudioSource audioSource;

    private bool didDialogueStart;
    private int lineIndex;
    private bool nextLine = false;
    private bool zapataTalking;

    public static GameplayDialogsSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // No destruir el LM durante el cambio de escenas
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        zapataTalking = true;
        //StartDialogue();
    }

    private void StartDialogue()
    {
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
                //    audioSource.Play();
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
        zapataTalking = false;

    }

    public void RequestCakeHitDialogue()
    {
        dialogueLines = cakeHitLines;
        StartDialogue();
    }
}
