using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, speed, 0)*Time.deltaTime;
    }
}
