using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.Mathematics;
using UnityEngine;

public class ufoHover : MonoBehaviour
{
    public float hoverheight = 0;
    float speed;
    public float hoverSpeed;
    public float huntSpeed;
    public Transform[] patrolPoints;
    public int targPoint = 0;
    public Animator aiState;
    public Animator camState;
    public Transform player;
    public Vector3 lastKnownPlayerPos;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hoverX = 0;
        float hoverY = 0;
        float hoverZ = 0;
        //patrol State
        if (aiState.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
        {
            speed = hoverSpeed;
            checkPatrol();
             hoverX = patrolPoints[targPoint].position.x;
             hoverY = hoverheight;
             hoverZ = patrolPoints[targPoint].position.z;
        }

        //aggro State
        if (aiState.GetCurrentAnimatorStateInfo(0).IsName("aggro"))
        {
            speed = huntSpeed;
            hoverX = player.position.x;
            hoverY = hoverheight;
            hoverZ = player.position.z;

        }

        //search State
        if (aiState.GetCurrentAnimatorStateInfo(0).IsName("search"))
        {
            speed = hoverSpeed;
            hoverX = lastKnownPlayerPos.x;
            hoverY = hoverheight;
            hoverZ = lastKnownPlayerPos.z;
            if(new Vector3(transform.position.x,hoverheight,transform.position.z) == new Vector3(lastKnownPlayerPos.x, hoverheight, lastKnownPlayerPos.z))
            {
                aiState.SetTrigger("searched");
            }
        }
        //kill State
        if (aiState.GetCurrentAnimatorStateInfo(0).IsName("BeamPlayer"))
        {
            hoverX = playerPos.x;
            hoverY = hoverheight;
            hoverZ = playerPos.z;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(hoverX,hoverY,hoverZ), speed * Time.deltaTime);
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
    public void BeamPlayer(GameObject victim)
    {
        victim.GetComponent<player_Movement>().beamed = true;
        victim.GetComponent<player_Movement>().ufoAttacker = transform.gameObject;

        playerPos = victim.transform.position;
        aiState.SetTrigger("beamPlayer");
        camState.SetTrigger("suck");

    }


}
