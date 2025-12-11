using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate1 : MonoBehaviour
{
    public GameObject TankHead;
    public float RotateSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 turretForward = this.transform.forward;
        turretForward.y = 0; // 同样锁定Y轴，确保在同一平面
        Vector3 Direction = this.TankHead.transform.forward;
        Direction.y = 0; // 锁定Y轴，避免炮台上下倾斜
        float angleToPlayer = Vector3.Angle(turretForward, Direction);
        if (angleToPlayer >= 30)
        {
            RotateSpeed *= -1;
        }
        this.TankHead.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.World);
    }
}
