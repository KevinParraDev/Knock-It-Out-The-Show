using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    [SerializeField]
    private float _timeRemaining = 5.0f;

    private float _initialTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        _initialTimeRemaining = _timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeRemaining <= 0)
        {
            gameObject.SetActive(false);
            _timeRemaining = _initialTimeRemaining;
        }
        else
        {
            _timeRemaining -= Time.deltaTime;
        }
    }
}
