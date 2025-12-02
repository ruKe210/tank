using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    enum Status
    {
        Default,Accelerating
    }
    int PlayerStatus = (int)Player.Status.Default;
    public float Acceleration = 10;
    public float AccelerationCoolDown = 1.0f;
    public GameObject TankHead;
    public GameObject[] Weapon;
    Rigidbody player_rb;
    public float move_speed = 10;
    public float roate_speed = 10;
    public float MouseSensitivity = 10;
    public float WeaponCoolDown = 1.0f;
    float NextFireTime = 0;

    float NextAccTime = 0;
    public float BulletSpeed = 30;

    public GameObject BulletPrefab;

    public GameObject CannonHitEffect;
    public GameObject[] SetInvisible;


    public GameObject hp;
    private void Awake()
    {
        //
    }

    //void BeHit()
    //{

    //}


    void checkhp()
    {
        RectTransform hprect = this.hp.GetComponent<RectTransform>();
        
       
    }
    void Start()
    {
        player_rb = this.GetComponent<Rigidbody>();
        var enemy = this.GetComponent<Enemy>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

    }
    void Fire()
    {
        if ((Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))&&NextFireTime<=Time.time&&Weapon!=null)
        {
            print("fire!");
            for(int i=0;i<Weapon.Length;i++)
            {
                GameObject Bullet = Instantiate(BulletPrefab, Weapon[i].transform.position, Weapon[i].transform.rotation);
                Bullet.GetComponent<Bullet>().SetShooter(gameObject);
                //print(Weapon[i].transform.position);
                //print(Weapon[i].transform.rotation);
                
                //Bullet.transform.Translate(Vector3.forward * BulletSpeed);
            }

            //GameObject Bullet = Instantiate(BulletPrefab, Weapon[0].transform.position, Weapon[0].transform.rotation);
            //Bullet.transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
            this.GetComponent<AudioSource>().Play();
            //var FireSound = this.transform.Find("CannonShoot");
            //FireSound.GetComponent<AudioSource>().Play();
            NextFireTime = Time.time + WeaponCoolDown;
        }
 

    }
    void ViewMove()
    {
        TankHead.transform.Rotate(Vector3.up*Input.GetAxis("Mouse X")*MouseSensitivity*Time.deltaTime);
        //float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        //TankHead.transform.Rotate(TankHead.transform.right, scrollInput);
    }
    void BasicMove()
    {

        
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * move_speed*Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            
            this.transform.Translate(Vector3.back * move_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {

            // 按下A键向左旋转（使用负的扭矩）
           
            if(Input.GetKey(KeyCode.S))
                this.transform.Rotate(Vector3.up * roate_speed * Time.deltaTime);
            else
                this.transform.Rotate(Vector3.up * -roate_speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            // 按下D键向右旋转（使用正的扭矩）
            if(Input.GetKey(KeyCode.S))
                this.transform.Rotate(Vector3.up * -roate_speed * Time.deltaTime);
            else
                this.transform.Rotate(Vector3.up * roate_speed * Time.deltaTime);
            
        }
    }
    void Accelerate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && NextAccTime <= Time.time)
        {
            this.player_rb.AddRelativeForce(Vector3.forward *this.player_rb.mass*3000);
            //this.transform.Translate(Vector3.forward * move_speed * Acceleration*Time.deltaTime);
            NextAccTime = Time.time + AccelerationCoolDown;
            //print(Time.deltaTime);
            this.PlayerStatus = (int)Player.Status.Accelerating;
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift)&& Input.GetKey(KeyCode.S) && NextAccTime <= Time.time)
        {
            this.player_rb.AddRelativeForce(Vector3.back * this.player_rb.mass * 3000);
            //this.transform.Translate(Vector3.forward * move_speed * Acceleration*Time.deltaTime);
            NextAccTime = Time.time + AccelerationCoolDown;
            //print(Time.deltaTime);
            this.PlayerStatus = (int)Player.Status.Accelerating;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(this.PlayerStatus==(int)Player.Status.Accelerating)
        {
            var CannonHitEffectClone = Instantiate(CannonHitEffect, this.transform.position, this.transform.rotation);
            CannonHitEffectClone.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (this.NextAccTime <= Time.time)
            this.PlayerStatus = (int)Player.Status.Default;
        if (this.PlayerStatus!=(int)Player.Status.Accelerating)
            BasicMove();
        ViewMove();
        Fire();
        Accelerate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        //重新点击后再次进入鼠标开启
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
  


    }
}
