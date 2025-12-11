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
    float MouseSensitivity = 10;
    public float WeaponCoolDown = 1.0f;
    float NextFireTime = 0;

    float NextAccTime = 0;
    public float BulletSpeed = 30;

    public GameObject BulletPrefab;

    public GameObject CannonHitEffect;
    public GameObject[] SetInvisible;

    public bool isControl;
    public GameObject hp;
    public GameObject hpbg;


    public void BeHit()
    {
        float width =  this.hpbg.GetComponent<RectTransform>().rect.width;
        float maxhp = this.GetComponent<Basic>().maxHP;
        float nowhp = this.GetComponent<Basic>().HP;
        RectTransform hprect = this.hp.GetComponent<RectTransform>();

        //Vector2 sizeDelta= hprect.sizeDelta;
        hprect.sizeDelta = new Vector2(width*(nowhp/maxhp), hprect.sizeDelta.y);
        print(hp.transform.position);
    }


    void checkhp()
    {
        RectTransform hprect = this.hp.GetComponent<RectTransform>();
        
       
    }
    void Start()
    {
        UIMgr.Instance.canvas = GameObject.Instantiate(Resources.Load<Canvas>("UI/Canvas"));
        UIMgr.Instance.ShowPanel<hpPanel>();
        hp= GameObject.Find("hp");
        hpbg= GameObject.Find("hp_back");
        this.isControl = true;
        player_rb = this.GetComponent<Rigidbody>();
        var enemy = this.GetComponent<Enemy>();
        MouseSensitivity = DataMgr.Instance.GetSoundData().mouseSensitivity*1000;
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
            if (collision.gameObject.layer != LayerMask.NameToLayer("FixedWall"))
                Destroy(collision.gameObject);
        }

        //if(collision.gameObject.layer==LayerMask.NameToLayer("END"))
        //{
        //    Camera.main.GetComponent<Camera1>().Rotate();
        //    this.isControl = false;
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("END"))
        {

            this.isControl = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UIMgr.Instance.ShowPanel<EndPanel>();
        }
    }
    private void OnTrigge(Collider other)
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if(!UIMgr.Instance.isPause)
        {
            if (this.NextAccTime <= Time.time)
                this.PlayerStatus = (int)Player.Status.Default;
            if (isControl)
            {
                if (this.PlayerStatus != (int)Player.Status.Accelerating)
                    BasicMove();
                ViewMove();
                Fire();
                Accelerate();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    UIMgr.Instance.ShowPanel<PausePanel>();
                    UIMgr.Instance.isPause = true;
                }
                //重新点击后再次进入鼠标开启
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                }

            }

        }




    }
}
