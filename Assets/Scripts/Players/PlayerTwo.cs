using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{

    [SerializeField]
    private GameObject _camera;

    private static PlayerTwo Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // No destruir el LM durante el cambio de escenas
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_camera)
        {
            transform.position = _camera.transform.position;
        }
    }

}