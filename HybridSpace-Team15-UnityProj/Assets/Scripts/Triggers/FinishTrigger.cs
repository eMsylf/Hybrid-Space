using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour
{

  public GameObject finishText;

  void OnTriggerEnter(Collider collider)
  {
    if (collider.name == "PlayerSimulated")
    {
      finishText.SetActive(true);
      collider.transform.GetComponent<PlayerMovementSimulated>().activeSimulation = false;
      collider.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    if (collider.name == "PlayerControlled")
    {
      finishText.SetActive(true);
      collider.transform.GetComponent<PlayerMovementControl>().activeSimulation = false;
      collider.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

  }
}
