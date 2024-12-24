using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���U���g��ʂ̏����S��
/// </summary>
public class Result : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text oreText;
    [SerializeField] Text totalText;

    [SerializeField] GameObject meterMoveObj;
    Vector3 meterStartPos = new Vector3(25, -450, 0);
    Vector3 meterEndPos = new Vector3(25, 450, 0);

    float timeScore;
    float timeDisplayScore;
    float oreScore;
    float oreDisplayScore;
    float totalScore;
    float totalDisplayScore;
    float countUpTime = 1;

    float timer;
    enum ResultState
    {
        time,
        ore,
        tortal,
        end
    }
    ResultState resultState;

    void Start()
    {
        resultState = ResultState.time;
        
        timeDisplayScore = 0;
        oreDisplayScore = 0;
        totalDisplayScore = 0;
        timer = 0;
#if true
        timeScore = 1234567;
        oreScore = 1234567;
        totalScore = 1234567;
#endif
    }

    void Update()
    {
        switch (resultState)
        {
            case ResultState.time:
                if(timeScore > timeDisplayScore && !Input.GetMouseButtonDown(0))
                {
                    timeDisplayScore += timeScore * Time.deltaTime / countUpTime;
                    timeText.text = "+  " + timeDisplayScore.ToString("f0") + "  Pt";
                }
                else
                {
                    timeDisplayScore = timeScore;
                    timeText.text = "+  " + timeDisplayScore.ToString("f0") + "  Pt";
                    resultState = ResultState.ore;
                }
                break;
            case ResultState.ore:
                if (oreScore > oreDisplayScore && !Input.GetMouseButtonDown(0))
                {
                    oreDisplayScore += oreScore * Time.deltaTime / countUpTime;
                    oreText.text = "+  " + oreDisplayScore.ToString("f0") + "  Pt";
                }
                else
                {
                    oreDisplayScore = oreScore;
                    oreText.text = "+  " + oreDisplayScore.ToString("f0") + "  Pt";
                    resultState = ResultState.tortal;
                }
                break;
            case ResultState.tortal:
                if(totalScore > totalDisplayScore && !Input.GetMouseButtonDown(0))
                {
                    totalDisplayScore += totalScore * Time.deltaTime / countUpTime;
                    totalText.text = "" + totalDisplayScore.ToString("f0");
                }
                else
                {
                    totalDisplayScore = totalScore;
                    totalText.text = "" + totalDisplayScore.ToString("f0");
                    resultState = ResultState.end;
                }
                break;
            case ResultState.end:
                timer += Time.deltaTime;
                if (Input.GetMouseButtonDown(0) || timer > 5)
                {
                    SceneManager.LoadScene("Title");
                }
                break;
            default:
                resultState = ResultState.end;
                break;
        }
    }
}
