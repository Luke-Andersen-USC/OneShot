using UnityEngine;

public class Gun : Shooter
{ 
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private GameObject projectilePrefab;

    [Header("Firing")] 
    [SerializeField] private int _ammo = 1;
    [SerializeField] private float fireRecharge = 0.3f;
    [SerializeField] private float firePushback = 0.5f;
    
    public int Ammo => _ammo;
    
    private float _currentRechargeTime = 0f;

     private void Update()
     {
         if (_currentRechargeTime > 0f) _currentRechargeTime -= Time.deltaTime;
     }

     public override void ShootStart()
     {
         base.ShootStart();
         if (_currentRechargeTime <= 0f)
         {
             Debug.Log("Firing start");
             _currentRechargeTime = fireRecharge;
             if (!projectilePrefab || !firePoint) return;

             Instantiate(projectilePrefab, firePoint.position, transform.rotation);
         }
     }
     
}
