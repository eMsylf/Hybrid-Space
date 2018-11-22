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
    //Debug.Log("Not adding force because touchingplatform == " + touchingPlatform);
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
    if (c.contacts.Length > 0)
    {
      // extract contact point and see if it is on a flat surface
      ContactPoint contact = c.contacts[0];
      if (Vector3.Dot(contact.normal.normalized, Vector3.up) > 0.9)
      {
        touchingPlatform = c.transform.gameObject;
      }
      else
      {
        touchingPlatform = null;
      }
      Debug.Log("Collision detected, normal: " + contact.normal);
    }
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
