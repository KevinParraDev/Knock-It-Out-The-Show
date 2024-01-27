using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    [SerializeField]
    private float _timeRemaining = 5.0f;

    [SerializeField]
    private float _killerZone = 1.0f;

    private float _initialTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        _initialTimeRemaining = _timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_timeRemaining <= 0.2 || _timeRemaining >= 0)
        //{
        //    if ((transform.position - PlayerOne.Instance.transform.position).magnitude < 2)
        //    {
        //        PlayerOne.Instance.Death();
        //    }
        //}
        //else if(_timeRemaining < 0)
        //{
        //    gameObject.SetActive(false);
        //    _timeRemaining = _initialTimeRemaining;
        //}
        //else
        //{
        //    _timeRemaining -= Time.deltaTime;
        //}

        if(_timeRemaining <= 0)
        {
            if ((transform.position - PlayerOne.Instance.transform.position).magnitude < _killerZone && this.isActiveAndEnabled)
            {
                PlayerOne.Instance.Death();
                EventManager.OnPlayerHit?.Invoke();
            }

            gameObject.SetActive(false);
            _timeRemaining = _initialTimeRemaining;
        }
        else
        {
            _timeRemaining -= Time.deltaTime;
        }
    }
}
