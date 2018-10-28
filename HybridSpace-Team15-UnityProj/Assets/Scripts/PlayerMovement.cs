using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	public float speed;

	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
	
	}
	
	// only move player when colliding with platform
	void OnCollisionStay(Collision c)
	{
		rb.AddForce(Vector3.left * speed);
	}
}
