using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCheckpointPosition : MonoBehaviour
{
  public void GoToNextCheckPoint(Checkpoint activeCheckpoint)
  {
    transform.position = activeCheckpoint.cameraPosition;
  }
}
