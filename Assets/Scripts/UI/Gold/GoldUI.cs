using System;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private GoldManager goldManager;

    [SerializeField] private TMP_Text goldText;
    
    private void OnEnable()
    {
        if (goldManager == null)
            return;

        goldManager.OnGoldChanged += RefreshGold;

        RefreshGold(goldManager.CurrentGold);


    }

    private void OnDisable()
    {
        if (goldManager == null)
            return;

        goldManager.OnGoldChanged -= RefreshGold;

    }



    void RefreshGold(int gold)
    {


        if (goldText == null)
            return;
        goldText.text = $"{gold:N0} G";

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
