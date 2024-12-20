using UnityEngine;

public class CustomAntiTankRifleBullet : MonoBehaviour
{
    private WeaponConfig antiTankRifleConfig;

    private Rigidbody rb;
    
    private float duration;
    [HideInInspector] public bool startCountDown;

    private void Awake()
    {
        antiTankRifleConfig = GameAssets.i.antiTankRifleConfig;
        rb = GetComponent<Rigidbody>();
    }

    public void StartCountDown()
    {
        duration = antiTankRifleConfig.bulletDuration;
        startCountDown = true;
    }
    
    private void Update()
    {
        if (startCountDown) duration -= Time.deltaTime;

        if (duration > 0) return;
        startCountDown = false;
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
        duration = antiTankRifleConfig.bulletDuration;
    }

    private void OnTriggerEnter(Collider objectHitByBullet)
    {
        IDamagable damagable = objectHitByBullet.transform.parent.GetComponent<IDamagable>();
        damagable.Damage(antiTankRifleConfig.damage);
    }
}
