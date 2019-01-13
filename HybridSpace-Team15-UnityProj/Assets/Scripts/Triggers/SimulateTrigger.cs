using UnityEngine;
using UnityEngine.UI;

public class SimulateTrigger : MonoBehaviour
{
	public Vector2 offset;
	public float timeBeforeStart;

	public Color startColor;
	public Color endColor;

	private GameObject simulateTarget;
	private MeshRenderer targetMeshRenderer;

	private Rect intersectBounds;
	private float currentTime;
	private bool intersecting;
	private Image panelImage;

	void Start()
	{
		simulateTarget = GameObject.Find("SimulateTarget");
		targetMeshRenderer = simulateTarget.transform.GetChild(0).GetComponent<MeshRenderer>();

		Vector2 boundPos = new Vector2(transform.position.x - offset.x, transform.position.y - offset.y);
		intersectBounds = new Rect(boundPos, offset * 2);

		currentTime = 0f;
		panelImage = GetComponent<Image>();
		panelImage.color = startColor;

		intersecting = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (CheckIntersection())
		{
			intersecting = true;
			currentTime += Time.deltaTime;
			panelImage.color = Color.Lerp(startColor, endColor, currentTime / timeBeforeStart);
			//Debug.Log("<b>INTERSECTING</b>");
		}
		else if (intersecting)
		{
			intersecting = false;
			currentTime = 0f;
			panelImage.color = startColor;
		}

		if (currentTime >= timeBeforeStart)
		{
			GameManager.instance.StartSimulation();
			intersecting = false;
			currentTime = 0f;
			panelImage.color = startColor;
		}
	}

	bool CheckIntersection()
	{
		if (!targetMeshRenderer.enabled) return false;

		Vector2 screenPos = Camera.main.WorldToScreenPoint(simulateTarget.transform.position);
		return intersectBounds.Contains(screenPos);
	}
}