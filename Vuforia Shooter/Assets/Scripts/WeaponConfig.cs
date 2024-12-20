using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Weapon Config")]
public class WeaponConfig : ScriptableObject
{
   public string weaponNname;
   public GameObject weaponModel;

   public FireMode fireMode;
   public float fireRate;
   public float spread;
   public float gunFireForce;
   public float damage;
   public float bulletDuration;
   
   public enum FireMode
   {
      Semi,
      Brust,
      Auto
   }
}
