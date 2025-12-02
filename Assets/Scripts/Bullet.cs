using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 200;
    GameObject shooter;
    public float Demage = 1;
    public GameObject CannonHitEffect;
    public float EliminateDistancce=100;

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        if (collision.gameObject != shooter)
        {
            var CannonHitEffectClone = Instantiate(CannonHitEffect, this.transform.position, this.transform.rotation);
            CannonHitEffectClone.GetComponent<AudioSource>().Play();
            

            
            if(collision.gameObject.layer==LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<Basic>().HP--;
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if(this.gameObject.layer!= LayerMask.NameToLayer("Enemy"))
                {
                    if(collision.gameObject.GetComponent<Enemy>()!=null)
                        collision.gameObject.GetComponent<Enemy>().BeHit();
                    else
                        collision.gameObject.GetComponent<EnemyTower>().BeHit();
                    collision.gameObject.GetComponent<Basic>().HP--;
                }
                   
            }
            else
            {
                Destroy(collision.gameObject);
            }

            
            Destroy(this.gameObject);
            Destroy(CannonHitEffectClone, 2);
        }


    }
    public void SetShooter(GameObject _shooter)
    {
        this.shooter = _shooter;
        //print(shooter+"123");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        //print(this.transform.position);
        Vector3 bulldir = transform.forward;
        //print(bulldir);
        bulldir.y = 0;
        rb.AddForce(bulldir * BulletSpeed);
        //print(this.shooter + "456");
        float distance = Vector3.Distance(this.transform.position, shooter.transform.position);
        if (distance >= EliminateDistancce)
        {
            Destroy(this.gameObject);
        }

    }
  
}

