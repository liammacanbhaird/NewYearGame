using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Airplane : MonoBehaviour
{
  Rigidbody rb = null;
  [SerializeField] float _speed;
  [SerializeField] float _crashRotation = 1f;
  [SerializeField] public string Pilot { get; set; }
  [SerializeField] Color _color;
  TextMeshPro _textMeshPro;
  MaterialPropertyBlock mpb;
  public GameObject textPos;
  static readonly int shPropColor = Shader.PropertyToID("_Color");
  bool falling = false;
  bool exploded = false;
  float torqueDir = 0f;
  [SerializeField] GameObject smoke;
  MaterialPropertyBlock Mpb
  {
    get
    {
      if (mpb == null)
      {
        mpb = new MaterialPropertyBlock();
      }
      return mpb;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    torqueDir = Random.Range(-1f, 1f);
    rb = GetComponent<Rigidbody>();
    rb.velocity = Vector3.right * _speed;
    //GetComponent<Canvas>().worldCamera = Camera.current;
  }
  private void Update()
  {
    if (textPos != null)
    {
      textPos.transform.position = transform.position;
    }
    if (falling && !exploded) rb.AddTorque(Vector3.right * 4.5f * torqueDir);
  }

  public void Hit()
  {
    Crash();
    smoke.SetActive(true);
  }

  public void Crash()
  {
    falling = true;
    rb.useGravity = true;
    Vector3 randRot = new Vector3(Random.Range(-_crashRotation, _crashRotation), Random.Range(-_crashRotation, _crashRotation), Random.Range(-_crashRotation, _crashRotation));
    if(!exploded) rb.AddTorque(randRot);

  }

  public void ApplyColor(Color color)
  {
    MeshRenderer rnd = GetComponent<MeshRenderer>();
    Mpb.SetColor(shPropColor, color);
    rnd.SetPropertyBlock(Mpb);
  }

  void Explode()
  {
    exploded = true;
    // play explosion graphic

  }
}
