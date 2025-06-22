using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NightLordScript : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 10f;

    public GameObject LightGhost;
    public GameObject HardGhost;

    public GameObject Ppreset1;
    public GameObject Ppreset2;

    bool secondPhase = false;

    public GameObject Beam;

    public GameObject ExplosiveAtt;

    public Transform[] TpPositions;
    private int currentIndex = 0;

    public AudioSource Hurt;

    public Slider HpBar;
    int HP = 300;

    public static bool isDying = false;

    public Animator SpinningThings1;
    public Animator SpinningThings2;
    public Animator SpinningThings3;
    public Animator SpinningThings4;
    public Animator SpinningThings5;
    public Animator SpinningThings6;

    public ParticleSystem Death;

    public GameObject FadeIn;

    public AudioSource Music;

    SpriteRenderer sp;

    public AudioSource DeathSound;
    public AudioSource DeathExplosion;

    public static bool InNightLordScene;
    void Start()
    {
        StartCoroutine(AttackCycle());  
        sp = GetComponent<SpriteRenderer>();

        InNightLordScene = true;
        isDying = false;

        PlayerMovementScript.ResetAltarEffects();
    }

    void Update()
    {
        HpBar.value = HP;

        if(HP <=  150) secondPhase = true;

        if (HP <= 0 && !isDying)
        {
            isDying = true;
            StopAllCoroutines();
            StartCoroutine(DyingCoroutine());
        }
    }

    private IEnumerator GhostAttack()
    {
        if (!secondPhase)
        {
            LightGhost.SetActive(true);
            yield return new WaitForSeconds(15f);
            LightGhost.SetActive(false);
        }
        else
        {
            HardGhost.SetActive(true);
            yield return new WaitForSeconds(15f);
            HardGhost.SetActive(false);
        }
    }

    private IEnumerator ProjectileAttack()
    {
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Instantiate(Ppreset2, transform.position, Quaternion.identity);
    }

    private IEnumerator BeamAttack()
    {
        GameObject beamInstance = Instantiate(Beam, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(15f);
        Destroy(beamInstance);
    }

    void ExplosiveAttack()
    {
        Quaternion rot = Quaternion.Euler(0, 0, -90);
        Instantiate(ExplosiveAtt, transform.position, rot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AirTrap"))
        {
            HP -= 20;

            if (HP > 0)
            {
                TeleportToNextPoint();
            }
        }
    }
    void TeleportToNextPoint()
    {
        Hurt.Play();
        if (currentIndex >= 4)
        {
            currentIndex = -1;
        }
        currentIndex = (currentIndex + 1);

        transform.position = TpPositions[currentIndex].position;
    }

    void StartRandomAttackPattern()
    {
        int ChooseAttack = Random.Range(0, 8);

        switch (ChooseAttack)
        {
            case 0:
                StartCoroutine(ProjectileAttack());
                StartCoroutine(GhostAttack());
                break;

            case 1:
                StartCoroutine(BeamAttack());
                StartCoroutine(ProjectileAttack());
                break;

            case 2:
                ExplosiveAttack();
                StartCoroutine(ProjectileAttack());
                break;

            case 3:
                StartCoroutine(GhostAttack());
                StartCoroutine(BeamAttack());
                ExplosiveAttack();
                break;

            case 4:
                StartCoroutine(GhostAttack());
                StartCoroutine(BeamAttack());
                break;
            case 5:
                StartCoroutine(ProjectileAttack());
                StartCoroutine(BeamAttack());
                ExplosiveAttack();
                break;
            case 6:
                StartCoroutine(ProjectileAttack());
                break;
            case 7:
                StartCoroutine(GhostAttack());
                break;
        }

        print(ChooseAttack + "Attack");
    }
    private IEnumerator AttackCycle()
    {
        while (true)
        {
            StartRandomAttackPattern();
            yield return new WaitForSeconds(10f);
        }
    }

    private IEnumerator DyingCoroutine()
    {
        Music.Stop();
        Death.Play();
        SpinningThings1.SetBool("Dying", true);
        SpinningThings2.SetBool("Dying", true);
        SpinningThings3.SetBool("Dying", true);
        SpinningThings4.SetBool("Dying", true);
        SpinningThings5.SetBool("Dying", true);
        SpinningThings6.SetBool("Dying", true);
        yield return new WaitForSeconds(2f);
        DeathExplosion.Play();
        sp.color = new Color(1f, 1f, 1f, 0f);
        Death.Play();
        yield return new WaitForSeconds(2f);
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(6);
    }
}
