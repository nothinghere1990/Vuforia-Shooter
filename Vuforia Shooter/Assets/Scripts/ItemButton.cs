using System;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    private GameObject holdingItem;
    public static Action onFoundHoldingItem;
    
    public ScriptableObject Item;
    //public Weapon weapon;

    private void Start()
    {
        holdingItem = GameObject.Find("HoldingItem");
        onFoundHoldingItem?.Invoke();
    }

    public void OnMouseUpAsButton()
    {
        Type weaponType = Item.GetType();
        
        if (weaponType == typeof(Weapon))
        {
            Weapon weapon = (Weapon)Item;
            holdingItem.GetComponent<HoldingItem>().HoldItem(weapon.weaponModel);
        } 
    }
}
