using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GooCleanerScript : MonoBehaviour
{
    [SerializeField] private Transform gun;

    Vector2 Direction;

    public static bool TrapChosen;

    public static bool Trap3Chosen;

    public static bool TrapUsed = false;

    public GameObject AirTrap;

    public static bool CanShoot = true;

    public Transform GunTip;

    public ParticleSystem AirParticles;

    public GameObject AirSprite;

    public GameObject GooSprite;

    public Animator GooAnim;

    public Animator AirAnim;

    public Animator Trap3Anim;
    public Animator Trap4Anim;

    public Animator Candle3Anim;
    public Animator Candle4Anim;

    public GameObject Trap3UI;
    public GameObject Trap4UI;

    public AudioSource SwitchSound;

    public AudioSource AirBlow;

    public AudioSource PlaceSound;

    public GameObject Gun;

    public GameObject TrapInHand;

    public GameObject Trap;

    public static bool GunInHand;

    public ParticleSystem FireParticles;

    public GameObject Candles3;
    public GameObject Candles4;

    public GameObject CandleInHand;

    public static bool CandlesChosen;

    int CandlesUsed;

    public GameObject Candle;

    public AudioSource CandlePlaceSound;

    bool SwitchDone = true;

    public Slider CooldownSlider;

    public static bool Cooldown;

    public ParticleSystem FireParticles2;

    private void Start()
    {
        GooAnim.SetBool("Chosen", true);

        if (ShopScript.Trap3Bought && ShopScript.CandlesBoughtFirst == false)
        {
            Trap3UI.SetActive(true);
        }

        if (ShopScript.Trap3Bought && ShopScript.CandlesBoughtFirst == true)
        {
            Trap4UI.SetActive(true);
        }

        if (ShopScript.CandlesBought && ShopScript.CandlesBoughtFirst == false)
        {
            Candles4.SetActive(true);
        }

        if (ShopScript.CandlesBought && ShopScript.CandlesBoughtFirst == true)
        {
            Candles3.SetActive(true);
        }

        GunInHand = true;
        TrapUsed = false;
        CanShoot = true;
        CandlesUsed = 0;
    }

    private void Update()
    {
        int Trap3Bought = PlayerPrefs.GetInt("Trap3Bought");
        int CandlesBought = PlayerPrefs.GetInt("CandlesBought");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Direction = mousePos - (Vector2)gun.position;
        FaceMouse();

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (worldMousePos.x < transform.position.x)
        {
            gun.transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
        }
        else
        {
            gun.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerMovementScript.Cobwebed == false)
        {
            TrapChosen = true;
            Trap3Chosen = false;
            GunInHand = true;
            CandlesChosen = false;

            GooAnim.SetBool("Chosen", false);
            AirAnim.SetBool("Chosen", true);

            SwitchSound.Play();

            Trap3Anim.SetBool("Chosen", false);
            Trap4Anim.SetBool("Chosen", false);
            Candle3Anim.SetBool("Chosen", false);
            Candle4Anim.SetBool("Chosen", false);

            TrapInHand.SetActive(false);
            CandleInHand.SetActive(false);
            Gun.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerMovementScript.Cobwebed == false)
        {
            TrapChosen = false;
            Trap3Chosen = false;
            GunInHand = true;

            GooAnim.SetBool("Chosen", true);
            AirAnim.SetBool("Chosen", false);

            SwitchSound.Play();

            Trap3Anim.SetBool("Chosen", false);
            Trap4Anim.SetBool("Chosen", false);
            Candle3Anim.SetBool("Chosen", false);
            Candle4Anim.SetBool("Chosen", false);

            TrapInHand.SetActive(false);
            CandleInHand.SetActive(false);
            Gun.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Trap3Bought == 1 && PlayerMovementScript.Cobwebed == false && ShopScript.CandlesBoughtFirst == false && !TrapUsed)
        {
            Trap3Chosen = true;
            CandlesChosen = false;
            GunInHand = false;
            
            Gun.SetActive(false);
            GooAnim.SetBool("Chosen", false);
            AirAnim.SetBool("Chosen", false);

            Trap3Anim.SetBool("Chosen", true);
            Trap4Anim.SetBool("Chosen", false);

            Candle3Anim.SetBool("Chosen", false);
            Candle4Anim.SetBool("Chosen", false);

            SwitchSound.Play();
            TrapInHand.SetActive(true);
            CandleInHand.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Trap3Bought == 1 && PlayerMovementScript.Cobwebed == false && ShopScript.CandlesBoughtFirst == true && !TrapUsed)
        {
            Trap3Chosen = true;
            CandlesChosen = false;
            GunInHand = false;
            
            Gun.SetActive(false);
            GooAnim.SetBool("Chosen", false);
            AirAnim.SetBool("Chosen", false);

            Trap3Anim.SetBool("Chosen", false);
            Trap4Anim.SetBool("Chosen", true);

            Candle3Anim.SetBool("Chosen", false);
            Candle4Anim.SetBool("Chosen", false);

            SwitchSound.Play();
            TrapInHand.SetActive(true);
            CandleInHand.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && CandlesBought == 1 && PlayerMovementScript.Cobwebed == false && ShopScript.CandlesBoughtFirst == true)
        {
            CandlesChosen = true;
            Trap3Chosen = false;
            TrapChosen = false;
            GunInHand = false;
            
            Gun.SetActive(false);
            GooAnim.SetBool("Chosen", false);
            AirAnim.SetBool("Chosen", false);

            Trap3Anim.SetBool("Chosen", false);
            Trap4Anim.SetBool("Chosen", false);

            Candle3Anim.SetBool("Chosen", true);
            Candle4Anim.SetBool("Chosen", false);

            SwitchSound.Play();
            TrapInHand.SetActive(false);
            CandleInHand.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && CandlesBought == 1 && PlayerMovementScript.Cobwebed == false && ShopScript.CandlesBoughtFirst == false)
        {
            CandlesChosen = true;
            Trap3Chosen = false;
            TrapChosen = false;
            GunInHand = false;
            
            Gun.SetActive(false);
            GooAnim.SetBool("Chosen", false);
            AirAnim.SetBool("Chosen", false);

            Trap3Anim.SetBool("Chosen", false);
            Trap4Anim.SetBool("Chosen", false);

            Candle3Anim.SetBool("Chosen", false);
            Candle4Anim.SetBool("Chosen", true);

            SwitchSound.Play();
            TrapInHand.SetActive(false);
            CandleInHand.SetActive(true);
        }

        if (TrapChosen == true && CanShoot == true && !Trap3Chosen && Input.GetKeyDown(KeyCode.Mouse0) && FireGunScript.FiredUp == false && !CandlesChosen)
        {
            StartCoroutine(SpawnTimer());
        }
        if (TrapChosen == true && CanShoot == true && !Trap3Chosen && Input.GetKeyDown(KeyCode.Mouse0) && FireGunScript.FiredUp == true && !CandlesChosen)
        {
            StartCoroutine(FireSpawnTimer());
        }

        if (Trap3Chosen == true && !TrapUsed && Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Instantiate(Trap, gameObject.transform.position, gameObject.transform.rotation);
            PlaceSound.Play();
            TrapInHand.SetActive(false);
            Gun.SetActive(true);
            Trap3Chosen = false;
            Trap3UI.SetActive(false);
            Trap4UI.SetActive(false);
            TrapUsed = true;
        }

        if (CandlesChosen == true && CandlesUsed < 5 && Input.GetKeyDown(KeyCode.Mouse0) && !TrapChosen && !Trap3Chosen && !GunInHand && !EventSystem.current.IsPointerOverGameObject())  
        {
            Instantiate(Candle, gameObject.transform.position, gameObject.transform.rotation);
            CandlePlaceSound.Play();
            CandlesUsed++;
        }

        if(CandlesUsed == 5)
        {
            CandleInHand.SetActive(false);
            Candles3.SetActive(false);
            Candles4.SetActive(false);
            CandlesChosen = false;

            if(SwitchDone == true)
            {
                Gun.SetActive(true);
                SwitchDone = false;
            }
        }
            
        AirTrap.transform.position = GunTip.position;

        if(FireParticles2.isPlaying == true)
        {
            FireGunScript.FiredUp = true;
        }
        else
        {
            FireGunScript.FiredUp = false;
        }
}

    private void FaceMouse()
    {
        gun.transform.right = Direction;
    }

    private IEnumerator SpawnTimer()
    {
        AirParticles.Play();
        AirBlow.Play();
        CanShoot = false;
        AirTrap.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        AirParticles.Stop();
        AirTrap.SetActive(false);
        Cooldown = true;
        CooldownSlider.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Cooldown = false;
        CooldownSlider.gameObject.SetActive(false);
        CanShoot = true;
    }

    private IEnumerator FireSpawnTimer()
    {
        FireParticles.Play();
        AirBlow.Play();
        CanShoot = false;
        AirTrap.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        FireParticles.Stop();
        AirTrap.SetActive(false);
        Cooldown = true; 
        CooldownSlider.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Cooldown = false;
        CooldownSlider.gameObject.SetActive(false);
        CanShoot = true;
    }
}

