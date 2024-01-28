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
            GameManager.Instance.HandleLevelCompleted();
        }
    }
}
