using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour
{

  public GameObject finishText;

  void OnTriggerEnter(Collider collider)
  {
    finishText.SetActive(true);
    collider.transform.GetComponent<PlayerMovementSimulated>().EnableMovement(false);
    collider.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
}
