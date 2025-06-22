using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfOfferingsScript : MonoBehaviour
{
    public ParticleSystem Fire;

    public GameObject E;

    public GameObject Player;

    bool Used;

    bool DoOnce = true;

    public AudioSource Choir;

    public GameObject SoulForBreathUI;
    public GameObject HeavySoulUI;
    public GameObject StillEnduranceUI;
    public GameObject BlindSpeedsterUI;
    void Start()
    {
        
    }

    void Update()
    {
        int passed = PlayerPrefs.GetInt("Passed");

        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance <= 1f && !Used)
        {
            E.SetActive(true);
        }
        else
        {
            E.SetActive(false);
        }

        if (distance <= 1f && Input.GetKeyDown(KeyCode.E) && !Used)
        {
            int ChooseOffer = Random.Range(1, 5);

            Fire.Play();
            Choir.Play();

            if (ChooseOffer == 1) StillEndurance();

            if (ChooseOffer == 2) SoulForBreath();

            if (ChooseOffer == 3) BlindSpeedster();

            if (ChooseOffer == 4) HeavySoul(); 

            Used = true;
        }
    }

    void StillEndurance()
    {
        if (DoOnce)
        {
            DoOnce = false;
            StillEnduranceUI.SetActive(true);
            PlayerMovementScript.StaminaRegen = 0.02f;
            PlayerMovementScript.MaxStamina *= 6f;
            PlayerMovementScript.UpdateStamina();
            PlayerMovementScript.stamina = PlayerMovementScript.MaxStamina;
        }
    }

    void SoulForBreath()
    {
        if (DoOnce)
        {
            DoOnce = false;
            SoulForBreathUI.SetActive(true);
            PlayerMovementScript.HPregen = 0;
            PlayerMovementScript.MaxHP += 10;
            PlayerMovementScript.StaminaRegen *= 4;
            PlayerMovementScript.UpdateHP();
            PlayerMovementScript.HP = PlayerMovementScript.MaxHP;
        }
    }

    void BlindSpeedster()
    {
        if (DoOnce)
        {
            DoOnce = false;
            BlindSpeedsterUI.SetActive(true);
            PlayerMovementScript.NormSpeed += 1f;
            PlayerMovementScript.SprintSpeed += 1f;
            PlayerMovementScript.DarkSpotX = 0.4f;
            PlayerMovementScript.DarkSpotY = 0.4f;
            PlayerMovementScript.MaxHP -= 20;
            PlayerMovementScript.UpdateHP();
            PlayerMovementScript.HP = PlayerMovementScript.MaxHP;
        }
    }

    void HeavySoul()
    {
        if (DoOnce)
        {
            DoOnce = false;
            HeavySoulUI.SetActive(true);    
            PlayerMovementScript.NormSpeed -= 0.5f;
            PlayerMovementScript.SprintSpeed -= 0.5f;
            PlayerMovementScript.MaxStamina -= 10;
            PlayerMovementScript.MaxHP *= 3;
            PlayerMovementScript.HPregen *= 5;
            PlayerMovementScript.UpdateHP();
            PlayerMovementScript.UpdateStamina();
            PlayerMovementScript.HP = PlayerMovementScript.MaxHP;
            PlayerMovementScript.stamina = PlayerMovementScript.MaxStamina;
        }
    }
}
