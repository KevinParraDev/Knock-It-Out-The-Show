using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private Checkpoint _initialCheckpoint;

    [SerializeField]
    private Transform _endPoint;

    [SerializeField]
    private Animator _loadAnimation;

    private bool _levelFinished = false;

    private void Start()
    {

        if (_loadAnimation)
        {
            _loadAnimation.SetBool("EndLevel", false);
        }

        PlayerOne.Instance.GetCheckpointManager().lastCheckpoint = _initialCheckpoint.transform;
        PlayerOne.Instance.LoadCheckpoint();

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Logica de completado con el punto final
        if ((_endPoint.position - PlayerOne.Instance.transform.position).magnitude < 1 && !_levelFinished)
        {
            _levelFinished = true;
            _loadAnimation.SetBool("EndLevel", true);
        }
    }
}
