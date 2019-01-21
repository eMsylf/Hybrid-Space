using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddClickEvent : MonoBehaviour
{
  public bool startEvent;
  public bool resetEvent;

  static GameManager gameManager;

  void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
  {
    Debug.Log("Level Loaded");
    /*Debug.Log(scene.name);
    Debug.Log(mode);*/

    //GameManager.GameManagerRemoved += UnsubscribeOldGameManager;
  }

}