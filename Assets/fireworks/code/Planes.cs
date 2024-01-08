using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class Planes : MonoBehaviour
{
  [SerializeField] public int planesToSpawn = 3;
  [SerializeField] public float offset = 25.5f;
  [SerializeField] Vector3 startPos = Vector3.zero;
  [SerializeField] float startRange = 5.0f;
  [SerializeField] GameObject planePrefab;
  // Start is called before the first frame update
  [SerializeField] float interval = 0.0f;
  [SerializeField] GameObject text;
  private System.Random _rand;

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetupPlanes()
  {
    int i = 0;
    Game.Instance.participants.Shuffle();
    foreach (Participant p in Game.Instance.participants)
    {
      // if activeplayername == p.name return;
      SpawnPlane(p._name, p._color, offset * i);
      spawnNames();
      i++;
    }
  }


  void SpawnPlane(string name, Color color, float offset)
  {
    if(Game.Instance.participants[Game.Instance.activePlayerIndex]._name == name)
    {
      //return;
    }
    GameObject planeGO = Instantiate(planePrefab);
    Material m = planeGO.GetComponent<Material>();
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    planeGO.transform.SetParent(transform.parent);
    planeGO.transform.position = startPos + new Vector3(-offset, startRange * Random.Range(-startRange, startRange), 0);
    Airplane p = planeGO.GetComponent<Airplane>();
    p.ApplyColor(color);
    p.Pilot = name;
    Game.Instance.planes.Add(p);
  }

  void spawnNames()
  {
    foreach (Airplane plane in Game.Instance.planes)
    {
      if (plane.textPos == null)
      {
        plane.textPos = Instantiate(text);
        TextMeshPro tmp = plane.textPos.AddComponent<TextMeshPro>();
        tmp.text = plane.Pilot;
        tmp.fontSize = 118;
        RectTransform rt = new RectTransform();
        //rt.sizeDelta = new Vector2(0.2f, 0.2f) ;
        plane.textPos.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        plane.textPos.transform.SetParent(transform);
      }
    }
  }
}
