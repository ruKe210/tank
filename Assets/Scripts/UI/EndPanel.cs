using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : BasePanel
{

    public Text text;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        
        quit.onClick.AddListener(() =>{
            SceneManager.LoadScene("BeginScene");
            UIMgr.Instance.HidePanel<EndPanel>();
            UIMgr.Instance.HidePanel<hpPanel>();
        });

    }
    public void ChangeTitle(string text)
    {
        this.text.text = text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
