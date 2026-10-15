using UnityEngine;
using System;

/*
 
기지의 현재 체력 관리
피해 적용
체력이 0이 되면 OnBaseDestroyed 이벤트 발생 

 */

public class BaseHealth : MonoBehaviour
{






    [SerializeField]
    private int maxHealth = 20;

    public int CurrentHealth { get; private set; }

    public int MaxHealth => maxHealth;

    //<CurrentHealth, MaxHealth>
    public event Action<int, int> OnHealthChanged;
    public event Action OnBaseDestroyed;


    public bool isDestroy = false;



    void Awake()
    {

        CurrentHealth =Mathf.Max(1, maxHealth);
        isDestroy = false;

    }

    public void TakeDamage(int damage)
    {
        if (isDestroy || damage <= 0)
            return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);


        Debug.Log(
           $"Base HP: {CurrentHealth}/{maxHealth}"
       );

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);


        //체력이 0 이면 파괴
        if(CurrentHealth==0)
        {
            isDestroy = true;
            Debug.Log("Base Destroyed");
            OnBaseDestroyed?.Invoke();
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
