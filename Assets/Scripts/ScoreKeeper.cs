using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

  public Text scoreUI;
  public float scoreToIncreaseBy;
  float coinScore;
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    scoreUI.text = ((int)coinScore).ToString("D3");
  }

  void OnTriggerEnter(Collider triggerCollider){
    if(triggerCollider.tag == "Coin") {
      coinScore += scoreToIncreaseBy;
      Destroy(triggerCollider.gameObject);
    }
  }

}
