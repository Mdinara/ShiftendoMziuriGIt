
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ManageTime : MonoBehaviour
{
    public static int Hour = 12;

    public Text TimeText;

    public static int NightCount;

    public GameObject Night2ClosedGate;
    public GameObject Night2OpenGate;

    public GameObject Night3ClosedGate;
    public GameObject Night3OpenGate;

    public GameObject Lvl11;
    public GameObject Lvl12;
    public GameObject Lvl13;

    public GameObject Lvl21;
    public GameObject Lvl22;

    public GameObject Lvl31;
    public GameObject Lvl32;
    public GameObject Lvl33;

    public GameObject Lvl41;
    public GameObject Lvl42;
    public GameObject Lvl43;

    public Text ToDoText;

    public static int GooCount;
    public static int GhostCount;
    public static int SpiderCount;
    public static int CowardCount;
    public static int SpiderNestCount;

    public static int GooCountRn;
    public static int GhostCountRn;
    int SpiderCountRn;
    int CowardCountRn;

    public GameObject NightBox;

    public Text NightText;

    bool levelPassed;

    public GameObject FadeIn;

    bool Counted = false;

    public Animator Clock;
    public Animator AM;

    bool TimeEnded = true;

    public Text Counter;
    void Start()
    {
        AddNight();
        ChooseToDo();
        ChooseEnemyPreset();
        OpenGates();

        StartCoroutine(PrintNight());

        GhostMovement.DecreaseHP = false;

        DecreasePassed();

        Counted = false;

        GooCount = 0;
        GhostCount = 0;
        SpiderCount = 0;
        CowardCount = 0;
        SpiderNestCount = 0;
        Hour = 12;
    }

    void Update()
    {
        int passed = PlayerPrefs.GetInt("Passed");

        TimeText.text = "" + Hour;

        if (Hour == 7 && !Counted && passed == 1)
        {
            StartCoroutine(ClockAnimTimer());
        }

        if (Hour == 7 && !Counted && passed == 0)
        {
            StartCoroutine(ClockAnimTimerLost());
        }

        if(Hour >= 7 && Hour != 12) Hour = 7;

        if(ObiliskScript.EndNightEarly == true && !Counted)
        {
            CountMoney();
        }


        int nightCounter = PlayerPrefs.GetInt("NightCounter", 1);

        if (!levelPassed)
        {
            if (nightCounter == 1 && GooCount >= GooCountRn && GhostCount >= GhostCountRn)
            {
                IncreasePassed();
            }
            if (nightCounter == 2 && GooCount >= GooCountRn && GhostCount >= GhostCountRn && CowardCount >= CowardCountRn)
            {
                IncreasePassed();
            }
            if (nightCounter == 3 && GooCount >= GooCountRn && GhostCount >= GhostCountRn && CowardCount >= CowardCountRn && SpiderCount >= SpiderCountRn)
            {
                IncreasePassed();
            }
            if (nightCounter >= 4 && GooCount >= GooCountRn && GhostCount >= GhostCountRn && CowardCount >= CowardCountRn && SpiderCount >= SpiderCountRn && SpiderNestCount >= 5)
            {
                IncreasePassed();
            }
        }

        if (ObiliskScript.EndNightEarly == true && !Counted)
        {
            CountMoney();
            Counted = true;
        }

        if(PlayerMovementScript.UpdateTodo == true)
        {
            TodoCounter();
        }
    }

    private IEnumerator Timer()
    {
        int nightcounter = PlayerPrefs.GetInt("NightCounter");

        while (true)
        {
            if(nightcounter >= 4)
            {
                yield return new WaitForSeconds(50);
                Hour++;
            }
            else
            {
                yield return new WaitForSeconds(30);
                Hour++;
            }
        }
    }

    private IEnumerator FirstHour()
    {
        int nightcounter = PlayerPrefs.GetInt("NightCounter");

        if(nightcounter >= 4)
        {
            yield return new WaitForSeconds(50);
            Hour = 1;
            StartCoroutine(Timer());
        }
        else
        {
            yield return new WaitForSeconds(30);
            Hour = 1;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator PrintNight()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");
        NightText.text = "Night " + nightCounter;
        NightBox.SetActive(true);
        yield return new WaitForSeconds(4f);
        NightBox.SetActive(false);
        StartCoroutine(FirstHour());
    }

    void AddNight()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");
        nightCounter += 1;

        if (nightCounter > 5)
        {
            PlayerPrefs.SetInt("Money", 0);
            nightCounter = 1; 
        }

        PlayerPrefs.SetInt("NightCounter", nightCounter);
        PlayerPrefs.Save();
    }

    void OpenGates()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        if (nightCounter >= 2)
        {
            Night2ClosedGate.SetActive(false);
            Night2OpenGate.SetActive(true);
        }

        if (nightCounter >= 3)
        {
            Night3ClosedGate.SetActive(false);
            Night3OpenGate.SetActive(true);
        }
    }

    void ChooseEnemyPreset()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        int ChoosePresetLvl1 = Random.Range(1, 4);

        int ChoosePresetLvl2 = Random.Range(1, 3);

        int ChoosePresetLvl3 = Random.Range(1, 4);

        int ChoosePresetLvl4 = Random.Range(1, 4);


        if (nightCounter >= 1)
        {
            if (ChoosePresetLvl1 == 1)
            {
                Lvl11.SetActive(true);
            }
            if (ChoosePresetLvl1 == 2)
            {
                Lvl12.SetActive(true);
            }
            if (ChoosePresetLvl1 == 3)
            {
                Lvl13.SetActive(true);
            }
        }

        if (nightCounter >= 2)
        {
            if (ChoosePresetLvl2 == 1)
            {
                Lvl21.SetActive(true);
            }
            if (ChoosePresetLvl2 == 2)
            {
                Lvl22.SetActive(true);
            }
        }

        if (nightCounter >= 3)
        {
            if (ChoosePresetLvl3 == 1)
            {
                Lvl31.SetActive(true);
            }
            if (ChoosePresetLvl3 == 2)
            {
                Lvl32.SetActive(true);
            }
            if (ChoosePresetLvl3 == 3)
            {
                Lvl33.SetActive(true);
            }
        }

        if (nightCounter >= 4)
        {
            if (ChoosePresetLvl4 == 1)
            {
                Lvl41.SetActive(true);
            }
            if (ChoosePresetLvl4 == 2)
            {
                Lvl42.SetActive(true);
            }
            if (ChoosePresetLvl4 == 3)
            {
                Lvl43.SetActive(true);
            }
        }
    }

    void ChooseToDo()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        int GoosN1 = nightCounter + nightCounter * 3;
        int GoosN2 = nightCounter + nightCounter * 5;

        int GhostsN1 = nightCounter * 2;
        int GhostsN2 = nightCounter * 3;

        int SpidersN1 = nightCounter - 1;
        int SpidersN2 = nightCounter + 1;

        int CowardGhosts = nightCounter - 1;

        int Goos = Random.Range(GoosN1, GoosN2);
        int Ghosts = Random.Range(GhostsN1, GhostsN2);
        int Spiders = Random.Range(SpidersN1, SpidersN2);

        GooCountRn = Goos;
        GhostCountRn = Ghosts;
        SpiderCountRn = Spiders;
        CowardCountRn = CowardGhosts;


        if (nightCounter == 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts";
        }
        if (nightCounter == 2 && CowardGhosts == 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward soul";
        }
        if (nightCounter == 2 && CowardGhosts > 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward souls";
        }

        if (nightCounter >= 3 && CowardGhosts > 1 && Spiders > 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward souls\n\nGet rid of " + Spiders + " Spiders";
        }
        if (nightCounter >= 3 && CowardGhosts == 1 && Spiders > 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward soul\n\nGet rid of " + Spiders + " Spiders";
        }
        if (nightCounter >= 3 && CowardGhosts > 1 && Spiders == 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward souls\n\nGet rid of " + Spiders + " Spider";
        }
        if (nightCounter >= 3 && CowardGhosts == 1 && Spiders == 1)
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward soul\n\nGet rid of " + Spiders + " Spider";
        }

        if ((nightCounter >= 4))
        {
            ToDoText.text = "Suck up " + Goos + " Ghost goos\n\nVaporize " + Ghosts + " Ghosts\n\nCatch " + CowardGhosts + " Coward souls\n\nGet rid of " + Spiders + " Spiders\nAnd don't forget to burn ALL of their nests down";
        }
    }

    void TodoCounter()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        if (nightCounter == 1)
        {
            Counter.text = GooCount + "/" + GooCountRn + "\n\n" + GhostCount + "/" + GhostCountRn;
        }

        if (nightCounter == 2)
        {
            Counter.text = GooCount + "/" + GooCountRn + "\n\n" + GhostCount + "/" + GhostCountRn + "\n\n" + CowardCount + "/" + CowardCountRn;
        }

        if (nightCounter >= 3)
        {
            Counter.text = GooCount + "/" + GooCountRn + "\n\n" + GhostCount + "/" + GhostCountRn + "\n\n" + CowardCount + "/" + CowardCountRn + "\n\n" + SpiderCount + "/" + SpiderCountRn;
        }

        if (nightCounter >= 4)
        {
            Counter.text = GooCount + "/" + GooCountRn + "\n\n" + GhostCount + "/" + GhostCountRn + "\n\n" + CowardCount + "/" + CowardCountRn + "\n\n" + SpiderCount + "/" + SpiderCountRn + "\n\n" + SpiderNestCount + "/5";
        }
    }

    void CountMoney()
    {
        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        int earnedMoney = 0;

        if (PlayerPrefs.GetInt("Passed") == 1)
        {
            earnedMoney = (GooCount - GooCountRn)
                        + (GhostCount - GhostCountRn)
                        + (SpiderCount - SpiderCountRn)
                        + (CowardCount - CowardCountRn);

            currentMoney += earnedMoney;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.Save();
        }
    }

    void IncreasePassed()
    {
        PlayerPrefs.SetInt("Passed", 1);
        PlayerPrefs.Save();
        levelPassed = true;
    }

    void DecreasePassed()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");
        if (nightCounter > 1)
        {
            PlayerPrefs.SetInt("Passed", 0); 
            PlayerPrefs.Save();
            levelPassed = false;
        }
    }

    private IEnumerator ClockAnimTimer()
    {
        int nightCounter = PlayerPrefs.GetInt("NightCounter");

        if (TimeEnded && nightCounter < 5)
        {
            TimeEnded = false;
            CountMoney();
            FadeIn.SetActive(true);
            Clock.SetBool("7am", true);
            AM.SetBool("7am", true);
            yield return new WaitForSeconds(4f);
            Counted = true;
            SceneManager.LoadScene(1);
        }

        if(TimeEnded && nightCounter == 5)
        {
            TimeEnded = false;
            FadeIn.SetActive(true);
            Clock.SetBool("7am", true);
            AM.SetBool("7am", true);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(7);
        }
    }

    private IEnumerator ClockAnimTimerLost()
    {
        if (TimeEnded)
        {
            TimeEnded = false;
            CountMoney();
            FadeIn.SetActive(true);
            Clock.SetBool("7am", true);
            AM.SetBool("7am", true);
            yield return new WaitForSeconds(4.5f);
            Counted = true;
            SceneManager.LoadScene(3);
        }
    }
}
