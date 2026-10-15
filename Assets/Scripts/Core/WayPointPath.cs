using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    public int Count => waypoints.Length;

    public Vector3 GetPoint(int index)
    {
        return waypoints[index].position;

    }


    private void OnDrawGizmos()
    {

        if (waypoints == null) return;

        Gizmos.color = Color.yellow;
        for(int i=0; i<waypoints.Length; ++i)
        {

            if (waypoints[i] == null)
                continue;
            
            Gizmos.DrawSphere(waypoints[i].position,0.1f);

            if(i+1 < waypoints.Length && waypoints[i+1] !=null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }

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
