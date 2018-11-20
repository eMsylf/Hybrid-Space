using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public KeyCode LevelResetButton = KeyCode.R;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(LevelResetButton)) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
	}
}
