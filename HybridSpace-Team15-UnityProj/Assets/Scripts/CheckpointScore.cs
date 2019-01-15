using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckpointScore : MonoBehaviour
{
	// we need a singleton because when player wants to retry level, we want to reload the entire scene,
	//  but keep 1 copy of checkpoint score data. We explicitely destroy
	public static CheckpointScore instance;

	private Vector3[] collectablesPosition; // used to respawn collectables on retry
	private GameObject[] collectables; // used to see which collectables are still in the scene
	private int numCollectables;

	public int currentScore;
	private int highestScore;
	private Medal currentMedal;
	private Medal highestMedal;

	// public UI properties...
	private GameObject checkpointPanel;
	private Text numApplesText;
	private Text bestNumApplesText;
	private Image currentMedalImage;
	private Image bestMedalImage;

	public Sprite[] medalSprites;

	// these need to be deactivated
	private GameObject startButton;
	private GameObject resetButton;

	public GameObject applePrefab;
	public int silverScoreMin;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject); // destroy object when player goes to next level
	}

	void Start()
	{
		collectables = GameObject.FindGameObjectsWithTag("Collectable");
		collectablesPosition = new Vector3[collectables.Length];
		for (int i = 0; i < collectablesPosition.Length; i++)
		{
			collectablesPosition[i] = collectables[i].transform.position;
		}
		numCollectables = collectables.Length;
		currentScore = 0;

		currentMedal = Medal.BRONZE;
		highestMedal = Medal.BRONZE;
	}

	public void OnLevelFinish()
	{
		GetUIElements();

		startButton.SetActive(false);
		resetButton.SetActive(false);
		checkpointPanel.SetActive(true);

		// check new highest score
		if (currentScore > highestScore)
		{
			highestScore = currentScore;
			// show some information?
		}

		// set UI text information
		numApplesText.text = string.Format("{0} / {1}", currentScore, numCollectables);
		bestNumApplesText.text = string.Format("{0}x", highestScore);

		ShowMedal();

		// wait for button input
	}

	public void OnGameFinish()
	{
		checkpointPanel.SetActive(false);

		// find panel and set active
		GameObject finishObject = GameObject.Find("FinishObject");
		GameObject finishPanel = finishObject.transform.GetChild(0).gameObject;
		Transform checkpointsParent = finishPanel.transform.Find("Checkpoints");
		finishPanel.SetActive(true);

		// create list of checkpoint scores
		GameObject checkpointPrefab = (GameObject) Resources.Load("Checkpoint");
		List<CheckpointResult> results = CollectableManager.Instance.GetCheckpointResults;
		foreach (CheckpointResult result in results)
		{
			GameObject newCheckpointPrefab = Instantiate(checkpointPrefab);
			var valueSet = newCheckpointPrefab.GetComponent<SetCheckpointPrefabValues>();

			valueSet.SetLevel(result.level);
			valueSet.SetScore(result.score);
			valueSet.SetMedalSprite(medalSprites[(int) result.medal]);
			newCheckpointPrefab.transform.SetParent(checkpointsParent, false);
		}

	}

	// when player wants to try again
	public void RetryLevel()
	{
		// destroy remaining collectables
		foreach (GameObject go in collectables)
		{
			Destroy(go);
		}

		// respawn all collectables
		foreach (Vector3 position in collectablesPosition)
		{
			GameObject.Instantiate(applePrefab, position, Quaternion.identity);
		}

		// reset current score
		currentScore = 0;

		// enable simulation buttons
		startButton.SetActive(true);
		resetButton.SetActive(true);

		// reload level
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		// disable checkpoint screen
		checkpointPanel.SetActive(false);
	}

	// when player wants to go to next level
	public void ContinueNextLevel()
	{
		CollectableManager.Instance.Score += currentScore;

		CheckpointResult result = new CheckpointResult();
		result.medal = currentMedal;
		result.score = currentScore;
		result.level = SceneTransition.Instance.CurrentLevel;

		CollectableManager.Instance.AddCheckpointResult(result);

		// something else?
		// destroy singleton
		instance = null;

		if (SceneTransition.Instance.IsLastLevel)
		{
			OnGameFinish();
		}
		else
		{
			SceneTransition.Instance.GoToNextLevel();
			Destroy(gameObject);
		}
	}

	private void ShowMedal()
	{
		if (currentScore < silverScoreMin)
		{
			currentMedal = Medal.BRONZE;
			// show bronze medal
		}
		else if (currentScore == numCollectables)
		{
			currentMedal = Medal.GOLD;
			// show gold medal
		}
		else
		{
			currentMedal = Medal.SILVER;
			// show silver medal
		}

		// check if player got new highest medal
		if (currentMedal > highestMedal)
		{
			highestMedal = currentMedal;
		}

		// set UI images
		currentMedalImage.sprite = medalSprites[(int) currentMedal];
		bestMedalImage.sprite = medalSprites[(int) highestMedal];
	}

	private void GetUIElements()
	{
		CheckpointUI uiElements = GameObject.Find("_CheckpointUI").GetComponent<CheckpointUI>();

		checkpointPanel = uiElements.checkpointPanel;
		numApplesText = uiElements.numApplesText;
		bestNumApplesText = uiElements.bestNumApplesText;
		currentMedalImage = uiElements.currentMedalImage;
		bestMedalImage = uiElements.bestMedalImage;

		startButton = uiElements.startButton;
		resetButton = uiElements.resetButton;

		Button retryButton = uiElements.retryButton.GetComponent<Button>();
		Button continueButton = uiElements.continueButton.GetComponent<Button>();

		retryButton.onClick.AddListener(RetryLevel);
		continueButton.onClick.AddListener(ContinueNextLevel);
	}

}