using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    enum team {Friendly, Opponent};
    [SerializeField] team AISide;
    public GameObject aimTarget;

    public Material friendlyMaterial;
    public GameObject ball;
    public Material opponentMaterial;
    public bool moveSideToSide = false;
    public float moveDistance = 50.0f;

    bool actionAlreadyComplete = false;
    SceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        if (ball == null) ball = GameObject.Find("Ball");
        if (aimTarget == null)aimTarget = GameObject.Find("AimTarget");
        if (moveSideToSide) StartCoroutine(moveAISideToSide());
        if (AISide == team.Friendly){
            GetComponent<Renderer>().material = friendlyMaterial;
        } else {
            GetComponent<Renderer>().material = opponentMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Ball" && !actionAlreadyComplete){
            if (AISide == team.Friendly){
                StartCoroutine(kickBallTowards(aimTarget));
            }
            else if (AISide == team.Opponent){
                grabBall();
                sceneManager.handleGameOver();
            }
            actionAlreadyComplete = true;
        }
    }

    IEnumerator kickBallTowards(GameObject target){
        //Stop the ball
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sceneManager.BallHit();
        //Wait for a second
        yield return new WaitForSeconds(.5f);
        //Calculate the direction to the target
        Vector3 direction = target.transform.position - ball.transform.position;
        //Move the ball to a better position with relation to direction
        ball.transform.position += direction.normalized * 2;
        //Kick the ball in that direction
        ball.GetComponent<Rigidbody>().AddForce(direction.normalized * 3000);
        //end the coroutine
        yield break;
    }

    void grabBall(){
        //Move the ball to slightly in front of the AI
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.position = transform.position + transform.forward;
    }


    IEnumerator moveAISideToSide(){
        //Move the AI on the x axis back and forth
        while (moveSideToSide){
            for (int i = 0; i < moveDistance * 4; i++){
                transform.position += new Vector3(0.25f, 0, 0);
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < moveDistance * 4; i++){
                transform.position += new Vector3(-0.25f, 0, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield break;
    }

}
