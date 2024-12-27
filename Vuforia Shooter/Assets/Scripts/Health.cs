using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamagable
{
    private GameObject model;
    
    public Team team;
    public float fullHealthAmount;
    
    private Transform content;
    private Slider healthSliderUI;
    private float currentHealthAmount;
    private bool isAlive;

    private void Awake()
    {
        model = transform.Find("Capsule").gameObject;
        content = GameObject.Find("Content").transform;
        isAlive = false;
    }

    public void InitializeHealth()
    {
        switch (team)
        {
            case Team.Player:
                break;

            case Team.Monster:
                healthSliderUI = Instantiate(GameAssets.i.healthSliderUI, content).GetComponent<Slider>();
                healthSliderUI.gameObject.name = "MonsterHealthBar";
                break;

            case Team.Item:
                break;
        }

        isAlive = true;
        model.SetActive(true);
        currentHealthAmount = fullHealthAmount;
        healthSliderUI.maxValue = fullHealthAmount;
    }

    public void Damage(float damageAmount)
    {
        currentHealthAmount -= damageAmount;
    }

    private void Update()
    {
        if (healthSliderUI == null) return;
        
        healthSliderUI.value = Mathf.Lerp(healthSliderUI.value, currentHealthAmount, .1f);

        if (currentHealthAmount <= 0 && isAlive)
        {
            StartCoroutine(DeathAni());
        }
    }

    private IEnumerator DeathAni()
    {
        isAlive = false;

        for (int i = 0; i < 3; i++)
        {
            model.SetActive(true);
            yield return new WaitForSeconds(.2f);
            model.SetActive(false);
            yield return new WaitForSeconds(.2f);
        }
    }

    public void DestroyHealth()
    {
        Destroy(healthSliderUI.gameObject);
    }

    public enum Team
    {
        Player,
        Monster,
        Item
    }
}
