using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadSceneAsync("Scenes/Lvl01");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}