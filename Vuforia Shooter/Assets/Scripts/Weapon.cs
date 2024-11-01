using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
   public string name;
   public GameObject weaponModel;

   public FireMode fireMode;
   public float fireRate;
   
   public enum FireMode
   {
      Semi,
      Brust,
      Auto
   }
}
