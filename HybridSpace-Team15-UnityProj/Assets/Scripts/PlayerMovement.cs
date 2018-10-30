using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	public float speed;
	public bool active;
	public GameObject platformPrefab;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		active = false;
		rb = GetComponent<Rigidbody>();
	}
	
	// only move player when colliding with platform
	void OnCollisionStay(Collision c)
	{
		if (active)
		{
			rb.AddForce(Vector3.left * speed);
			Debug.Log("Should move");
		}
	}

	public void EnableMovement()
	{
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		foreach(GameObject platform in platforms)
		{
			Vector3 pos = platform.transform.position;
			pos.z = 20;
			Instantiate(platformPrefab, pos, Quaternion.identity);
			Destroy(platform);
		}
		
		
		TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
		//active = true;
		//rb.AddForce(Vector3.left * speed);
	}
	
}
