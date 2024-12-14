using UnityEngine;
using Random = UnityEngine.Random;

public class AntiTankRifleController : MonoBehaviour
{
    private HoldingButton attackBtn;
    
    private Transform eyes;
    
    private WeaponConfig antiTankRifleConfig;
    private Transform firePoint;
    private Transform bulletPool;
    private float timeBetweenShots;
    
    private bool readyToShoot;
    private bool allowInvoke;
    
    private void Awake()
    {
        attackBtn = GameObject.Find("AttackButton").GetComponent<HoldingButton>();
        
        eyes = transform.parent.Find("Eyes");
        
        antiTankRifleConfig = GameAssets.i.antiTankRifleConfig;
        firePoint = transform.Find("FirePoint");
        
        bulletPool = transform.Find("BulletPool");
    }
    
    private void Start()
    {
        readyToShoot = true;
        allowInvoke = true;
        
        foreach (Transform bullet in bulletPool)
        {
            bullet.gameObject.SetActive(false);
        }
        
        timeBetweenShots = 1f / antiTankRifleConfig.fireRate * 60f;
    }

    private GameObject InactiveBulletInPool()
    {
        foreach (Transform bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy) return bullet.gameObject;
        }
        
        return null;
    }

    private void FixedUpdate()
    {
        if (readyToShoot && attackBtn.isHolding) Shoot();
    }

    private void Shoot()
    {
        readyToShoot = false;
        
        GameObject bullet = InactiveBulletInPool();
        if (bullet == null) goto ResetShot;
        
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
        
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(spreadX, spreadY, 0);

        //Active and fire bullet in pool.
        bullet.transform.position = firePoint.position;
        bullet.transform.up = directionWithSpread.normalized;
        bullet.SetActive(true);
        
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * antiTankRifleConfig.gunFireForce, ForceMode.Impulse);
        
        bullet.GetComponent<CustomBullet>().StartCountDown();
        
        ResetShot:
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
    }
    
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
