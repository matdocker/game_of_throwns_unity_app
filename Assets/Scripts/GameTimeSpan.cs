using UnityEngine;
using System.Collections;
using System;

public class GameTimeSpan : MonoBehaviour
{

  // Use this for initialization
  public TimeSpan timeSpan;
  private GameTimeSpan instance;
  public bool isFirstTime;
  //public GameScene gs;

  public GameTimeSpan Instance
  {
    get { return instance; }
  }

  void Start()
  {
    if (instance != null && instance != this)
    {
      Destroy(this.gameObject);
      return;
    }
    else
    {
      Debug.Log("gts instance");
      instance = this;

    }

    if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
    {
      Debug.Log("gts first time");
      isFirstTime = true;
      PlayerPrefs.SetInt("FirstTime", 1);
      PlayerPrefs.SetFloat("CurrentXP", 0f);
      PlayerPrefs.SetInt("CurrentLevel", 1);
      PlayerPrefs.SetFloat("LevelXP", 50f);
      PlayerPrefs.SetInt("TotalCoinCount", 20);//20
      PlayerPrefs.SetInt("TotalCoinTimerCounter", 30);
    }
    else
    {
      print(PlayerPrefs.GetInt("FirstTime"));
      Debug.Log("gts not first time");
      isFirstTime = false;
      DateTime dateNow = DateTime.Now;
      timeSpan = dateNow - Convert.ToDateTime(PlayerPrefs.GetString("LastTime"));
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnApplicationQuit()
  {
    //PlayerPrefs.DeleteAll ();
    //PlayerPrefs.SetInt("FirstTime",0);
    PlayerPrefs.SetString("LastTime", Convert.ToString(DateTime.Now));
  }

  void OnApplicationFocus(bool focusStatus)
  {
    if (!focusStatus)
    {
      PlayerPrefs.SetString("LastTime", Convert.ToString(DateTime.Now));
    }
    else
    {
      isFirstTime = false;
      DateTime dateNow = DateTime.Now;
      timeSpan = dateNow - Convert.ToDateTime(PlayerPrefs.GetString("LastTime"));
    }
  }
  void OnApplicationPause(bool pauseStatus)
  {
    if (pauseStatus)
    {
      PlayerPrefs.SetString("LastTime", Convert.ToString(DateTime.Now));
    }
    else
    {
      isFirstTime = false;
      DateTime dateNow = DateTime.Now;
      timeSpan = dateNow - Convert.ToDateTime(PlayerPrefs.GetString("LastTime"));
    }
  }

  void deleteAll()
  {
    Debug.Log("Delete All");
    PlayerPrefs.DeleteAll();
    //PlayerPrefs.SetInt ("FirstTime",0);
  }
}
