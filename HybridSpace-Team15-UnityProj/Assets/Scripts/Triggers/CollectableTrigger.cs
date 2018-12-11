using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTrigger : MonoBehaviour
{
  void OnTriggerEnter(Collider c)
  {
    if (c.gameObject.tag == "Player")
    {
      CollectableManager.Instance.Score++;
      Destroy(gameObject); // get rid of collectable

      Debug.Log(CollectableManager.Instance.Score);
    }
  }

}
