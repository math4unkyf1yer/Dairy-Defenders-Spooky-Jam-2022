using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class RayShoot : MonoBehaviour
{
    public bool isShooting;
    public float recoil = 1.8f;
    public int gunDamage = 100;
    public float fireRate = 0.25f;
    public float weaponRange = 100f;
    public float hitforce = 100f;
    public Transform gunEnd;
    private Camera fpsCamera;
    private WaitForSeconds shotDuration = new WaitForSeconds(.3f);
    private LineRenderer laserLine;
    [Header("paricle for later")]
    public GameObject startShotPT;
    public GameObject endShotPT;
    public int Ammo = 1000;
    public LayerMask ufoLayer;
    [Header("for now not needed")]
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShotEffect());
            Shooting();
        }
    }

    public void Shooting()
    {
        if(Ammo >= 100)
        {
            isShooting = true;
            //Start particle will be here 

            Ammo -= 100;

         //   Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0.0f));
         

            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if(Physics.Raycast(gunEnd.position,transform.forward,out hit,weaponRange,ufoLayer))
            {
                laserLine.SetPosition(1, hit.point);
                EnemyHitBox health = hit.collider.GetComponent<EnemyHitBox>();
                if(health != null)
                {
                    health.Damage(gunDamage);
                }
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitforce);
                }
            }
            else
            {
                laserLine.SetPosition(1, gunEnd.position + (transform.forward * weaponRange));
            }
            //impact particle
        }
    }
    public IEnumerator ShotEffect()
    {
        //if audio needed
        if(Ammo >= 100)
        {
            laserLine.enabled = true;
            yield return shotDuration;
            laserLine.enabled = false;
        }
    }
    public void ChangeMaterial()
    {
        laserLine.material.SetColor("laser", new Color(1f, 1f, 1f, 0.3f));
    }
    public void SetCustomeEmission(string materialFile, float intensity)
    {
        Material mat = (Material)AssetDatabase.LoadAssetAtPath(materialFile, typeof(Material));
        Color color = mat.GetColor("_Color");

        float adjustedIntensity = intensity - (100);

        color *= Mathf.Pow(2.0f, adjustedIntensity);
        mat.SetColor("_EmissionColor", color);
    }
}
