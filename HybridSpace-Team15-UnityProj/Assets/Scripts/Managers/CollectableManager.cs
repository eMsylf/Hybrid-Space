using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Medal
{
  BRONZE = 0,
  SILVER = 1,
  GOLD = 2
}

public struct CheckpointResult
{
  public Medal medal;
  public int score;
  public int maxPossibleScore;
  public int level;
}

public class CollectableManager : MonoBehaviour
{

  public static CollectableManager Instance;

  // container for total score in game
  public int Score
  {
    get;
    set;
  }

  private List<CheckpointResult> checkpointResults;
  public List<CheckpointResult> GetCheckpointResults
  {
    get { return checkpointResults; }
  }

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

    checkpointResults = new List<CheckpointResult>();
  }

  public void AddCheckpointResult(CheckpointResult result)
  {
    checkpointResults.Add(result);
  }

}