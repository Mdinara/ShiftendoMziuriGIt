using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public GameObject FadeIn;

    public ParticleSystem DeathParticles;

    Animator anim;

    bool hasPlayed = false;

    SpriteRenderer sp;

    AudioSource audio1;

    public GameObject Handle;

    public static bool GamePaused = false;

    public static bool Dying;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        audio1 = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerMovementScript.HP <= 0 && NightLordScript.InNightLordScene == false)
        {
            if (!hasPlayed)
            {
                sp.color = new Color(1f, 1f, 1f, 1f);
                Handle.SetActive(false);
                FadeIn.SetActive(true);
                StartCoroutine(Timer());
            }
        }

        if (PlayerMovementScript.HP <= 0 && NightLordScript.InNightLordScene == true)
        {
            if (!hasPlayed)
            {
                sp.color = new Color(1f, 1f, 1f, 1f);
                Handle.SetActive(false);
                FadeIn.SetActive(true);
                StartCoroutine(Timer2());
            }
        }
    }

    private IEnumerator Timer()
    {
        StopAllOtherAudio();
        Dying = true;
        GhostMovement.DecreaseHP = false;
        yield return new WaitForSeconds(1f);
        hasPlayed = true;
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.6f);
        audio1.Play();
        yield return new WaitForSeconds(2.6f);
        DeathParticles.Play();
        sp.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(2f);
        int night = PlayerPrefs.GetInt("NightCounter");
        night -= 1;
        PlayerPrefs.SetInt("NightCounter", night);
        SceneManager.LoadScene(1);
    }

    private IEnumerator Timer2()
    {
        StopAllOtherAudio();
        Dying = true;
        GhostMovement.DecreaseHP = false;
        yield return new WaitForSeconds(1f);
        hasPlayed = true;
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.6f);
        audio1.Play();
        yield return new WaitForSeconds(2.6f);
        DeathParticles.Play();
        sp.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(7);
    }

    private void StopAllOtherAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allAudioSources)
        {
            if (source != null && source != audio1)
            {
                source.Stop();            
                source.enabled = false;    
            }
        }
    }
}
