using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
  public static SceneTransition Instance;

  // Use this for initialization
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  public void GoToNextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
      GameObject finishText = GameObject.Find("FinishText");
      finishText.GetComponent<Text>().enabled = true;
    }
  }
}
