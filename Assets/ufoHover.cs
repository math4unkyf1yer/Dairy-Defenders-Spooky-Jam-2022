using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ufoHover : MonoBehaviour
{
    public float hoverheight = 0;
    public float hoverSpeed;
    public Transform[] patrolPoints;
    public int targPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPatrol();
        float hoverX = patrolPoints[targPoint].position.x;
        float hoverY = hoverheight;
        float hoverZ = patrolPoints[targPoint].position.z;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(hoverX,hoverY,hoverZ), hoverSpeed * Time.deltaTime);
    }
    void checkPatrol()
    {
        if( transform.position == new Vector3(patrolPoints[targPoint].position.x, hoverheight, patrolPoints[targPoint].position.z))
        {
            if(targPoint != patrolPoints.Length -1)
            {
                targPoint = targPoint + 1;
            }
            else
            {
                
                targPoint = 0;
            }
        }
    }


}
