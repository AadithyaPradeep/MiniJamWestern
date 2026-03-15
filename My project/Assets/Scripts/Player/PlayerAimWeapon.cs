
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {

    public float bulletSpeed;
    public Bullet bulletScript;
    public GameObject gunSmoke;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireDelay;
    public event EventHandler<OnShootEventArgs> OnShoot;
    private Vector3 lookAtPosition;
    public bool canShoot;
    public float screenShake;
    
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
        
    }
    
    public CinemachineImpulseSource source;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;


    [SerializeField] private GameplayAudio gameplayAudio;

    private void Awake() {

        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update() {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming() {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            aimLocalScale.y = -1f;
        } else {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;

        SetLookAtPosition(mousePosition);
    }

    private void HandleShooting() {
        if (Mouse.current.leftButton.wasPressedThisFrame && canShoot) {
            Vector3 mousePosition = GetMouseWorldPosition();

            gameplayAudio.PlayShootWeapon01();
            

            aimAnimator.SetTrigger("Shoot");
            if (bullet!= null)
            {
                GameObject newbullet = Instantiate(bullet,bulletSpawn.position,Quaternion.identity);
                newbullet.GetComponent<Bullet>().target = mousePosition;
                newbullet.GetComponent<Bullet>().force = bulletSpeed;
                GameObject newgunSmoke = Instantiate(gunSmoke,bulletSpawn.position,Quaternion.identity);
                newgunSmoke.GetComponent<ParticleSystem>().Play();
                
                StartCoroutine("timer");
            }
            source.GenerateImpulse(lookAtPosition.normalized * screenShake);
            
            OnShoot?.Invoke(this, new OnShootEventArgs { 
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position,
            });
        }
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Mouse.current.position.value, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Mouse.current.position.value, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Mouse.current.position.value, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        screenPosition.z = Mathf.Abs(worldCamera.transform.position.z);
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void HandleLookAtMouse()
    {
        lookAtPosition = GetMouseWorldPosition();
    }
    public void SetLookAtPosition(Vector3 lookAtPosition)
    {
        this.lookAtPosition = lookAtPosition;
    }
    private IEnumerator timer()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }
}
