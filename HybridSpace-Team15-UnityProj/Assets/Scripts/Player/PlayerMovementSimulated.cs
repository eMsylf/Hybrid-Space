using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimulated : MonoBehaviour
{
  public float speed;
  public float jumpSpeed;
  public float gravitySpeed;

  private Rigidbody rb;
  private int jumpCounter;
  private GameObject touchingPlatform;
  private bool activeSimulation;

  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    jumpCounter = 0;
  }

  void Update()
  {
    // only move while touching platform
    if (touchingPlatform != null && !touchingPlatform.name.Contains("JumpPlatform"))
    {
      // simulate while simulation is active
      if (activeSimulation)
      {
        rb.AddForce(Vector3.left * speed);
      }

    }

    // add gravity
    rb.AddForce(Vector3.down * gravitySpeed);

  }

  void OnCollisionEnter(Collision c)
  {
    touchingPlatform = c.transform.gameObject;
  }

  void OnCollisionExit(Collision collision)
  {
    touchingPlatform = null;
    jumpCounter = 0;
  }

  public void EnableMovement(bool b)
  {
    activeSimulation = b;
  }

}
