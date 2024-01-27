using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private Checkpoint _initialCheckpoint;

    [SerializeField]
    private Transform _endPoint;

    private void Start()
    {
        PlayerOne.Instance.GetCheckpointManager().lastCheckpoint = _initialCheckpoint.transform;
        PlayerOne.Instance.LoadCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Logica de completado con el punto final
        if ((_endPoint.position - PlayerOne.Instance.transform.position).magnitude < 1)
        {
            StopAllCoroutines();
            GameManager.Instance.HandleLevelCompleted();
        }
    }
}
