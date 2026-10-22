using UnityEngine;

public class UnitDrawManager : MonoBehaviour
{

    [SerializeField] private UnitPlacementController unitPlacementContoller;
    [SerializeField] private GoldManager goldManager;

    [SerializeField]
    private UnitDrawPool[] pools;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonE() => TrySummon(UnitRank.E);
    public void SummonD() => TrySummon(UnitRank.D);
    public void SummonC() => TrySummon(UnitRank.C);
    public void SummonB() => TrySummon(UnitRank.B);
    public void SummonA() => TrySummon(UnitRank.A);

    public void SummonS() => TrySummon(UnitRank.S);


    public bool TrySummon(UnitRank rank)
    {
        if (unitPlacementContoller == null)
            return false;

        UnitSlot selectedSlot = unitPlacementContoller.SelectedSlot;
        if (selectedSlot == null)
            return false;

        if (selectedSlot.IsOccupied)
            return false;


        GameObject prefab;
        if(TryDraw(rank, out prefab) == false)
        {
            return false;
        }

        return unitPlacementContoller.PlaceUnit(prefab);

    

    }


    private bool TryDraw(UnitRank rank, out GameObject prefab)
    {

        prefab = null;


        UnitDrawPool pool = FindPool(rank);

        if (pool == null || pool.Prefabs == null ||pool.Prefabs.Count==0)
            return false;


        for (int i = 0; i < pool.Prefabs.Count; i++)
        {
            if (pool.Prefabs[i] == null)
            {
                Debug.LogError($"뽑기 풀에 빈 프리팹이 있습니다: {pool.name}");
                return false;
            }
        }


        if(goldManager.TrySpendGold(pool.SummonCost) == false)
        {
            return false;
        }




        int index = Random.Range(0, pool.Prefabs.Count);

        prefab = pool.Prefabs[index];

        return true;
    }


    private UnitDrawPool FindPool(UnitRank rank)
    {

        if (pools == null)
            return null;

        foreach(UnitDrawPool pool in pools)
        {
            if (pool != null && pool.Rank == rank)
                return pool;

        }

        return null;


    }

}
