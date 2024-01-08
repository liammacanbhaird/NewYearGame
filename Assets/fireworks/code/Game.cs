using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Xml;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using System.Xml.Serialization;
using TreeEditor;

public class Game : MonoBehaviour
{
  [SerializeField] Planes spawner;
  [SerializeField] public List<Airplane> planes;
  [SerializeField] public List<Participant> participants;
  [SerializeField] public ActivePlayer activePlayer;
    List<Participant> turnOrder;
  public static Game Instance { get; private set; }
  public int activePlayerIndex = 0;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
      return;
    }
    Instance = this;
    activePlayer.SetActivePlayer("Player");
  }


  // Start is called before the first frame update
  void Start()
  {
    spawner.SetupPlanes();
    //NextPlayer();
  }

  // Update is called once per frame
  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      NextPlayer();
      ResetGame();
    }
    if (Input.GetKeyDown(KeyCode.Backspace))
    {
      NextPlayer();
    }
  }

  private void ResetGame()
  {
    //ScoreBoard.Instance.ResetScore();
    activePlayerIndex = 0;
    foreach(Airplane plane in planes)
    {
      Debug.Log(plane.Pilot);
      plane.ApplyColor(Color.grey);
    }
    planes.Clear();
    spawner.SetupPlanes();
    ScoreBoard.Instance.ResetScore();
  }

  private void NextPlayer()
  {
    activePlayerIndex++;
    if (activePlayerIndex > participants.Count-1) activePlayerIndex = 0;
    activePlayer.SetActivePlayer("Player");
    activePlayer.ResetPlayer();
  }

}
