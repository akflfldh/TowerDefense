using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Transform spawnPoint;



    [SerializeField]
    private GameObject selectionMark;

    private GameObject currentUnit;

    public event Action<UnitSlot> OnSlotClicked;

    public bool IsOccupied => currentUnit != null;

    public GameObject CurrentUnit => currentUnit;

    public Vector3 SpawnPosition => spawnPoint != null ? spawnPoint.position : transform.position;

    void Awake()
    {
        SetSelected(false);

    }


    public void SetSelected(bool selected)
    {

        if (selectionMark != null)
            selectionMark.SetActive(selected);


    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
        OnSlotClicked?.Invoke(this);

    }

    //배치
    public bool TryAssign(GameObject unit)
    {
        if (IsOccupied || unit == null)
            return false;

        currentUnit = unit;
    
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
