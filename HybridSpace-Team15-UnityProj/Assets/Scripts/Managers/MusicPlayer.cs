using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
  AMBIENT,
  MELODIC,
  NONE
}

public class MusicPlayer : MonoBehaviour
{
  public AudioClip ambientClip;
  public AudioClip melodicClip;
  public MusicType musicType;

  private AudioSource audioSource;

  private MusicPlayer Instance;

  // Use this for initialization
  void Start()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    // only one music player in scene
    else if (Instance != this)
    {
      Destroy(gameObject);
    }

    audioSource = GetComponent<AudioSource>();

    switch (musicType)
    {
      case MusicType.AMBIENT:
        audioSource.clip = ambientClip;
        audioSource.Play();
        break;
      case MusicType.MELODIC:
        audioSource.clip = melodicClip;
        audioSource.Play();
        break;
      default:
        break;
    }

    DontDestroyOnLoad(gameObject);
  }

}
