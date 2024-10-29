using System;
using System.Linq;
using System.Reflection;
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
       // var weapon = GetSubclassByName<ScriptableObject>("Weapon");
        //if (Item.GetType() == typeof(Weapon))
       // {
        //    holdingItem.GetComponent<HoldingItem>().HoldItem(weapon.weaponModel);
       // }
        //if (weapon != null) holdingItem.GetComponent<HoldingItem>().HoldItem(weapon.weaponModel);
    }
    
    public static Type GetSubclassByName<T>(string subclassName) where T : ScriptableObject
    {
        return Assembly.GetAssembly(typeof(T))
            .GetTypes()
            .FirstOrDefault(type => type.IsSubclassOf(typeof(T)) && type.Name == subclassName);
    }
}
