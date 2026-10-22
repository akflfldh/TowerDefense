using UnityEngine;
using System.Collections.Generic;
using System;

public class UnitInventory : MonoBehaviour
{

    private readonly Dictionary<JobType, int> units = new();

    public event Action<JobType, int> OnCountChanged;


    public void ResetForRun()
    {
 
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
