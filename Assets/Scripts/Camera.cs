using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Camera1 : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHead;
    float MouseSensitivity;
    public Vector3 offset = new Vector3(0, 4, -4); // 相机与目标的偏移量（x:左右, y:高低, z:前后）
    public float smoothSpeed = 1f; // 跟随平滑度（值越大越灵敏）
    Vector3 FinalPos = new Vector3(0, 0, 0);
    private float currentHorizontalAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        MouseSensitivity = Player.GetComponent<Player>().MouseSensitivity;
    }
    public void Rotate()
    {
        this.transform.RotateAround(FinalPos, Vector3.up, 50 * Time.deltaTime);
        this.transform.LookAt(FinalPos + Vector3.up);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Player.IsDestroyed()||Player.GetComponent<Player>().isControl==false)
        {
            Rotate();

            return;
        }
        else
        {
            FinalPos = Player.transform.position;
            Quaternion dir = PlayerHead.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(30, dir.eulerAngles.y, dir.eulerAngles.z);
            transform.position = Vector3.Lerp(transform.position, Player.transform.position + Vector3.up * 4 - PlayerHead.transform.forward * 4, smoothSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(
               transform.rotation,    // 当前旋转
               targetRotation,        // 目标旋转
               10 * Time.deltaTime  // 插值系数（与时间挂钩，确保不同帧率下速度一致）
           );
        }    
        
    }
}

