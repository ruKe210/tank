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
    public void BeKilled()
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

            var DestroyAnimationClone = Instantiate(this.DestroyAnimation, this.transform.position, this.transform.rotation);
            if (DestroyAnimationClone.GetComponent<AudioSource>() != null)
            {
                //DestroyAnimationClone.GetComponent<AudioSource>().mute = !DataMgr.Instance.GetSoundData().isSound;
                DestroyAnimationClone.GetComponent<AudioSource>().mute = !DataMgr.Instance.GetSoundData().isSound;
                DestroyAnimationClone.GetComponent<AudioSource>().volume = DataMgr.Instance.GetSoundData().soundVolume;
            }


            Destroy(DestroyAnimationClone, 2);

            Destroy(this.gameObject);
        }
           
    }
    void Start()
    {

    }
    public void kill ()
    {
        var DestroyAnimationClone = Instantiate(this.DestroyAnimation, this.transform.position, this.transform.rotation);
        if (DestroyAnimationClone.GetComponent<AudioSource>() != null)
        {
            //DestroyAnimationClone.GetComponent<AudioSource>().mute = !DataMgr.Instance.GetSoundData().isSound;
            DestroyAnimationClone.GetComponent<AudioSource>().mute = !DataMgr.Instance.GetSoundData().isSound;
            DestroyAnimationClone.GetComponent<AudioSource>().volume = DataMgr.Instance.GetSoundData().soundVolume;
        }


        Destroy(DestroyAnimationClone, 2);

        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        BeKilled();
    }
 
}
