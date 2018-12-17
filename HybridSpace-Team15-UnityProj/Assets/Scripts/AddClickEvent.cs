using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddClickEvent : MonoBehaviour
{

  void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
  {
    Debug.Log("Level Loaded");
    Debug.Log(scene.name);
    Debug.Log(mode);
  }

}
