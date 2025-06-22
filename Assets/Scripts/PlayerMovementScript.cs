using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb;

    public static float Speed = 1.5f;

    Animator anim;

    public ParticleSystem RunParticle;

    public Slider StaminaSlider;

    public static float MaxStamina = 50;

    bool isRunning;

    public static float stamina;

    bool canRun;

    bool particlePlaying;

    public static bool ChangeGunsOrder;

    public Slider HPslider;

    public static float MaxHP = 80;

    public static float HP;

    public GameObject StaminaIndicator;

    public Text MoneyText;

    public GameObject DarkSpot;
    public static float DarkSpotX = 0.6f;
    public static float DarkSpotY = 0.6f;

    public AudioSource HitSound;

    public GameObject Cobweb;

    public GameObject Gun;

    public GameObject TrapInHand;

    public static bool Cobwebed;

    bool SpikeDamageCooldown;

    private Coroutine spikeDamageCoroutine;

    bool DamageCoroutineRunning;

    public GameObject ToDoList;

    bool ListOpen = false;

    public static float HPregen = 0.02f;

    public static float StaminaRegen = 0.1f;

    public static float NormSpeed = 1.5f;
    public static float SprintSpeed = 3f;

    public static bool UpdateTodo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        stamina = 50;
        HP = 80;

        canRun = true;

        DarkSpot.transform.localScale = new Vector3(DarkSpotX, DarkSpotY, 0);

        if (ShopScript.StaminaPotionBought)
        {
            HPslider.maxValue = 120;
            MaxHP = HPslider.maxValue;
        }

        if(ShopScript.StaminaPotionBought)
        {
            StaminaSlider.maxValue = 80;
            MaxStamina = StaminaSlider.maxValue;
        }

        MaxHP = HPslider.maxValue;
        HP = MaxHP;
        MaxStamina = StaminaSlider.maxValue;
        stamina = MaxStamina;

        Cobwebed = false;

        HPregen = 0.02f;
        StaminaRegen = 0.1f;

        NormSpeed = 1.5f;
        SprintSpeed = 3f;
    }

    void Update()
    { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!Cobwebed)
        {
            rb.velocity = new Vector2(x * Speed, y * Speed);
        }

        StaminaSlider.value = stamina;

        anim.SetBool("RunDown", Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
        anim.SetBool("RunUp", Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        anim.SetBool("RunLeft", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
        anim.SetBool("RunRight", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))) 
        {
            anim.SetBool("RunUp", true);
            anim.SetBool("RunRight", false);
        }
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)))
        {
            anim.SetBool("RunUp", true);
            anim.SetBool("RunLeft", false);
        }
        if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            anim.SetBool("RunDown", true);
            anim.SetBool("RunRight", false);
        }
        if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)))
        {
            anim.SetBool("RunDown", true);
            anim.SetBool("RunLeft", false);
        }

        isRunning = Input.GetKey(KeyCode.LeftShift) && (x != 0 || y != 0) && canRun;

        if(anim.GetBool("RunUp") || anim.GetBool("RunLeft"))
            ChangeGunsOrder = true;
        if (anim.GetBool("RunDown") || anim.GetBool("RunRight"))
            ChangeGunsOrder = false;

        HPslider.value = HP;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!ListOpen && Functions.Paused == false)
            {
                UpdateTodo = true;
                ToDoList.SetActive(true);
                Time.timeScale = 0f;
                ListOpen = true;
            }
            else
            {
                UpdateTodo = false;
                ToDoList.SetActive(false);
                Time.timeScale = 1f;
                ListOpen = false;
            }
        }

        DarkSpot.transform.localScale = new Vector3(DarkSpotX, DarkSpotY, 0);

        MaxStamina = StaminaSlider.maxValue;
        MaxHP = HPslider.maxValue;
    }

    private void FixedUpdate()
    {
        stamina = StaminaSlider.value;

        if (isRunning && !Cobwebed)
        {
            Speed = SprintSpeed;
            stamina -= 0.2f;

            if (!particlePlaying)
            {
                RunParticle.Play();
                particlePlaying = true;
            }

            if (stamina < 0)
            {
                stamina = 0;
                StartCoroutine(SprintCooldown());
            }
        }
        else
        {
            Speed = NormSpeed;

            if(particlePlaying) 
            {
                RunParticle.Stop();
                particlePlaying = false;
            }

            if (stamina < StaminaSlider.maxValue)
            {
                stamina += StaminaRegen;
                if (stamina > StaminaSlider.maxValue) stamina = StaminaSlider.maxValue;
            }
        }

        if(GhostMovement.DecreaseHP == true && !DamageCoroutineRunning)
        {
            StartCoroutine(GhostDamageTimer());
        }
        else if (HP < MaxHP)
        {
            HP += HPregen;
        }
    }

    private IEnumerator SprintCooldown()
    {
        StaminaIndicator.SetActive(true);
        Speed = NormSpeed;
        isRunning = false;
        canRun = false;

        yield return new WaitUntil(() => StaminaSlider.value >= 15);

        canRun = true;
        StaminaIndicator.SetActive(false);
    }

    private IEnumerator CobwebStun()
    {
        Cobwebed = true;
        Speed = 0f;
        rb.velocity = Vector3.zero;
        Cobweb.SetActive(true);

        if(GooCleanerScript.TrapUsed == false) TrapInHand.SetActive(false);
        Gun.SetActive(false);

        yield return new WaitForSeconds(2f);

        Cobweb.SetActive(false);

        if(GooCleanerScript.Trap3Chosen) TrapInHand.SetActive(true);
        if(GooCleanerScript.GunInHand) Gun.SetActive(true);

        Speed = NormSpeed;
        Cobwebed = false;
    }

    private IEnumerator SpikeDamageTimer()
    {
        while (true)
        {
            HitSound.Play();
            HP -= 5f;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator GhostDamageTimer()
    {
        DamageCoroutineRunning = true;

        while (GhostMovement.DecreaseHP)
        {
            HitSound.Play();
            HP -= 5f;
            yield return new WaitForSeconds(1f);
        }

        DamageCoroutineRunning = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goo"))
        {
            Speed = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goo"))
        {
            Speed = NormSpeed;
        }

        if (collision.gameObject.CompareTag("Beam") && spikeDamageCoroutine != null)
        {
            StopCoroutine(spikeDamageCoroutine);
            spikeDamageCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spit"))
        {
            HP -= 10f;
            HitSound.Play();
        }

        if (collision.gameObject.CompareTag("Cobweb"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(CobwebStun());
        }

        if (collision.gameObject.CompareTag("BiteHitbox"))
        {
            SpikeEnemyScript.BiteHitboxPlayer.SetActive(false);
            HP -= 7.5f;
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            HP -= 2f;
            HitSound.Play();
        }

        if (collision.gameObject.CompareTag("Flame") && NightLordScript.isDying == false)
        {
            HP -= 3f;
            HitSound.Play();
        }

        if (collision.gameObject.CompareTag("Beam") && spikeDamageCoroutine == null)
        {
            spikeDamageCoroutine = StartCoroutine(SpikeDamageTimer());
        }

        if (collision.gameObject.CompareTag("GhostNightlord"))
        {
            HP -= 5f;
            HitSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") && spikeDamageCoroutine == null)
        {
            spikeDamageCoroutine = StartCoroutine(SpikeDamageTimer());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") && spikeDamageCoroutine != null)
        {
            StopCoroutine(spikeDamageCoroutine);
            spikeDamageCoroutine = null;
        }
    }
    void OnApplicationQuit()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter", 0);
        if (nightCounter > 0)
        {
            nightCounter -= 1;
        }
        PlayerPrefs.SetInt("NightCounter", nightCounter);
        PlayerPrefs.Save();
    }

    public static void UpdateHP()
    {
        GameObject.FindObjectOfType<PlayerMovementScript>().HPslider.maxValue = MaxHP;
    }

    public static void UpdateStamina()
    {
        GameObject.FindObjectOfType<PlayerMovementScript>().StaminaSlider.maxValue = MaxStamina;
    }

    public static void ResetAltarEffects()
    {
        MaxHP = 80;
        MaxStamina = 50;
        HPregen = 0.02f;
        StaminaRegen = 0.1f;
        NormSpeed = 1.5f;
        SprintSpeed = 3f;
        DarkSpotX = 0.6f;
        DarkSpotY = 0.6f;

        HP = MaxHP;
        stamina = MaxStamina;

        UpdateHP();
        UpdateStamina();
    }
}
