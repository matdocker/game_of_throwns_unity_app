using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{

  // Use this for initialization
  public Transform coin_parent;
  public GameObject normalCoin;
  public Image[] coinImageStack;

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

  private int coinComboCount = 0;

  private GameTimeSpan gts;
  private bool checkCoin = false;

  private int Level = 1;
  private float LevelXP = 50f;
  private float CurrentXP = 0f;

  public GameObject LevelText;

  private float smallCoinXP = 1.5f;
  private float silverCoinXP = 7f;
  private float bigCoinXP = 3f;
  private SoundManager sm;
  public GameObject Castle;
  public bool CastleShake = false;
  private float CastleSpeed = 2f;
  private float CastleShakeRot = -20f;
  private float amount;

  public GameObject XPBar;
  private float originalXPBarWidth;

  public GameObject ShakeBar;
  private float originalShakeBarHeight;
  private int coinDropCount = 0;

  private CoinManager coinManager;
  private Rewardmanager rewardManager;

  public GameObject plusCoin_obj;
  public GameObject coinParticle;
  public GameObject shakeParticle;
  private float shakePercentage;


  public bool isPrizePanelVisible = false;
  private bool mIsTouchEnabled = true;
  public GameObject TapHere;
  private bool isInstiateSpecialCoin = false;
  public GameObject StartPanel;

  public GameObject NoCoinPanel;
  public Sprite coinStackDisble;
  public Sprite coinStackEnable;
  int tier = 0;
  private bool hasBonus = false;


  void Start()
  {
    //		PlayerPrefs.DeleteAll ();
    if (!StartPanel.activeSelf)
    {
      StartPanel.SetActive(true);
      isTouchEnabled = false;
    }
    StartCoroutine(TappedHereBlink());
    rewardManager = Transform.FindObjectOfType<Rewardmanager>();
    gts = Transform.FindObjectOfType<GameTimeSpan>().Instance;
    sm = Transform.FindObjectOfType<SoundManager>();
    coinManager = Transform.FindObjectOfType<CoinManager>();

    coinStackCount = coinStackMaxCount;
    coinStackTimerCounter = coinStackRegenTimeInSec;
    originalShakeBarHeight = ShakeBar.GetComponent<RectTransform>().sizeDelta.y;
    originalXPBarWidth = XPBar.GetComponent<RectTransform>().sizeDelta.x;
    coinRegenTimeInSec = rewardManager.getActivatedBonus("CoinRegen");
    if (gts.isFirstTime)
    {
      coinCount = 20;//20;
      coinTimerCounter = coinRegenTimeInSec;
      coinTextCounter.text = coinCount.ToString();
      coinTextTimer.text = "00:" + coinTimerCounter.ToString();
      UpdateXP(0);
      UpdateShake(0);
    }
    else
    {
      Load();
      checkTime();
    }
    //		coinCount = 1000;
    StartCoroutine(coinRegen());
  }
  public bool isTouchEnabled
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
    GameObject new_coin = coinManager.GetRandomNormalCoin();
    new_coin.transform.position = CoinPos;
    new_coin.transform.rotation = coinManager.baseRotation;
    new_coin.GetComponent<CoinScript>().enabled = true;
    new_coin.GetComponent<CoinScript>().isLanded = false;
    new_coin.rigidbody.freezeRotation = true;
    new_coin.SetActive(true);
    UpdateCoinCount(-1);
    coinStackCount--;
    coinImageStack[coinStackCount].sprite = coinStackDisble;
  }
  void Update()
  {
    if (isTouchEnabled)
    {
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

          if (hit.collider.tag == "TapArea")
          {
            if (coinCount > 0 && coinStackCount > 0)
            {
              InstantiateNormalcoin(new Vector3(hit.point.x, 3f, hit.point.z));
            }
            else
            {
              if (coinCount == 0)
              {
                NoCoinPanel.SetActive(true);
                isTouchEnabled = false;
                sm.Play(SoundManager.SFX.nomore);
              }
            }
          }
        }
      }
    }
  }
  private void checkTime()
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
  IEnumerator coinRegen()
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
    PlayerPrefs.SetInt("TotalCoinCount", coinCount);
    PlayerPrefs.SetInt("TotalCoinTimerCounter", coinTimerCounter);
    Save();
  }
  void OnApplicationPause(bool pauseStatus)
  {
    if (!pauseStatus)
    {
      if (sm && rewardManager)
      {
        if (!sm.isActive && !rewardManager.isActive)
        {
          isTouchEnabled = true;
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
        checkTime();
      }
    }
  }
  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "NormalCoin")
    {
      UpdateCoinCount(1);
      UpdateShake(1);
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
        coincombo.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Coin" + coinComboCount.ToString());
        coincombo.GetComponent<Animator>().Play("CoinComboAnim");
        plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();
        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
      }
      else if (coinComboCount > 3)
      {
        sm.DropCoinClip = 3;
        sm.Play(SoundManager.SFX.drop);
        GameObject coincombo = GameObject.FindGameObjectWithTag("CoinCombo");
        coincombo.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/CoinAttack");
        coincombo.GetComponent<Animator>().Play("CoinComboAnim");
        plusCoin_obj.GetComponent<Text>().text = "+" + coinComboCount.ToString();
        if (!isInstiateSpecialCoin)
        {
          isInstiateSpecialCoin = true;
          //------- Special Coin to be intiated for Coin Attack -----//
          string[] specialCoins = {
            "SilverCoin_I",
            "SilverCoin_G",
            "CoinShower",
            "CoinWall"
          };
          coinManager.InstantiateSpecialCoin(coin_parent, specialCoins, 2);
        }
        if (coinComboCount > 5)
        {
          int r = Random.Range(1, 100);
          if (r <= rewardManager.getActivatedBonus("PrizeChance"))
          {
            rewardManager.InstantiateRandomGift(0);
          }
        }
        StartCoroutine(ClearText(plusCoin_obj.GetComponent<Text>()));
      }
    }
    else if (col.gameObject.tag == "SilverCoin")
    { //gives two additional coins.
      UpdateCoinCount(3); //silverCount + 2 bonus coin
      UpdateShake(1);
      UpdateXP(silverCoinXP + rewardManager.getActivatedBonus("MoreXP"));
      GameObject coincombo = GameObject.FindGameObjectWithTag("OtherSplash");
      coincombo.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/CoinBonus");
      coincombo.GetComponent<Animator>().Play("CoinComboAnim");
      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "XPCoin")
    { //add point to the XP meter with the amount shown.
      if (col.gameObject.name == "XPCoin15")
      {
        UpdateCoinCount(1);
        UpdateShake(1);
        UpdateXP(15f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
      }
      else if (col.gameObject.name == "XPCoin30")
      {
        UpdateCoinCount(1);
        UpdateShake(1);
        UpdateXP(30f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
      }
      else if (col.gameObject.name == "XPCoin50")
      {
        UpdateCoinCount(1);
        UpdateShake(1);
        UpdateXP(50f + rewardManager.getActivatedBonus("MoreXP"));
        col.gameObject.SetActive(false);
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
			UpdateCoinCount(5);
      UpdateShake(1);
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
      GiantCoin.transform.parent = coin_parent;
      GiantCoin.transform.localPosition = new Vector3(0f, 3f, -3f);

      int gChance = Random.Range(1, 101);
      if (gChance < rewardManager.getActivatedBonus("GiftChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
        extraCoin += 1;
        UpdateCoinCount(Random.Range(1, extraCoin));
      }
      tier = Random.Range(1, 101);

      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "summoned_GiantCoin")
    { ///this will shake the machine, dropping more coins.
			UpdateCoinCount(1);
      UpdateShake(1);
      UpdateXP(20f + rewardManager.getActivatedBonus("MoreXP"));
      float iPos = 1.5f;
      float iPos2 = 1.1f;
      int counter = 0;
      for (int i = 0; i < rewardManager.getActivatedBonus("MoreCoins"); i++)
      {
        GameObject new_coin = coinManager.GetRandomNormalCoin();
        new_coin.SetActive(true);
        new_coin.transform.parent = coin_parent;
        new_coin.transform.rotation = coinManager.baseRotation;
        new_coin.transform.localPosition = new Vector3(iPos, 1f, iPos2);

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
      int r = Random.Range(1, 100);
      if (r <= rewardManager.getActivatedBonus("PrizeChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "CoinWall")
    { //walls are raised on both sides to prevent coins and prizes from dropping there
      GameObject sidewalls = GameObject.FindGameObjectWithTag("SideWalls");
      Hashtable hash = new Hashtable();
      hash.Add("x", 0f);
      hash.Add("y", 0.06f);
      hash.Add("z", sidewalls.transform.position.z);
      hash.Add("time", 5f);
      iTween.MoveTo(sidewalls, hash);
      Hashtable hash2 = new Hashtable();
      float dura = (float)rewardManager.getActivatedBonus("WallDuration");
      hash2.Add("delay", dura);
      hash2.Add("x", 0f);
      hash2.Add("y", -4f);
      hash2.Add("z", sidewalls.transform.position.z);
      hash2.Add("time", 7f);
      iTween.MoveTo(sidewalls, hash2);

      int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
        extraCoin += 1;
        UpdateCoinCount(Random.Range(1, extraCoin));
      }
      sm.Play(SoundManager.SFX.wall);
      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "CoinPresent")
    { //this will give you four special coins on top of the platform
      rewardManager.InstantiateRandomGift(0);
      UpdateCoinCount(1);
      UpdateShake(1);
      UpdateXP(10f + rewardManager.getActivatedBonus("MoreXP"));
      if (!isInstiateSpecialCoin)
      {
        isInstiateSpecialCoin = true;
        //------- Special Coin to be intiated for Coin Attack -----//
        string[] specialCoins = {
          "SilverCoin_I",
          "SilverCoin_G",
          "CoinShower",
          "CoinWall",
          "GiantCoin",
          "CoinPresent"
        };
        coinManager.InstantiateSpecialCoin(coin_parent, specialCoins, 4);
      }
      int r = Random.Range(1, 100);
      if (r <= rewardManager.getActivatedBonus("PrizeChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      col.gameObject.SetActive(false);
    }
    else if (col.gameObject.tag == "CoinShower")
    { //several coins will be dropped on the moving slide
      float iPos = 1.5f;
      for (int i = 0; i < 4; i++)
      {
        GameObject new_coin = coinManager.GetRandomNormalCoin();
        new_coin.SetActive(true);
        new_coin.transform.parent = coin_parent;
        new_coin.transform.rotation = coinManager.baseRotation;
        new_coin.transform.localPosition = new Vector3(iPos, 1f, Random.Range(-1f, -1.5f));
        iPos -= 1f;
      }
      int extraCoin = rewardManager.getActivatedBonus("ExtraCoin");
      if (extraCoin > 0)
      {
        extraCoin += 1;
        UpdateCoinCount(Random.Range(1, extraCoin));
      }
      col.gameObject.SetActive(false);

    }
    else if (col.gameObject.tag == "Gift")
    {
      rewardManager.AddToCollection(col.gameObject.name);
      Debug.Log("New Gift: " + col.gameObject.name);
      rewardManager.ShowRewardPopup(col.gameObject.name);
      UpdateCoinCount(1);
      UpdateShake(1);
      UpdateXP(20f + rewardManager.getActivatedBonus("MoreXP"));
      sm.Play(SoundManager.SFX.prize);
      col.gameObject.SetActive(false);
    }
  }
  private IEnumerator CoinTimer()
  {
    yield return new WaitForSeconds(3);
    coinComboCount = 0;
    checkCoin = false;
    isInstiateSpecialCoin = false;
  }
  public void UpdateCoinCount(int plusCoin)
  {
    //		if(coinCount < 40) {
    if (plusCoin > 0)
    {
      if (!coinParticle.activeSelf && !isPrizePanelVisible && isTouchEnabled)
      {
        coinParticle.SetActive(true);
      }
      StartCoroutine(StartParticle());
    }
    coinCount += plusCoin;
    coinTextCounter.text = coinCount.ToString();
    //		Debug.Log("PLus " + plusCoin.ToString());
    //		}
  }
  public void ShakeCastle()
  {
    Hashtable ht1 = new Hashtable();
    ht1.Add("z", 0.25f);
    ht1.Add("x", 0.15f);
    ht1.Add("y", 0.25f);
    ht1.Add("time", 1.0f);
    iTween.ShakePosition(Castle, ht1);
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
      Level++;
      LevelXP += (LevelXP * 0.65f);
      LevelText.GetComponent<Text>().text = Level.ToString();

      coinManager.InstantiateRandomSpecialCoin();
      int r = Random.Range(1, 100);
      if (r <= rewardManager.getActivatedBonus("PrizeChance"))
      {
        rewardManager.InstantiateRandomGift(0);
      }
      GameObject levelup = GameObject.FindGameObjectWithTag("LevelUpSplash");
      levelup.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Levelup");
      levelup.GetComponent<Animator>().Play("CoinComboAnim");
    }

    float percentage = CurrentXP / LevelXP;

    Vector2 _size = XPBar.GetComponent<RectTransform>().sizeDelta;
    _size.x = originalXPBarWidth * percentage;
    XPBar.GetComponent<RectTransform>().sizeDelta = _size;
  }
  private void UpdateShake(int plusCoinCount)
  {
    coinDropCount += plusCoinCount;
    if (coinDropCount <= 0)
    {
      coinDropCount = 0;
    }
    shakePercentage = (float)coinDropCount / 100f;
    if (shakePercentage > 1f)
    {
      shakePercentage = 1f;
    }
    if (shakePercentage >= 0.3f)
    {
      if (!shakeParticle.activeSelf && !isPrizePanelVisible && isTouchEnabled)
      {
        shakeParticle.SetActive(true);
      }
      StartCoroutine(shakeParticleDisable());
    }
    Vector2 _size = ShakeBar.GetComponent<RectTransform>().sizeDelta;
    _size.y = originalShakeBarHeight * shakePercentage;
    ShakeBar.GetComponent<RectTransform>().sizeDelta = _size;
    if (plusCoinCount == 1)
    {
      int rr = coinDropCount % (Random.Range(6, 15));
      if (rr == 0)
      {
        int rx = Random.Range(1, 100);
        if (rx <= rewardManager.getActivatedBonus("PrizeChance"))
        {
          rewardManager.InstantiateRandomGift(0);
        }
        else
        {
          coinManager.InstantiateRandomSpecialCoin();
        }
      }
    }
  }
  IEnumerator shakeParticleDisable()
  {
    yield return new WaitForSeconds(20f);
    if (shakeParticle.activeSelf && coinCount > 0)
    {
      shakeParticle.SetActive(false);
    }
  }
  public void UsedShake()
  {
    if (shakeParticle.activeSelf)
    {
      shakeParticle.SetActive(false);
    }
    if (shakePercentage >= 0.3f && shakePercentage < 0.6f)
    {
      Hashtable ht1 = new Hashtable();
      ht1.Add("z", 0.1f);
      ht1.Add("x", 0.05f);
      ht1.Add("y", 0.1f);
      ht1.Add("time", 1.0f);
      iTween.ShakePosition(Castle, ht1);
      shakePercentage -= 0.3f;
      coinDropCount -= (int)((coinDropCount * 0.3f) * 100f);
      if (shakeParticle.activeSelf)
      {
        shakeParticle.SetActive(false);
      }
      UpdateShake(0);
    }
    else if (shakePercentage >= 0.6f && shakePercentage < 1f)
    {
      Hashtable ht1 = new Hashtable();
      ht1.Add("z", 0.15F);
      ht1.Add("x", 0.1f);
      ht1.Add("y", 0.15F);
      ht1.Add("time", 1.0f);
      iTween.ShakePosition(Castle, ht1);
      shakePercentage -= 0.6f;
      coinDropCount -= (int)((coinDropCount * 0.6f) * 100f);
      if (shakeParticle.activeSelf)
      {
        shakeParticle.SetActive(false);
      }
      UpdateShake(0);
    }
    else if (shakePercentage >= 1f)
    {
      Hashtable ht1 = new Hashtable();
      ht1.Add("z", 0.2F);
      ht1.Add("x", 0.15F);
      ht1.Add("y", 0.2F);
      ht1.Add("time", 1.0f);
      iTween.ShakePosition(Castle, ht1);
      shakePercentage = 0f;
      coinDropCount = 0;
      if (shakeParticle.activeSelf)
      {
        shakeParticle.SetActive(false);
      }
      UpdateShake(0);
    }
  }
  public void Save()
  {
    coinManager.SaveAllCoins();
    rewardManager.SaveAllGift();
    rewardManager.SaveCollectedGift();

    sm.Save();

    if (Level == 1 || Level == 0)
    {
      Level = 1;
      LevelXP = 50f;
    }
    PlayerPrefs.SetFloat("CurrentXP", CurrentXP);
    PlayerPrefs.SetInt("CurrentLevel", Level);
    PlayerPrefs.SetFloat("LevelXP", LevelXP);
    PlayerPrefs.SetInt("CoinDropCount", coinDropCount);
    PlayerPrefsX.SetBool("hasBonus", false);
  }
  private void Load()
  {
    Level = PlayerPrefs.GetInt("CurrentLevel");
    LevelXP = PlayerPrefs.GetFloat("LevelXP");
    LevelText.GetComponent<Text>().text = Level.ToString();
    UpdateXP(PlayerPrefs.GetFloat("CurrentXP"));
    UpdateShake(PlayerPrefs.GetInt("CoinDropCount"));
    hasBonus = PlayerPrefsX.GetBool("hasBonus");
  }
}
/*
Notes

level up, drop coin to platform
coinShower - 4 or more
CoinAtack coin - 2 Random Special Coin any

 */
