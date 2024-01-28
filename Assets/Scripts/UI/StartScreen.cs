using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject magicWizzard;
    [SerializeField] private DialogSystem dialogSystem;
    private bool primerToque;

    public void AparecerMago()
    {
        primerToque = true;
        magicWizzard.SetActive(true);
        dialogSystem.StartDialogue();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !primerToque)
        {
            AudioManager.Instance.SetVolume(1f, AudioChannel.Sfx);
            AudioManager.Instance.PlaySound2D("RedobleTambores");
            AudioManager.Instance.SetVolume(0.2f, AudioChannel.Sfx);
            GetComponent<Animator>().SetTrigger("Quitar");
        }
    }
}
