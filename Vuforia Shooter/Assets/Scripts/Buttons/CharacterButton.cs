using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    private Transform chracterModel => transform.GetChild(0);
    private Transform holdingItem;

    private PlayerController playerController;

    private void Awake()
    {
        holdingItem = chracterModel.Find("HoldingItem");
        playerController = GetComponent<PlayerController>();
    }

    private void OnMouseUpAsButton()
    {
        if (ButtonManager.Instance.clickedScriptableObject == null || transform.childCount < 1) return;

        if (ButtonManager.Instance.clickedScriptableObject.GetType() == typeof(Weapon))
        { 
            Weapon weapon = (Weapon)ButtonManager.Instance.clickedScriptableObject;
            ButtonManager.Instance.clickedScriptableObject = null;
          
            playerController.holdingItem = weapon;
                  
            Transform playerWeapon = Instantiate(weapon.weaponModel, chracterModel).transform;
            playerWeapon.position = holdingItem.position;
            playerWeapon.localScale = new Vector3(.2f, .2f, .2f);
            if (chracterModel.childCount > 1) holdingItem.gameObject.SetActive(false);
                  
            playerController.ActiveAimTracking(true);  
        }
    }

    public void OnTargetLost()
    {
        if (chracterModel.childCount < 2) return; //No Weapon Holding
        
        holdingItem.gameObject.SetActive(false);
        Destroy(chracterModel.GetChild(2).gameObject);
        
        playerController.ActiveAimTracking(false);
    }
}