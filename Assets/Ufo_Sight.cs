using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo_Sight : MonoBehaviour
{
    public GameObject player;
    public LayerMask rayMask;
    public Animator aiState;
    Vector3 lastPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        RaycastHit hit;
        Vector3 dir = (transform.parent.position - other.transform.position).normalized;

        if (Physics.Raycast(transform.parent.position, -dir, out hit, Mathf.Infinity,rayMask) && hit.transform.gameObject.tag == "Player")
        {
            lastPlayerPos = player.transform.position;
            Debug.DrawRay(transform.parent.position, -dir * 100, Color.yellow,1f);
            Debug.Log(hit.transform);
            InSight();
            // Debug.DrawRay(transform.position, dir, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.parent.position, -dir * 1000, Color.red, 1f);
            //Debug.Log(hit.transform);
            Hidden(lastPlayerPos);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        Hidden(lastPlayerPos);

    }

    void InSight()
    {
        aiState.SetBool("canSee",true);

    }

    void Hidden(Vector3 lastSeen)
    {
        aiState.SetBool("canSee", false);
        transform.parent.GetComponent<ufoHover>().lastKnownPlayerPos = lastSeen;

    }
}
