using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
  public static SceneTransition Instance;

  private int currentLevel;
  private const int NumberOfLevels = 5;

  public bool IsLastLevel { get { return currentLevel == NumberOfLevels; } }
  public int CurrentLevel { get { return currentLevel; } }

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

  void Start()
  {
    string firstLevel = SceneManager.GetActiveScene().name;

    currentLevel = int.Parse("" + firstLevel[(firstLevel.Length - 1)]);
  }

  public void GoToNextLevel()
  {
    currentLevel++;

    if (currentLevel <= NumberOfLevels)
    {
      SceneManager.LoadScene(string.Format("Scenes/Lvl0{0}", currentLevel));
    }
  }
}