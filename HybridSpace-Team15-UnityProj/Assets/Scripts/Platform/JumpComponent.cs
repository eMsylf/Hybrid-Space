using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
  public int upStrength;
  int counter = 0;

  void Start()
  {
    // Debug.Log("Normals:");
    // Vector3[] normals = GetComponent<MeshFilter>().mesh.normals;

    // for (int i = 0; i < normals.Length; i++)
    // {
    //   normals[i].y *= -1.0f;
    // }

    // GetComponent<MeshFilter>().mesh.normals = normals;

  }


  void OnCollisionEnter(Collision collider)
  {
    if (collider.contacts.Length > 0)
    {
      ContactPoint cp = collider.contacts[0];
      if (Vector3.Dot(cp.normal.normalized, Vector3.down) > 0.9)
      {
        float speed = collider.transform.GetComponent<PlayerMovementSimulated>().speed;
        Vector3 force = new Vector3(-1.0f * speed / 4.0f, 1.0f * upStrength, 0.0f); // divide by literal 4, decrease impulse force
        collider.transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
      }
      else
      {
        Debug.Log("collision detected, normals are: ");
        foreach (ContactPoint _cp in collider.contacts)
        {
          Debug.Log(_cp.normal);
        }
      }

    }

  }

}
