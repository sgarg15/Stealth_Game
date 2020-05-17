using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

  public event System.Action OnReachedEndOfLevel;
  public float speed = 10.0f;

  Rigidbody myRigidbody;
  bool disabled;
  // Start is called before the first frame update
  void Start() {
    myRigidbody = GetComponent<Rigidbody> ();
    Guard.OnGuardHasSpottedPlayer += Disabled;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update() {
    if(!disabled){
      float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
      float straffe = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
      transform.Translate(straffe, 0, translation);
    }
    if(Input.GetKeyDown("escape")){
      Cursor.lockState = CursorLockMode.None;
    }
  }

  void OnTriggerEnter(Collider hitCollider){
    if(hitCollider.tag == "Finish"){
      Disabled();
      if(OnReachedEndOfLevel != null){
        OnReachedEndOfLevel ();
      }
    }
  }

  void Disabled(){
    disabled = true;
  }

  void OnDestroy(){
    Guard.OnGuardHasSpottedPlayer -= Disabled;
  }
}
