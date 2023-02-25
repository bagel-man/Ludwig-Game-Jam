using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject UIprompt;
    private bool canpress = false;
    public GameObject[] setactive;
        public GameObject[] setfalse;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canpress){
            if(Input.GetKeyDown(KeyCode.E)){
                       anim.Play("press");
                for(int i = 0; i < setactive.Length; i++){
                                 setactive[i].SetActive(true);
                }
                
                for(int i = 0; i < setfalse.Length; i++){
                                 setfalse[i].SetActive(false);
                }

            }
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            UIprompt.SetActive(true);
            canpress = true;
        }
    }
            private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            UIprompt.SetActive(false);
            canpress = false;
        }
    }
}
