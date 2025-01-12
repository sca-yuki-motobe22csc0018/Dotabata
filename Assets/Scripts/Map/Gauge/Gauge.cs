using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject magma;
    [SerializeField] private Text altitudeText;
    private const float revision = -6.5f;
    private const float goalHeight = 95.0f;
    private const float gaugeHeight = 500;
    private const float gaugeScale = 5.0f;
    private float playerProgress;
    private float magmaProgress;

    [SerializeField] private GameObject playerIcon;
    [SerializeField] private GameObject magmaIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pPosY = player.transform.position.y-revision >= 0.0f ? player.transform.position.y : 0.0f;
        playerProgress = pPosY / goalHeight;
        altitudeText.text = "___" + (goalHeight - playerProgress * goalHeight) + "m";
        playerIcon.transform.localPosition = new Vector3(0, gaugeHeight * playerProgress + 25, 0);

        float mPosY = magma.transform.position.y-revision >= 0.0f ? magma.transform.position.y-revision : 0.0f;
        magmaProgress = mPosY / goalHeight;
        magmaIcon.transform.localScale = new Vector3(1, 0.1f + magmaProgress * gaugeScale);
    }
}
