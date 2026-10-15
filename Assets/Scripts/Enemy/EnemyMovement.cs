using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private WayPointPath path;

    [SerializeField]
    private float moveSpeed= 2.0f;



    public event Action<EnemyMovement> OnReachedGoal;


    private int nextWaypointIndex = 1;
    private bool initialized = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!initialized)
            Initialize(path);

    }

    public void Initialize(WayPointPath newPath)
    {
        if(newPath == null || newPath.Count< 2)
        {
            Debug.Log("Invalid WaypointPath");
            enabled = false;
            return;

        }


        path = newPath;
        transform.position = path.GetPoint(0);

        nextWaypointIndex = 1;
        initialized = true;
        enabled = true;


    }

    // Update is called once per frame
    void Update()
    {


        if (!initialized)
            return;

        Vector3 target = path.GetPoint(nextWaypointIndex);

        //move 
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);


        //Reach Waypoint
        float distanceSqr = (transform.position - target).sqrMagnitude;

        if (distanceSqr < 0.0001f)
        {

            nextWaypointIndex++;
            if(nextWaypointIndex >= path.Count)
            {
                //목적지 도달
                ReachedGoal();

            }

        }


        
    }

    void ReachedGoal()
    {
        enabled = false;

        OnReachedGoal?.Invoke(this);

        Destroy(gameObject);

    }




}
