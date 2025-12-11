using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float Force =10;
    Rigidbody rb;
    public float Demage=1;
    GameObject shooter;
    public GameObject MissileBeHitEffect;
    // Start is called before the first frame update

    public void SetShooter(GameObject _shooter)
    {
        this.shooter = _shooter;
        //print(shooter+"123");

    }
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
         rb.AddForce(this.transform.forward * Force, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject!=this.shooter.gameObject&& collision.gameObject.layer != LayerMask.NameToLayer("Missile"))
        {
            print(collision.gameObject.name);
            Vector3 hitpos = this.transform.position;
            hitpos.y = 0;
            var MissileBeHitEffectClone = Instantiate(MissileBeHitEffect, hitpos, this.transform.rotation);
            MissileBeHitEffectClone.GetComponent<AudioSource>().Play();
            Destroy(MissileBeHitEffectClone, 2);
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
             
                    Destroy(this.gameObject);
            }
            else
            {
                if(collision.gameObject.layer==LayerMask.NameToLayer("Player"))
                {
                    collision.gameObject.GetComponent<Basic>().HP-=1;
                    collision.gameObject.GetComponent<Player>().BeHit();


                    print(collision.gameObject.name + "Be Hit, -1Hp");
                }
                else
                {
                    if (collision.gameObject.layer != LayerMask.NameToLayer("FixedWall"))
                        Destroy(collision.gameObject);
                }
                    
                Destroy(this.gameObject);
            }
        }
     
    }
    
    // Update is called once per frame
    void Update()
    {

       
    }
}
