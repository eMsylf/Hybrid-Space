using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSimulated : MonoBehaviour
{
  public float speed;
  public float jumpSpeed;
  public float gravitySpeed;

  public Vector3 collisionSpherePosition;
  public float collisionSphereRadius;
  public LayerMask collisionLayerMask;

  private Vector2 prevPosition;
  private Vector2 newPosition;
  private float timeStuck;

  private Rigidbody rb;
  private GameObject touchingPlatform;
  private bool activeSimulation;
  private Animator animator;
  private string walkingAnimation;
  private string idleAnimation;

  // Use this for initialization
  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();

    walkingAnimation = "BounceAnimation";
    idleAnimation = "Idle";
  }

  private void Update()
  {
    CheckCollision();

    // only move while touching platform
    if (touchingPlatform != null)
    {
      // simulate while simulation is active
      if (activeSimulation)
      {
        // update position
        prevPosition = newPosition;
        rb.AddForce(Vector3.left * speed);
        newPosition = transform.position;

        // if player stuck, reset position
        if (PlayerStuck())
        {
          Debug.Log("PLAYER STUCK");

          GameManager.instance.ResetSimulation();
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(walkingAnimation)) animator.Play(walkingAnimation);
      }

    }

    if (touchingPlatform == null)
    {
      // add gravity
      rb.AddForce(Vector3.down * gravitySpeed);

      if (animator.GetCurrentAnimatorStateInfo(0).IsName(walkingAnimation)) animator.Play(idleAnimation);
      Debug.Log("Not on any platform");
    }
  }

  private void CheckCollision()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position + collisionSpherePosition, collisionSphereRadius, collisionLayerMask);
    if (colliders.Length > 0)
    {
      foreach (Collider c in colliders)
      {
        if (colliders[0].gameObject.tag != "Collectable")
        {
          touchingPlatform = colliders[0].gameObject;
          Debug.Log("<b>Colliding with: </b>" + colliders[0].name);

          return;
        }
      }
    }
    else if (touchingPlatform != null)
    {
      touchingPlatform = null;
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position + collisionSpherePosition, collisionSphereRadius);
  }

  /*private void OnCollisionStay(Collision c)
  {
    if (c.contacts.Length > 0)
    {
      // extract contact point and see if it is on a flat surface
      for (int i = 0; i < c.contacts.Length; i++)
      {
        ContactPoint contact = c.contacts[i];
        if (Vector3.Dot(contact.normal.normalized, Vector3.up) > 0.9)
        {
          touchingPlatform = c.transform.gameObject;
          return;
        }
      }
    }

    touchingPlatform = null;
  }*/

  private void OnCollisionExit(Collision collision)
  {
    // if collision with checkpoint trigger, don't reset touching platform (causes 
    //  player to be immovable)
    if (collision.gameObject.GetComponent<CheckpointTrigger>() != null) return;

    Debug.Log("Exit from: " + collision.gameObject.name);
    touchingPlatform = null;
  }

  public void EnableMovement(bool b)
  {
    if (!b && animator.GetCurrentAnimatorStateInfo(0).IsName(walkingAnimation))
    {
      animator.Play(idleAnimation);
    }
    activeSimulation = b;
  }

  private bool PlayerStuck()
  {
    if ((prevPosition - newPosition).magnitude < 0.05f)
    {
      timeStuck += Time.deltaTime;
      if (timeStuck >= 2f)
      {
        return true;
      }
    }
    else
    {
      timeStuck = 0f;
    }

    return false;
  }

}