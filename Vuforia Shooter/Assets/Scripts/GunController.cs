using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    private Button attackBtn;

    private void Start()
    {
        attackBtn = GameObject.Find("AttackButton").GetComponent<Button>();
        attackBtn.onClick.AddListener(PlayerAttack);
    }

    private void PlayerAttack()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit touchRayHit;
            if (Physics.Raycast(touchRay, out touchRayHit)) Debug.Log("Attack");
        }
    }
}
