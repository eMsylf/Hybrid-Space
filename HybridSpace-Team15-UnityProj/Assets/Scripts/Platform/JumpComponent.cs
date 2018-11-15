using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{

  public int upStrength;

  void OnCollisionEnter(Collision collider)
  {
    if (collider.transform.name == "PlayerControlled")
    {
      float speed = collider.transform.GetComponent<PlayerMovementControl>().speed;
      Vector3 force = new Vector3(-1 * speed, 1 * upStrength, 0);
      collider.transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
    if (collider.transform.name == "PlayerSimulated")
    {
      float speed = collider.transform.GetComponent<PlayerMovementSimulated>().speed;
      Vector3 force = new Vector3(-1.0f * speed / 4.0f, 1.0f * upStrength, 0.0f); // divide by 4, decrease impulse force
      collider.transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
  }

}
