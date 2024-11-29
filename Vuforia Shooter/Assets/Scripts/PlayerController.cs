using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private CharacterController charCon;
   private FixedJoystick joystick;

   private float moveSpeed;

   private void Awake()
   {
      charCon = GetComponent<CharacterController>();
      joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
   }

   private void Start()
   {
      moveSpeed = 5;
   }

   private void FixedUpdate()
   {
      charCon.Move(new Vector3(joystick.Horizontal * moveSpeed * Time.fixedDeltaTime, 0, joystick.Vertical * moveSpeed * Time.fixedDeltaTime));
   }
}