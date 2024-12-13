using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class AntiTankRifleController : MonoBehaviour
{
    private PlayerController playerController;
    
    private Transform eyes;
    
    private Weapon antiTankRifleConfig;
    private Transform firePoint;

    private Transform bulletPool;

    private bool startShooting;
    private float bulletDuration;
    
    private void Awake()
    {
        playerController = transform.parent.parent.GetComponent<PlayerController>();
        
        eyes = transform.parent.Find("Eyes");
        
        antiTankRifleConfig = GameAssets.i.antiTankRifleConfig;
        firePoint = transform.Find("FirePoint");
        
        bulletPool = transform.Find("BulletPool");
    }
    
    private void Start()
    {
        playerController.onFire += FireGun;

        startShooting = false;
        
        foreach (Transform bullet in bulletPool)
        {
            bullet.gameObject.SetActive(false);
        }

        bulletDuration = antiTankRifleConfig.bulletDuration;
    }

    private GameObject InactiveBulletInPool()
    {
        foreach (Transform bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy) return bullet.gameObject;
        }
        
        return null;
    }

    private void FireGun()
    {
        startShooting = true;
        //bullet.transform.DOMove(playerController.monster.position, .5f);
    }

    private void FixedUpdate()
    {
        if (!startShooting) return;
        
        GameObject bullet = InactiveBulletInPool();
        
        if (bullet == null) return;
        
        
        
        //Inactive bullet.
        if (bulletDuration <= 0)
        {
            print("disappear");
            startShooting = false;
            bullet.SetActive(false);
            bullet.transform.position = firePoint.position;
            bulletDuration = antiTankRifleConfig.bulletDuration;
        }
        
        //Calculate hit point.
        Ray ray = new Ray(eyes.position, eyes.forward);
        RaycastHit hit;
        Vector3 hitPoint;
        
        if (Physics.Raycast(ray, out hit)) hitPoint = hit.point;
        else hitPoint = ray.GetPoint(100);
        
        //Calculate direction with spread.
        Vector3 directionWithoutSpread = hitPoint - eyes.position;
        
        float spreadX = Random.Range(-antiTankRifleConfig.spread, antiTankRifleConfig.spread);
        float spreadY = Random.Range(-antiTankRifleConfig.spread, antiTankRifleConfig.spread);
        
        Vector3 directionWithSpread = new Vector3
            (directionWithoutSpread.x + spreadX, directionWithoutSpread.y + spreadY, directionWithoutSpread.z);

        //Active and fire bullet in pool.
        bullet.transform.position = firePoint.position;
        bullet.transform.forward = directionWithSpread.normalized;
        bullet.SetActive(true);
        
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * antiTankRifleConfig.gunFireForce, ForceMode.Impulse);
        print("force");
        bulletDuration -= Time.fixedDeltaTime;
    }

    private void OnDisable()
    {
        playerController.onFire -= FireGun;
    }
}
