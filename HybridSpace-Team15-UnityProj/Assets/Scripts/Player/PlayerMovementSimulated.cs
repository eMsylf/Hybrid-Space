using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimulated : MonoBehaviour
{
  public float speed;
  public float jumpSpeed;
  public float gravitySpeed;

  private Rigidbody rb;
  private GameObject touchingPlatform;
  private bool activeSimulation;
  private Animator animator;
  private string walkingAnimation;
  private string idleAnimation;

  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();

    walkingAnimation = "BounceAnimation";
    idleAnimation = "Idle";
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(walkingAnimation)) animator.Play(walkingAnimation);
      }

    }

    if (touchingPlatform == null)
    {
      if (animator.GetCurrentAnimatorStateInfo(0).IsName(walkingAnimation)) animator.Play(idleAnimation);
      Debug.Log("Not on any platform");
    }

    // add gravity
    rb.AddForce(Vector3.down * gravitySpeed);
  }

  void OnCollisionStay(Collision c)
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
    }
  }

  void OnCollisionExit(Collision collision)
  {
    // if collision with checkpoint trigger, don't reset touching platform (causes 
    //  player to be immovable)
    if (collision.gameObject.GetComponent<CheckpointTrigger>() != null) return;

    Debug.Log("Exit from: " + collision.gameObject.name);
    touchingPlatform = null;
  }

  public void EnableMovement(bool b)
  {
    activeSimulation = b;
  }

}
