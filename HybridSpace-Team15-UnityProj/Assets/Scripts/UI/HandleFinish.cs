using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleFinish : MonoBehaviour
{
	public void GoToMainMenu()
	{
		CleanUp();
		SceneManager.LoadSceneAsync("Scenes/Menu");
	}

	public void RestartGame()
	{
		CleanUp();
		SceneManager.LoadSceneAsync("Scenes/Lvl01");
	}

	private void CleanUp()
	{
		GameObject sceneTransition = GameObject.Find("SceneTransition");
		GameObject musicPlayer = GameObject.Find("MusicPlayer");
		GameObject collectableManager = GameObject.Find("CollectableManager");
		GameObject checkpointScore = GameObject.Find("_CheckpointScore");

		Destroy(sceneTransition);
		Destroy(musicPlayer);
		Destroy(collectableManager);
		Destroy(checkpointScore);
	}

}