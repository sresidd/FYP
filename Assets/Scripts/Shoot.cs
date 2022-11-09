using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using TMPro;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    AudioSource audioSource;
    public VisualEffect muzzleFlash;
    public GameObject ImapcatEffect;
    
    [SerializeField] 
    float rateOfFire = 5f;

    [HideInInspector]
    public bool rapidFire = false;
    public void ToggleFireMode(bool fireMode){
        rapidFire = fireMode;
    }

    public void ToggleADS(bool adsMode){
        lerpTimer = 0f;
        isADS = adsMode;
    }

    [HideInInspector] 
    public bool isADS = false;
    bool startReloading = false;

    [SerializeField]
    float impulseForce = 10f;


    [SerializeField]
    TMP_Text ammoCounter;


    [SerializeField] 
    int maxAmmo;


    [SerializeField] 
    float reloatTime = 2f;

    int currentAmmo;
    WaitForSeconds rapidFireWait;
    WaitForSeconds reloadWait;


    [SerializeField]Transform ADS_Position,Default_Position,weaponPositon;

    [SerializeField]
    float animationSpeed = 10f;
    float lerpTimer;
    [HideInInspector]
    public bool isReloading = false;
    void Awake(){
        rapidFireWait = new WaitForSeconds(1/rateOfFire);
        currentAmmo = maxAmmo;
        reloadWait = new WaitForSeconds(reloatTime);
        ammoCounter.text = currentAmmo.ToString()+"/"+maxAmmo.ToString();
        weaponPositon.position = Default_Position.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }    

    public void Fire(){
        currentAmmo--;
        ammoCounter.text = currentAmmo.ToString()+"/"+maxAmmo.ToString();
        audioSource.PlayOneShot(audioSource.clip);
        muzzleFlash.Play();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100)){
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy!=null){
                enemy.TakeDamageEnemy();
            }
            if(hit.rigidbody!=null){
                hit.rigidbody.AddForce(-hit.normal*impulseForce,ForceMode.Impulse);
            }
            GameObject impact = Instantiate(ImapcatEffect,hit.point,Quaternion.identity);
            Destroy(impact,2f);
        }
    }

    public IEnumerator RapidFire(){

        if(rapidFire){
            while(CanFire()){
                Fire();
                yield return rapidFireWait;
            }
            StartCoroutine(Reload());
        }
        else if(CanFire()){
            Fire();
            yield return null;
        }
        else
            StartCoroutine(Reload());
    }

    public IEnumerator Reload(){
        if(currentAmmo==maxAmmo)
            yield return null;
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
    void FixedUpdate(){
        if(isADS)
        {
            LearpingValues(weaponPositon.position, ADS_Position.position, 60, 40);
        }
        else if(!isADS)
        {
            LearpingValues(weaponPositon.position, Default_Position.position, 40, 60);
        }
    }

    private void LearpingValues(Vector3 initPos, Vector3 setPos, float initFOV, float setFOV)
    {
        lerpTimer += Time.deltaTime;
        float percentCompleted = lerpTimer / animationSpeed;
        weaponPositon.position = Vector3.Lerp(initPos,setPos, percentCompleted);
        cam.fieldOfView = Mathf.Lerp(initFOV, setFOV, percentCompleted);

    }

    bool CanFire(){
        bool enableFire = currentAmmo>0 && !isReloading;
        return enableFire;
    }

}
