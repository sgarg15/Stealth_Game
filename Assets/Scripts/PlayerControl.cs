using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

  public event System.Action OnReachedEndOfLevel;
  public float moveSpeed = 6;
  public float smoothMoveTime = 0.1f;
  public float turnSpeed = 8;

  float angle;

  float smoothInputMagnitude;
  float smoothMoveVelocity;

  Rigidbody myRigidbody;
  Vector3 velocity;
  Vector3 inputDirection;
  bool disabled;
  // Start is called before the first frame update
  void Start() {
    myRigidbody = GetComponent<Rigidbody> ();
    Guard.OnGuardHasSpottedPlayer += Disabled;
  }

  // Update is called once per frame
  void Update() {
    Vector3 inputDirection = Vector3.zero;
    if(!disabled){
     inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }
    float inputMagnitude = inputDirection.magnitude;
    smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

    float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
    angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

    velocity = transform.forward * moveSpeed * smoothInputMagnitude;
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

  void FixedUpdate(){
    myRigidbody.MoveRotation (Quaternion.Euler(Vector3.up * angle));
    myRigidbody.MovePosition (myRigidbody.position + velocity * Time.deltaTime);
  }

  void OnDestroy(){
    Guard.OnGuardHasSpottedPlayer -= Disabled;
  }
}
