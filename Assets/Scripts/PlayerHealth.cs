using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;


    [Header("Heath Bar")]
    public float chipSpeed = 2f;
    public float maxHealth = 100f;


    [SerializeField] Image frontHeathBar;
    [SerializeField] Image backHeathBar;

    [SerializeField]TMP_Text healthText;

    [Header("Damage Effect")]
    public Image damageOverlay;
    public float overLayTDuration;
    public float fadeSpeed;

    private float durationTimer;
    private float maxOverlayAlphaValue = .5f;

    
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
        damageOverlay.color = new Color(damageOverlay.color.r,damageOverlay.color.g,damageOverlay.color.b,0);
        // frontHeathBar.fillAmount = 1f;
        // backHeathBar.fillAmount = 1f;
        
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0 , maxHealth);
        UpdateHealthUI();
        if(damageOverlay.color.a>0){
            if(health<30)
                return;
            durationTimer += Time.deltaTime;
            if(durationTimer>overLayTDuration){
                float tempAlpha = damageOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                damageOverlay.color = new Color(damageOverlay.color.r,damageOverlay.color.g,damageOverlay.color.b,tempAlpha);
            }

        }
    }

    private void UpdateHealthUI(){
        // Debug.Log(health);
        healthText.text = health.ToString();
        float fillF = frontHeathBar.fillAmount;
        float fillB = backHeathBar.fillAmount;
        float hFraction = health/maxHealth;

        if(fillB>hFraction){
            frontHeathBar.fillAmount = hFraction;
            backHeathBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentCompleted = lerpTimer/chipSpeed;
            // percentCompleted = percentCompleted*percentCompleted;
            backHeathBar.fillAmount = Mathf.Lerp(fillB,hFraction,percentCompleted);
        }

        else if(fillF<hFraction){
            backHeathBar.color = Color.green;
            backHeathBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentCompleted = lerpTimer/chipSpeed;
            frontHeathBar.fillAmount = Mathf.Lerp(fillF,backHeathBar.fillAmount,percentCompleted);
        }
    }

    public void TakeDamage(float damageAmt){
        // damageAmt = Random.Range(5,10);
        health -= damageAmt;
        lerpTimer = 0f;
        durationTimer = 0;
        damageOverlay.color = new Color(damageOverlay.color.r,damageOverlay.color.g,damageOverlay.color.b,maxOverlayAlphaValue);
    }

    public void IncreaseHeath(float addHeath){
        health += addHeath;
        lerpTimer = 0f;
    }
}
