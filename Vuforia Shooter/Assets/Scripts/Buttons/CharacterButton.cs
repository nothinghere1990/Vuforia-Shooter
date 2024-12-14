using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    private Transform characterModel;
    private Transform holdingItem;

    private PlayerController playerController;

    private void Awake()
    {
        characterModel = transform.GetChild(0);
        holdingItem = characterModel.Find("HoldingItem");
        playerController = GetComponent<PlayerController>();
    }

    private void OnMouseUpAsButton()
    {
        if (ButtonManager.Instance.clickedConfig == null || transform.childCount < 1) return;

        if (ButtonManager.Instance.clickedConfig.GetType() == typeof(WeaponConfig))
        { 
            //Get weapon from ButtonManager.
            WeaponConfig weaponConfig = (WeaponConfig)ButtonManager.Instance.clickedConfig;
            ButtonManager.Instance.clickedConfig = null;
            
            //Spawn weapon model on character.
            Transform playerWeapon = Instantiate(weaponConfig.weaponModel, characterModel).transform;
            playerWeapon.position = holdingItem.position;
            playerWeapon.localScale = new Vector3(.2f, .2f, .2f);
            if (characterModel.childCount > 1) holdingItem.gameObject.SetActive(false);
            
            playerController.ActiveAimTracking(true);  
        }
    }

    public void OnTargetLost()
    {
        if (characterModel.childCount < 3) return; //No Weapon Holding
        
        holdingItem.gameObject.SetActive(false);
        Destroy(characterModel.GetChild(2).gameObject);
        
        playerController.ActiveAimTracking(false);
    }
}