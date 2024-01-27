using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private Checkpoint _initialCheckpoint;

    private void Start()
    {
        PlayerOne.Instance.GetCheckpointManager().lastCheckpoint = _initialCheckpoint.transform;
        PlayerOne.Instance.LoadCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
