using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using Facebook.Unity;
using System.Collections.Generic;

public class GameScene : MonoBehaviour
{

  // Use this for initializationu
  // public GameObject Coins;
  public GameObject tap;
  public GameObject miniGameParent;
  public GameObject initialCoins;
  public Transform coin_parent;
  public Transform coinShower_parent;
  public GameObject normalCoin;
  public Image[] coinImageStack;
  public GameObject MiniGameSplash;
  public Text miniGameCounter;
  public Text coinWallCounter;
  public Text shakeCounter;
  public Vector3 posCoin;
  public int miniGameLvl;
  public GameObject Menu;
  //	public GoalArea ga;

  public Text coinTextCounter;
  public Text coinTextTimer;


  private int coinCount = 20; //20;
  private int coinMaxCount = 40; //40;
  private int coinRegenTimeInSec = 30;
  private int coinTimerCounter = 0;

  private int coinStackCount = 5;
  private int coinStackMaxCount = 5;
  private int coinStackRegenTimeInSec = 1;
  private int coinStackTimerCounter = 0;
  private MiniGameRewards mgr;
  private int coinComboCount = 0;

  private GameTimeSpan gts;
  private bool checkCoin = false;

  public int LevelTex = 1;
  private float LevelXP = 50f;
  private float CurrentXP = 1f;

  public GameObject LevelText;
  private int starCount = -1;
  public int giftCount = 0;

  private float smallCoinXP = 1.5f;
  //    private float silverCoinXP = 7f;
  //#pragma warning disable CS0414 // The field 'GameScene.bigCoinXP' is assigned but its value is never used
  //    private float bigCoinXP = 3f;
  //#pragma warning restore CS0414 // The field 'GameScene.bigCoinXP' is assigned but its value is never used
  private SoundManager sm;

  public GameObject XPBar;
  private float originalXPBarWidth;

  //public GameObject ShakeBar;
  private float originalShakeBarHeight;
  private int coinDropCount = 0;

  private CoinManager coinManager;
  private Rewardmanager rewardManager;

  public GameObject plusCoin_obj;
  public GameObject coinParticle;
  //public GameObject shakeParticle;
  //private float shakePercentage;
  public int dropCount = 0;

  public bool isPrizePanelVisible = false;
  private bool mIsTouchEnabled = true;
  public GameObject TapHere;
  // private bool isInstiateSpecialCoin = false;
  public GameObject StartPanel;

  public GameObject NoCoinPanel;
  public Sprite coinStackDisble;
  public Sprite coinStackEnable;
  int tier = 0;
  private bool hasBonus = false;


  public GameObject Pusher, tapArea;
  public GameObject GoalPanel;
  public GameObject OneTimeOffer;
  public GameObject CloseOTO;
  public int x = 0;
  public MiniGameManager mg;
  public bool isTouchEnabled;
  public Vector2 touchOrigin = -Vector2.one;
  public int count_MG = 0;
  public int count_CW = 0;
  //public Animator AnimMGR;
  public Animator AnimSW;
  public GameObject minGameRings;
  public Button levelOne;
  public Button levelTwo;
  public Button levelThree;
  public Button levelFour;
  public Button levelFive;

  public GameObject[] stars;
  private int cwActivated = 1;
  private int mgPlayed = 1;

  public GameObject truck;
  public GameObject bear;
  public GameObject gun;

  //public MiniGameLvlUp MGLU;
  public Vector3 coinsFirstTime;
  public Vector3 coinsNotFirstTime;
  public GameObject CoinDozerHud;
  private GameObject[] destroyBall;
  private string gameId;
  public GameObject[] barrels;

  //Objective stars
  public GameObject[] star1_1;
  public GameObject[] star1_2;
  public GameObject[] star1_3;

  public GameObject[] star2_1;
  public GameObject[] star2_2;
  public GameObject[] star2_3;

  public GameObject[] star3_1;
  public GameObject[] star3_2;
  public GameObject[] star3_3;

  public GameObject[] star4_1;
  public GameObject[] star4_2;
  public GameObject[] star4_3;

  public int score50 = 1;
  public int score30 = 1;
  private int giftCollect = 1;
  public int loopStop = 10;
  public int giftCollection = 1;
  public int score100 = 1;

  private int a = 1;
  private int b = 1;
  private int c = 1;

  private int giftCollect_3 = 1;
  public float shakeCnt = 0.0f;

  private CoinManager cm;

  //Facebook SDK
  private string androidGameId = "1707582";
  private string iosGameId = "1707583";


  private float? valueToSum = 0;


  void Start()
  {
    if (FB.IsInitialized)
    {
      FB.ActivateApp();
    }
    else
    {
      //Handle FB.Init
      FB.Init(() =>
      {
        FB.ActivateApp();
      });
    }



    print("Level =" + LevelTex);
    sm = Transform.FindObjectOfType<SoundManager>();
    //this.GetComponent<Animation>().Play();

    //Objective star initialisation

    print(star1_1.Length);

#if UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_IOS
                     gameId = iosGameId;
#endif


    if (Advertisement.isSupported && !Advertisement.isInitialized)
    {
      Advertisement.Initialize(gameId);
    }



    //////////////////////////////////
    //DeleteAll();
    //////////////////////////////////
    if (!StartPanel.activeSelf)
    {
      StartPanel.SetActive(true);
      IsTouchEnabled = false;
      sm.Play(SoundManager.SFX.bgm);
    }

    objectiveLoad();
    onMapClick();
    destroyBall = GameObject.FindGameObjectsWithTag("ShootedBall");
    for (int i = 0; i < destroyBall.Length; i++)
    {
      destroyBall[i].SetActive(false);
    }

    GameObject sidewalls = GameObject.FindGameObjectWithTag("SideWalls");
    //AnimSW = sidewalls.GetComponent<Animator>();
    cm = Transform.FindObjectOfType<CoinManager>();
    MiniGameManager.IfMiniGame = false;
    GameObject.Find("Stage").GetComponent<BoxCollider>().enabled = false;
    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shooter>().enabled = false;
    StartCoroutine(TappedHereBlink());
    rewardManager = Transform.FindObjectOfType<Rewardmanager>();
    gts = Transform.FindObjectOfType<GameTimeSpan>();
    //sm = Transform.FindObjectOfType<SoundManager>();
    coinManager = Transform.FindObjectOfType<CoinManager>();
    coinStackCount = coinStackMaxCount;
    coinStackTimerCounter = coinStackRegenTimeInSec;
    //originalShakeBarHeight = ShakeBar.GetComponent<RectTransform>().sizeDelta.y;
    originalXPBarWidth = XPBar.GetComponent<RectTransform>().sizeDelta.x;
    coinRegenTimeInSec = rewardManager.getActivatedBonus("CoinRegen");

    if (gts.isFirstTime == true)
    {

      Debug.Log("Is First time");
      //			coinsFirstTime = initialCoins.transform;
      ///////////
      count_CW = 0;
      coinWallCounter.text = count_CW.ToString();
      count_MG = 0;
      miniGameCounter.text = count_MG.ToString();
      coinCount = 20;//20;
      coinTimerCounter = coinRegenTimeInSec;
      coinTextCounter.text = coinCount.ToString();
      coinTextTimer.text = "00:" + coinTimerCounter.ToString();
      UpdateXP(0);
      //UpdateShake(0);
      print(" Playing First time");

    }
    else
    {
      Debug.Log("Not First time");
      Load();
      CheckTime();
      //print(" Nott Playing First time");
    }
    ///////////////////////
    //coinCount = 500;
    StartCoroutine(CoinRegen());

  }


  public bool IsTouchEnabled
  {
    set { mIsTouchEnabled = value; }
    get { return mIsTouchEnabled; }
  }

  IEnumerator TappedHereBlink()
  {
    while (TapHere.activeSelf)
    {
      yield return new WaitForSeconds(0.3f);
      if (TapHere.GetComponent<SpriteRenderer>().enabled)
      {
        TapHere.GetComponent<SpriteRenderer>().enabled = false;
      }
      else
      {
        TapHere.GetComponent<SpriteRenderer>().enabled = true;
      }
      yield return new WaitForSeconds(0.5f);
    }
  }
  // Update is called once per frame


  private void InstantiateNormalcoin(Vector3 CoinPos)
  {
    if (TapHere.activeSelf)
    {
      TapHere.SetActive(false);
    }

    // Instantiate Normal Coin on Touch Position
    GameObject new_coin = coinManager.GetCoin("NormalCoin");

    //cm.GetRandomNormalCoin();
    //Instantiate(normalCoin, CoinPos, coinManager.baseRotation);
    //coinManager.GetCoin("NormalCoin");    //Instantiate (normalCoin, CoinPos, coinManager.baseRotation);
    Debug.Log("Get CoinNormal Coin Success");

    if (new_coin != null)
    {

      //new_coin.transform.parent = Coins.transform;
      new_coin.transform.parent = initialCoins.transform;
      //new_coin.GetComponent<Rigidbody>().AddForce(0f, 0f, -10f);
      new_coin.transform.position = CoinPos;
      new_coin.transform.rotation = coinManager.baseRotation;
      new_coin.GetComponent<CoinScript>().enabled = true;
      new_coin.GetComponent<CoinScript>().isLanded = false;
      new_coin.GetComponent<Rigidbody>().freezeRotation = true;  //constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
      new_coin.SetActive(true);
      UpdateCoinCount(-1);
      coinStackCount--;
      coinImageStack[coinStackCount].sprite = coinStackDisble;
    }

  }








  void Update()
  {
    onMapClick();

    if (IsTouchEnabled)
    {
      if (Input.touchCount > 0 && Input.touchCount < 2)
      {
        Touch myTouch = Input.GetTouch(0);

        if (myTouch.phase == TouchPhase.Began)
        {
          touchOrigin = myTouch.position;
          //onMapClick();
        }

        else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
        {
          Vector2 touchEnd = myTouch.position;
        }

      }
    }
  }
  public void CheckTime()
  {
    coinCount = PlayerPrefs.GetInt("TotalCoinCount");
    coinTimerCounter = PlayerPrefs.GetInt("TotalCoinTimerCounter");
    int plusCoin = 0;
    if (coinCount <= coinMaxCount)
    {
      int cointLeft = coinMaxCount - coinCount;
      double secsLeft = (double)(cointLeft * (60 * 9)) + coinTimerCounter;

      if (secsLeft < gts.timeSpan.TotalSeconds)
      {
        coinRegenTimeInSec = rewardManager.getActivatedBonus("CoinRegen");
        coinTimerCounter = coinRegenTimeInSec;
        coinCount = coinMaxCount;
      }
      else
      {
        int r = (int)gts.timeSpan.TotalSeconds / (60 * 9);
        int seclft = (int)((r * (60 * 9)) + coinTimerCounter) - (int)gts.timeSpan.TotalSeconds;
        plusCoin = r;
        coinTimerCounter = seclft;
      }
      if (plusCoin > 0)
      {
        plusCoin_obj.GetComponent<Text>().text = "+" + plusCoin.ToString();
        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
      }
    }
    UpdateCoinCount(plusCoin);
  }
  IEnumerator CoinRegen()
  {
    while (true)
    {
      yield return new WaitForSeconds(1);
      if (coinCount < coinMaxCount)
      {
        coinTimerCounter--;

        if (coinTimerCounter <= 0)
        {
          coinRegenTimeInSec = rewardManager.getActivatedBonus("CoinRegen");
          coinTimerCounter = coinRegenTimeInSec;
          UpdateCoinCount(1);
          plusCoin_obj.GetComponent<Text>().text = "+1";
          StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
          sm.Play(SoundManager.SFX.regen);
        }

        if (coinTimerCounter > 9)
        {
          coinTextTimer.text = "00:" + coinTimerCounter.ToString();
        }
        else
        {
          coinTextTimer.text = "00:0" + coinTimerCounter.ToString();
        }

        if (coinCount == coinMaxCount)
        {
          coinTextTimer.text = "";
        }
      }
      if (coinStackCount < coinStackMaxCount)
      {
        coinStackTimerCounter--;
        if (coinStackTimerCounter == 0)
        {
          coinStackTimerCounter = coinStackRegenTimeInSec;

          coinImageStack[coinStackCount].sprite = coinStackEnable;
          coinStackCount++;
        }
      }
    }
  }
  void OnApplicationQuit()
  {

    //coinsNotFirstTime = initialCoins.transform;
    PlayerPrefs.SetInt("TotalCoinCount", coinCount);
    PlayerPrefs.SetInt("TotalCoinTimerCounter", coinTimerCounter);
    //initialCoins.gameObject.SetActive(false);
    Save();
    print("On Application quit");
    MiniGameManager.IfMiniGame = false;

    //DeleteAll();
  }
  void OnApplicationPause(bool pauseStatus)
  {
    if (!pauseStatus)
    {
      if (sm && rewardManager)
      {
        if (!sm.isActive && !rewardManager.isActive)
        {
          IsTouchEnabled = true;
          if (!TapHere.activeSelf)
          {
            TapHere.SetActive(true);
            StartCoroutine(TappedHereBlink());
          }
        }
      }
    }
    else
    {
      PlayerPrefs.SetInt("TotalCoinCount", coinCount);
      PlayerPrefs.SetInt("TotalCoinTimerCounter", coinTimerCounter);
      Save();
      if (gts != null)
      {
        CheckTime();
      }
    }
  }
  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "NormalCoin")
    {
      dropCount = dropCount - 1;
      //Destroy(col.gameObject);
      UpdateCoinCount(1);
      UpdateShakeCnt(0.02f);
      UpdateXP(smallCoinXP);
      col.gameObject.SetActive(false);
      coinComboCount++;
      if (!checkCoin)
      {
        checkCoin = true;
        sm.DropCoinClip = (coinComboCount > 3) ? 3 : coinComboCount;
        sm.Play(SoundManager.SFX.drop);
        plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();
        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
        StartCoroutine(CoinTimer());
      }
      if (coinComboCount < 4 && coinComboCount > 0)
      {
        sm.DropCoinClip = (coinComboCount > 3) ? 3 : coinComboCount;
        sm.Play(SoundManager.SFX.drop);
        GameObject coincombo = GameObject.FindGameObjectWithTag("CoinCombo");
        coincombo.GetComponent<Animator>().Play("CoinComboAnim");
        plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();
        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
        //instantiateSpecialCoin (Random.Range(1, 9));

      }
      else if (coinComboCount > 2)
      {
        sm.DropCoinClip = 3;
        sm.Play(SoundManager.SFX.win1);
        GameObject coincombo = GameObject.FindGameObjectWithTag("CoinCombo");
        coincombo.GetComponent<Animator>().Play("CoinComboAnim");
        plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();

        Debug.Log("instantiate random coin");
        instantiateSpecialCoin(Random.Range(1, 9));

        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));

      }
    }
    else if (col.gameObject.tag == "XPCoin")
    {
      GameObject coincombo = GameObject.FindGameObjectWithTag("XpCoinSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      //add point to the XP meter with the amount shown.
      if (col.gameObject.name == "XPCoin15")
      {
        print("XPCoin15");
        UpdateCoinCount(1);
        UpdateShakeCnt(0.02f);
        sm.Play(SoundManager.SFX.giant);
        UpdateXP(15f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
      }
      else if (col.gameObject.name == "XPCoin30")
      {
        print("XPCoin30");
        UpdateCoinCount(1);
        UpdateShakeCnt(0.03f);
        sm.Play(SoundManager.SFX.giant);
        UpdateXP(30f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
      }
      else if (col.gameObject.name == "XPCoin50")
      {
        print("XPCoin50");
        UpdateCoinCount(1);
        UpdateShakeCnt(0.04f);
        sm.Play(SoundManager.SFX.giant);
        UpdateXP(50f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
        instantiateSpecialCoin(Random.Range(1, 9));
      }
      int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
        extraCoin += 1;
        UpdateCoinCount(Random.Range(1, extraCoin));
      }
    }
    else if (col.gameObject.tag == "GiantCoin")
    { ///this will shake the machine, dropping more coins.
      GameObject coincombo = GameObject.FindGameObjectWithTag("GiantCoinSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      UpdateCoinCount(5);
      UpdateShakeCnt(0.05f);
      UpdateXP(20f + rewardManager.getActivatedBonus("MoreXP"));
      GameObject GiantCoin = coinManager.GetCoin("summoned_GiantCoin");
      GiantCoin.transform.rotation = coinManager.baseRotation;
      GiantCoin.GetComponent<CoinScript>().enabled = true;
      GiantCoin.GetComponent<CoinScript>().isLanded = false;
      GiantCoin.GetComponent<CoinScript>().giantShake = false;
      GiantCoin.SetActive(true);
      GiantCoin.name = "summoned_GiantCoin";
      sm.GiantAudio.clip = sm.giantCoin[1];
      sm.Play(SoundManager.SFX.giant);
      GiantCoin.transform.parent = coinShower_parent;
      GiantCoin.transform.localPosition = new Vector3(0f, 3f, -3f);
      //Instatiate Random coin
      Debug.Log("instantiate random coin");
      //instantiateSpecialCoin(Random.Range(1, 9));

      int gChance = Random.Range(1, 101);
      if (gChance < rewardManager.getActivatedBonus("GiftChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      /*int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
          extraCoin += 1;
          UpdateCoinCount(Random.Range(1, extraCoin));
      }*/
      tier = Random.Range(1, 101);

      col.gameObject.SetActive(false);

    }
    else if (col.gameObject.tag == "summoned_GiantCoin")
    { ///this will shake the machine, dropping more coins.
      GameObject coincombo = GameObject.FindGameObjectWithTag("GiantCoinSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      UpdateCoinCount(1);
      UpdateShakeCnt(0.05f);
      sm.Play(SoundManager.SFX.giant);
      UpdateXP(20f + rewardManager.getActivatedBonus("MoreXP"));
      //Instatiate Random coin
      Debug.Log("instantiate random coin");
      //instantiateSpecialCoin(Random.Range(1, 9));
      /* float iPos = 1.5f;
          float iPos2 = 1.1f;
          int counter = 0;
          for (int i = 0; i < rewardManager.getActivatedBonus("MoreCoins"); i++) {
              GameObject new_coin = coinManager.GetRandomNormalCoin();
              new_coin.SetActive(true);
              new_coin.transform.parent = coinShower_parent;
              new_coin.transform.rotation = coinManager.baseRotation;
              new_coin.transform.localPosition = new Vector3(iPos, 1f, iPos2);

              counter++;
              if (counter == 4) {
                  counter = 0;
                  iPos = 1.5f;
                  iPos2 += 0.5f;
              } else {
                  iPos -= 1f;
              }
          }*/
      int r = Random.Range(1, 100);
      if (r <= rewardManager.getActivatedBonus("PrizeChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      col.gameObject.SetActive(false);

    }
    else if (col.gameObject.tag == "SideWalls")
    { //walls are raised on both sides to prevent coins and prizes from dropping there
      GameObject coincombo = GameObject.FindGameObjectWithTag("CoinWallSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      //coinWall();
      count_CW = count_CW + 1;
      string CW_count = count_CW.ToString();
      coinWallCounter.text = CW_count;
      col.gameObject.SetActive(false);
      UpdateShakeCnt(0.05f);


    }
    else if (col.gameObject.tag == "CoinPresent")
    {
      sm.Play(SoundManager.SFX.prize);
      rewardManager.InstantiateRandomGift(0);
      GameObject coincombo = GameObject.FindGameObjectWithTag("GiftCoinSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      //this will give you four special coins on top of the platform
      //rewardManager.InstantiateRandomGift(0);
      UpdateCoinCount(1);
      UpdateShakeCnt(0.02f);
      UpdateXP(10f + rewardManager.getActivatedBonus("MoreXP"));
      plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();

      //Instatiate Random coin
      // Debug.Log("instantiate random coin");
      //instantiateSpecialCoin (Random.Range(1, 9));

      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "CoinShower")
    {
      UpdateShakeCnt(0.05f);
      GameObject coincombo = GameObject.FindGameObjectWithTag("CoinShowerSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      //several coins will be dropped on the moving slide
      float iPos = 1.5f;
      float iPos2 = 1.1f;
      int counter = 0;
      for (int i = 0; i < rewardManager.getActivatedBonus("MoreCoins"); i++)
      {
        GameObject new_coin = coinManager.GetRandomNormalCoin();
        new_coin.SetActive(true);
        new_coin.transform.parent = coinShower_parent;
        new_coin.transform.rotation = coinManager.baseRotation;
        new_coin.transform.localPosition = new Vector3(iPos, 0.6f, iPos2);

        counter++;
        if (counter == 4)
        {
          counter = 0;
          iPos = 1.5f;
          iPos2 += 0.5f;
        }
        else
        {
          iPos -= 1f;
        }
      }

      sm.Play(SoundManager.SFX.win2);
      int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
        extraCoin += 1;
        UpdateCoinCount(Random.Range(1, extraCoin));
      }
      col.gameObject.SetActive(false);

    }
    else if (col.gameObject.tag == "Gift" || col.gameObject.tag == "Rewards")
    {
      rewardManager.AddToCollection(col.gameObject.name);
      Debug.Log("New Gift: " + col.gameObject.name);
      rewardManager.ShowRewardPopup(col.gameObject.name);
      isTouchEnabled = false;
      UpdateCoinCount(1);
      UpdateShakeCnt(0.05f);
      UpdateXP(20f + rewardManager.getActivatedBonus("MoreXP"));
      sm.Play(SoundManager.SFX.prize);
      col.gameObject.SetActive(false);

      if (levelTwo.interactable == true)
      {
        if (giftCollect == 1)
        {
          giftCollect = 2;
          sm.Play(SoundManager.SFX.levelup);
        }
      }

      if (levelThree.interactable == true && levelFour.interactable == false)
      {
        if (giftCollect_3 == 1 && LevelTex >= 15 && LevelTex < 40)
        {
          giftCount = giftCount + 1;
          if (giftCount == 3)
          {
            giftCollect_3 = 2;
            sm.Play(SoundManager.SFX.levelup);
          }

        }
      }

      else if (levelFour.interactable == true && levelFive.interactable == false && giftCollection == 1)
      {
        if (col.gameObject.name.Contains("Dragon_"))
        {
          a = a + 1;

          if (a == 4)
          {
            sm.Play(SoundManager.SFX.levelup);
            giftCollection = 2;
            a = 0;
          }

        }
        else if (col.gameObject.name.Contains("Sword_"))
        {
          b = b + 1;

          if (b == 4)
          {
            sm.Play(SoundManager.SFX.levelup);
            giftCollection = 2;
            b = 0;
          }

        }
        else if (col.gameObject.name.Contains("Shield_"))
        {
          c = c + 1;

          if (c == 4)
          {
            sm.Play(SoundManager.SFX.levelup);
            giftCollection = 2;
            c = 0;
          }
        }
      }
    }

    else if (col.gameObject.tag == "MiniGame")
    {
      UpdateShakeCnt(0.02f);
      col.gameObject.SetActive(false);
      sm.Play(SoundManager.SFX.mg);
      GameObject coincombo = GameObject.FindGameObjectWithTag("MiniGameSplash");
      coincombo.GetComponent<Animator>().Play("LevelUpSplash");
      invokeMiniGameCoin();
    }

  }

  private static void CoinCombo(GameObject coincombo)
  {
    coincombo.GetComponent<Animator>().Play("CoinComboAnim");
  }

  private IEnumerator CoinTimer()
  {
    yield return new WaitForSeconds(3);
    coinComboCount = 0;
    checkCoin = false;
    //isInstiateSpecialCoin = false;
  }
  public void UpdateCoinCount(int plusCoin)
  {
    coinCount += plusCoin;
    coinTextCounter.text = coinCount.ToString();
  }
  public void ShakeCastle()
  {
    Hashtable ht1 = new Hashtable
        {
            { "z", 0.25f },
            { "x", 0.15f },
            { "y", 0.25f },
            { "time", 1.0f }
        };
    //iTween.ShakePosition(Castle, ht1);
    sm.Play(SoundManager.SFX.shake);
  }
  IEnumerator ClearText(Text pText)
  {
    yield return new WaitForSeconds(2f);
    pText.text = "";
  }
  IEnumerator StartParticle()
  {
    yield return new WaitForSeconds(10f);
    coinParticle.SetActive(false);
  }
  public void UpdateGlobalCoin()
  {
    coinCount = PlayerPrefs.GetInt("TotalCoinCount");
    UpdateCoinCount(0);
  }
  private void UpdateXP(float plusXP)
  {
    CurrentXP += plusXP;
    if (CurrentXP >= LevelXP)
    {
      sm.Play(SoundManager.SFX.levelup);
      CurrentXP -= LevelXP;
      LevelTex++;
      LevelXP += (LevelXP * 0.65f);
      LevelText.GetComponent<Text>().text = LevelTex.ToString();

      //instantiateSpecialCoin(Random.Range(1,9));
      int r = Random.Range(1, 100);
      if (r <= rewardManager.getActivatedBonus("PrizeChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      GameObject levelup = GameObject.FindGameObjectWithTag("LevelUpSplash");
      //levelup.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Levelup");
      levelup.GetComponent<Animator>().Play("LevelUpSplash");
    }

    float percentage = CurrentXP / LevelXP;

    Vector2 _size = XPBar.GetComponent<RectTransform>().sizeDelta;
    _size.x = originalXPBarWidth * percentage;
    XPBar.GetComponent<RectTransform>().sizeDelta = _size;

    if (LevelTex == 10 || LevelTex == 15 || LevelTex == 30 || LevelTex == 50)
    {
      sm.Play(SoundManager.SFX.levelup);
    }
  }

  public void Save()
  {
    coinManager.SaveAllCoins();
    rewardManager.SaveAllGift();
    rewardManager.SaveCollectedGift();

    sm.Save();

    if (LevelTex == 1 || LevelTex == 0)
    {
      LevelTex = 1;
      LevelXP = 50f;
    }
    PlayerPrefs.SetInt("StarCount", starCount);
    PlayerPrefs.SetFloat("CurrentXP", CurrentXP);
    PlayerPrefs.SetInt("CurrentLevel", LevelTex);
    PlayerPrefs.SetFloat("LevelXP", LevelXP);
    PlayerPrefs.SetInt("CoinDropCount", coinDropCount);
    PlayerPrefsX.SetBool("hasBonus", false);

    PlayerPrefs.SetInt("MiniGameCnt", count_MG);
    PlayerPrefs.SetString("MiniGameCntStr", miniGameCounter.text);
    PlayerPrefs.SetInt("SideWallsCnt", count_CW);
    PlayerPrefs.SetString("SideWallCntStr", coinWallCounter.text);
    PlayerPrefs.SetFloat("Shake", shakeCnt);
    saveObjectives();

    PlayerPrefs.SetInt("cwActivated", cwActivated);
    PlayerPrefs.SetInt("mgPlayed", mgPlayed);
    PlayerPrefs.SetInt("loopStop", loopStop);
    PlayerPrefs.SetInt("score30", score30);
    PlayerPrefs.SetInt("score50", score50);
    PlayerPrefs.SetInt("score100", score100);
    PlayerPrefs.SetInt("giftCollect", giftCollect);
    PlayerPrefs.SetInt("giftCollect_3", giftCollect_3);
    PlayerPrefs.SetInt("giftCollection", giftCollection);

    //PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
    PlayerPrefs.Save();

    print("SCENE SAVED!!!");
  }
  private void Load()
  {
    //GameScene.DontDestroyOnLoad(initialCoins);
    LevelTex = PlayerPrefs.GetInt("CurrentLevel");
    LevelXP = PlayerPrefs.GetFloat("LevelXP");
    LevelText.GetComponent<Text>().text = LevelTex.ToString();
    UpdateXP(PlayerPrefs.GetFloat("CurrentXP"));
    //UpdateShake(PlayerPrefs.GetInt("CoinDropCount"));
    hasBonus = PlayerPrefsX.GetBool("hasBonus");
    shakeCnt = PlayerPrefs.GetFloat("Shake");
    shakeCounter.text = shakeCnt.ToString("F2");
    starCount = PlayerPrefs.GetInt("StarCount");
    count_MG = PlayerPrefs.GetInt("MiniGameCnt", count_MG);
    miniGameCounter.text = PlayerPrefs.GetString("MiniGameCntStr", miniGameCounter.text);
    count_CW = PlayerPrefs.GetInt("SideWallsCnt", count_CW);
    coinWallCounter.text = PlayerPrefs.GetString("SideWallCntStr", coinWallCounter.text);
    loadObjectives();

    levelOne.interactable = PlayerPrefsX.GetBool("Level One");
    levelTwo.interactable = PlayerPrefsX.GetBool("Level Two");
    levelThree.interactable = PlayerPrefsX.GetBool("Level Three");
    levelFour.interactable = PlayerPrefsX.GetBool("Level Four");
    levelFive.interactable = PlayerPrefsX.GetBool("Level Five");
    //SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));

    cwActivated = PlayerPrefs.GetInt("cwActivated");
    mgPlayed = PlayerPrefs.GetInt("mgPlayed");
    loopStop = PlayerPrefs.GetInt("loopStop");
    score30 = PlayerPrefs.GetInt("score30");
    score50 = PlayerPrefs.GetInt("score50");
    score100 = PlayerPrefs.GetInt("score100");
    giftCollect = PlayerPrefs.GetInt("giftCollect");
    giftCollect_3 = PlayerPrefs.GetInt("giftCollect_3");
    giftCollection = PlayerPrefs.GetInt("giftCollection");

    print("SCENE LOADED!!!");
  }

  private void Load_MG()
  {
    initialCoins.SetActive(true);
    LevelTex = PlayerPrefs.GetInt("CurrentLevel");
    LevelXP = PlayerPrefs.GetFloat("LevelXP");
    LevelText.GetComponent<Text>().text = LevelTex.ToString();
    UpdateXP(PlayerPrefs.GetFloat("CurrentXP"));
    //UpdateShake(PlayerPrefs.GetInt("CoinDropCount"));
    hasBonus = PlayerPrefsX.GetBool("hasBonus");

    //loadObjectives();
  }
  public void invokeMiniGameCoin()
  {
    sm.Play(SoundManager.SFX.mg);
    Invoke("PlayMiniGameCoin", 2);
  }
  public void invokeMiniGame()
  {
    if (count_MG >= 1)
    {
      sm.Play(SoundManager.SFX.mg);
      Invoke("PlayMiniGame", 3.0f);
    }
  }

  public void PlayMiniGame()
  {
    if (count_MG >= 1)
    {
      sm.Play(SoundManager.SFX.bgm);
      if (mgPlayed == 1)
      {
        sm.Play(SoundManager.SFX.levelup);
        mgPlayed = 2;
      }

      for (int i = 0; i < destroyBall.Length; i++)
      {
        destroyBall[i].SetActive(true);
      }

      MiniGameManager.IfMiniGame = true;
      MiniGameRewards.coinCW = 0;
      MiniGameRewards.coinMG = 0;
      MiniGameRewards.gift = 0;
      count_MG = count_MG - 1;
      miniGameCounter.text = count_MG.ToString();
      Save();
      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("CameraMovement");
      GameObject.FindGameObjectWithTag("TapAreaButton").SetActive(false);
      CoinDozerHud.SetActive(false);
      //BaskitBallHud.SetActive (true);
      TapHere.SetActive(false);
      tapArea.SetActive(false);
      GameObject.FindWithTag("ShootedBall").SetActive(true);
      Pusher.gameObject.GetComponent<CoinPusher>().enabled = false;
      GameObject.Find("Stage").GetComponent<BoxCollider>().enabled = true;
      GameObject.Find("Stage1").GetComponent<BoxCollider>().enabled = true;
      GameObject.Find("Stage3").GetComponent<BoxCollider>().enabled = true;
      MiniGameRewards.score = 0;
      initialCoins.gameObject.SetActive(false);
      GoalPanel.SetActive(true);

    }
    else
    {
      //sm.Play(SoundManager.SFX.none);
    }

  }

  public void PlayMiniGameCoin()
  {
    sm.Play(SoundManager.SFX.bgm);
    if (mgPlayed == 1)
    {
      sm.Play(SoundManager.SFX.levelup);
      mgPlayed = 2;
    }

    for (int i = 0; i < destroyBall.Length; i++)
    {
      destroyBall[i].SetActive(true);
    }

    MiniGameManager.IfMiniGame = true;
    MiniGameRewards.coinCW = 0;
    MiniGameRewards.coinMG = 0;
    MiniGameRewards.gift = 0;
    Save();
    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("CameraMovement");
    GameObject.FindGameObjectWithTag("TapAreaButton").SetActive(false);
    CoinDozerHud.SetActive(false);
    //BaskitBallHud.SetActive (true);
    TapHere.SetActive(false);
    tapArea.SetActive(false);
    GameObject.FindWithTag("ShootedBall").SetActive(true);
    Pusher.gameObject.GetComponent<CoinPusher>().enabled = false;
    GameObject.Find("Stage").GetComponent<BoxCollider>().enabled = true;
    GameObject.Find("Stage1").GetComponent<BoxCollider>().enabled = true;
    GameObject.Find("Stage3").GetComponent<BoxCollider>().enabled = true;
    MiniGameRewards.score = 0;
    initialCoins.gameObject.SetActive(false);
    GoalPanel.SetActive(true);


  }


  public void BackToCoinDozer()
  {
    Load_MG();
    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shooter>().enabled = false;
    //	BaskitBallHud.SetActive (false);
    CoinDozerHud.SetActive(true);
    UpdateMGcoinWallCount();
    UpdateMGminiGameCount();
    for (int i = 0; i < destroyBall.Length; i++)
    {
      destroyBall[i].SetActive(false);
    }

    TapHere.SetActive(true);
    tapArea.SetActive(true);
    Pusher.gameObject.GetComponent<CoinPusher>().enabled = true;
    isTouchEnabled = true;
    initialCoins.gameObject.SetActive(true);
    GameObject.Find("Stage").GetComponent<BoxCollider>().enabled = false;
    GameObject objBall = GameObject.FindGameObjectWithTag("Ball");
    if ((objBall != null))
    {
      GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
      for (int i = 0; i < balls.Length; i++)
      {
        Destroy(balls[i].gameObject);
      }
    }
    var children = miniGameParent.GetComponentsInChildren<Transform>();
    foreach (var child in children)
    {
      if (child.tag == "Rewards")
      {
        rewardManager.AddToCollection(child.name);
      }
    }
    print("BackToCoinDozer");
    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("BackToGame");
    sm.Play(SoundManager.SFX.prize);
    //activeBarrels ();


  }


  public void TapToStartMiniGame()
  {
    print("tap to start Mini Game");
    MiniGameManager.IfMiniGame = true;

    GoalPanel.SetActive(false);
    GameObject.Find("GameManager").GetComponent<MiniGameManager>().SendMessage("ResetTimer");
    GameObject.Find("GameManager").GetComponent<MiniGameManager>().SendMessage("StartMiniGame");
    //MiniGameAnim (miniGameLvl);

  }
  IEnumerator WaitToStartMiniGame()
  {
    yield return new WaitForSeconds(1f);
    PlayMiniGame();
    //MiniGameLvLOne();
  }

  IEnumerator WaitToStartMiniGameCoin()
  {
    yield return new WaitForSeconds(1f);
    PlayMiniGameCoin();
    //MiniGameLvLOne();
  }
  IEnumerator CallOneTimeOffer()
  {
    yield return new WaitForSeconds(2);
    tapArea.SetActive(false);
    tap.SetActive(false);
    OneTimeOffer.SetActive(true);
    IsTouchEnabled = false;
    sm.OTO.Play();
  }

  public void CloseOTOffer()
  {
    OneTimeOffer.SetActive(false);
    IsTouchEnabled = true;
  }

  public void HideMenu()
  {
    sm.ButtonClicked();
    Menu.SetActive(false);
    IsTouchEnabled = true;
    //		admob.ShowAd ();

  }

  public void UpdateMGCoinCount()
  {
    for (int i = 0; i < MiniGameRewards.score; i++)
    {
      UpdateCoinCount(1);
    }
  }

  public void UpdateMGminiGameCount()
  {
    for (int i = 0; i < MiniGameRewards.coinMG; i++)
    {
      count_MG = count_MG + 1;
      miniGameCounter.text = count_MG.ToString();
    }
  }

  public void UpdateMGcoinWallCount()
  {
    for (int i = 0; i < MiniGameRewards.coinCW; i++)
    {
      count_CW = count_CW + 1;
      coinWallCounter.text = count_CW.ToString();
    }
  }

  public void UpdateCoinWallCount(int x)
  {
    count_CW = count_CW + x;
    coinWallCounter.text = count_CW.ToString();

  }

  public void UpdateMiniGameCount(int x)
  {
    count_MG = count_MG + x;
    miniGameCounter.text = count_MG.ToString();

  }

  public void TapArea()
  {
    if (IsTouchEnabled)
    {
      Ray ray = Camera.main.ScreenPointToRay(touchOrigin);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))

        if (coinCount > 0 && coinStackCount > 0)
        {
          InstantiateNormalcoin(new Vector3(hit.point.x, 3f, hit.point.z));
        }
        else
        {
          if (coinCount == 0)
          {
            NoCoinPanel.SetActive(true);
            IsTouchEnabled = false;
            GameObject.FindWithTag("TapAreaButton").SetActive(false);
            sm.Play(SoundManager.SFX.nomore);
          }
        }
    }
  }
  public void TouchEnabled()
  {
    IsTouchEnabled = true;

  }

  public void coinWall()
  {
    if (count_CW > 0 && AnimSW.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !AnimSW.IsInTransition(0))
    {

      if (cwActivated == 1)
      {
        sm.Play(SoundManager.SFX.levelup);
        cwActivated = 2;
      }

      print("SideWalls");
      sm.Play(SoundManager.SFX.wall);


      if (AnimSW != null)
      {
        AnimSW.Play("CoinWallAnim");
        count_CW = count_CW - 1;
        coinWallCounter.text = count_CW.ToString();
      }
      else
      {

        AnimSW.Rebind();

      }
    }


  }

  public void onMapClick()
  {

    levelOne.onClick.AddListener(onLevelOneClick);
    levelTwo.onClick.AddListener(onLevelTwoClick);
    levelThree.onClick.AddListener(onLevelThreeClick);
    levelFour.onClick.AddListener(onLevelFourClick);
    levelFive.onClick.AddListener(onLevelFiveClick);

    if (loopStop == 10 || cwActivated == 1 || mgPlayed == 1 ||
        levelOne.interactable == true)
    {
      levelOne.interactable = true;
      PlayerPrefsX.SetBool("Level One", levelOne.interactable);
      if (cwActivated == 2)
      {

        cwActivated = 3;

        starCount = ++starCount;
        foreach (GameObject obj in star1_1)
        {
          obj.SetActive(true);
        }

        stars[starCount].SetActive(true);
      }
      if (mgPlayed == 2)
      {
        print("MiniGame Played");
        mgPlayed = 3;
        starCount = ++starCount;

        foreach (GameObject obj in star1_2)
        {
          obj.SetActive(true);
        }

        stars[starCount].SetActive(true);
      }
      if (LevelTex >= 10 && loopStop == 10)
      {

        starCount = ++starCount;
        foreach (GameObject obj in star1_3)
        {
          obj.SetActive(true);
        }
        stars[starCount].SetActive(true);
        loopStop = 15;
      }
    }

    if (LevelTex >= 10 && loopStop == 15 && mgPlayed == 3 && cwActivated == 3 || levelTwo.interactable == true)
    {

      if (score30 == 1 || giftCollect == 1 || loopStop == 15 ||
          levelTwo.interactable == true)
      {
        miniGameLvl = 2;
        levelTwo.interactable = true;
        PlayerPrefsX.SetBool("Level Two", levelTwo.interactable);

        if (score30 == 2)
        {

          score30 = 3;
          starCount = ++starCount;
          foreach (GameObject obj in star2_1)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);
        }

        if (giftCollect == 2)
        {
          giftCollect = 3;
          starCount = ++starCount;
          foreach (GameObject obj in star2_2)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
        if (LevelTex >= 15 && loopStop == 15)
        {
          loopStop = 30;
          starCount = ++starCount;
          foreach (GameObject obj in star2_3)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
      }
    }
    if (LevelTex == 15 && loopStop == 30 && giftCollect == 3 && score50 == 1 || levelThree.interactable == true)
    {
      miniGameLvl = 3;


      if (score50 == 3 || giftCollect_3 == 1 || loopStop == 15 ||
              levelThree.interactable == true)
      {
        levelThree.interactable = true;
        PlayerPrefsX.SetBool("Level Three", levelThree.interactable);
        if (score50 == 2)
        {

          score50 = 3;
          starCount = ++starCount;
          foreach (GameObject obj in star3_1)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);
        }

        if (giftCollect_3 == 2)
        {
          giftCollect_3 = 3;
          starCount = ++starCount;

          foreach (GameObject obj in star3_2)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
        if (LevelTex >= 30 && loopStop == 30)
        {
          loopStop = 40;
          starCount = ++starCount;

          foreach (GameObject obj in star3_3)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
      }

    }
    if (LevelTex >= 30 && giftCollect_3 == 3 && score50 == 3 || levelFour.interactable == true)
    {
      miniGameLvl = 4;


      if (giftCollection == 1 || score100 == 1 || loopStop == 40 ||
               levelFour.interactable == true)
      {
        levelFour.interactable = true;
        PlayerPrefsX.SetBool("Level Four", levelFour.interactable);
        if (giftCollection == 2)
        {
          giftCollection = 3;
          starCount = ++starCount;

          foreach (GameObject obj in star4_1)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);
        }

        if (score100 == 2)
        {
          giftCollect = 3;
          starCount = ++starCount;

          foreach (GameObject obj in star4_2)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
        if (LevelTex >= 40 && loopStop == 40)
        {
          loopStop = 50;
          starCount = ++starCount;

          foreach (GameObject obj in star4_3)
          {
            obj.SetActive(true);
          }
          stars[starCount].SetActive(true);

        }
      }
    }
    if (LevelTex >= 50 && score100 == 3 && giftCollection == 3 || levelFive.interactable == true)
    {
      miniGameLvl = 5;
      levelFive.interactable = true;
      PlayerPrefsX.SetBool("Level Five", levelFive.interactable);
    }
  }

  public void objectiveLoad()
  {
    for (int i = 0; i < starCount; i++)
    {
      stars[i].SetActive(true);
    }

  }


  public void onLevelOneClick()
  {

    Debug.Log("Level One");
    miniGameLvl = 1;

  }
  public void onLevelTwoClick()
  {
    Debug.Log("Level Two");
    miniGameLvl = 2;
  }

  public void onLevelThreeClick()
  {
    Debug.Log("Level Three");
    miniGameLvl = 3;
  }

  public void onLevelFourClick()
  {
    Debug.Log("Level Four");
    miniGameLvl = 4;
  }

  public void onLevelFiveClick()
  {
    Debug.Log("Level Five");
    miniGameLvl = 5;

  }


  public void instantiateSpecialCoin(int randomCoin)
  {
    float iPos = Random.Range(-2.0f, 2.0f);
    float iPos2 = Random.Range(-5.0f, 2.0f);

    GameObject new_Coin = null;

    if (randomCoin == 1)
    {
      //			Debug.Log ("CoinWall");
      new_Coin = coinManager.GetCoin("CoinWall");
    }
    else if (randomCoin == 2)
    {
      //			Debug.Log ("CoinShower");
      new_Coin = coinManager.GetCoin("CoinShower");
    }
    else if (randomCoin == 3)
    {
      //			Debug.Log ("CoinPresent");
      new_Coin = coinManager.GetCoin("CoinPresent");
    }
    else if (randomCoin == 4)
    {
      //			Debug.Log ("summoned_GiantCoin");
      new_Coin = coinManager.GetCoin("summoned_GiantCoin");
    }
    else if (randomCoin == 5)
    {
      //			Debug.Log ("GiantCoin");
      new_Coin = coinManager.GetCoin("GiantCoin");
    }
    else if (randomCoin == 6)
    {
      //			Debug.Log ("MiniGameCoin");
      new_Coin = coinManager.GetCoin("MiniGameCoin");
    }
    else if (randomCoin == 7)
    {
      //			Debug.Log ("XPCoin15");
      new_Coin = coinManager.GetCoin("XPCoin15");

    }
    else if (randomCoin == 8)
    {
      //			Debug.Log ("XPCoin30");
      new_Coin = coinManager.GetCoin("XPCoin30");

    }
    else if (randomCoin == 9)
    {
      //			Debug.Log ("XPCoin50");
      new_Coin = coinManager.GetCoin("XPCoin50");

    }

    //sm.Play(SoundManager.SFX.levelup);
    if (new_Coin != null)
    {
      new_Coin.transform.parent = coinShower_parent;
      new_Coin.transform.localPosition = new Vector3(iPos, 0.6f, iPos2);
      new_Coin.SetActive(true);

    }

  }

  public void DeleteAll()
  {

    PlayerPrefs.DeleteAll();
    PlayerPrefs.SetInt("FirstTime", 0);
  }


  public void prizeCount(GameObject gift)
  {
    rewardManager.AddToCollection(gift.name);
    return;
  }


  public void ShowRewardedVideo()
  {
    ShowOptions options = new ShowOptions();
    options.resultCallback = HandleShowResult;

    Advertisement.Show("rewardedVideo", options);
  }

  void HandleShowResult(ShowResult result)
  {
    if (result == ShowResult.Finished)
    {
      Debug.Log("Video completed - Offer a reward to the player");
      UpdateCoinCount(25);
      sm.Play(SoundManager.SFX.win2);
    }
    else if (result == ShowResult.Skipped)
    {
      Debug.LogWarning("Video was skipped - Do NOT reward the player");

    }
    else if (result == ShowResult.Failed)
    {
      Debug.LogError("Video failed to show");
    }
  }

  public void oneTimeOffer()
  {
    if (gts.isFirstTime == true)
    {
      StartCoroutine(CallOneTimeOffer());
      gts.isFirstTime = false;
    }
  }

  public void ShowOTORewardedVideo()
  {
    ShowOptions options = new ShowOptions();
    options.resultCallback = HandleOTOResult;

    Advertisement.Show("rewardedVideo", options);
  }

  void HandleOTOResult(ShowResult result)
  {
    if (result == ShowResult.Finished)
    {
      Debug.Log("Video completed - Offer a reward to the player");
      UpdateCoinCount(80);
      sm.Play(SoundManager.SFX.win2);

    }
    else if (result == ShowResult.Skipped)
    {
      Debug.LogWarning("Video was skipped - Do NOT reward the player");

    }
    else if (result == ShowResult.Failed)
    {
      Debug.LogError("Video failed to show");
    }
  }

  public void doubleMiniGamePrizes()
  {
    ShowOptions options = new ShowOptions();
    options.resultCallback = HandleMGWinnings;

    Advertisement.Show("rewardedVideo", options);
  }

  void HandleMGWinnings(ShowResult result)
  {
    if (result == ShowResult.Finished)
    {
      Debug.Log("Video completed - Offer a reward to the player");
      int x = MiniGameRewards.score * 2;
      //int y = MiniGameRewards.coinCW * 2;
      //int z = MiniGameRewards.coinMG * 2;
      UpdateCoinCount(x);
      //UpdateMiniGameCount(y);
      //UpdateCoinWallCount(z);
      BackToCoinDozer();
      //print(x + y + z);

      BackToCoinDozer();

      sm.Play(SoundManager.SFX.win2);

    }
    else if (result == ShowResult.Skipped)
    {
      Debug.LogWarning("Video was skipped - Do NOT reward the player");

    }
    else if (result == ShowResult.Failed)
    {
      Debug.LogError("Video failed to show");
    }
  }

  public void doubleMGWinnings()
  {
    MiniGameRewards.score = MiniGameRewards.score * 2;
    MiniGameRewards.coinMG = MiniGameRewards.coinMG * 2;
    MiniGameRewards.coinCW = MiniGameRewards.coinCW * 2;


  }

  public void shake()
  {
    GameObject tableTop = GameObject.FindWithTag("Platform");
    tableTop.GetComponent<Animator>().Play("shake");
  }

  public void onShakeClick()
  {
    if (shakeCnt >= 1)
    {
      shake();
      UpdateShakeCnt(-1.0f);
    }
  }

  public void UpdateShakeCnt(float x)
  {
    shakeCnt = shakeCnt + x;
    shakeCounter.text = shakeCnt.ToString("F2");
  }

  public void OnInitializeFailed(InitializationFailureReason error)
  {
    throw new System.NotImplementedException();
  }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
  {
    throw new System.NotImplementedException();
  }

  public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
  {
    throw new System.NotImplementedException();
  }

  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
    throw new System.NotImplementedException();
  }

  public void saveObjectives()
  {
    PlayerPrefsX.SetBool("stars0", stars[0].activeSelf);
    PlayerPrefsX.SetBool("stars1", stars[1].activeSelf);
    PlayerPrefsX.SetBool("stars2", stars[2].activeSelf);
    PlayerPrefsX.SetBool("stars3", stars[3].activeSelf);
    PlayerPrefsX.SetBool("stars4", stars[4].activeSelf);
    PlayerPrefsX.SetBool("stars5", stars[5].activeSelf);
    PlayerPrefsX.SetBool("stars6", stars[6].activeSelf);
    PlayerPrefsX.SetBool("stars7", stars[7].activeSelf);
    PlayerPrefsX.SetBool("stars8", stars[8].activeSelf);
    PlayerPrefsX.SetBool("stars9", stars[9].activeSelf);
    PlayerPrefsX.SetBool("stars10", stars[10].activeSelf);
    PlayerPrefsX.SetBool("stars11", stars[11].activeSelf);

    PlayerPrefsX.SetBool("star1_1", star1_1[0].activeSelf);
    PlayerPrefsX.SetBool("star1_2", star1_2[0].activeSelf);
    PlayerPrefsX.SetBool("star1_3", star1_3[0].activeSelf);
    PlayerPrefsX.SetBool("star2_1", star2_1[0].activeSelf);
    PlayerPrefsX.SetBool("star2_2", star2_2[0].activeSelf);
    PlayerPrefsX.SetBool("star2_3", star2_3[0].activeSelf);
    PlayerPrefsX.SetBool("star3_1", star3_1[0].activeSelf);
    PlayerPrefsX.SetBool("star3_2", star3_2[0].activeSelf);
    PlayerPrefsX.SetBool("star3_3", star3_3[0].activeSelf);
    PlayerPrefsX.SetBool("star4_1", star4_1[0].activeSelf);
    PlayerPrefsX.SetBool("star4_2", star4_2[0].activeSelf);
    PlayerPrefsX.SetBool("star4_3", star4_3[0].activeSelf);

    PlayerPrefsX.SetBool("star1_12", star1_1[1].activeSelf);
    PlayerPrefsX.SetBool("star1_22", star1_2[1].activeSelf);
    PlayerPrefsX.SetBool("star1_32", star1_3[1].activeSelf);
    PlayerPrefsX.SetBool("star2_12", star2_1[1].activeSelf);
    PlayerPrefsX.SetBool("star2_22", star2_2[1].activeSelf);
    PlayerPrefsX.SetBool("star2_32", star2_3[1].activeSelf);
    PlayerPrefsX.SetBool("star3_12", star3_1[1].activeSelf);
    PlayerPrefsX.SetBool("star3_22", star3_2[1].activeSelf);
    PlayerPrefsX.SetBool("star3_32", star3_3[1].activeSelf);
    PlayerPrefsX.SetBool("star4_12", star4_1[1].activeSelf);
    PlayerPrefsX.SetBool("star4_22", star4_2[1].activeSelf);
    PlayerPrefsX.SetBool("star4_32", star4_3[1].activeSelf);

    return;
  }

  public void loadObjectives()
  {
    stars[0].SetActive(PlayerPrefsX.GetBool("stars0"));
    stars[1].SetActive(PlayerPrefsX.GetBool("stars1"));
    stars[2].SetActive(PlayerPrefsX.GetBool("stars2"));
    stars[3].SetActive(PlayerPrefsX.GetBool("stars3"));
    stars[4].SetActive(PlayerPrefsX.GetBool("stars4"));
    stars[5].SetActive(PlayerPrefsX.GetBool("stars5"));
    stars[6].SetActive(PlayerPrefsX.GetBool("stars6"));
    stars[7].SetActive(PlayerPrefsX.GetBool("stars7"));
    stars[8].SetActive(PlayerPrefsX.GetBool("stars8"));
    stars[9].SetActive(PlayerPrefsX.GetBool("stars9"));
    stars[10].SetActive(PlayerPrefsX.GetBool("stars10"));
    stars[11].SetActive(PlayerPrefsX.GetBool("stars11"));

    star1_1[0].SetActive(PlayerPrefsX.GetBool("star1_1"));
    star1_1[1].SetActive(PlayerPrefsX.GetBool("star1_12"));
    star1_2[0].SetActive(PlayerPrefsX.GetBool("star1_2"));
    star1_2[1].SetActive(PlayerPrefsX.GetBool("star1_22"));
    star1_3[0].SetActive(PlayerPrefsX.GetBool("star1_3"));
    star1_3[1].SetActive(PlayerPrefsX.GetBool("star1_32"));

    star2_1[0].SetActive(PlayerPrefsX.GetBool("star2_1"));
    star2_1[1].SetActive(PlayerPrefsX.GetBool("star2_12"));
    star2_2[0].SetActive(PlayerPrefsX.GetBool("star2_2"));
    star2_2[1].SetActive(PlayerPrefsX.GetBool("star2_22"));
    star2_3[0].SetActive(PlayerPrefsX.GetBool("star2_3"));
    star2_3[1].SetActive(PlayerPrefsX.GetBool("star2_32"));

    star3_1[0].SetActive(PlayerPrefsX.GetBool("star3_1"));
    star3_1[1].SetActive(PlayerPrefsX.GetBool("star3_12"));
    star3_2[0].SetActive(PlayerPrefsX.GetBool("star3_2"));
    star3_2[1].SetActive(PlayerPrefsX.GetBool("star3_22"));
    star3_3[0].SetActive(PlayerPrefsX.GetBool("star3_3"));
    star3_3[1].SetActive(PlayerPrefsX.GetBool("star3_32"));

    star4_1[0].SetActive(PlayerPrefsX.GetBool("star4_1"));
    star4_1[1].SetActive(PlayerPrefsX.GetBool("star4_12"));
    star4_2[0].SetActive(PlayerPrefsX.GetBool("star4_2"));
    star4_2[1].SetActive(PlayerPrefsX.GetBool("star4_22"));
    star4_3[0].SetActive(PlayerPrefsX.GetBool("star4_3"));
    star4_3[1].SetActive(PlayerPrefsX.GetBool("star4_32"));

    return;
  }

  int boolToInt(bool val)
  {
    if (val)
      return 1;
    else
      return 0;
  }

  bool intToBool(int val)
  {
    if (val != 0)
      return true;
    else
      return false;
  }

}





