using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireScreenFunctions : MonoBehaviour
{
    public GameObject FadeIn;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void Yes()
    {
        StartCoroutine(YesTimer());
    }

    private IEnumerator YesTimer()
    {
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(2f);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        ShopScript.GunBought = false;
        ShopScript.Trap3Bought = false;
        ShopScript.CandlesBought = false;
        ShopScript.CandlesBoughtFirst = false;
        ShopScript.HPPotionBought = false;
        ShopScript.StaminaPotionBought = false;
        ShopScript.VisionPotionBought = false;

        SceneManager.LoadScene(1);
    }
}
