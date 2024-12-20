using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamagable
{
    private Slider healthSliderUI;
    public Team team;
    public float healthAmount;

    private void Awake()
    {
        healthSliderUI = GameObject.Find("MonsterHealthBar").GetComponent<Slider>();
    }

    private void Start()
    {
        healthSliderUI.gameObject.SetActive(false);
    }

    public void InitializeHealth()
    {
        healthSliderUI.gameObject.SetActive(true);
        healthSliderUI.maxValue = healthAmount;
    }

    public void Damage(float damageAmount)
    {
        healthAmount -= damageAmount;
    }

    private void Update()
    {
        healthSliderUI.value = Mathf.Lerp(healthSliderUI.value, healthAmount, .5f);
    }

    public void CloseHealth()
    {
        healthSliderUI.gameObject.SetActive(false);
    }

    public enum Team
    {
        Player,
        Monster,
        Item
    }
}
