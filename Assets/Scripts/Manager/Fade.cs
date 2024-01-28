using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public void EndLevel()
    {
        StopAllCoroutines();
        if (GameManager.Instance)
        {
            AudioManager.Instance.PlaySound2D("Claps");
            GameManager.Instance.HandleLevelCompleted();
        }
    }
}
