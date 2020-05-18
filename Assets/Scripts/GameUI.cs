using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

  public GameObject gameLoseUI;
  public GameObject gameWinUI;
  public GameObject totalGameWinUI;
  bool gameIsOver;
  bool notReachedEnd = true;
  int currentScene;
  // Start is called before the first frame update
  void Start() {
    Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
    FindObjectOfType<PlayerControl>().OnReachedEndOfLevel += ShowGameWinUI;
  }

  // Update is called once per frame
  void Update() {
    currentScene = SceneManager.GetActiveScene().buildIndex;
    if (gameIsOver) {
      if(Input.GetKeyDown(KeyCode.Space)) {
        if(currentScene != 1 & notReachedEnd){
          SceneManager.LoadScene(0);
        } else {
          SceneManager.LoadScene(1);
        }
      }
    }
  }

  void loadNextScene(){
    SceneManager.LoadScene(1);
  }

  void ShowGameWinUI(){
    if (currentScene == 1){
      OnGameOver(totalGameWinUI);
    } else {
      OnGameOver(gameWinUI);
    }
    notReachedEnd = false;
  }

  void ShowGameLoseUI(){
    if(ScoreKeeper.coinScore < 15 ){
      ScoreKeeper.coinScore = 0;
    } else {
      ScoreKeeper.coinScore -= 15;
    }
    OnGameOver(gameLoseUI);
  }

  void OnGameOver (GameObject gameOverUI) {
    gameOverUI.SetActive(true);
    gameIsOver = true;
    Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
    FindObjectOfType<PlayerControl>().OnReachedEndOfLevel -= ShowGameWinUI;
  }
}
