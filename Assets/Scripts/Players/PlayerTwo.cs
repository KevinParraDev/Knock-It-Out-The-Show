using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{

    [SerializeField]
    private GameObject _camera;

    private void Update()
    {
        if (_camera)
        {
            transform.position = _camera.transform.position;
        }
    }

}