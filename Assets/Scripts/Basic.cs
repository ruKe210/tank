using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Basic : MonoBehaviour
{
    public GameObject DestroyAnimation;


    // Start is called before the first frame update
    public float HP=10;
    public float maxHP = 10;
    void BeKilled()
    {
        if (this.HP <= 0)
        //gameObject.SetActive(false);
        {
            if (this.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UIMgr.Instance.ShowPanel<EndPanel>();
                UIMgr.Instance.Getpanel<EndPanel>().ChangeTitle("Failed");
            }
            Destroy(this.gameObject);
        }
           
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BeKilled();
    }
    private void OnDestroy()
    {
        
        var DestroyAnimationClone = Instantiate(this.DestroyAnimation, this.transform.position, this.transform.rotation);
        Destroy(DestroyAnimationClone, 2);
    }
}
