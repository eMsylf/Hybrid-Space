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
		levelText.text = string.Format("#{0}", level);
	}

	public void SetScore(int score, int max)
	{
		scoreText.text = string.Format("{0}/{1}", score, max);
	}

	public void SetMedalSprite(Sprite sprite)
	{
		medalImage.sprite = sprite;
	}
}