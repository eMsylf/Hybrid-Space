using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverReset : MonoBehaviour
{
  public float threshold;

  void Update()
  {
    if (transform.position.y < threshold)
    {
      GetComponent<Rigidbody>().isKinematic = true; // remove momentum
      GameManager.instance.ResetSimulation();
      GetComponent<Rigidbody>().isKinematic = false;
    }
  }

}
