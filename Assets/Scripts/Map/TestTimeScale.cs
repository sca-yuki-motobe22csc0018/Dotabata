using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimeScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.state==PlayerManager.PlayerState.MapCreate)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
       
    }
}
