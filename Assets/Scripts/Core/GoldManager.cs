using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{

    [SerializeField]
    private int startingGold = 100;


    public int CurrentGold { get; private set; }


    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        CurrentGold = startingGold;

    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;



        CurrentGold += amount;

        OnGoldChanged?.Invoke(CurrentGold);
    }


    public bool TrySpendGold(int amount)
    {

        if (amount <= 0  || CurrentGold <amount)
            return false;

        CurrentGold -= amount;

        OnGoldChanged?.Invoke(CurrentGold);

        return true;

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
