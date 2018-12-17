using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollisionFlash : MonoBehaviour
{

  public float flashSpeed;
  public float flashTime;
  public float maxWhiteness;

  private Material platformMaterial;
  private float timer;
  private bool isFlashing;

  void Start()
  {
    platformMaterial = GetComponent<SpriteRenderer>().material;
    timer = 0f;
    isFlashing = false;
  }

  void Update()
  {
    if (isFlashing)
    {
      if (timer < flashTime)
      {
        timer += Time.deltaTime;
        float currentValue = ((Mathf.Sin(timer * flashSpeed) + 1f) * maxWhiteness) / 2f;
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", currentValue);
      }
      else
      {
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0f);
        isFlashing = false;
      }
    }
  }

  public void DoFlashing()
  {
    isFlashing = true;
    timer = 0f;
    //StartCoroutine(FlashAnimation());
  }
}
