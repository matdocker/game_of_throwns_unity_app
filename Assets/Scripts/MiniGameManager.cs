using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public GameObject three, two, one, go, TimeUpPanel, timeBar, Clock, CoinCountPanel;

    public Text Timer, EarnedCoins;
    private float CountDownTime = 21f;
    private bool StartTimer = false, IsTimeUp = false;

    public Text textScore;
    public bool goalX2 = false, goalx3 = false;
    public static bool IfMiniGame = false;
    public int doubleCoin;
    public GameScene gs;
    public BarrelManager bm;

    public Text count_CW;
    public Text count_MG;

    public Text coinsCW;
    public Text coinsMG;
    public Text coins;

    public MiniGameRewards mgr;



    // Use this for initialization
    void Start()
    {
        Transform.FindObjectOfType<BarrelManager>();
        IfMiniGame = false;
        CoinCountPanel.SetActive(false);
        timeBar.SetActive(false);
        TimeUpPanel.SetActive(false);
        Clock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IfMiniGame == true)
        {

            if (StartTimer == true)
            {
                Timer1();
                textScore.text = "" + MiniGameRewards.score.ToString();
                count_CW.text = "" + MiniGameRewards.coinCW.ToString();
                count_MG.text = "" + MiniGameRewards.coinMG.ToString();

            }

            if (IsTimeUp == true)
            {
                timeUp();

            }

        }
    }

    public IEnumerator AddWait()
    {

        yield return new WaitForSeconds(1f);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shooter>().enabled = true;

        StartTimer = true;

    }

    public IEnumerator EndWait()
    {

        yield return new WaitForSeconds(1f);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shooter>().enabled = false;

    }


    public void StartMiniGame()
    {

        StartCoroutine(AddPause());
    }

    public IEnumerator AddPause()
    {

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(AddWait());

    }

    public void Timer1()
    {
        timeBar.SetActive(true);
        CoinCountPanel.SetActive(true);
        CountDownTime -= Time.deltaTime;
        int countDown = (int)CountDownTime;
        Timer.text = "" + countDown;
        if (countDown <= 0f)
        {
            IsTimeUp = true;
        }
        if (countDown > 0 && countDown <= 30f)
        {

            Clock.SetActive(true);
        }


    }

    public void timeUp()
    {
        StartCoroutine(EndWait());
        IsTimeUp = false;
        StartTimer = false;
        timeBar.SetActive(false);
        TimeUpPanel.SetActive(true);
        CoinCountPanel.SetActive(false);
        coins.text = "" + MiniGameRewards.score.ToString();
        coinsCW.text = "" + MiniGameRewards.coinCW.ToString();
        coinsMG.text = "" + MiniGameRewards.coinMG.ToString();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shooter>().enabled = false;


        //Objectives

        if (gs.score30 == 1 && MiniGameRewards.score >= 30 && gs.levelTwo.interactable == true)
        {
            gs.score30 = 2;
        }

        if (gs.score50 == 1 && MiniGameRewards.score >= 50 && gs.levelThree.interactable == true)
        {
            gs.score50 = 2;
        }
        if (gs.score100 == 1 && MiniGameRewards.score >= 100 && gs.levelFour.interactable == true)
        {
            gs.score100 = 2;
        }

    }

    public void BackToGame()
    {
        IfMiniGame = false;
        bm.barrelsActive();
        Time.timeScale = 1;
        TimeUpPanel.SetActive(false);
        StartTimer = false;
        MiniGameManager.IfMiniGame = false;
        this.gameObject.GetComponent<GameScene>().SendMessage("BackToCoinDozer");
        ResetTimer();

    }

    public void ResetTimer()
    {
        print("ResetTimer");
        CountDownTime = 31f;
        Clock.SetActive(false);
        goalx3 = false;
        goalX2 = false;
        EarnedCoins.text = "" + 0;
        textScore.text = "" + 0;


    }

}
