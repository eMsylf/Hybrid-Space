using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimpleVersion : MonoBehaviour {

	public float speed;
	public bool active;
	public GameObject platformPrefab;

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
			Debug.Log("Should move");
		}
	}

	public void EnableMovement()
	{
		active = true;
		rb.AddForce(Vector3.left * speed);
	}
	
}
