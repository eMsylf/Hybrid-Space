using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
  public int checkpointID;

  void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.tag == "Player")
    {
      // colliding with current checkpoint --> do nothing
      if (GameManager.instance.ActiveCheckpointIndex == checkpointID)
      {
        return;
      }

      GameManager.instance.NextCheckpoint();
    }
  }
}
