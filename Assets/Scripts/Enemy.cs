using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CoverRange;
    public GameObject[] PatrolPos;




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
    public GameObject PlayerInComing;
    public GameObject CannonHitEffect;
    public GameObject[] SetInvisible;
    public float RotateSpeed = 10;
    float Suspect = 0;
    public float SuspectRate = 10;
    public float DetectSuspect=100;
    bool  IsPlayerDetected=false;
    bool  IsHit = false;

    float visbleTime = 5;

    public Texture HP;
    public Texture HPbg;

    Rect HPPos;
    Rect HPPosBack;

    float HPWidth;
    float HPHeight;
 

    float TurnCool = 10;
    float NextTurn = 0;
    int PosNum = 0;

    bool flag = true;
    public void BeHit()
    {
        if((Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position) < 50.0f))
        {
            this.IsPlayerDetected = true;
            return;
        }
    }
    void Detect()
    {
        if (this.PlayerInComing.IsDestroyed())
        {
            IsPlayerDetected = false;
            return;
        }
  
        if (Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position) < 50.0f)
        {
            Vector3 turretForward = this.TankHead.transform.forward;
            turretForward.y = 0; // 同样锁定Y轴，确保在同一平面
            Vector3 targetDirection = PlayerInComing.transform.position - TankHead.transform.position;
            targetDirection.y = 0; // 锁定Y轴，避免炮台上下倾斜
            float angleToPlayer = Vector3.Angle(turretForward, targetDirection);

        
            //print(transform.position);
            //print(targetDirection);
            if (angleToPlayer <= 90.0f)
            {
                    
                Debug.DrawRay(transform.position  + Vector3.up * 0.5f, targetDirection.normalized * 15, IsPlayerDetected ? Color.green : Color.red);
                if (Physics.Raycast(transform.position +  Vector3.up * 0.5f, targetDirection.normalized, out RaycastHit hitInfo, 15.0f))
                {
                    //print(hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        print("hit");
                        //print(Suspect);
                        if (Suspect<DetectSuspect)
                            Suspect += (60-angleToPlayer)* (15-Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position)) * Time.deltaTime;
                        if(Suspect>=DetectSuspect)
                        {
                            IsPlayerDetected = true;
                        }
                            
                    }
                    else
                    {
                        IsPlayerDetected = false;
                        Suspect = 0;
                    }

                }
                else
                {
                    //print("nohit");
                    IsPlayerDetected = false;
                    Suspect = 0;
                }
            }

           
        }
        else
        {
            IsPlayerDetected = false;
            Suspect = 0;
        }
    }
    void Fire()
    {
        if ( NextFireTime <= Time.time && Weapon != null)
        {
            print("fire!");
            for (int i = 0; i < Weapon.Length; i++)
            {
                GameObject Bullet = Instantiate(BulletPrefab, Weapon[i].transform.position, Weapon[i].transform.rotation);
                if(Bullet.GetComponent<AudioSource>()!=null)
                {
                    Bullet.GetComponent<AudioSource>().mute = !DataMgr.Instance.GetSoundData().isSound;
                    Bullet.GetComponent<AudioSource>().volume = DataMgr.Instance.GetSoundData().soundVolume;
                }
   
                Bullet.GetComponent<Bullet>().SetShooter(gameObject);
 
            }

            this.GetComponent<AudioSource>().Play();

            NextFireTime = Time.time + WeaponCoolDown;
        }


    }
    void Lock()
    {
        Vector3 targetDirection = PlayerInComing.transform.position - TankHead.transform.position;
        targetDirection.y = 0; // 锁定Y轴，避免炮台上下倾斜
        if (targetDirection.magnitude > 0.1f)
        {
            // 3. 计算目标旋转角度（看向目标方向）
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // 4. 平滑旋转到目标角度（使用Lerp实现平滑过渡）
            TankHead.transform.rotation = Quaternion.Lerp(
                TankHead.transform.rotation,    // 当前旋转
                targetRotation,        // 目标旋转
                RotateSpeed / 5 * Time.deltaTime  // 插值系数（与时间挂钩，确保不同帧率下速度一致）
            );


        }

    }
    void Patrol()
    {

        //Vector3 turretForward = this.transform.forward;
        //turretForward.y = 0; // 同样锁定Y轴，确保在同一平面
        //Vector3 Direction = this.transform.position;
        //Direction.y = 0; // 锁定Y轴，避免炮台上下倾斜
        //float angleToPlayer = Vector3.Angle(turretForward, Direction);
        //if (angleToPlayer >= 30)
        //{
        //    roate_speed *= -1;
        //}
        //this.TankHead.transform.Rotate(Vector3.up * roate_speed * 30 * Time.deltaTime, Space.World);


        Vector3 targetDirection = PatrolPos[PosNum].transform.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(
                transform.rotation,    // 当前旋转
                targetRotation,        // 目标旋转
                RotateSpeed * Time.deltaTime  // 插值系数（与时间挂钩，确保不同帧率下速度一致）
            );

        this.transform.Translate(this.transform.forward  *move_speed* Time.deltaTime, Space.World);

        //print(PosNum);
        //print(Vector3.Distance(this.transform.position, PatrolPos[PosNum].transform.position));
        if (Vector3.Distance(this.transform.position, PatrolPos[PosNum].transform.position) < 2f)
        {
            if (flag)
                PosNum++;
            else
                PosNum--;
            if (PosNum >= PatrolPos.Length )
            {
                flag = !flag;
                PosNum -= 2;
            }
            if(PosNum < 0)
            {
                flag = !flag;
                PosNum+=2;
            }
               
        }
  
    }
    void Follow()
    {
        if(Vector3.Distance(this.transform.position,CoverRange.transform.position)>50.0f)
        {
            this.IsPlayerDetected = false;
            return;
        }
        Vector3 targetDirection = PlayerInComing.transform.position - transform.position;
        targetDirection.y = 0; // 锁定Y轴，避免炮台上下倾斜

        Vector3 turretForward = this.TankHead.transform.forward;
        turretForward.y = 0; // 同样锁定Y轴，确保在同一平面

        float angleToPlayer = Vector3.Angle(turretForward, targetDirection);
        if (targetDirection.magnitude > 0.1f)
        {
            // 3. 计算目标旋转角度（看向目标方向）
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            //4.平滑旋转到目标角度（使用Lerp实现平滑过渡）
            transform.rotation = Quaternion.Lerp(
                transform.rotation,    // 当前旋转
                targetRotation,        // 目标旋转
                RotateSpeed * Time.deltaTime  // 插值系数（与时间挂钩，确保不同帧率下速度一致）
            );


        }
        if (Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position) > 5.0f)
        {

            this.transform.Translate(this.transform.forward *move_speed* Time.deltaTime,Space.World);
        }
            

    }

    private void OnGUI()
    {

        if(IsPlayerDetected)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y = Screen.height - screenPos.y;

            HPPosBack.x = screenPos.x - 50;
            HPPosBack.y = screenPos.y - 45;
            HPPosBack.width = 100;
            HPPosBack.height = 10;


            GUI.DrawTexture(HPPosBack, HPbg);

            HPPos = HPPosBack;
            HPPos.width = this.GetComponent<Basic>().HP / this.GetComponent<Basic>().maxHP * HPPosBack.width;
            GUI.DrawTexture(HPPos, HP);

        }



    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIMgr.Instance.isPause)
        {
            if (IsPlayerDetected)
                print(IsPlayerDetected);

            if (IsPlayerDetected)
            {
                Lock();
                Fire();
                Follow();
            }
            else
            {
                Detect();
                Patrol();
                Vector3 turretForward = this.transform.forward;
                turretForward.y = 0; // 同样锁定Y轴，确保在同一平面
                Vector3 Direction = this.TankHead.transform.forward;
                Direction.y = 0; // 锁定Y轴，避免炮台上下倾斜
                float angleToPlayer = Vector3.Angle(turretForward, Direction);
                //print(angleToPlayer);
                if (angleToPlayer >= 30)
                {
                    roate_speed *= -1;
                }
                this.TankHead.transform.Rotate(Vector3.up * roate_speed * Time.deltaTime, Space.World);
            }
        }
          
 
        
    }
}
