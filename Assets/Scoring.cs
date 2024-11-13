using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    bool callOnce = false;
    SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        if (sceneManager == null){
            Debug.Log("Scene Manager not found on goal game object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Ball"){
            if (!callOnce){
                callOnce = true;
                sceneManager.BallHit();
                sceneManager.handleGameWin();
            }
        }
    }



}
