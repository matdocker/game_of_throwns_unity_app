using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Rewardmanager : MonoBehaviour
{

  // Use this for initialization
  public GameObject[] Dragons;
  public GameObject[] Swords;
  public GameObject[] Shields;
  public GameObject[] Chests;
  public GameObject[] Staffs;
  public GameObject[] Crowns;

  private List<GameObject> Dragon_1 = new List<GameObject>();
  private List<GameObject> Dragon_2 = new List<GameObject>();
  private List<GameObject> Dragon_3 = new List<GameObject>();
  private List<GameObject> Dragon_4 = new List<GameObject>();
  private List<GameObject> Sword_1 = new List<GameObject>();
  private List<GameObject> Sword_2 = new List<GameObject>();
  private List<GameObject> Sword_3 = new List<GameObject>();
  private List<GameObject> Sword_4 = new List<GameObject>();
  private List<GameObject> Shield_1 = new List<GameObject>();
  private List<GameObject> Shield_2 = new List<GameObject>();
  private List<GameObject> Shield_3 = new List<GameObject>();
  private List<GameObject> Shield_4 = new List<GameObject>();
  private List<GameObject> Chest_1 = new List<GameObject>();
  private List<GameObject> Chest_2 = new List<GameObject>();
  private List<GameObject> Chest_3 = new List<GameObject>();
  private List<GameObject> Chest_4 = new List<GameObject>();
  private List<GameObject> Staff_1 = new List<GameObject>();
  private List<GameObject> Staff_2 = new List<GameObject>();
  private List<GameObject> Staff_3 = new List<GameObject>();
  private List<GameObject> Staff_4 = new List<GameObject>();
  private List<GameObject> Crown_1 = new List<GameObject>();
  private List<GameObject> Crown_2 = new List<GameObject>();
  private List<GameObject> Crown_3 = new List<GameObject>();
  private List<GameObject> Crown_4 = new List<GameObject>();

  public List<string> collectedGifts = new List<string>();
  public List<GameObject> giftsOnPlatform = new List<GameObject>();
  GameScene gamescene;
  public Transform AvatarParent;
  public GameObject NewCoinParticle;
  public Text itemDiscription;
  public Text bonusDiscription;
  public Button sellBtn;
  private string CurrentItemSelected = "";
  private string itemSub = "";
  public GameObject prizePanel;
  public Button prizeBtn;
  public GameObject[] PrizeButtons;
  private bool m_isActive = false;
  public Button back;
  private string prevReward = "";
  public Sprite[] rewadtexts;
  public GameObject rewardpoup;
  public Image rewardTextCont;
  public bool isRewardPopupVisible = false;
  public Button close;
  private bool adsVisible = false;
  public Transform gift_parent;
  private bool isInstantiating = false;
  private bool isGiftInstantiating = false;
  private float timer = 0f;
  private SoundManager sm;
  private int GiftCountOnQue = 0;
  private CoinManager coinManager;
  //		private AdGoogle admob;
  void Start()
  {
    //				admob = FindObjectOfType<AdGoogle> ();
    sm = Transform.FindObjectOfType<SoundManager>();
    coinManager = FindObjectOfType<CoinManager>();
    ActivateBonus("MoreXP", 10);
    ActivateBonus("MoreCoins", 20);
    ActivateBonus("CoinRegen", 40);
    ActivateBonus("WallDuration", 25);
    ActivateBonus("PrizeChance", 50);
    ActivateBonus("ExtraCoin", 10);

    gamescene = Transform.FindObjectOfType<GameScene>();
    prizeBtn.onClick.AddListener(ShowPrizes);
    back.onClick.AddListener(HidePrizes);
    sellBtn.onClick.AddListener(SellGift);
    close.onClick.AddListener(HideRewardPoup);

    // =----------- dragons ------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Dragons[0]);
      obj.name = "Dragon_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Dragon_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Dragons[1]);
      obj.name = "Dragon_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Dragon_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Dragons[2]);
      obj.name = "Dragon_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Dragon_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Dragons[3]);
      obj.name = "Dragon_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Dragon_4.Add(obj);
    }

    // ----------  Crowns -----------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Crowns[0]);
      obj.name = "Crown_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Crown_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Crowns[1]);
      obj.name = "Crown_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Crown_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Crowns[2]);
      obj.name = "Crown_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Crown_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Crowns[3]);
      obj.name = "Crown_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Crown_4.Add(obj);
    }
    //---------- SWords ------------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Swords[0]);
      obj.name = "Sword_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Sword_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Swords[1]);
      obj.name = "Sword_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Sword_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Swords[2]);
      obj.name = "Sword_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Sword_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Swords[3]);
      obj.name = "Sword_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Sword_4.Add(obj);
    }

    // ----------- Staff ----------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Staffs[0]);
      obj.name = "Staff_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Staff_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Staffs[1]);
      obj.name = "Staff_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Staff_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Staffs[2]);
      obj.name = "Staff_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Staff_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Staffs[3]);
      obj.name = "Staff_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Staff_4.Add(obj);
    }
    // -------------- Sheilds ---------------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Shields[0]);
      obj.name = "Shield_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Shield_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Shields[1]);
      obj.name = "Shield_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Shield_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Shields[2]);
      obj.name = "Shield_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Shield_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Shields[3]);
      obj.name = "Shield_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Shield_4.Add(obj);
    }
    // --------   Chest ---------------
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Chests[0]);
      obj.name = "Chest_1";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Chest_1.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Chests[1]);
      obj.name = "Chest_2";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Chest_2.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Chests[2]);
      obj.name = "Chest_3";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Chest_3.Add(obj);
    }
    for (int i = 0; i < 3; i++)
    {
      GameObject obj = (GameObject)Instantiate(Chests[3]);
      obj.name = "Chest_4";
      obj.transform.parent = gift_parent;
      obj.SetActive(false);
      Chest_4.Add(obj);
    }
    Invoke("invokeLoad", 0.25f);
  }
  void invokeLoad()
  {
    LoadAllGift();
    LoadCollectedGifts();
  }
  void Awake()
  {


  }
  // Update is called once per frame
  void Update()
  {

  }
  private GameObject GetInActiveObject(List<GameObject> objects)
  {
    for (int i = 0; i < objects.Count; i++)
    {
      if (!objects[i].activeInHierarchy)
      {
        return objects[i];
      }
    }
    return null;
  }
  public GameObject GetGift(string gNameS)
  {
    string gName = gNameS.ToLower();
    if (gName.Equals("dragon_1"))
    {
      return GetInActiveObject(Dragon_1);
    }
    else if (gName.Equals("dragon_2"))
    {
      return GetInActiveObject(Dragon_2);
    }
    else if (gName.Equals("dragon_3"))
    {
      return GetInActiveObject(Dragon_3);
    }
    else if (gName.Equals("dragon_4"))
    {
      return GetInActiveObject(Dragon_4);
    }
    else if (gName.Equals("sword_1"))
    {
      return GetInActiveObject(Sword_1);
    }
    else if (gName.Equals("sword_2"))
    {
      return GetInActiveObject(Sword_2);
    }
    else if (gName.Equals("sword_3"))
    {
      return GetInActiveObject(Sword_3);
    }
    else if (gName.Equals("sword_4"))
    {
      return GetInActiveObject(Sword_4);
    }
    else if (gName.Equals("crown_1"))
    {
      return GetInActiveObject(Crown_1);
    }
    else if (gName.Equals("crown_2"))
    {
      return GetInActiveObject(Crown_2);
    }
    else if (gName.Equals("crown_3"))
    {
      return GetInActiveObject(Crown_3);
    }
    else if (gName.Equals("crown_4"))
    {
      return GetInActiveObject(Crown_4);
    }
    else if (gName.Equals("shield_1"))
    {
      return GetInActiveObject(Shield_1);
    }
    else if (gName.Equals("shield_2"))
    {
      return GetInActiveObject(Shield_2);
    }
    else if (gName.Equals("shield_3"))
    {
      return GetInActiveObject(Shield_3);
    }
    else if (gName.Equals("shield_4"))
    {
      return GetInActiveObject(Shield_4);
    }
    else if (gName.Equals("chest_1"))
    {
      return GetInActiveObject(Chest_1);
    }
    else if (gName.Equals("chest_2"))
    {
      return GetInActiveObject(Chest_2);
    }
    else if (gName.Equals("chest_3"))
    {
      return GetInActiveObject(Chest_3);
    }
    else if (gName.Equals("chest_4"))
    {
      return GetInActiveObject(Chest_4);
    }
    else if (gName.Equals("staff_1"))
    {
      return GetInActiveObject(Staff_1);
    }
    else if (gName.Equals("staff_2"))
    {
      return GetInActiveObject(Staff_2);
    }
    else if (gName.Equals("staff_3"))
    {
      return GetInActiveObject(Staff_3);
    }
    else if (gName.Equals("staff_4"))
    {
      return GetInActiveObject(Staff_4);
    }
    /*
		if(gName.ToLower().Contains("dragon")) {
			for(int i = 0 ; i <Dragons.Length;i++) {
				if(Dragons[i].name == gName) {
					return Dragons[i];
				}
			}
		} else if(gName.ToLower().Contains("sword")) {
			for(int i = 0 ; i <Swords.Length;i++) {
				if(Swords[i].name == gName) {
					return Swords[i];
				}
			}
		} else if(gName.ToLower().Contains("shield")) {
			for(int i = 0 ; i <Shields.Length;i++) {
				if(Shields[i].name == gName) {
					return Shields[i];
				}
			}
		} else if(gName.ToLower().Contains("chest")) {
			for(int i = 0 ; i <Chests.Length;i++) {
				if(Chests[i].name == gName) {
					return Chests[i];
				}
			}
		} else if(gName.ToLower().Contains("crown")) {
			for(int i = 0 ; i <Crowns.Length;i++) {
				if(Crowns[i].name == gName) {
					return Crowns[i];
				}
			}
		} else if(gName.ToLower().Contains("staff")) {
			for(int i = 0 ; i <Staffs.Length;i++) {
				if(Staffs[i].name == gName) {
					return Staffs[i];
				}
			}
		}
*/
    return null;
  }
  public bool isActive
  {
    set { m_isActive = value; }
    get { return m_isActive; }
  }
  public void ShowPrizes()
  {
    sm.ButtonClicked();
    isActive = true;
    prizePanel.SetActive(true);
    gamescene.isPrizePanelVisible = true;
    gamescene.isTouchEnabled = false;
    if (gamescene.shakeParticle.activeSelf)
    {
      gamescene.shakeParticle.SetActive(false);
    }
    if (gamescene.coinParticle.activeSelf)
    {
      gamescene.coinParticle.SetActive(false);
    }
    if (PrizeButtons.Length == 0)
    { // Add Listener to buttons once only 
      PrizeButtons = GameObject.FindGameObjectsWithTag("PrizeButton");
      for (int i = 0; i < PrizeButtons.Length; i++)
      {
        Button btn = PrizeButtons[i].GetComponent<Button>();
        if (PrizeButtons[i].transform.childCount > 0)
        {
          GameObject d = PrizeButtons[i].transform.GetChild(0).gameObject;
          if (!d.name.ToLower().Contains("bonus"))
          {
            PrizeButtons[i].GetComponent<Text>().text = "x" + getItemCount(d.name);
          }
          btn.onClick.AddListener(() => { ChangeAvatarTo(d); });
        }
      }
    }
    else
    {
      for (int i = 0; i < PrizeButtons.Length; i++)
      {
        GameObject d = PrizeButtons[i].transform.GetChild(0).gameObject;
        if (!d.name.ToLower().Contains("bonus"))
        {
          PrizeButtons[i].GetComponent<Text>().text = "x" + getItemCount(d.name);
        }
      }
    }
    //		admob.HideAd ();
  }

  public void HidePrizes()
  {
    sm.ButtonClicked();
    prizePanel.SetActive(false);
    gamescene.isPrizePanelVisible = false;
    gamescene.isTouchEnabled = true;
    //		admob.ShowAd ();
    isActive = false;
  }
  public void ChangeAvatarTo(GameObject pObj)
  {
    sm.ButtonClicked();
    if (!pObj.name.ToLower().Contains("bonus"))
    {
      if (!sellBtn.gameObject.activeSelf)
      {
        sellBtn.gameObject.SetActive(true);
      }
      if (bonusDiscription.gameObject.activeSelf)
      {
        bonusDiscription.gameObject.SetActive(false);
      }
      if (AvatarParent.childCount > 0)
      {
        DestroyObject(AvatarParent.GetChild(0).gameObject);
      }
      GameObject newAvatar = Instantiate(pObj) as GameObject;
      newAvatar.transform.parent = AvatarParent;
      newAvatar.transform.localScale = pObj.transform.localScale;
      newAvatar.name = pObj.name;
      itemSub = "item";
      CurrentItemSelected = pObj.name;
      if (pObj.name.ToLower().Contains("sword"))
      {
        newAvatar.transform.localPosition = new Vector3(20, 30, -193);
        itemSub = "Sword";
      }
      else if (pObj.name.ToLower().Contains("dragon"))
      {
        newAvatar.transform.localPosition = new Vector3(0, -27, -193);
        itemSub = "Dragon";
      }
      else if (pObj.name.ToLower().Contains("staff"))
      {
        newAvatar.transform.localPosition = new Vector3(-40, -80, -193);
        itemSub = "Staff";
      }
      else if (pObj.name.ToLower().Contains("crown"))
      {
        newAvatar.transform.localPosition = new Vector3(-0, -40, -193);
        itemSub = "Crown";
      }
      else if (pObj.name.ToLower().Contains("chest"))
      {
        newAvatar.transform.localPosition = new Vector3(0, -25, -193);
        itemSub = "Chest";
      }
      else if (pObj.name.ToLower().Contains("shield"))
      {
        newAvatar.transform.localPosition = new Vector3(0, 0, -193);
        itemSub = "Shield";
      }
      int coinValue = GetCoinValue(pObj.name);
      if (coinValue > 0)
      {
        itemDiscription.text = "Sell " + itemSub + " for " + coinValue.ToString() + " coins";
        sellBtn.interactable = true;
      }
      else
      {
        itemDiscription.text = "Sell " + itemSub + " for " + coinValue.ToString() + " coins";
        sellBtn.interactable = false;
      }
    }
    else
    {
      if (AvatarParent.childCount > 0)
      {
        DestroyObject(AvatarParent.GetChild(0).gameObject);
      }
      if (sellBtn.gameObject.activeSelf)
      {
        sellBtn.gameObject.SetActive(false);
      }
      itemDiscription.text = "";
      if (!bonusDiscription.gameObject.activeSelf)
      {
        bonusDiscription.gameObject.SetActive(true);
      }
      Bonus(pObj.name);
    }
  }
  private bool isCanGetBonus(string pBonusSuffix)
  {
    int count = 0;
    for (int i = 1; i < 5; i++)
    {
      if (getItemCount(pBonusSuffix + i.ToString()) > 0)
      {
        count++;
      }
    }
    if (count == 4)
    {
      return true;
    }
    return false;
  }
  public void Bonus(string pBonus)
  {
    if (pBonus.ToLower().Contains("sword"))
    {
      bonusDiscription.text = "Special Coins and Prizes give more XP";
      if (isCanGetBonus("Sword_"))
      {
        RemoveToCollection("Sword_");
        PlayerPrefs.SetInt("MoreXP", 5);
      }
    }
    else if (pBonus.ToLower().Contains("dragon"))
    {
      bonusDiscription.text = "More new Coins will apear after a Giant Coin drops.";
      if (isCanGetBonus("Dragon_"))
      {
        RemoveToCollection("Dragon_");
        PlayerPrefs.SetInt("MoreCoins", getActivatedBonus("MoreCoins") + 2);
      }
    }
    else if (pBonus.ToLower().Contains("staff"))
    {
      bonusDiscription.text = "Coin regenerate faster.";
      if (isCanGetBonus("Staff_"))
      {
        RemoveToCollection("Staff_");
        PlayerPrefs.SetInt("CoinRegen", getActivatedBonus("CoinRegen") - 2);
      }
    }
    else if (pBonus.ToLower().Contains("crown"))
    {
      bonusDiscription.text = "Coin Walls stay up longer";
      if (isCanGetBonus("Crown_"))
      {
        RemoveToCollection("Crown_");
        PlayerPrefs.SetInt("WallDuration", getActivatedBonus("WallDuration") + 5);
      }
    }
    else if (pBonus.ToLower().Contains("chest"))
    {
      bonusDiscription.text = "Sometimes a new prize will appear whenever you collect a Giant Coin.";
      if (isCanGetBonus("Chest_"))
      {
        RemoveToCollection("Chest_");
        PlayerPrefs.SetInt("PrizeChance", getActivatedBonus("PrizeChance") + 10);
      }
    }
    else if (pBonus.ToLower().Contains("shield"))
    {
      bonusDiscription.text = " Get extra Coins whenever a Special Coin is collected.";
      if (isCanGetBonus("Shield_"))
      {
        RemoveToCollection("Shield_");
        PlayerPrefs.SetInt("ExtraCoin", getActivatedBonus("ExtraCoin") + 2);
      }
    }
  }
  private void ActivateBonus(string pBonus, int val)
  {
    if (!PlayerPrefs.HasKey(pBonus))
    {
      PlayerPrefs.SetInt(pBonus, val);
    }
  }
  public int getActivatedBonus(string pBonus)
  {
    return PlayerPrefs.GetInt(pBonus);
  }
  public void InstantiateGift(string pBaseName)
  {
    if (!isInstantiating)
    {
      isInstantiating = true;
      int r = Random.Range(1, 3);
      float xPos = Random.Range(-1.75f, 1.8f);
      Vector3 iPos = new Vector3(xPos, 3.5f, -3.3f);
      string newGiftName = pBaseName + r.ToString();
      GameObject newGift = GetGift(newGiftName); ;
      if (newGift)
      {
        newGift.transform.position = iPos;
        newGift.name = newGiftName;
        newGift.SetActive(true);
      }
      StartCoroutine(enableIns());
    }
    else
    {
      StartCoroutine(WaitFor(pBaseName));
    }
  }
  IEnumerator enableIns()
  {
    yield return new WaitForSeconds(0.3f);
    isInstantiating = false;
  }
  IEnumerator WaitFor(string pName)
  {
    yield return new WaitForSeconds(0.1f);
    InstantiateGift(pName);
  }
  int GiftCountOnPlatform(string pName)
  {
    int count = 0;
    foreach (GameObject obj in giftsOnPlatform)
    {
      if (obj)
      {
        if (obj.name.Equals(pName))
        {
          count++;
        }
      }
    }
    return count;
  }
  public void ShowRewardPopup(string pName)
  {
    if (!rewardpoup.activeSelf)
    {
      rewardpoup.SetActive(true);
    }
    isRewardPopupVisible = true;

    int i = -1;
    if (pName.Contains("Dragon_"))
    { //"Shield_","Dragon_","Sword_","Staff_","Shield_","Crown_","Chest"};
      rewardTextCont.sprite = rewadtexts[0];
      i = 0;
    }
    else if (pName.Contains("Shield_"))
    {
      rewardTextCont.sprite = rewadtexts[1];
      i = 1;
    }
    else if (pName.Contains("Sword_"))
    {
      rewardTextCont.sprite = rewadtexts[2];
      i = 2;
    }
    else if (pName.Contains("Chest_"))
    {
      rewardTextCont.sprite = rewadtexts[3];
      i = 3;
    }
    else if (pName.Contains("Crown_"))
    {
      rewardTextCont.sprite = rewadtexts[4];
      i = 4;
    }
    else if (pName.Contains("Staff_"))
    {
      rewardTextCont.sprite = rewadtexts[5];
      i = 5;
    }
    if (IsInvoking("ShowAdd"))
    {
      CancelInvoke("ShowAdd");
    }
    Invoke("ShowAdd", 3f);
  }
  void ShowAdd()
  {
    if (!adsVisible)
    {
      adsVisible = true;
      AdChartboost cb = Transform.FindObjectOfType<AdChartboost>();
      cb.RequestFromRewardManager = true;
      cb.ShowInterstitial();
    }
  }
  public void HideRewardPoup()
  {
    if (rewardpoup.activeSelf)
    {
      sm.ButtonClicked();
      rewardpoup.SetActive(false);
      isRewardPopupVisible = false;
      adsVisible = false;
    }
  }
  public void InstantiateRandomGift(int i)
  {
    if (GiftCountOnQue < 3)
    {
      if (i == 0)
      {
        GiftCountOnQue++;
        if (!IsInvoking("IntantiateFunc"))
        {
          Invoke("IntantiateFunc", 0.5f);
        }
      }
      else
      {
        Invoke("IntantiateFunc", 0.5f);
      }
    }
  }
  void IntantiateFunc()
  {
    if (GiftCountOnQue > 0)
    {
      isGiftInstantiating = true;
      int r = Random.Range(1, 5);
      string[] giftNames = { "Dragon_", "Shield_", "Dragon_", "Sword_", "Staff_", "Crown_", "Chest_" };
      Vector3 iPos = new Vector3(Random.Range(-1.5f, 0.6f), 5f, -Random.Range(2f, 5.4f));
      string newReward = giftNames[Random.Range(0, giftNames.Length)];
      while (prevReward.Equals(newReward))
      {
        if (GiftCountOnPlatform(newReward) < 3)
        {
          newReward = giftNames[Random.Range(0, giftNames.Length)];
        }
      }
      prevReward = newReward;
      string newGiftName = newReward + r.ToString();
      GameObject newGift = GetGift(newGiftName);
      if (newGift)
      {
        newGift.transform.position = iPos;
        newGift.name = newGiftName;
        newGift.SetActive(true);
        giftsOnPlatform.Add(newGift);
      }
      GiftCountOnQue--;
      InstantiateRandomGift(1);
    }
    else
    {
      isGiftInstantiating = false;
      InstantiateRandomGift(1);
    }
  }
  public void SaveGifts()
  {
    GameObject[] gifts_array = GameObject.FindGameObjectsWithTag("Gift");
    if (gifts_array.Length > 0)
    {
      Quaternion[] gift_rotation = new Quaternion[gifts_array.Length];
      Vector3[] gift_position = new Vector3[gifts_array.Length];
      string[] gift_names = new string[gifts_array.Length];

      for (int i = 0; i < gifts_array.Length; i++)
      {
        gift_rotation[i] = gifts_array[i].transform.rotation;
        gift_position[i] = gifts_array[i].transform.position;
        gift_names[i] = gifts_array[i].name;
      }

      PlayerPrefsX.SetQuaternionArray("GiftQuaternion", gift_rotation);
      PlayerPrefsX.SetVector3Array("GiftPosition", gift_position);
      PlayerPrefsX.SetStringArray("GiftName", gift_names);
      PlayerPrefs.SetInt("GiftCount", gifts_array.Length);
    }
    else
    {
      PlayerPrefs.SetInt("GiftCount", 0);
    }
  }
  public void LoadGifts()
  {
    if (PlayerPrefs.HasKey("GiftCount"))
    {

      int giftCount = PlayerPrefs.GetInt("GiftCount");
      if (giftCount > 0)
      {
        Quaternion[] gift_rotation = PlayerPrefsX.GetQuaternionArray("GiftQuaternion");
        Vector3[] gift_position = PlayerPrefsX.GetVector3Array("GiftPosition");
        string[] gift_name = PlayerPrefsX.GetStringArray("GiftName");
        for (int i = 0; i < giftCount; i++)
        {
          GameObject newGift = GetGift(gift_name[i]);
          Debug.Log(" sdasd     asdsadsdss          " + gift_name[i]);
          newGift.SetActive(true);
          giftsOnPlatform.Add(newGift);
        }
      }
    }
  }
  public void RemoveToCollection(string suffix)
  {
    int counter = 1;
    if (collectedGifts.Count > 0)
    {
      string[] arr = collectedGifts.ToArray();
      for (int i = 0; i < arr.Length; i++)
      {
        string s = suffix + counter.ToString();
        if (arr[i].ToLower().Equals(s.ToLower()) && counter <= 4)
        {
          collectedGifts.Remove(arr[i]);
          counter++;
        }
      }
      for (int i = 0; i < PrizeButtons.Length; i++)
      {
        GameObject d = PrizeButtons[i].transform.GetChild(0).gameObject;
        if (d.name.ToLower().Contains(suffix.ToLower()))
        {
          PrizeButtons[i].GetComponent<Text>().text = "x" + getItemCount(d.name);
        }
      }
    }
  }
  public void SaveCollectedGift()
  {
    if (collectedGifts.Count > 0)
    {
      string[] collectedGiftsArr = collectedGifts.ToArray();
      PlayerPrefsX.SetStringArray("CollectedGifts", collectedGiftsArr);
      PlayerPrefs.SetInt("CollectedGiftCount", collectedGiftsArr.Length);
    }
    else
    {
      PlayerPrefs.SetInt("CollectedGiftCount", 0);
    }
  }
  public void LoadCollectedGifts()
  {
    if (PlayerPrefs.HasKey("CollectedGiftCount"))
    {
      if (PlayerPrefs.GetInt("CollectedGiftCount") > 0)
      {
        string[] collectedGiftsArr = PlayerPrefsX.GetStringArray("CollectedGifts");
        collectedGifts = collectedGiftsArr.OrderBy(t => t).ToList();
      }
    }
  }
  public int GetCoinValue(string itemName)
  {
    if (collectedGifts.Contains(itemName))
    {
      if (itemName.ToLower().Contains("dragon"))
      {
        return 6 * getItemCount(itemName);
      }
      else if (itemName.ToLower().Contains("shield"))
      {
        return 7 * getItemCount(itemName);
      }
      else if (itemName.ToLower().Contains("sword"))
      {
        return 8 * getItemCount(itemName);
      }
      else if (itemName.ToLower().Contains("crown"))
      {
        return 9 * getItemCount(itemName);
      }
      else if (itemName.ToLower().Contains("staff"))
      {
        return 10 * getItemCount(itemName);
      }
      else if (itemName.ToLower().Contains("chest"))
      {
        return 11 * getItemCount(itemName);
      }
    }
    return 0;
  }
  private int getItemCount(string pName)
  {
    int count = 0;
    foreach (string item in collectedGifts)
    {
      if (item.ToLower().Equals(pName.ToLower()))
      {
        count++;
      }
    }
    return count;
  }
  public void SellGift()
  {
    sm.ButtonClicked();
    if (collectedGifts.Contains(CurrentItemSelected))
    {
      gamescene.UpdateCoinCount(GetCoinValue(CurrentItemSelected));
      string[] arr = collectedGifts.ToArray();
      foreach (string s in arr)
      {
        if (CurrentItemSelected.Equals(s))
        {
          collectedGifts.Remove(s);
        }
      }
      for (int i = 0; i < PrizeButtons.Length; i++)
      {
        GameObject d = PrizeButtons[i].transform.GetChild(0).gameObject;
        if (!d.name.ToLower().Contains("bonus"))
        {
          PrizeButtons[i].GetComponent<Text>().text = "x" + getItemCount(d.name);
        }
      }
      sellBtn.interactable = false;
      itemDiscription.text = "Collect " + itemSub + " to get special prizes";
      itemSub = "";
      CurrentItemSelected = "none";
    }
  }
  public void AddToCollection(string giftName)
  {
    collectedGifts.Add(giftName);
  }
  public int GetCoinAtGift(string giftName)
  {
    if (collectedGifts.Count > 0)
    {
      collectedGifts.Remove(giftName);
      string gift = giftName.ToLower();
      if (gift.Contains("sword") || gift.Contains("shield") || gift.Contains("dragon"))
      {
        SaveCollectedGift();
        return 6;
      }
      else if (gift.Contains("crown") || gift.Contains("staff") || gift.Contains("chest"))
      {
        SaveCollectedGift();
        return 7;
      }
      else if (gift.Contains("umbrella") || gift.Contains("whistle") || gift.Contains("yoyo"))
      {
        SaveCollectedGift();
        return 8;
      }
    }
    return 0;
  }
  public void SaveAllGift()
  {
    SaveGiftsRev2(Dragon_1, "Dragon_1");
    SaveGiftsRev2(Dragon_2, "Dragon_2");
    SaveGiftsRev2(Dragon_3, "Dragon_3");
    SaveGiftsRev2(Dragon_4, "Dragon_4");

    SaveGiftsRev2(Sword_1, "Sword_1");
    SaveGiftsRev2(Sword_2, "Sword_2");
    SaveGiftsRev2(Sword_3, "Sword_3");
    SaveGiftsRev2(Sword_4, "Sword_4");

    SaveGiftsRev2(Crown_1, "Crown_1");
    SaveGiftsRev2(Crown_2, "Crown_2");
    SaveGiftsRev2(Crown_3, "Crown_3");
    SaveGiftsRev2(Crown_4, "Crown_4");

    SaveGiftsRev2(Shield_1, "Shield_1");
    SaveGiftsRev2(Shield_2, "Shield_2");
    SaveGiftsRev2(Shield_3, "Shield_3");
    SaveGiftsRev2(Shield_4, "Shield_4");

    SaveGiftsRev2(Staff_1, "Staff_1");
    SaveGiftsRev2(Staff_2, "Staff_2");
    SaveGiftsRev2(Staff_3, "Staff_3");
    SaveGiftsRev2(Staff_4, "Staff_4");

    SaveGiftsRev2(Chest_1, "Chest_1");
    SaveGiftsRev2(Chest_2, "Chest_2");
    SaveGiftsRev2(Chest_3, "Chest_3");
    SaveGiftsRev2(Chest_4, "Chest_4");
  }
  public void LoadAllGift()
  {
    LoadGiftsRev2(Dragon_1, "Dragon_1");
    LoadGiftsRev2(Dragon_2, "Dragon_2");
    LoadGiftsRev2(Dragon_3, "Dragon_3");
    LoadGiftsRev2(Dragon_4, "Dragon_4");

    LoadGiftsRev2(Sword_1, "Sword_1");
    LoadGiftsRev2(Sword_2, "Sword_2");
    LoadGiftsRev2(Sword_3, "Sword_3");
    LoadGiftsRev2(Sword_4, "Sword_4");

    LoadGiftsRev2(Crown_1, "Crown_1");
    LoadGiftsRev2(Crown_2, "Crown_2");
    LoadGiftsRev2(Crown_3, "Crown_3");
    LoadGiftsRev2(Crown_4, "Crown_4");

    LoadGiftsRev2(Shield_1, "Shield_1");
    LoadGiftsRev2(Shield_2, "Shield_2");
    LoadGiftsRev2(Shield_3, "Shield_3");
    LoadGiftsRev2(Shield_4, "Shield_4");

    LoadGiftsRev2(Staff_1, "Staff_1");
    LoadGiftsRev2(Staff_2, "Staff_2");
    LoadGiftsRev2(Staff_3, "Staff_3");
    LoadGiftsRev2(Staff_4, "Staff_4");

    LoadGiftsRev2(Chest_1, "Chest_1");
    LoadGiftsRev2(Chest_2, "Chest_2");
    LoadGiftsRev2(Chest_3, "Chest_3");
    LoadGiftsRev2(Chest_4, "Chest_4");
  }
  public void SaveGiftsRev2(List<GameObject> objects, string prefix)
  {

    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();
    List<string> names = new List<string>();

    for (int i = 0; i < objects.Count; i++)
    {
      if (objects[i].activeInHierarchy)
      {
        positions.Add(objects[i].transform.position);
        rotations.Add(objects[i].transform.rotation);
        names.Add(objects[i].name);
      }
    }
    if (names.Count > 0)
    {
      PlayerPrefsX.SetQuaternionArray(prefix + "Quaternion", rotations.ToArray());
      PlayerPrefsX.SetVector3Array(prefix + "Position", positions.ToArray());
      PlayerPrefsX.SetStringArray(prefix + "Name", names.ToArray());
      PlayerPrefs.SetInt(prefix + "Count", names.Count);
    }
    else
    {
      PlayerPrefs.SetInt(prefix + "Count", 0);
    }
  }
  public void LoadGiftsRev2(List<GameObject> objects, string prefix)
  {
    if (PlayerPrefs.HasKey(prefix + "Count"))
    {
      int count = PlayerPrefs.GetInt(prefix + "Count");
      if (count > 0)
      {
        Quaternion[] rotations = PlayerPrefsX.GetQuaternionArray(prefix + "Quaternion");
        Vector3[] positions = PlayerPrefsX.GetVector3Array(prefix + "Position");
        string[] names = PlayerPrefsX.GetStringArray(prefix + "Name");
        for (int i = 0; i < count; i++)
        {
          GameObject gobject = GetInActiveObject(objects);
          gobject.transform.rotation = rotations[i];
          gobject.transform.position = positions[i];
          gobject.name = names[i];
          gobject.SetActive(true);
          Debug.Log(gobject.name);
        }
      }
    }
  }

}