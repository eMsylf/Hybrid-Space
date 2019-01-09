using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

[System.Serializable]
public struct Checkpoint
{
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool fixedPlatformRotation = false;
    public GameObject normalPlatform;
    public GameObject jumpPlatform;
    public Transform[] checkpointTransforms;

    private GameObject player;
    private int activeCheckpointIndex;

    public int ActiveCheckpointIndex { get { return activeCheckpointIndex; } }
    public Transform ActiveCheckpoint { get { return checkpointTransforms[activeCheckpointIndex]; } }

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSimulation();
        }
    }

    // called when player presses "simulate"
    public void StartSimulation()
    {
        if (SpawnPlatforms())
        {
            player.GetComponent<PlayerMovementSimulated>().EnableMovement(true);
            TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        }

        /*bool blockadeCollision = CollidingWithBlockade();
        Debug.Log("Blockade collision: " + blockadeCollision);

        if (!blockadeCollision)
        {
          player.GetComponent<PlayerMovementSimulated>().EnableMovement(true);
          TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        }
        else
        {
          player.GetComponent<PlayerCollisionFlash>().DoFlashing();
          //ResetLevel();
        }*/
    }

    private bool CollidingWithBlockade(GameObject platform)
    {
        GameObject[] blockades = GameObject.FindGameObjectsWithTag("Blockade");
        foreach (GameObject blockade in blockades)
        {
            //Debug.Log("There is a blockade, its name is: " + blockade.name);
            //Debug.Log("Its collision value is: " + blockade.GetComponent<BlockadeCollision>().IsColliding());
            if (blockade.GetComponent<BlockadeCollision>().IsColliding(platform))
            {
                return true;
            }
        }
        return false;
    }

    // called when player dies
    public void ResetSimulation()
    {
        player.GetComponent<PlayerMovementSimulated>().EnableMovement(false);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        player.transform.position = ActiveCheckpoint.position;

        ResetLevel();
    }

    public void NextCheckpoint()
    {
        player.GetComponent<PlayerMovementSimulated>().EnableMovement(false);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        activeCheckpointIndex++;
        Camera.main.GetComponent<NextCheckpointPosition>().GoToNextCheckPoint(checkpointTransforms[activeCheckpointIndex]);

        ResetLevel();
    }

    private void ResetLevel()
    {
        // remove platforms
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            // only get rendered platforms
            Destroy(platform);
        }

        // start tracking
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
    }

    // returns true if platforms could be spawned
    private bool SpawnPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Target");
        List<GameObject> platformCache = new List<GameObject>();

        foreach (GameObject platform in platforms)
        {
            // only get rendered platforms
            if (!platform.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled) continue;

            Vector3 pos = platform.transform.position;
            Quaternion rot = platform.transform.rotation;
            string type = platform.transform.GetChild(0).name;
            GameObject platformPrefab = null;
            switch (type)
            {
                case "NormalPlatform":
                    platformPrefab = normalPlatform;
                    break;
                case "JumpPlatform":
                    platformPrefab = jumpPlatform;
                    break;
                default:
                    Debug.Log("ERROR: platform not valid");
                    return false;
            }

            GameObject platform_instance = (GameObject) Instantiate(platformPrefab, pos, rot);
            SetScaleAndPosition(ref platform_instance);

            if (!CollidingWithBlockade(platform_instance))
            {
                platformCache.Add(platform_instance);
            }
            else
            {

                platform.GetComponentInChildren<PlatformCollisionFlash>().DoFlashing();
                // first destroy the colliding platform
                Destroy(platform_instance);

                // then destroy every platform previously added
                foreach (GameObject spawnedPlatform in platformCache)
                {
                    Destroy(spawnedPlatform);
                }
                return false;
            }

            platform.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        return true;
    }

    private void SetScaleAndPosition(ref GameObject obj)
    {
        Camera cam = Camera.main;

        float oldDistance = (obj.transform.position - cam.transform.position).magnitude;
        Vector3 newBoxPos = new Vector3(obj.transform.position.x, obj.transform.position.y, 19);

        float newDistance = (newBoxPos - cam.transform.position).magnitude;
        float newSize = (newDistance / oldDistance);

        // REPOSITIONING
        Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 19));

        obj.transform.position = worldPos;
        obj.transform.localScale = Vector3.one * newSize * obj.transform.localScale.x;
        if (fixedPlatformRotation) obj.transform.rotation = Quaternion.identity;
    }
}