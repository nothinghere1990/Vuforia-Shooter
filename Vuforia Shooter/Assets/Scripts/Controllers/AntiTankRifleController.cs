using UnityEngine;

public class AntiTankRifleController : MonoBehaviour
{
    private PlayerController playerController;
    
    private Transform eyes;
    
    private Weapon antiTankRifleConfig;
    private Transform firePoint;

    private Transform bulletPool;
    
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
        GameObject bullet = InactiveBulletInPool();
        if (bullet == null) return;
        
        //Inactive bullet.
        if (antiTankRifleConfig.bulletDuration <= 0) bullet.SetActive(false);
        
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
        antiTankRifleConfig.bulletDuration -= Time.deltaTime;
    }

    private void OnDisable()
    {
        playerController.onFire -= FireGun;
    }
}
