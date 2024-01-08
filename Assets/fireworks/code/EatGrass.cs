using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EatGrass : MonoBehaviour
{
  [SerializeField] float rotationSpeed;
  [SerializeField] Vector3 startRot;
    // Start is called before the first frame update
    void Start()
    {
        startRot = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAroundLocal(Vector3.up, rotationSpeed * Time.deltaTime * 0.01f);
    }
}
