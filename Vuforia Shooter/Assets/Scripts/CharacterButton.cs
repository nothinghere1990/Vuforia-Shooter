using System;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    private Transform chracterModel => transform.GetChild(0);
    private Transform holdingItem;

    private void Awake()
    {
        holdingItem = chracterModel.Find("HoldingItem");
    }

    private void OnMouseUpAsButton()
    {
        if (ButtonManager.Instance.clickedScriptableObject == null || transform.childCount < 1) return;
        
        Weapon weapon = (Weapon)ButtonManager.Instance.clickedScriptableObject;
        
        Transform playerWeapon = Instantiate(weapon.weaponModel, chracterModel).transform;
        playerWeapon.position = holdingItem.position;
        playerWeapon.localScale = new Vector3(.2f, .2f, .2f);
        if (chracterModel.childCount > 1) holdingItem.gameObject.SetActive(false);
    }

    public void OnWeaponLost()
    {
        if (chracterModel.childCount > 1)
        {
            holdingItem.gameObject.SetActive(false);
            Destroy(chracterModel.GetChild(2).gameObject);
        }
    }
}