using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointEvent CheckpointUpdateEvent;

    private void Awake()
    {
        if (CheckpointUpdateEvent == null)
            CheckpointUpdateEvent = new CheckpointEvent();
    }
}

[System.Serializable]
public class CheckpointEvent : UnityEvent<Checkpoint> { }