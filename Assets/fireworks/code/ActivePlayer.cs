using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class ActivePlayer : MonoBehaviour
{
  [SerializeField] int startAmmo = 3;
  int ammo = 3;
  [SerializeField] float playerSpeed;
  [SerializeField] GameObject rocketPrefab;
  [SerializeField] public string activePlayerName;
  [SerializeField] GameObject _nameGraphic;
  [SerializeField] Vector3 nameGraphicOffset;
  [SerializeField] TextMeshProUGUI tmp;
  [SerializeField] GameObject fireEffect;
  // Start is called before the first frame update
  void Awake()
  {
    tmp = _nameGraphic.GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {
    // Movement controls

    transform.position += Vector3.right * playerSpeed * Input.GetAxis("Horizontal");


    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
    {
      if (ammo > 0)
      {
        ammo--;
        GameObject rocket = Instantiate(rocketPrefab);
        rocket.transform.position = transform.position;
      }
      else
      {
        // no ammo
        Debug.Log("no ammo");
        ScoreBoard.Instance.Tally($"{activePlayerName} is thisty" );
        SoundManager.Instance.PlayBeer();
        // play no ammo sound
        // end game?
      }
    }

    _nameGraphic.transform.position = transform.position + nameGraphicOffset;
  }

  public void SetActivePlayer(string name)
  {
    activePlayerName = name;
    tmp.text = name;
  }
  public void ResetPlayer()
  {
    ammo = startAmmo;
    fireEffect.SetActive(false); 
  }

  public void SetOnFire()
  {
    SoundManager.Instance.PlaySetFire();
    fireEffect.SetActive(true);
  }

}
