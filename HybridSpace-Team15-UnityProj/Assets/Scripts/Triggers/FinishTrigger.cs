using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour
{
  void OnTriggerEnter(Collider collider)
  {
    collider.transform.GetComponent<PlayerMovementSimulated>().EnableMovement(false);
    collider.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

    CheckpointScore.instance.OnLevelFinish();

  }
}