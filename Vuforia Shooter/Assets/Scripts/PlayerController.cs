using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private CharacterController charCon;
   private FixedJoystick joystick;

   private Transform monster;
   
   private bool activeControl;
   private float moveSpeed;
   private bool isAimTracking;
   private float rotateSpeed;

   private void Awake()
   {
      charCon = GetComponent<CharacterController>();
      joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
   }

   private void Start()
   {
      moveSpeed = 5;
      rotateSpeed = 6;
   }
   
   public void ActiveController(bool input)
   {
      StartCoroutine(ActiveControllerCoroutine(input));
   }
   IEnumerator ActiveControllerCoroutine(bool inputValue)
   {
      yield return new WaitForSeconds(0.5f);
      activeControl = inputValue;
   }

   public void ActiveAimTracking(bool inputValue)
   {
      isAimTracking = inputValue;

      monster = isAimTracking ? GameObject.Find("MonsterCard/Capsule").transform : null;
   }

   private void FixedUpdate()
   {
      //Move
      if (!charCon.enabled || !activeControl) return;
      charCon.Move(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.fixedDeltaTime);

      //Aim
      if (isAimTracking)
      {
         Vector3 aimDir = monster.position - transform.position;
         Quaternion trackingLookAt = Quaternion.LookRotation(aimDir);
         trackingLookAt = Quaternion.Slerp(transform.rotation, trackingLookAt, rotateSpeed * Time.fixedDeltaTime);
         transform.rotation = trackingLookAt;
         transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
         return;
      }
      
      //Rotate
      if (joystick.Horizontal == 0 && joystick.Vertical == 0) return;
      
      Quaternion lookAt = Quaternion.LookRotation(charCon.velocity);
      lookAt = Quaternion.Slerp(transform.rotation, lookAt, rotateSpeed * Time.fixedDeltaTime);
      transform.rotation = lookAt;
      transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
   }
}