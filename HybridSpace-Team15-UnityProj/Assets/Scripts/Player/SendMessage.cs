using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{

  public void StopMoving()
  {
    if (name == "PlayerControlled")
      GetComponent<PlayerMovementControl>().activeSimulation = false;

    if (name == "PlayerSimulated")
      GetComponent<PlayerMovementSimulated>().activeSimulation = false;
  }
}
