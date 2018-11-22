using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeOpacity : MonoBehaviour
{

  public float opacityMin, opacityMax, speed;

  Material material;
  Color color;
  float divider;


  // Use this for initialization
  void Start()
  {
    material = GetComponent<Renderer>().material;
    color = material.color;

    divider = 2.0f / (opacityMax - opacityMin);
  }

  // Update is called once per frame
  void Update()
  {

    float alpha = ((Mathf.Sin(Time.time * speed) + 1) / divider) + opacityMin;

    color.a = alpha;
    material.color = color;
  }
}
