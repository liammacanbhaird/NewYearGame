using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance { get; private set; }

  [SerializeField] AudioSource effectsSource;
  [SerializeField] AudioSource musicSource;

  [SerializeField] AudioClip music;
  [SerializeField] AudioClip fireworkLaunch;
  [SerializeField] AudioClip fireworkFuse;
  [SerializeField] AudioClip fireworkExplosion;
  [SerializeField] AudioClip fireWorkNoAmmo;
  [SerializeField] AudioClip playerOnFire;
  [SerializeField] AudioClip CatchingFire;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
      return;
    }
    Instance = this;
    PlayMusic();
  }

  void PlayMusic ()
  {
    musicSource.clip = music;
    musicSource.Play();
  }

  public void PlayFuse()
  {
    effectsSource.PlayOneShot(fireworkFuse, 10);
  }

  public void PlayExplosion()
  {
    effectsSource.PlayOneShot(fireworkExplosion);
  }

  public void PlayBeer()
  {
    effectsSource.PlayOneShot(fireWorkNoAmmo, 10);
  }

  public void PlaySetFire()
  {
    effectsSource.PlayOneShot(playerOnFire, 2);
    effectsSource.PlayOneShot(CatchingFire, 4);
  }

  public void PlayLaunch()
  {
    effectsSource.PlayOneShot(fireworkLaunch,2);
  } 


}
