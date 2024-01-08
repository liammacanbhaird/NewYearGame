using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
  [SerializeField] float fuseTime = 1.5f;
  [SerializeField] float rndFuseTime = 0;
  [SerializeField] float explosionTime = 3.0f;
  [SerializeField] Vector2 force = Vector2.up;
  [SerializeField] float explosionHeight = 30.0f;
  [SerializeField] float explosionRadius = 5.0f;
  [SerializeField] float fireRadius = 2.0f;
  Rigidbody rb;
  float t = 0.0f;
  [SerializeField] GameObject rocketVisual;
  [SerializeField] GameObject explosionEffect;
  bool launched = false;
  bool exploded = false;
    bool lit;
    
    // Start is called before the first frame update
    void Start()
  {
    rb = GetComponent<Rigidbody>();
    rndFuseTime = Random.Range(-1f,1f); ;
  }

  // Update is called once per frame
  void Update()
  {
    t += Time.deltaTime;
    if (t >= fuseTime + rndFuseTime)
    {
      Fire();
    }
    if (t >= fuseTime + explosionTime + 30f)
    {
      Destroy(gameObject);
    }
  }

  private void Fire()
  {
    if(!launched) SoundManager.Instance.PlayLaunch();
    rb.AddForce(force);
    if (transform.position.y >= explosionHeight && !exploded)
    {
      Explode();
    }
    if (Vector3.Distance(Game.Instance.activePlayer.transform.position, transform.position) <= fireRadius && !launched)
    {
      Game.Instance.activePlayer.SetOnFire();
      ScoreBoard.Instance.Tally($"{Game.Instance.activePlayer.activePlayerName} stood too close to their firework");
    }
    launched = true;
  }



  private void Explode()
  {
    exploded = true;
    // show explosion
    SoundManager.Instance.PlayExplosion();
    rocketVisual.SetActive(false);
    GameObject explosion = Instantiate(explosionEffect, Game.Instance.transform);
    ParticleSystem ps = explosionEffect.GetComponent<ParticleSystem>();
    explosion.transform.position = transform.position;
    rb.velocity = Vector3.zero;
    //Dictionary<Player, Airplane> destroyedPlanes = new Dictionary<Player, Airplane>();
    bool AircraftHit = false;
    foreach (Airplane plane in Game.Instance.planes) 
    {
      if (Vector3.Distance(plane.transform.position, transform.position) < explosionRadius)
      {
        plane.Hit();
        Debug.Log("hit aircraft");
        ScoreBoard.Instance.Tally($"{plane.Pilot} was shot");
        AircraftHit = true;
        //Instantiate(explosionAnimation);
        // sound
      }
     }
    if(!AircraftHit) ScoreBoard.Instance.Tally($"{Game.Instance.activePlayer.activePlayerName} missed");
  }

}
