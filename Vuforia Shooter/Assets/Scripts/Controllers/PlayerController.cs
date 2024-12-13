using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private FixedJoystick joystick;
   
   private CharacterController charCon;
   private Transform characterModel;
   
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
      characterModel = transform.GetChild(0);
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

   private void FixedUpdate()
   {
      //Move
      if (!charCon.enabled || !activeControl) return;
      charCon.Move(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.fixedDeltaTime);
   }
   
   private void Update()
   {
      //Aim
      if (isAimTracking && isMonsterFound)
      {
         Vector3 aimDir = monster.position - characterModel.position;
         Quaternion trackingLookAt = Quaternion.LookRotation(aimDir);
         trackingLookAt = Quaternion.Slerp(characterModel.rotation, trackingLookAt, rotateSpeed * Time.fixedDeltaTime);
         characterModel.rotation = trackingLookAt;
         characterModel.eulerAngles = new Vector3(0, characterModel.eulerAngles.y, 0);
         return;
      }
      
      //Rotate
      if (joystick.Horizontal != 0 || joystick.Vertical != 0)
      {
         Quaternion lookAt = Quaternion.LookRotation(charCon.velocity);
         lookAt = Quaternion.Slerp(characterModel.rotation, lookAt, rotateSpeed * Time.fixedDeltaTime);
         characterModel.rotation = lookAt;
         characterModel.eulerAngles = new Vector3(0, characterModel.eulerAngles.y, 0);
         print(lookAt);
      }
      
      //Fire
      if (holdingItem != null && holdingItem.GetType() == typeof(Weapon) && Input.GetMouseButtonDown(0)) 
         onFire?.Invoke();
   }
}