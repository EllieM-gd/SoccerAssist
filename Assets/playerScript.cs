using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject ball;

    public int kickStrength = 1500;

    private bool hasKicked = false;
    SceneManager sceneManager;

    public int degreesLeft;
    public int degreesRight;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(handleKickTime(5.0f));
        StartCoroutine(moveBackAndForth());        
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (!hasKicked){
                hasKicked = true;
                sceneManager.ballKicked();
                StopCoroutine(moveBackAndForth());
                kickBall();
            }
        }
    }


    void kickBall(){
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * kickStrength);
    }


    void endGameRestart(){
        sceneManager.handleGameOver();
    }

    IEnumerator handleKickTime(float time){
        yield return new WaitForSeconds(time);
        if (!hasKicked) endGameRestart();
        yield break;
    }

    


    IEnumerator moveBackAndForth(){
        while (!hasKicked){
            //IGNORE THIS REALLY BAD CODE
            for (int i = 0; i < degreesRight * 2; i++){
                transform.Rotate(0, 0.5f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < degreesRight * 2; i++){
                transform.Rotate(0, -0.5f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < degreesLeft * 2; i++){
                transform.Rotate(0, -0.5f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < degreesLeft * 2; i++){
                transform.Rotate(0, 0.5f, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }




}
