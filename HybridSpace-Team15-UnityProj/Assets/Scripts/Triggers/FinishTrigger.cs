using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour {

	public GameObject finishText;

	void OnCollisionEnter(Collision collider)
	{
		finishText.SetActive(true);
	}
}
