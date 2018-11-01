using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour {

	public GameObject finishText;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name != "Player") return;

		finishText.SetActive(true);
		collider.transform.GetComponent<PlayerMovement>().active = false;
		collider.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse); // add force for natural look
	}
}
