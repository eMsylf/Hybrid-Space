using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCheckpointPrefabValues : MonoBehaviour
{
	public Text levelText;
	public Text scoreText;
	public Image medalImage;

	public void SetLevel(int level)
	{
		levelText.text = level.ToString();
	}

	public void SetScore(int score)
	{
		scoreText.text = string.Format("{0}x", score);
	}

	public void SetMedalSprite(Sprite sprite)
	{
		medalImage.sprite = sprite;
	}
}