using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitPlacementController : MonoBehaviour
{

    [Serializable]
    private class JobPrefabEntry
    {
        public JobType job;
        public GameObject prefab;


    }


    [SerializeField]
    private BattleUIController battleUIController;



    [Header("Managers")]
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private UnitInventory unitInventory;




    [SerializeField]
    private UnitSlot[] slots;


    [Header("Selection")]
    private JobType? selectedJob = null;


    private UnitSlot selectedSlot;

    public UnitSlot SelectedSlot => selectedSlot;


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


 




    private void HandleSlotClicked(UnitSlot slot)
    {

        if (gameManager == null || unitInventory == null || gameManager.State != GameState.Playing)
            return;


        if (slot == null)
            return;

        SetSelectedSlot(null);

        if (slot.IsOccupied)
        {

            UnitRuntimeStats stats = slot.CurrentUnit.GetComponentInChildren<UnitRuntimeStats>(true);

            if (stats == null)
                return;
            //info panel show 

            if (battleUIController != null)
                battleUIController.OpenUnitInfo(stats);

            return;
        }

        battleUIController?.CloseUnitInfo();

        SetSelectedSlot(slot);
   
        Debug.Log("소환할 슬롯을 선택했습니다.");

    }

  
    private void SetSelectedSlot(UnitSlot slot)
    {
        if(selectedSlot !=null)
        {
            selectedSlot.SetSelected(false);
        }

        selectedSlot = slot;
        if(selectedSlot!=null)
        {
            selectedSlot.SetSelected(true);   

        }


    }



    public bool PlaceUnit(GameObject prefab)
    {
        if (selectedSlot == null)
            return false;


        GameObject unit = Instantiate(prefab, selectedSlot.SpawnPosition, Quaternion.identity);


        if (!selectedSlot.TryAssign(unit))
        {
            Destroy(unit);
            return false;
        }

        foreach (MonoBehaviour component in unit.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (component is IUnitInitializer initializer)
            {
                initializer.Initialize(gameManager);
            }

        }


        SetSelectedSlot(null);
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
