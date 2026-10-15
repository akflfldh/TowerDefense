using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BaseHealthUI : MonoBehaviour
{

    [SerializeField]
    private BaseHealth baseHealth;

    [SerializeField]
    private TMP_Text hpText;


    [SerializeField]
    private Image hpBarFill;

    

    private void OnEnable()
    {
        if (baseHealth == null)
            return;

        baseHealth.OnHealthChanged += HandleHealthChanged;

        Refresh();


    }

    private void OnDisable()
    {
        if (baseHealth != null)
            baseHealth.OnHealthChanged -= HandleHealthChanged;
    }

    void HandleHealthChanged(int current,int max)
    {


        UpdateUI(current, max);
    }

    void Refresh()
    {

        UpdateUI(
           baseHealth.CurrentHealth,
           baseHealth.MaxHealth
       );
    }

    void UpdateUI(int current , int max)
    {

        if(hpText !=null)
        {
            hpText.text = $"HP {current}/{max}";
        }


        if(hpBarFill !=null)
        {
            hpBarFill.fillAmount = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;
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
