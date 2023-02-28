using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class nextscene : MonoBehaviour
{
        public string scene;
        public float waittime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        Invoke("next", waittime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void next(){
                SceneManager.LoadScene(scene);
    }
}
