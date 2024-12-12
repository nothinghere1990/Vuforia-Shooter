using System;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ScriptableObject Item;

    private void OnMouseUpAsButton()
    {
        //Check if this card is weapon and send weapon game script obj.
        if (Item.GetType() == typeof(Weapon))
        {
            Weapon weapon = (Weapon)Item;
            ButtonManager.Instance.clickedScriptableObject = weapon;
        }
    }
}
