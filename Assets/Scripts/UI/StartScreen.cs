using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject magicWizzard;
    [SerializeField] private DialogSystem dialogSystem;
    [SerializeField] private GameObject botones;
    private int numeroToques;

    public void AparecerMago()
    {
        numeroToques++;
        magicWizzard.SetActive(true);
        dialogSystem.StartDialogue();
    }

    public void AparecerInputs()
    {
        numeroToques++;
        botones.SetActive(true);
    }

    public void ElegirDificultad(int dif)
    {
        Debug.Log("Dificultad: " + dif);

        if (dif == 1)
            GameManager.Instance.twoPlayers = false;
        else if (dif == 2)
            GameManager.Instance.twoPlayers = true;

        botones.SetActive(false);
        AudioManager.Instance.SetVolume(1f, AudioChannel.Sfx);
        AudioManager.Instance.PlaySound2D("RedobleTambores");
        AudioManager.Instance.SetVolume(0.2f, AudioChannel.Sfx);
        GetComponent<Animator>().SetTrigger("QuitarJugadores");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(numeroToques == 0)
                GetComponent<Animator>().SetTrigger("Quitar");
            
        }
    }
}
