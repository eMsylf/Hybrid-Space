using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public struct Checkpoint
{
	public int id;
	public Vector3 playerPosition;
	public Vector3 cameraPosition;
}

public class GameManager : MonoBehaviour {

	Checkpoint[] checkpoints;
	
	public static GameManager instance = null; 
	public Checkpoint activeCheckpoint;
	public GameObject normalPlatform;
	public GameObject jumpPlatform;

	// Use this for initialization
	void Start () {
		if (instance == null)
            instance = this;
            
        else if (instance != this)          
            Destroy(gameObject);

		checkpoints = new Checkpoint[2];
		
		checkpoints[0].id = 0;
		checkpoints[0].playerPosition = new Vector3(6.5f, 1.6f, 19.0f);
		checkpoints[0].cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);

		checkpoints[1].id = 1;
		checkpoints[1].playerPosition = new Vector3(-6.1f, -1.0f, 19.0f);
		checkpoints[1].cameraPosition = new Vector3(-12.5f, 0.0f, 0.0f);
		
		activeCheckpoint = checkpoints[0];
	}

	public void NextCheckpoint()
	{
		activeCheckpoint = checkpoints[activeCheckpoint.id + 1];
		Debug.Log("Checkpoint reached");
	}

	public void ResetLevel()
	{
		// remove platforms
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		foreach(GameObject platform in platforms)
		{
			// only get rendered platforms
			Destroy(platform);
		}

		// start tracking
		TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
	}

	public void SpawnPlatforms()
	{
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Target");
		foreach(GameObject platform in platforms)
		{
			// only get rendered platforms
			if (!platform.transform.GetChild(0).GetComponent<MeshRenderer>().enabled) continue;

			Vector3 pos = platform.transform.position;
			Quaternion rot = platform.transform.rotation;
			string type = platform.transform.GetChild(0).name;
			GameObject platformPrefab = null;
			switch(type)
			{
				case "NormalPlatform":
					platformPrefab = normalPlatform;
					break;
				case "JumpPlatform":
					platformPrefab = jumpPlatform;
					break;
				default:
					Debug.Log("ERROR: platform not valid");
					return;
			}

			GameObject platform_instance = (GameObject)Instantiate(platformPrefab, pos, rot);
			SetScaleAndPosition(ref platform_instance);

			platform.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		}

		TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
	}
	
	private void SetScaleAndPosition(ref GameObject obj)
	{
		Camera cam = Camera.main;

		float oldDistance = (obj.transform.position - cam.transform.position).magnitude;
		Vector3 newBoxPos = new Vector3(obj.transform.position.x, obj.transform.position.y, 19);

		float newDistance = (newBoxPos - cam.transform.position).magnitude;
		float newSize = (newDistance/oldDistance);

		// REPOSITIONING
		Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
		Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 19));
		
		obj.transform.position = worldPos;
		obj.transform.localScale = Vector3.one * newSize;
		obj.transform.rotation = Quaternion.identity;
	}
}
