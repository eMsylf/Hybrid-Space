using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	public float speed;
	public bool active;
	public GameObject platformPrefab;
	public Camera cam;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// only move player when colliding with platform
	void OnCollisionStay(Collision c)
	{
		if (active)
		{
			rb.AddForce(Vector3.left * speed);
		}
	}

	public void EnableMovement()
	{
		GameManager.instance.SpawnPlatforms();
		
		active = true;
		rb.AddForce(Vector3.left * speed);
	}
	
}
