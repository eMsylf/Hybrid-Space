using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
  public int checkpointID;

  void OnCollisionEnter(Collision collider)
  {
    if (collider.gameObject.name == "PlayerControlled" || collider.gameObject.name == "PlayerSimulated")
    {
      // colliding with current checkpoint --> do nothing
      if (GameManager.instance.ActiveCheckpoint.id == checkpointID)
      {
        return;
      }

      GameManager.instance.NextCheckpoint();
    }
  }
}
