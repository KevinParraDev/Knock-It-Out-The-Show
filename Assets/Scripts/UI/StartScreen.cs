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
            GetComponent<Animator>().SetTrigger("Quitar");
        }
    }
}
