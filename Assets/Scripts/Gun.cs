using UnityEngine;
using System.Collections;

public class Gun : Shooter
{ 
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private GameObject projectilePrefab;

    [Header("Firing")] 
    [SerializeField] private int _ammo = 1;
    [SerializeField] private float fireRecharge = 0.3f;
    [SerializeField] private float firePushback = 0.5f;
    
    [Header("Recoil")]
    [SerializeField] private AnimationCurve recoilCurve;
    [SerializeField] private float recoilAngle = 10f;
    private bool canFire = true;
    
    [Header("SFX")]
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip clickSFX;
    
    

    
    public int Ammo => _ammo;
    
    private float _currentRechargeTime = 0f;
    
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        
        canFire = true;
        _audioSource = GetComponent<AudioSource>();
    }

     protected override void ShootStart()
     {
         base.ShootStart();

         if (!canFire) return;

         if (_ammo <= 0)
         {
             _audioSource.PlayOneShot(clickSFX);
             return;
         }
         
         _audioSource.PlayOneShot(shootSFX);
         
         _ammo--;
         canFire = false;
         _currentRechargeTime = fireRecharge;

         if (projectilePrefab && firePoint)
             Instantiate(projectilePrefab, firePoint.position, transform.rotation);

         StartCoroutine(RecoilRoutine());
     }
     
     private IEnumerator RecoilRoutine()
     {
         float elapsed = 0f;
         Quaternion startRot = pivot.localRotation;

         while (elapsed < fireRecharge)
         {
             float t = elapsed / fireRecharge;
             float curveValue = recoilCurve.Evaluate(t);

             float recoil = curveValue * recoilAngle;
             pivot.localRotation = startRot * Quaternion.Euler(0f, 0f, -recoil);

             elapsed += Time.deltaTime;
             yield return null;
         }

         pivot.localRotation = startRot;
         if (_ammo > 0)
         {
             canFire = true;
         }
     }
     
}
