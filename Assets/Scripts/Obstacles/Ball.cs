using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject center;
    [SerializeField] private float boundForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Toco la bola");
            PlayerOne.Instance.Bounse(center.transform.position, boundForce);
            AudioManager.Instance.PlaySound2D("Bounce");
        }
    }
}
