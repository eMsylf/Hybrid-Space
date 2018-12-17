using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeCollision : MonoBehaviour
{
  private Collider coll;

  void Start()
  {
    coll = GetComponent<Collider>();
  }

  public bool IsColliding(GameObject platform)
  {
    return coll.bounds.Intersects(platform.GetComponent<Collider>().bounds);
  }
}
