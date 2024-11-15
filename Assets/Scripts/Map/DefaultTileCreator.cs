using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DefaultTileCreator : MonoBehaviour
{
    private const int mapWidth = 10;
    private const int mapHeight = 10;
    [SerializeField] private GameObject defaultTile;
    private void Awake()
    {
        for(int x=0;x<mapWidth; x++)
        {
            for (int y=0;y<mapHeight; y++)
            {
                GameObject obj = Instantiate(defaultTile,new Vector3(x*13,y*13,0), Quaternion.identity);
                obj.transform.parent = transform;
            }

        }
    }
}
