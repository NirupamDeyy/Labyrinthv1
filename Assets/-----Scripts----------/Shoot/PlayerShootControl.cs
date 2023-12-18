using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShootControl : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
  
    [SerializeField] private GunSwitchControl gunSwitchControl;
    [SerializeField] private Transform shellPrefab;
    [SerializeField] private Transform[] shellEjectorPoints;
    //public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public int fireRate = 25;// fire 25 bullets per second
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public Transform[] raycastOrigins;
    public Transform raycastDestination;
    public TrailRenderer tracerEffect;
    public string weaponName;
    public GameObject magazine;
    public bool canShootbyGun ;

    public float forceAmount = 10f;
    public GameObject bulletCollider;

    //public WeaponRecoil recoil;
    //public RifleAudio rifleAudio;
    public int ammoCount;
    public int clipSize;

    Ray ray;
    public RaycastHit hitInfo;
    float accumulatedTime;// tells us when to fire the next bullet
    List<Bullet> bullets = new List<Bullet>();

    float maxLifeTime = 3.0f;
    public float reloadTime;
   
    private void Awake()
    {
        //recoil = GetComponent<WeaponRecoil>();
        canShootbyGun = true;
    }

    void Update()
    {
        UpdateBullets(Time.deltaTime);
        
        if(gunSwitchControl.canShootGun)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Debug.Log("stopFiring");
                StopFiring();

            }
            else if (Input.GetButtonDown("Fire1"))
            {
                StartFiring();
                Debug.Log("fire");
                UpdateBullets(Time.deltaTime);
                
            }
        }
        if (isFiring)
        {
            UpdateFiring(Time.deltaTime);
            
        }
    }
    Vector3 GetPosition(Bullet bullet)
    {
        // p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + 0.5f * gravity * bullet.time * bullet.time;
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);

        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        //FireBullet();
        accumulatedTime = 0.0f;
       // recoil.Reset();

    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate; // showTime b/w each bullet
        while (accumulatedTime >= 0.0f) //while showTime left
        {
            FireBullet();
            accumulatedTime -= fireInterval; // reduce the accumulutad showTime by the interval between the bullets
            EjectShells();
        }
    }

    public void UpdateBullets(float deltatime)
    {
        SimulateBullets(deltatime);
        DestroyBullets();
    }

    public void EjectShells()
    {
        for (int i = 0; i < shellEjectorPoints.Length; i++)
        {

            Transform shell = Instantiate(shellPrefab, shellEjectorPoints[i].position, Quaternion.identity);
            Rigidbody shellRigidBody = shell.GetComponent<Rigidbody>();
            shellRigidBody.AddForce(shellEjectorPoints[i].forward * .5f , ForceMode.Impulse);
            StartCoroutine(DestroyShellAfterDelay(shell));


        }
    }

    IEnumerator DestroyShellAfterDelay(Transform shell)
    {
        yield return new WaitForSeconds(4f);
        Destroy(shell.gameObject);
    }

    void SimulateBullets(float deltaTime)
    {
       bullets.ForEach(bullet =>
            {
                
                Vector3 p0 = GetPosition(bullet); //current position of bullet
                bullet.time += deltaTime; //update how long the bullet has been alive for
                Vector3 p1 = GetPosition(bullet); //current position of bullet
                RaycastSegment(p0, p1, bullet);
            });
       
        
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }
    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            Debug.Log(hitInfo.collider.transform.name);
            Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddForceAtPosition(ray.direction * forceAmount, hitInfo.point, ForceMode.Impulse);
            }
            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
            //Instantiate(hitEffect, hitInfo.transform);
            /* hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);*/
            
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
        
    }

    private void FireBullet()
    {
        /*if (ammoCount <= 0)
        {
            return;
        }
        ammoCount--;

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }*/
        for (int i = 0; i < raycastOrigins.Length; i++)
        {
            
            Vector3 velocity = (raycastDestination.position - raycastOrigins[i].position).normalized * bulletSpeed;
            var bullet = CreateBullet(raycastOrigins[i].position, velocity);
            bullets.Add(bullet);
        }
        //
        /*recoil.GenerateRecoil(weaponName);
        if (rifleAudio)
        {
            rifleAudio.PlayGunShotAudio();
        }*/

    }

    public void StopFiring()
    {
        isFiring = false;
        DestroyBullets();
    }
}
