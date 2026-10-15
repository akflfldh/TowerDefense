using UnityEngine;
using System;
using Unity.Collections.Tests.CoreCLR.TestJobs;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    private float maxHealth = 10.0f; 

    public float CurrentHealth { get; private set; }

    public event Action<EnemyHealth> OnDied;

    private bool isDead = false;

    void Awake()
    {
        CurrentHealth = maxHealth;

    }

    public void TakeDamage(float damage)
    {

        if (isDead || damage <= 0.0f)
            return;


        CurrentHealth = Mathf.Max(0.0f, CurrentHealth - damage);



        Debug.Log("Enemy HP : " + CurrentHealth);
        //Die 판정
        if(CurrentHealth <=0.0f)
        {
            Die();
        }

    }


    
    private void Die()
    {

        if (isDead)
            return;



        isDead = true;
        OnDied?.Invoke(this);

        Destroy(gameObject);


    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("Test  Damage (5)")]
    private void TestDamage()
    {
        if (!Application.isPlaying)
            return;

        TakeDamage(5.0f);



    }



}
