using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject GunButton;

    public GameObject ExitButton;

    public GameObject TrapButton;

    public static bool Trap3Bought;

    public AudioSource PotionDrink;

    public GameObject HPPotion;
    public GameObject StaminaPotion;
    public GameObject VisionPotion;

    public static bool CandlesBoughtFirst;

    public static bool CandlesBought;

    public GameObject CandleButton;

    public static bool GunBought;
    public static bool HPPotionBought;
    public static bool StaminaPotionBought;
    public static bool VisionPotionBought;

    public Text MoneyText;

    public AudioSource BuySound;

    public GameObject FadeIn;

    public GameObject PausePanel;

    bool Paused;
    void Start()
    {
        LoadData();
        DestroyOnStart();
    }

    void Update()
    {
        int currentMoney = PlayerPrefs.GetInt("Money");
        MoneyText.text = currentMoney + "$";

        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu();
    }

    public void GunBuy()
    {
        if (!ManageDialogues.inDialogue && !Paused)
        {
            BuySound.Play();
            GunBought = true;
            Destroy(GunButton);
            ExitButton.SetActive(true); 
        }
    }

    public void TrapBuy()
    {
        int currentMoney = PlayerPrefs.GetInt("Money");
        if(currentMoney >= 8 && !ManageDialogues.inDialogue && !Paused)
        {
            BuySound.Play();
            Destroy(TrapButton);
            Trap3Bought = true;
            if (!CandlesBought) CandlesBoughtFirst = false;
            currentMoney -= 8;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.Save();
        }
    }

    public void ExitShop()
    {
        int Tutorial = PlayerPrefs.GetInt("InTutorial");

        if(Tutorial == 0)
        {
            StartCoroutine(Timer1());
        }

        if (Tutorial == 1)
        {
            StartCoroutine(Timer2());
        }
    }

    public void HPpotion()
    {
        int currentMoney = PlayerPrefs.GetInt("Money");
        if(currentMoney >= 7 && !ManageDialogues.inDialogue && !Paused)
        {
            HPPotionBought = true;
            PotionDrink.Play();
            Destroy(HPPotion);
            currentMoney -= 7;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.Save();
        }
    }

    public void Staminapotion()
    {
        int currentMoney = PlayerPrefs.GetInt("Money");
        if(currentMoney >= 9 && !ManageDialogues.inDialogue && !Paused)
        {
            StaminaPotionBought = true;
            PotionDrink.Play();
            Destroy(StaminaPotion);
            currentMoney -= 9;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.Save();
        }
    }

    public void Visionpotion()
    {
        int currentMoney = PlayerPrefs.GetInt("Money");
        if(currentMoney >= 11 && !ManageDialogues.inDialogue && !Paused)
        {
            VisionPotionBought = true;
            PlayerMovementScript.DarkSpotX = 0.8f;
            PlayerMovementScript.DarkSpotY = 0.8f;
            PotionDrink.Play();
            Destroy(VisionPotion);
            currentMoney -= 11;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.Save();
        }
    }

    public void CandleBox()
    {
        if(!ManageDialogues.inDialogue && !Paused)
        {
            BuySound.Play();
            CandlesBought = true;
            if (!Trap3Bought) CandlesBoughtFirst = true;
            Destroy(CandleButton);
        }
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("GunBought", GunBought ? 1 : 0);
        PlayerPrefs.SetInt("Trap3Bought", Trap3Bought ? 1 : 0);
        PlayerPrefs.SetInt("CandlesBought", CandlesBought ? 1 : 0);
        PlayerPrefs.SetInt("HPPotionBought", HPPotionBought ? 1 : 0);
        PlayerPrefs.SetInt("StaminaPotionBought", StaminaPotionBought ? 1 : 0);
        PlayerPrefs.SetInt("VisionPotionBought", VisionPotionBought ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        GunBought = PlayerPrefs.GetInt("GunBought", 0) == 1;
        Trap3Bought = PlayerPrefs.GetInt("Trap3Bought", 0) == 1;
        CandlesBought = PlayerPrefs.GetInt("CandlesBought", 0) == 1;
        HPPotionBought = PlayerPrefs.GetInt("HPPotionBought", 0) == 1;
        StaminaPotionBought = PlayerPrefs.GetInt("StaminaPotionBought", 0) == 1;
        VisionPotionBought = PlayerPrefs.GetInt("VisionPotionBought", 0) == 1;
    }

    void DestroyOnStart()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        if (GunBought)
        {
            GunButton.SetActive(false);
            ExitButton.SetActive(true);
        }
        if (Trap3Bought) TrapButton.SetActive(false);
        if (HPPotionBought) HPPotion.SetActive(false);
        if (VisionPotionBought) VisionPotion.SetActive(false);
        if (StaminaPotionBought) StaminaPotion.SetActive(false);
        if (CandlesBought || nightCounter < 3) CandleButton.SetActive(false);
    }

    private IEnumerator Timer1()
    {
        SaveData();
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
    private IEnumerator Timer2()
    {
        SaveData();
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }



    public void PauseMenu()
    {
        Paused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Paused = false;
        PausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

