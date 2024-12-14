using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ScriptableObject itemConfig;

    private void OnMouseUpAsButton()
    {
        //Check if this card is weapon and send weapon game script obj.
        if (itemConfig.GetType() == typeof(WeaponConfig))
        {
            WeaponConfig weaponConfig = (WeaponConfig)itemConfig;
            ButtonManager.Instance.clickedConfig = weaponConfig;
        }
    }
}
