using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CoinManager : MonoBehaviour
{

  // Use this for initialization
  public GameObject NormalCoin_G;
  public GameObject NormalCoin_I;
  //	public GameObject SilverCoin_G;
  //	public GameObject SilverCoin_I;
  public GameObject GiantNormalCoin_G;
  //	public GameObject GiantNormalCoin_I;
  //	public GameObject GiantSilverCoin_G;
  //	public GameObject GiantSilverCoin_I;
  public GameObject CoinShower;
  public GameObject CoinPresent;
  public GameObject CoinWall;
  public GameObject XPCoin15;
  public GameObject XPCoin30;
  public GameObject XPCoin50;
  public GameObject GiantCoin;
  public GameObject MiniGameCoin;
  public GameObject CarnivalCoin;
  public GameScene gs;

  //Summon Coins
  public GameObject summoned_GiantCoin;
  //	public GameObject summoned_GiantNormalCoin_I;
  //	public GameObject summoned_GiantSilverCoin_G;
  //	public GameObject summoned_GiantSilverCoin_I;
  //	

  private List<GameObject> NormalCoin_G_arr = new List<GameObject>();
  private List<GameObject> NormalCoin_I_arr = new List<GameObject>();
  //	private List<GameObject> SilverCoin_G_arr = new List<GameObject>();
  //	private List<GameObject> SilverCoin_I_arr = new List<GameObject>();
  private List<GameObject> CoinShower_arr = new List<GameObject>();
  private List<GameObject> MiniGameCoin_arr = new List<GameObject>();
  private List<GameObject> CoinPresent_arr = new List<GameObject>();
  private List<GameObject> CoinWall_arr = new List<GameObject>();
  private List<GameObject> XPCoin15_arr = new List<GameObject>();
  private List<GameObject> XPCoin30_arr = new List<GameObject>();
  private List<GameObject> XPCoin50_arr = new List<GameObject>();
  private List<GameObject> GiantCoin_arr = new List<GameObject>();
  private List<GameObject> summonedGiantCoin_arr = new List<GameObject>();
  private List<GameObject> summoned_GiantNormalCoin_I_arr = new List<GameObject>();
  private List<GameObject> particle_arr = new List<GameObject>();


  public Transform coin_parent;
  public Transform particle_parent;
  public Quaternion baseRotation;
  public GameObject particle;
  private int normalcoinCount = 0;
  private int specialcoinCount = 0;
  private bool isInstantiating = false;
  private bool isInstantiatingSpecial = false;
  public List<GameObject> SpecialGameObjectOnQueuee = new List<GameObject>();
  float _duration = 70f;
  private Vector3 CoinPos;



  // Update is called once per frame


  void Update()
  {

    if (MiniGameManager.IfMiniGame == false)
    {
      if (SpecialGameObjectOnQueuee.Count > 0)
      {
        GameObject pObject = SpecialGameObjectOnQueuee.FirstOrDefault();
        Vector3 _localScale = Scale_value(pObject.tag);
        print("pObject.tag " + pObject.tag);
        if (pObject.transform.localScale != _localScale)
        {
          Vector3 _curr = pObject.transform.localScale;
          if (_curr.x < _localScale.x)
          {
            _curr.x += _duration * Time.deltaTime;
          }
          else
          {
            _curr.x = _localScale.x;
          }
          if (_curr.z < _localScale.z)
          {
            _curr.z += _duration * Time.deltaTime;
          }
          else
          {
            _curr.z = _localScale.z;
          }
          if (_curr.y < _localScale.y)
          {
            _curr.y += _duration * Time.deltaTime;
          }
          else
          {
            _curr.y = _localScale.y;
          }
          pObject.transform.localScale = _curr;
        }
        else
        {
          pObject.GetComponent<Rigidbody>().freezeRotation = false;
          SpecialGameObjectOnQueuee.Remove(pObject);
          if (SpecialGameObjectOnQueuee.Count > 0)
          {
            print("LocalScalePosition1");
            //StartCoroutine (ShowNewCoinParticle (SpecialGameObjectOnQueuee.FirstOrDefault ().transform.localScale));
          }
        }
      }
    }

  }
  public GameObject GetRandomNormalCoin()
  {
    //CoinPos = gs.posCoin;

    //Instantiate (NormalCoin_G, CoinPos, baseRotation);

    //		if((Random.Range(1,5)%2) == 1)
    //		{
    //			for(int i = 0 ; i < NormalCoin_I_arr.Count; i++) {
    //				if(!NormalCoin_I_arr[i].activeInHierarchy)
    //				{
    //					return NormalCoin_I_arr[i];
    //				}
    //			}
    //		}
    for (int i = 0; i < NormalCoin_G_arr.Count; i++)
    {
      if (!NormalCoin_G_arr[i].activeInHierarchy)
      {
        Debug.Log("getRandomCoin Loop");
        NormalCoin_G_arr[i] = Instantiate(NormalCoin_G, CoinPos, baseRotation);
        NormalCoin_G_arr.Add(NormalCoin_G_arr[i]);
        return NormalCoin_G_arr[i];
      }

    }
    return null;
  }




  void ResetMe()
  {
    //		Debug.Log ("Invoking.....");
    isInstantiating = false;
    if (normalcoinCount > 0)
    {
      OtherFunc();
    }
  }
  private void OtherFunc()
  {
    if (!isInstantiating)
    {
      isInstantiating = true;
      float iPos = 1.5f;
      float iPos2 = 1.1f;
      int counter = 0;
      GameObject new_coin = GetRandomNormalCoin();
      new_coin.transform.parent = coin_parent;
      new_coin.transform.rotation = baseRotation;
      new_coin.transform.localPosition = new Vector3(iPos, 0.8f, iPos2);
      new_coin.SetActive(true);

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
      if (normalcoinCount > 0)
      {
        normalcoinCount--;
      }
      Invoke("resetMe", 0.3f);
    }
    else
    {
      normalcoinCount++;
    }
  }
  public void SetRandomNormalCoin(int count)
  {
    for (int i = 0; i < count; i++)
    {
      OtherFunc();
    }
  }
  void Start()
  {

    baseRotation = new Quaternion(-0.3f, 0.6f, 0.6f, 0.3f);



    for (int i = 0; i < coin_parent.transform.childCount; i++)
    {
      GameObject obj = coin_parent.transform.GetChild(i).gameObject;
      if (obj.tag == "NormalCoin")
      {
        NormalCoin_G_arr.Add(obj);
      }

      else if (obj.tag == "XPCoin15")
      {
        XPCoin15_arr.Add(obj);
      }
      else if (obj.tag == "XPCoin30")
      {
        XPCoin30_arr.Add(obj);
      }
      else if (obj.tag == "XPCoin50")
      {
        XPCoin50_arr.Add(obj);
      }
      else if (obj.tag == "CoinShower")
      {
        CoinShower_arr.Add(obj);
      }
      else if (obj.tag == "CoinPresent")
      {
        CoinPresent_arr.Add(obj);
      }
      else if (obj.tag == "GiantCoin")
      {
        GiantCoin_arr.Add(obj);
      }
      else if (obj.tag == "summoned_GiantCoin")
      {
        summonedGiantCoin_arr.Add(obj);
      }
      else if (obj.tag == "MiniGameCoin")
      {
        GiantCoin_arr.Add(obj);
      }

      if (PlayerPrefs.HasKey("hadSaveData"))
      {
        if (obj.activeInHierarchy)
        {
          obj.SetActive(true);
        }
      }
    }


    for (int i = 0; i < 100; i++)
    {
      GameObject obj = (GameObject)Instantiate(NormalCoin_G);
      obj.name = NormalCoin_G.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      NormalCoin_G_arr.Add(obj);
    }
    //-------------------------
    //		for(int i = 0 ;i < 50; i++) {
    //			GameObject obj = (GameObject)Instantiate(NormalCoin_I);
    //			obj.name = NormalCoin_I.name;
    //			obj.transform.parent = coin_parent ;
    //			obj.SetActive (false);
    //			NormalCoin_I_arr.Add(obj);
    //		}
    // ----------------------
    //		for(int i = 0 ;i < 10; i++) {
    //			GameObject obj = (GameObject)Instantiate(SilverCoin_G);
    //			obj.name = SilverCoin_G.name;
    //			obj.transform.parent = coin_parent ;
    //			obj.SetActive (false);
    //			SilverCoin_G_arr.Add(obj);
    //		}
    // -----------------
    //		for(int i = 0 ;i < 10; i++) {
    //			GameObject obj = (GameObject)Instantiate(SilverCoin_I);
    //			obj.name = SilverCoin_I.name;
    //			obj.transform.parent = coin_parent ;
    //			obj.SetActive (false);
    //			SilverCoin_I_arr.Add(obj);
    //		}

    // ----------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(XPCoin15);
      obj.name = XPCoin15.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      XPCoin15_arr.Add(obj);
    }
    // ---------------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(XPCoin30);
      obj.name = XPCoin30.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      XPCoin30_arr.Add(obj);
    }
    //------------------s
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(XPCoin50);
      obj.name = XPCoin50.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      XPCoin50_arr.Add(obj);
    }
    // -----------------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(CoinWall);
      obj.name = CoinWall.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      CoinWall_arr.Add(obj);
    }
    // -------------------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(CoinShower);
      obj.name = CoinShower.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      CoinShower_arr.Add(obj);
    }
    // ----------------------

    // -------------------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(MiniGameCoin);
      obj.name = MiniGameCoin.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      MiniGameCoin_arr.Add(obj);
    }
    // ----------------------

    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(CoinPresent);
      obj.name = CoinPresent.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      CoinPresent_arr.Add(obj);
    }
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(summoned_GiantCoin);
      obj.name = summoned_GiantCoin.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      summonedGiantCoin_arr.Add(obj);
    }
    // ---------------------
    for (int i = 0; i < 5; i++)
    {
      GameObject obj = (GameObject)Instantiate(GiantCoin);
      obj.name = GiantCoin.name;
      obj.transform.parent = coin_parent;
      obj.SetActive(false);
      GiantCoin_arr.Add(obj);
    }
    // -------------------
    //		for(int i = 0 ;i < 5; i++) {
    //			GameObject obj = (GameObject)Instantiate(summoned_GiantNormalCoin_I);
    //			obj.name = summoned_GiantNormalCoin_I.name;
    //			obj.transform.parent = coin_parent ;
    //			obj.SetActive (false);
    //			summoned_GiantNormalCoin_I_arr.Add(obj);
    //		}
    // --------------------
    /*for(int i = 0 ;i < 6; i++) {
			GameObject obj = (GameObject)Instantiate(particle);
			obj.name = particle.name;
			obj.transform.parent = particle_parent ;
			obj.SetActive (false);

			particle_arr.Add(obj);
		}*/

    if (PlayerPrefs.HasKey("hadSaveData"))
    {
      LoadAllCoins();
    }
    else
    {
      PlayerPrefs.SetInt("hadSaveData", 1);
    }
  }

  public GameObject GetCoin(string pName)
  {

    switch (pName)
    {
      case "NormalCoin":
        for (int i = 0; i < NormalCoin_G_arr.Count; i++)
        {
          if (!NormalCoin_G_arr[i].activeInHierarchy)
          {
            return NormalCoin_G_arr[i];
          }
        }
        break;
      //		case "NormalCoin_I":
      //			for(int i = 0 ; i < NormalCoin_I_arr.Count; i++) {
      //				if(!NormalCoin_I_arr[i].activeInHierarchy)
      //				{
      //					return NormalCoin_I_arr[i];
      //				}
      //			}
      //			break;
      //		case "SilverCoin_G":
      //			for(int i = 0 ; i < SilverCoin_G_arr.Count; i++) {
      //				if(!SilverCoin_G_arr[i].activeInHierarchy)
      //				{
      //					return SilverCoin_G_arr[i];
      //				}
      //			}
      //			break;
      //		case "SilverCoin_I":
      //			for(int i = 0 ; i < SilverCoin_I_arr.Count; i++) {
      //				if(!SilverCoin_I_arr[i].activeInHierarchy)
      //				{
      //					return SilverCoin_I_arr[i];
      //				}
      //			}
      //			break;
      case "XPCoin15":
        for (int i = 0; i < XPCoin15_arr.Count; i++)
        {
          if (!XPCoin15_arr[i].activeInHierarchy)
          {
            return XPCoin15_arr[i];
          }
        }
        break;
      case "XPCoin30":
        for (int i = 0; i < XPCoin30_arr.Count; i++)
        {
          if (!XPCoin30_arr[i].activeInHierarchy)
          {
            return XPCoin30_arr[i];
          }
        }
        break;
      case "XPCoin50":
        for (int i = 0; i < XPCoin50_arr.Count; i++)
        {
          if (!XPCoin50_arr[i].activeInHierarchy)
          {
            return XPCoin50_arr[i];
          }
        }
        break;
      case "CoinWall":
        for (int i = 0; i < CoinWall_arr.Count; i++)
        {
          if (!CoinWall_arr[i].activeInHierarchy)
          {
            return CoinWall_arr[i];
          }
        }
        break;
      case "CoinShower":
        for (int i = 0; i < CoinShower_arr.Count; i++)
        {
          if (!CoinShower_arr[i].activeInHierarchy)
          {
            return CoinShower_arr[i];
          }
        }
        break;
      case "CoinPresent":
        for (int i = 0; i < CoinPresent_arr.Count; i++)
        {
          if (!CoinPresent_arr[i].activeInHierarchy)
          {
            return CoinPresent_arr[i];
          }
        }
        break;
      case "summoned_GiantCoin":
        for (int i = 0; i < summonedGiantCoin_arr.Count; i++)
        {
          if (!summonedGiantCoin_arr[i].activeInHierarchy)
          {
            return summonedGiantCoin_arr[i];
          }
        }
        break;
      case "GiantCoin":
        for (int i = 0; i < GiantCoin_arr.Count; i++)
        {
          if (!GiantCoin_arr[i].activeInHierarchy)
          {
            return GiantCoin_arr[i];
          }
        }
        break;
      case "MiniGameCoin":
        for (int i = 0; i < GiantCoin_arr.Count; i++)
        {
          if (!MiniGameCoin_arr[i].activeInHierarchy)
          {
            return MiniGameCoin_arr[i];
          }
        }
        break;
        /*case "summoned_GiantCoin":
          for(int i = 0 ; i < summoned_GiantNormalCoin_I_arr.Count; i++) {
            if(!summoned_GiantNormalCoin_I_arr[i].activeInHierarchy)
            {
              return summoned_GiantNormalCoin_I_arr[i];
            }
          }
          break;*/
    }
    return null;
  }
  private Vector3 Scale_value(string pTag)
  {
    if (pTag == "CoinShower" || pTag == "CoinPresent" ||
       pTag == "CoinWall" || pTag == "XPCoin")
    {
      return new Vector3(65, 65, 10);
    }
    else if (pTag == "MiniGame")
    {
      return new Vector3(65, 65, 10);
    }
    else if (pTag == "NormalCoin")
    {
      return new Vector3(50, 50, 10);
    }
    else if (pTag == "GiantCoin")
    {
      return new Vector3(85, 85, 10);
    }
    return Vector3.zero;
  }
  IEnumerator Createss(string[] coinNames, int count)
  {
    while (count > 0)
    {
      float posX = Random.Range(-2f, 2f);
      float posZ = Random.Range(4.5f, 6.8f);
      string c_name = coinNames[Random.Range(0, coinNames.Length)];
      GameObject new_coin = GetCoin(c_name);
      new_coin.SetActive(true);
      new_coin.GetComponent<Rigidbody>().mass = 1f;
      new_coin.transform.localPosition = new Vector3(posX, 0.75f, -posZ);
      new_coin.transform.rotation = baseRotation;
      new_coin.GetComponent<Rigidbody>().freezeRotation = true;  //constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
      new_coin.name = c_name;
      new_coin.GetComponent<CoinScript>().enabled = false;
      new_coin.transform.localScale = Vector3.zero;
      new_coin.GetComponent<CoinScript>().isLanded = true;
      //			new_coin.GetComponent<CoinScript>().Scale(0.15f, scale_value(new_coin.tag));
      if (!SpecialGameObjectOnQueuee.Contains(new_coin))
      {
        new_coin.SetActive(true);
        new_coin.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SpecialGameObjectOnQueuee.Add(new_coin);
      }
      if (SpecialGameObjectOnQueuee.Count == 1)
      {
        //StartCoroutine (ShowNewCoinParticle (new Vector3(posX,0.27f,-posZ)));
      }
      count--;
      if (count <= 0)
      {
        count = 0;
        isInstantiatingX = false;
        if (onQueuee > 0)
        {
          onQueuee--;
          isInstantiatingX = true;
          coinNames = names_que.FirstOrDefault();
          count = count_que.FirstOrDefault();
          names_que.Remove(coinNames);
          count_que.Remove(count);
        }
      }
      yield return new WaitForSeconds(0.5f);
    }
  }
  private bool isInstantiatingX = false;
  private int onQueuee = 0;
  private List<string[]> names_que = new List<string[]>();
  private List<int> count_que = new List<int>();
  public void InstantiateSpecialCoin(Transform pParent, string[] coinNames, int count)
  {

    if (!isInstantiatingX)
    {
      isInstantiatingX = true;
      if (count_que.Count == 0)
      {
        onQueuee = 0;
      }
      StartCoroutine(Createss(coinNames, count));
    }
    else
    {
      if (onQueuee < 3)
      {
        names_que.Add(coinNames);
        count_que.Add(count);
        onQueuee++;
      }
    }
  }
  IEnumerator InstantiateParticle(Vector3[] posis)
  {
    GameObject[] pObj = new GameObject[posis.Length];
    yield return new WaitForSeconds(0.7f);
    int count = posis.Length;
    for (int i = 0; i < particle_arr.Count; i++)
    {
      if (!particle_arr[i].activeInHierarchy)
      {
        if (count < posis.Length)
        {
          particle_arr[i].SetActive(true);
          particle_arr[i].transform.localPosition = posis[i];
          pObj[i] = particle_arr[i];
          count++;
          yield return new WaitForSeconds(0.3f);
        }
        else
        {
          break;
        }
      }
    }
    yield return new WaitForSeconds(3f);
    for (int i = 0; i < pObj.Length; i++)
    {
      if (pObj[i])
      {
        pObj[i].SetActive(false);
      }
    }
  }
  void SpecialReset()
  {
    isInstantiatingSpecial = false;
    if (specialcoinCount > 0)
    {
      //InstantiateRandomSpecialCoin();
    }
  }
  IEnumerator ShowNewCoinParticle(Vector3 locPos)
  {
    GameObject cur = null;
    yield return new WaitForSeconds(0.9f);
    for (int i = 0; i < particle_arr.Count; i++)
    {
      if (!particle_arr[i].activeInHierarchy)
      {
        particle_arr[i].SetActive(true);
        particle_arr[i].transform.localPosition = locPos;
        cur = particle_arr[i];
        break;
      }
    }
    yield return new WaitForSeconds(3f);
    if (cur)
    {
      cur.SetActive(false);
    }
  }

  private void SaveCoinFromArr(List<GameObject> coin_lst, string prefix)
  {
    List<Quaternion> coin_rotation = new List<Quaternion>();
    List<Vector3> coin_position = new List<Vector3>();
    List<string> coin_names = new List<string>();

    for (int i = 0; i < coin_lst.Count; i++)
    {
      if (coin_lst[i] != null)
      {
        if (coin_lst[i].activeInHierarchy == true)
        {/////////////////////////// change from != false

          if (coin_lst[i].activeInHierarchy)
          {
            coin_rotation.Add(coin_lst[i].transform.rotation);
            coin_position.Add(coin_lst[i].transform.position);
            coin_names.Add(coin_lst[i].name);
          }
        }
      }
    }
    PlayerPrefsX.SetQuaternionArray(prefix + "Quaternion", coin_rotation.ToArray());
    PlayerPrefsX.SetVector3Array(prefix + "Position", coin_position.ToArray());
    PlayerPrefsX.SetStringArray(prefix + "Name", coin_names.ToArray());

    if (coin_names.Count > 0)
    {
      PlayerPrefs.SetInt(prefix + "Count", coin_names.Count);
    }
    else
    {
      PlayerPrefs.SetInt(prefix + "Count", 0);
    }
  }
  public void SaveAllCoins()
  {
    SaveCoinFromArr(NormalCoin_G_arr, "NormalCoin");
    //SaveCoinFromArr (NormalCoin_I_arr, "NormalCoin_I");
    //		SaveCoinFromArr (SilverCoin_G_arr, "SilverCoin_G");
    //		SaveCoinFromArr (SilverCoin_I_arr, "SilverCoin_I");
    SaveCoinFromArr(XPCoin15_arr, "XPCoin15");
    SaveCoinFromArr(XPCoin30_arr, "XPCoin30");
    SaveCoinFromArr(XPCoin50_arr, "XPCoin50");
    SaveCoinFromArr(CoinWall_arr, "CoinWall");
    SaveCoinFromArr(CoinPresent_arr, "CoinPresent");
    SaveCoinFromArr(CoinShower_arr, "CoinShower");
    SaveCoinFromArr(GiantCoin_arr, "GiantCoin");
    SaveCoinFromArr(summonedGiantCoin_arr, "summoned_GiantCoin");
    SaveCoinFromArr(MiniGameCoin_arr, "MiniGameCoin");

  }
  private void ActivateCoinFromArr(List<GameObject> coin_lst, string prefix, float mass)
  {
    if (PlayerPrefs.HasKey(prefix + "Count"))
    {
      int coinCount = PlayerPrefs.GetInt(prefix + "Count");
      if (coinCount > 0)
      {
        Vector3[] positions = PlayerPrefsX.GetVector3Array(prefix + "Position");
        Quaternion[] rotations = PlayerPrefsX.GetQuaternionArray(prefix + "Quaternion");
        string[] names = PlayerPrefsX.GetStringArray(prefix + "Name");

        for (int i = 0; i < coin_lst.Count; ++i)
        {
          if (i < coinCount)
          {
            if (!coin_lst[i].activeInHierarchy)
            {
              coin_lst[i].SetActive(true);
            }
            coin_lst[i].GetComponent<Rigidbody>().freezeRotation = false;
            coin_lst[i].GetComponent<Rigidbody>().mass = mass;
            coin_lst[i].transform.rotation = rotations[i];
            coin_lst[i].transform.position = positions[i];
            coin_lst[i].name = names[i];
            coin_lst[i].transform.parent = coin_parent;
            coin_lst[i].GetComponent<CoinScript>().isLanded = false;
            coin_lst[i].GetComponent<CoinScript>().giantShake = true;
            coin_lst[i].GetComponent<CoinScript>().enabled = false;
          }
          else
          {
            if (coin_lst[i].activeInHierarchy)
            {
              coin_lst[i].GetComponent<CoinScript>().enabled = false;
              coin_lst[i].SetActive(false);
              coin_lst[i].GetComponent<Rigidbody>().freezeRotation = true; //////////////////////// Freeze Rotation
            }
          }
        }
      }
    }
  }
  public void LoadAllCoins()
  {

    ActivateCoinFromArr(NormalCoin_G_arr, "NormalCoin", 1f);
    //		ActivateCoinFromArr (NormalCoin_I_arr, "NormalCoin_I",1f);
    //		ActivateCoinFromArr (SilverCoin_G_arr, "SilverCoin_G",1f);
    //		ActivateCoinFromArr (SilverCoin_I_arr, "SilverCoin_I",1f);
    ActivateCoinFromArr(XPCoin15_arr, "XPCoin15", 1f);
    ActivateCoinFromArr(XPCoin30_arr, "XPCoin30", 1f);
    ActivateCoinFromArr(XPCoin50_arr, "XPCoin50", 1f);
    ActivateCoinFromArr(CoinWall_arr, "CoinWall", 1f);
    ActivateCoinFromArr(CoinPresent_arr, "CoinPresent", 1f);
    ActivateCoinFromArr(CoinShower_arr, "CoinShower", 1f);
    ActivateCoinFromArr(GiantCoin_arr, "GiantCoin", 1f);
    ActivateCoinFromArr(summonedGiantCoin_arr, "summoned_GiantCoin", 2f);
    ActivateCoinFromArr(MiniGameCoin_arr, "MiniGameCoin", 1f);

  }
}
