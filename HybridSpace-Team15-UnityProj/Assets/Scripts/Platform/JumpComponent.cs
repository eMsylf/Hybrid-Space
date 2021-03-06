﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
  public int upStrength;

  private int numJumps;
  private float prevXPos;

  void Start()
  {
    numJumps = 0;
    prevXPos = 0f;
  }

  void OnCollisionEnter(Collision collider)
  {
    if (collider.contacts.Length > 0)
    {
      ContactPoint cp = collider.contacts[0];

      // check if player hits jump platform on the top
      if (Vector3.Dot(cp.normal.normalized, Vector3.down) > 0.9)
      {
        float speed = collider.transform.GetComponent<PlayerMovementSimulated>().speed;
        Vector3 force = new Vector3(-1.0f * speed / 4.0f, 1.0f * upStrength, 0.0f); // divide by literal 4, decrease impulse force
        collider.transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        numJumps++;
        if (CheckPlayerStuck(collider.transform.position.x))
        {
          numJumps = 0;
          prevXPos = 0f;
          GameManager.instance.ResetSimulation();
        }
      }
    }

  }

  bool CheckPlayerStuck(float currentXPosition)
  {
    float xDistTraveled = currentXPosition - prevXPos;

    return xDistTraveled <= float.Epsilon && numJumps >= 2;
  }

}