using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
   public string weaponNname;
   public GameObject weaponModel;

   public FireMode fireMode;
   public float fireRate;
   public float spread;
   public float gunFireForce;
   public float bulletDuration;
   
   public enum FireMode
   {
      Semi,
      Brust,
      Auto
   }
}
