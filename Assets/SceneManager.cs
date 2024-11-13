using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneManager : MonoBehaviour
{

    public GameObject player;
    public Canvas gameOverUI;
    public Canvas tutorialUI;
    int level = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(gameOverUI.gameObject.transform.parent.gameObject);
        if (level == 1){
            tutorialUI.enabled = true;
            StartCoroutine(hideTutorial());
        }
    }

    IEnumerator hideTutorial(){
        yield return new WaitForSeconds(2.5f);
        tutorialUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool ballHitThisScene = false;
    public void BallHit(){
        ballHitThisScene = true;
    }
    public void ballKicked(){
        ballHitThisScene = false;
        StartCoroutine(checkIfBallMisses());
    }
    IEnumerator checkIfBallMisses(){
        yield return new WaitForSeconds(4.0f);
        if (ballHitThisScene == false){
            handleGameOver();
        }
    }

    public void RestartGame(){
        //Reload the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void gotoLevel(string levelName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void handleGameOver(){
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver(){
        //Turn on the UI
        gameOverUI.enabled = true;
        setUITextandColor("Miss...", Color.red);
        yield return new WaitForSeconds(2.0f);                
        //Restart the game
        RestartGame();
    }

    public void handleGameWin(){
        StartCoroutine(GameWin());
    }

    void setUITextandColor(string text, Color color){
        gameOverUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        gameOverUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
    }

    IEnumerator GameWin(){
        //Turn on the UI
        gameOverUI.enabled = true;
        setUITextandColor("Goal!!", Color.green);
        yield return new WaitForSeconds(2.0f);                
        //Load the next level
        level++;
        gotoLevel(level.ToString());
        gameOverUI.enabled = false;
        yield break;
    }


}
