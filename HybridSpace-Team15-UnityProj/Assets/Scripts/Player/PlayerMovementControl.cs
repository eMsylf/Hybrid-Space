/*using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementControl : MonoBehaviour
{
  public float speed;
  public bool activeSimulation;
  public float jumpSpeed;
  public float gravitySpeed;
  public float jumpForceMultiplier; // multiplier of jump force when on green jump platform

  private Rigidbody rb;
  private int jumpCounter;
  private GameObject touchingPlatform;

  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    jumpCounter = 0;
  }

  void Update()
  {
    if (activeSimulation)
    {
      float x = Input.GetAxis("Horizontal") * speed;
      rb.velocity = new Vector3(x, rb.velocity.y, 0);
    }

    // only move while touching platform
    if (touchingPlatform != null && activeSimulation)
    {
      if (Input.GetButton("Jump") && jumpCounter == 0)
      {
        Vector3 force = Vector3.up * jumpSpeed;
        bool onJumpPlatform = touchingPlatform.GetComponent<JumpComponent>() != null;
        force = onJumpPlatform ? force * jumpForceMultiplier : force;  // multiplier of jump force when on jump platform: force;

        rb.AddForce(force, ForceMode.Impulse);

        jumpCounter++;
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

  public void EnableMovement()
  {
    // put platforms in the right place
    //GameManager.instance.SpawnPlatforms();
    //activeSimulation = true;
  }

}
*/