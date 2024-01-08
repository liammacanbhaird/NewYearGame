using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
  Dictionary<string, int> deaths = new Dictionary<string, int>();
  public static ScoreBoard Instance { get; private set; }
  [SerializeField] GameObject text;
  [SerializeField] GameObject grid;


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
      return;
    }
    Instance = this;
  }

  public void ResetScore()
  {
    // clear board
    foreach (Transform child in grid.transform)
    {
      Destroy(child.gameObject);
    }
  }

  public void Tally(string name)
  {
    // write name on board
    GameObject tallyName = Instantiate(text, grid.transform);
    TextMeshPro tmp = tallyName.AddComponent<TextMeshPro>();
    tmp.text = name;
    tmp.fontSize = 500;
  }
}
