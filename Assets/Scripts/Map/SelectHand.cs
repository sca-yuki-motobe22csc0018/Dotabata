using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHand : MonoBehaviour
{
    [SerializeField] private int selectHandNumber;

    private void Start()
    {
    }

    private void Update()
    {
        
    }
    public void PointerClick()
    {
        selectHandNumber = int.Parse(this.gameObject.name);
    }
}
