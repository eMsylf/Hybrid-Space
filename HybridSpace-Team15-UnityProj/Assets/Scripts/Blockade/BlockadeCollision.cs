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
    /*foreach (GameObject platform in platforms)
    {
      if (coll.bounds.Intersects(platform.GetComponent<Collider>().bounds)) return true;
    }*/
    return coll.bounds.Intersects(platform.GetComponent<Collider>().bounds);
  }
}