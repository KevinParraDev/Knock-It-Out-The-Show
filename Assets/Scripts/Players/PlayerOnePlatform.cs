using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnePlatform : MonoBehaviour
{
    // Para comprobar si esta en una plataforma movible
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    // Para comprobar si ya no esta en plataforma movible
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
        }
    }
}
