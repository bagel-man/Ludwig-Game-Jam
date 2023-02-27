using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class asdfasd : MonoBehaviour
{
    public GameObject credit;
    public string scenetoload;
    private bool tgrue = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start(){
          SceneManager.LoadScene(scenetoload);
    }
        public void Credits(){
            if(!tgrue){
          credit.SetActive(true);
          tgrue = true;
          return;
            }
            if(tgrue){
                            tgrue = false;
          credit.SetActive(false);
            }
    }
}
