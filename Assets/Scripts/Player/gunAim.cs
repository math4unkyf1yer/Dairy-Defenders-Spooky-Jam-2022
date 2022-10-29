using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAim : MonoBehaviour
{

    public Transform aimPoint;
    public Transform gunPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aimPoint && gunPos)
        {
            gunPos.LookAt(aimPoint,Vector3.left);
        }
    }
}
