using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using TMPro;
using Unity.Netcode;

public class Shoot : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    private AudioSource audioSource;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private GameObject ImpactEffect;
    
    [SerializeField] private float rateOfFire = 5f;

    [HideInInspector] public bool fireMode = false;


    [HideInInspector] public bool adsMode = false;
    private bool startReloading = false;

    [SerializeField] private float impulseForce = 10f;
    [SerializeField] private TMP_Text ammoCounter;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime = 2f;
    private int currentAmmo;
    private WaitForSeconds rapidFireWait;
    private WaitForSeconds reloadWait;

    private Coroutine Coroutine_Fire;


    [SerializeField] private Transform ADS_Position,
                                    Default_Position,
                                    weaponPosition;

    [SerializeField] private float animationSpeed = 10f;
    private float lerpTimer;
    [HideInInspector] public bool isReloading = false;
    private void Awake()
    {
        rapidFireWait = new WaitForSeconds(1/rateOfFire);
        currentAmmo = maxAmmo;
        reloadWait = new WaitForSeconds(reloadTime);
        ammoCounter.text = currentAmmo.ToString()+"/"+maxAmmo.ToString();
        weaponPosition.position = Default_Position.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }   

    private void Start()
    {
        InputManager.Instance.OnShoot += InputManager_Firing;
        InputManager.Instance.OnReloadPerformed += InputManager_OnReload;
        InputManager.Instance.OnToggleFireModePerformed += InputManager_OnToggleFireMode;
        InputManager.Instance.OnToggleADSPerformed += InputManager_OnToggleADS;
    }

    private void InputManager_OnReload()
    {
        StartCoroutine(Reload());
    }

    private void InputManager_Firing(bool isFiring)
    {
        if(isFiring)
        {
            Coroutine_Fire = StartCoroutine(RapidFire());
        }

        else if(!isFiring)
        {
            if(Coroutine_Fire == null) return;
            StopCoroutine(Coroutine_Fire);
        }
    }



    public IEnumerator RapidFire(){

        if(fireMode)
        {
            while(CanFire())
            {
                Fire();
                yield return rapidFireWait;
            }
            StartCoroutine(Reload());
        }
        else if(CanFire())
        {
            Fire();
            yield return null;
        }
        else StartCoroutine(Reload());
    }

    public IEnumerator Reload()
    {
        if(currentAmmo==maxAmmo) yield return null;
        isReloading = true;
        startReloading = true;
        print("Reloading.....");

        yield return reloadWait;
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString()+"/"+maxAmmo.ToString();
        print("Reloaded.");

        startReloading = false;
        isReloading = false;
    }

    public void Fire()
    {
        currentAmmo--;
        ammoCounter.text = currentAmmo.ToString()+"/"+maxAmmo.ToString();
        audioSource.PlayOneShot(audioSource.clip);
        muzzleFlash.Play();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamageEnemy();
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impulseForce, ForceMode.Impulse);
            }
            GameObject impact = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }
    public void InputManager_OnToggleFireMode()
    {
        fireMode = !fireMode ; 
    }

    public void InputManager_OnToggleADS()
    {
        lerpTimer = 0f;
        adsMode = !adsMode ;
    }
    void FixedUpdate(){
        if(!IsOwner) return;
        if(adsMode)
        {
            LerpingValues(weaponPosition.position, ADS_Position.position, 60, 40);
        }
        else if(!adsMode)
        {
            LerpingValues(weaponPosition.position, Default_Position.position, 40, 60);
        }
    }

    private void LerpingValues(Vector3 initPos, Vector3 setPos, float initFOV, float setFOV)
    {
        lerpTimer += Time.deltaTime;
        float percentCompleted = lerpTimer / animationSpeed;
        weaponPosition.position = Vector3.Lerp(initPos,setPos, percentCompleted);
        cam.fieldOfView = Mathf.Lerp(initFOV, setFOV, percentCompleted);

    }

    private bool CanFire(){
        bool enableFire = currentAmmo>0 && !isReloading;
        return enableFire;
    }

}
