using UnityEngine;
using System.Collections.Generic;
using System;

public class UnitInventory : MonoBehaviour
{

    private readonly Dictionary<JobType, int> units = new();

    public event Action<JobType, int> OnCountChanged;

    public int GetCount(JobType job)
    {
        return units.TryGetValue(job, out int count) ? count : 0;
    }

    public void Add(JobType job, int amount = 1)
    {
        if (amount <= 0)
            return;
        int newCount = GetCount(job) + amount;

        units[job] = newCount;

        OnCountChanged?.Invoke(job, newCount);


        Debug.Log($"{job} +{amount}, Total: {newCount}");
    }


    public bool TryConsume(JobType job, int amount = 1)
    {
        if (amount <= 0)
            return false;


        int currentCount = GetCount(job);

        if (currentCount < amount)
            return false;

        int newCount = currentCount - amount;

        units[job] = newCount;

        OnCountChanged?.Invoke(job, newCount);

       

        return true;

    }


    public void ResetForRun()
    {
        units.Clear();
        foreach (JobType job in Enum.GetValues(typeof(JobType)))
        {
            OnCountChanged?.Invoke(job, 0);
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
