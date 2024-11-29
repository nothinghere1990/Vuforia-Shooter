using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (ButtonManager.Instance.clickedScriptableObject == null) return;
        
        if (transform.childCount > 1) Destroy(transform.GetChild(1).gameObject);
        Weapon weapon = (Weapon)ButtonManager.Instance.clickedScriptableObject;
        Instantiate(weapon.weaponModel, transform);
    }

    public void OnWeaponLost()
    {
        if (transform.childCount > 1) Destroy(transform.GetChild(1).gameObject);
    }
}