﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private bool showInStart;
    [SerializeField] private float typingTime = 0.025f;
    [SerializeField] private float typingTimeLong = 0.3f;

    [SerializeField] private Animator zapataAnim;

    //[SerializeField] private AudioClip zapatasVoice;
    [SerializeField] private int charsToPlaySound;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject magicJuan, magicWizzard;
    [SerializeField] private Animator blackCourtain;

    private AudioSource audioSource;

    private bool didDialogueStart;
    private int lineIndex;
    private bool nextLine = false;
    private bool zapataTalking;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(showInStart)
            StartDialogue();
    }

    public void StartDialogue()
    {
        zapataTalking = true;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            zapataTalking = false;
            if(!showInStart)
                GameManager.Instance.GameStart();
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        zapataAnim.SetBool("Talking", true);

        foreach (char ch in dialogueLines[lineIndex])
        {
            if (ch == '►')
            {
                yield return new WaitForSeconds(typingTimeLong);
            }
            else if (ch == 'Æ')
            {
                magicJuan.SetActive(true);
            }
            else if (ch == '§')
            {
                //GameManager.Instance.GameStart();
                magicWizzard.SetActive(true);
                magicJuan.SetActive(true);
                AudioManager.Instance.SetVolume(1f, AudioChannel.Sfx);
                AudioManager.Instance.PlaySound2D("Claps");
                AudioManager.Instance.SetVolume(0.2f, AudioChannel.Sfx);
                Debug.Log("Cerrar cortinas");
            }
            else if (ch == '¥')
            {
                AudioManager.Instance.SetVolume(1f, AudioChannel.Sfx);
                AudioManager.Instance.PlaySound2D("Claps");
                AudioManager.Instance.SetVolume(0.2f, AudioChannel.Sfx);
                blackCourtain.SetBool("Abierto", true);
                blackCourtain.enabled = true;
                //GameManager.Instance.HandleMenu();
            }
            else if (ch == '↕')
            {
                nextLine = true;
                zapataAnim.SetBool("Talking", false);
            }
            else
            {
                dialogueText.text += ch;

                if (charIndex % charsToPlaySound == 0)
                {
                    AudioManager.Instance.PlaySound2D("WizardSpeak");
                }

                charIndex++;
                yield return new WaitForSeconds(typingTime);
            }
        }

    }

    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && zapataTalking)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (nextLine == true)
            {
                nextLine = false;
                NextDialogueLine();
            }
        }

    }
}
