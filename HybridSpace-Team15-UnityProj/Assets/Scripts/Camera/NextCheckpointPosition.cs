using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCheckpointPosition : MonoBehaviour {
    public float offset;

    public Transform MidPointTransform;

    private void Start() {
        MidPointTransform = FindObjectOfType<MidPointScript>().transform;
        offset = -MidPointTransform.position.x;
        Debug.Log("Set new camera midpoint offset");
    }

    public void GoToNextCheckPoint(Transform activeCheckpoint) {


        Vector3 camEdgeWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, activeCheckpoint.position.y, 19f));
        float length = (activeCheckpoint.position - camEdgeWorldPoint).magnitude;

        Vector3 newPosition = Camera.main.transform.position;
        newPosition.x -= length - offset;

        transform.position = newPosition;
    }
}
