using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public GameObject MissilePrefab;
    public GameObject MissileShot;
    public float WeaponCoolDwon = 1.0f;
    float NextLunchTime = 0;
    public GameObject MissilePos;
    public GameObject[] ShootPoint;
    //public GameObject DetectRange;
    public GameObject PlayerInComing;
    public GameObject TowerHeah;
    public float RotateSpeed = 10;
    // Start is called before the first frame update
    bool IsPlayerDetected = false;
    float Suspect = 0;
    public float SuspectRate = 10;
    public float DetectSuspect = 100;

    public void BeHit()
    {
        if ((Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position) < 15.0f))
        {
            this.IsPlayerDetected = true;
            return;
        }
    }
    void Rotate()
    {
        this.TowerHeah.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }

    void Lock()
    {
        Vector3 targetDirection = PlayerInComing.transform.position - TowerHeah.transform.position;
        targetDirection.y = 0; // 锁定Y轴，避免炮台上下倾斜
        if (targetDirection.magnitude > 0.1f)
        {
            // 3. 计算目标旋转角度（看向目标方向）
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // 4. 平滑旋转到目标角度（使用Lerp实现平滑过渡）
            TowerHeah.transform.rotation = Quaternion.Lerp(
                TowerHeah.transform.rotation,    // 当前旋转
                targetRotation,        // 目标旋转
                RotateSpeed/5 * Time.deltaTime  // 插值系数（与时间挂钩，确保不同帧率下速度一致）
            );


        }
       
    }
    void Detect()
    {
        if (this.PlayerInComing.IsDestroyed())
        {
            IsPlayerDetected = false;
            return;
        }

        if (Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position) < 15.0f)
        {
            Vector3 turretForward = this.TowerHeah.transform.forward;
            turretForward.y = 0; // 同样锁定Y轴，确保在同一平面
            Vector3 targetDirection = PlayerInComing.transform.position - TowerHeah.transform.position;
            targetDirection.y = 0; // 锁定Y轴，避免炮台上下倾斜
            float angleToPlayer = Vector3.Angle(turretForward, targetDirection);


            //print(transform.position);
            //print(targetDirection);
            if (angleToPlayer <= 60.0f)
            {

                Debug.DrawRay(transform.position + Vector3.up * 0.5f, targetDirection.normalized * 15, IsPlayerDetected ? Color.green : Color.red);
                if (Physics.Raycast(transform.position + Vector3.up * 0.5f, targetDirection.normalized, out RaycastHit hitInfo, 15.0f))
                {
                    //print(hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        print("hit");
                        print(Suspect);
                        if (Suspect < DetectSuspect)
                            Suspect += (60 - angleToPlayer) * (15 - Vector3.Distance(this.gameObject.transform.position, this.PlayerInComing.transform.position)) * Time.deltaTime;
                        if (Suspect >= DetectSuspect)
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
                    print("nohit");
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
        if (NextLunchTime <= Time.time)
        {
            for (int i = 0; i < ShootPoint.Length; i++)
            {
                GameObject MissileClone = Instantiate(MissilePrefab, ShootPoint[i].transform.position, MissilePos.transform.rotation);
                MissileClone.gameObject.GetComponent<Missile>().SetShooter(this.gameObject);
                GameObject MissileShotClone = Instantiate(MissileShot, ShootPoint[i].transform.position, MissilePos.transform.rotation);
                Destroy(MissileShotClone, 2);

            }
            NextLunchTime = Time.time + WeaponCoolDwon;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Detect();
        if(IsPlayerDetected)
        {
            Lock();
            Invoke("Fire", 0.5f);
          
        }
        else
        {
            Rotate();
        }
        
    }

}
