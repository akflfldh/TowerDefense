using System.Collections.Generic;
using UnityEngine;

using System;

/*



웨이브 보상을 담당하는 클래스
 
 */
public class WaveRewardManager : MonoBehaviour
{


    [SerializeField]
    private WaveManager waveManager;



    [SerializeField]
    private UnitInventory unitInventory;



    [SerializeField, Min(0)]
    private int startingUnitCount = 2;


    [SerializeField]
    private JobType[] rewardPool =
    {

        JobType.Warrior

    };


    private readonly HashSet<int> rewardedWaves = new();


    public event Action<int, JobType> OnWaveRewardGranted;


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

        unitInventory.ResetForRun();
        for (int i = 0; i < startingUnitCount; i++)
        {
            GrantRandomJob();
        }

    }


    private JobType? GrantRandomJob()
    {
        if (unitInventory == null ||
           rewardPool == null ||
           rewardPool.Length == 0)
        {
            Debug.LogError("Invalid reward configuration!");
            return null;
        }


        int index = UnityEngine.Random.Range(0, rewardPool.Length);

        JobType job = rewardPool[index];

        unitInventory.Add(job,1);


        Debug.Log($"Reward Unit: {job}");


        return job;
    }



    private void HandleWaveCompleted(int wave)
    {
        //동일 웨이브대한 중복 보상 방지 
        if (!rewardedWaves.Add(wave))
            return;


        JobType? rewardedJob =   GrantRandomJob();

        if(rewardedJob.HasValue)
        {
            OnWaveRewardGranted?.Invoke(wave, rewardedJob.Value);
        }




    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
