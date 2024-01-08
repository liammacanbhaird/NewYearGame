using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
  [SerializeField] float delay = 30.0f;
  float t = 0.0f;
  private void Awake()
  {
    t = 0.0f;
  }

  void FixedUpdate()
    {
      if (t > delay) Destroy(gameObject);
      t += Time.fixedDeltaTime;
    }
}
