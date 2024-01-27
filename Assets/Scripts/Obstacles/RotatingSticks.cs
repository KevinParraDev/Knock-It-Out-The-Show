using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSticks : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private bool _reverse = false;

    private void Start()
    {
        if (_reverse)
        {
            _speed *= -1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 0, 360 * _speed * Time.deltaTime);
    }
}
