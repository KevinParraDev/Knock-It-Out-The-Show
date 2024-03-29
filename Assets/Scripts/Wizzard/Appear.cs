using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] private GameObject objectToAppear;

    public void ApearSomething()
    {
        objectToAppear.SetActive(!objectToAppear.activeSelf);
    }

    public void EndAppear()
    {
        gameObject.SetActive(false);
    }
}
