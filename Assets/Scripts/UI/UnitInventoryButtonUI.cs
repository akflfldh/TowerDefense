using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitInventoryButtonUI : MonoBehaviour
{


    [Header("Data")]
    [SerializeField] private JobType jobType;



    [Header("Reference")]
    [SerializeField] private UnitInventory unitInventory;
    [SerializeField] private UnitPlacementController unitPlacementController;
    [SerializeField] private TMP_Text countText;


    private Button button;

    private void Awake()
    {

        button = GetComponent<Button>();

    }


    private void OnEnable()
    {
        button.onClick.AddListener(HandleClick);

        if (unitInventory != null)
        {
            unitInventory.OnCountChanged += HandleCountChanged;
        }

        Refresh();
    }

    private void OnDisable()
    {

        button.onClick.RemoveListener(HandleClick);

        if (unitInventory != null)
        {
            unitInventory.OnCountChanged -= HandleCountChanged;

        }

    }

    private void HandleClick()
    {

        if (unitPlacementController == null)
            return;

        unitPlacementController.SelectJob(jobType);


    }


    private void HandleCountChanged(JobType job, int count)
    {

        if (job != jobType)
            return;


        Refresh();

    }

    void Refresh()
    {
        if (unitInventory == null)
            return;

        int count = unitInventory.GetCount(jobType);
        if(countText != null)
        {
            countText.text = $"x {count}";
        }

        button.interactable= count> 0 && unitPlacementController != null &&
            unitPlacementController.HasPrefab(jobType);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
