using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    private GameObject holdingItem;
    
    private void Start()
    {
        holdingItem = gameObject;
        ItemButton.onFoundHoldingItem += InactiveHoldingItem;
    }

    private void InactiveHoldingItem()
    {
        holdingItem.SetActive(false);
    }

    public void OnMouseUpAsButton()
    {
        
    }

    public void HoldItem(GameObject itemPrefab)
    {
        holdingItem.SetActive(true);
        
    }
}
