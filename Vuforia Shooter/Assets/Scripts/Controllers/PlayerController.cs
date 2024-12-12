using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private CharacterController charCon;
   private FixedJoystick joystick;
   
   private bool activeControl;
   private float moveSpeed;
   private bool isAimTracking;
   private float rotateSpeed;
   
   public ScriptableObject holdingItem;
   
   private bool isMonsterFound;
   private Transform monster;

   public Action onFire;
   
   private void Awake()
   {
      charCon = GetComponentInChildren<CharacterController>();
      joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
      monster = GameObject.Find("MonsterCard/Capsule").transform;
   }

   private void Start()
   {
      moveSpeed = 5;
      rotateSpeed = 6;
      ActiveAimTracking(false);
   }
   
   public void ActiveController(bool input)
   {
      StartCoroutine(ActiveControllerCoroutine(input));
   }
   
   IEnumerator ActiveControllerCoroutine(bool inputValue)
   {
      yield return new WaitForSeconds(.5f);
      activeControl = inputValue;
   }

   public void ActiveAimTracking(bool inputValue)
   {
      isAimTracking = inputValue;
   }

   public void OnFoundMonster(bool inputValue)
   {
      isMonsterFound = inputValue;
   }

   private void Update()
   {
      //Move
      if (!charCon.enabled || !activeControl) return;
      charCon.Move(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.fixedDeltaTime);

      //Aim
      if (isAimTracking && isMonsterFound)
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
      
      //Fire
      if (holdingItem.GetType() == typeof(Weapon) && Input.GetMouseButtonDown(0)) onFire?.Invoke();
   }
}