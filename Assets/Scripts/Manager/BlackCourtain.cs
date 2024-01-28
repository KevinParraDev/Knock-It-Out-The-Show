using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCourtain : MonoBehaviour
{
    public bool open;

    public void ResetGame()
    {
        GameManager.Instance.HandleMenu();
    }
}
