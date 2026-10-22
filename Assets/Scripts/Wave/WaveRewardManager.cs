using System.Collections.Generic;
using UnityEngine;

using System;
using Microsoft.Extensions.Logging.Abstractions;

/*



웨이브 보상을 담당하는 클래스
 
 */
public class WaveRewardManager : MonoBehaviour
{


    [SerializeField]
    private WaveManager waveManager;



    [SerializeField]
    private GoldManager goldManager;


    // 보상 금액은 예시
    [SerializeField, Min(1)] private int goldPerWave = 50;

    private readonly HashSet<int> rewardedWaves = new();



    //wave, 지급한 골드 
    public event Action<int, int> OnWaveRewardGranted;


    private void OnEnable()
    {
        if (waveManager != null)
        {

            waveManager.OnWaveCompleted += HandleWaveCompleted;
        }


    }

    private void OnDisable()
    {

        if (waveManager != null)
        {

            waveManager.OnWaveCompleted -= HandleWaveCompleted;
        }

    }

    public void InitializeRun()
    {

        rewardedWaves.Clear();

       

    }


    


    private void HandleWaveCompleted(int wave)
    {

        if (goldManager == null || goldPerWave <= 0)
        {
            Debug.LogError("웨이브 골드 보상 설정 확인필요");
            return;

        }
        //동일 웨이브대한 중복 보상 방지 
        if (!rewardedWaves.Add(wave))
            return;

        goldManager.AddGold(goldPerWave);
        OnWaveRewardGranted?.Invoke(wave, goldPerWave);
     

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
