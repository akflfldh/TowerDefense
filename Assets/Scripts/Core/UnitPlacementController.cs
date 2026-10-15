using System;
using UnityEngine;

public class UnitPlacementController : MonoBehaviour
{

    [Serializable]
    private class JobPrefabEntry
    {
        public JobType job;
        public GameObject prefab;


    }


    [Header("Managers")]
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private UnitInventory unitInventory;



    [Header("Unit Prefabs")]
    [SerializeField]
    private JobPrefabEntry[] jobPrefabs;


    [SerializeField]
    private UnitSlot[] slots;


    [Header("Selection")]
    [SerializeField] private JobType? selectedJob = null;


    private void OnEnable()
    {

        if (slots == null)
            return;

        foreach (UnitSlot slot in slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;

        }


    }

    private void OnDisable()
    {
        if (slots == null)
            return;


        foreach (UnitSlot slot in slots)
        {
            slot.OnSlotClicked -= HandleSlotClicked;

        }
    }


    public void SelectJob(JobType job)
    {
        
        if (unitInventory == null)
            return;
  
        if (unitInventory.GetCount(job) <= 0)
            return;
 
        if (FindPrefab(job) == null)
            return;


        selectedJob = job;
        Debug.Log($"Selected Job: {job}");
    }

    // 나중에 UI Button에서 호출
    public void SelectWarrior()
    {
        SelectJob(JobType.Warrior);
    }

    public void SelectArcher()
    {
        SelectJob(JobType.Archer);
    }

    public void SelectMage()
    {
        SelectJob(JobType.Mage);
    }

    private GameObject FindPrefab(JobType job)
    {

        if (jobPrefabs == null)
            return null;


        foreach (JobPrefabEntry entry in jobPrefabs)
        {
            if (entry.job == job)
            {
                return entry.prefab;
            }

        }

        return null;

    }




    private void HandleSlotClicked(UnitSlot slot)
    {

        if (gameManager == null || unitInventory == null || gameManager.State != GameState.Playing)
            return;


        if (slot == null || slot.IsOccupied)
            return;

        if(!selectedJob.HasValue)
        {
            Debug.Log("먼저 직업을 선택하세요.");
            return;
        }


        JobType job = selectedJob.Value;
        GameObject prefab = FindPrefab(job);
        
        if (prefab == null)
        {

            Debug.LogWarning("Unit prefab not found!");
            return;

        }


        if (!unitInventory.TryConsume(job))
        {

            Debug.Log("Not enough unit cards!");
            return;
        }


        GameObject unit = Instantiate(prefab, slot.SpawnPosition, Quaternion.identity);
       
        
        if(unit == null|| !slot.TryAssign(unit))
        {
            if(unit!=null)
            {
                Destroy(unit);
            }
            //배치실패 카드반환 
            unitInventory.Add(job);
        }



        foreach (MonoBehaviour component in
              unit.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (component is IUnitInitializer initializer)
            {
                initializer.Initialize(gameManager);
            }
        }


        selectedJob = null;

        Debug.Log($"Summoned: {selectedJob}");




    }

    public bool HasPrefab(JobType job)
    {
        return FindPrefab(job) !=null;


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
