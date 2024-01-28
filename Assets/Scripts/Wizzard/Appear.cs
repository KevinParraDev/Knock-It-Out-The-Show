using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] private GameObject objectToAppear;
    [SerializeField] private bool hide;

    public void ApearSomething()
    {
        objectToAppear.SetActive(!hide);
    }

    public void EndAppear()
    {
        gameObject.SetActive(false);
    }
}
