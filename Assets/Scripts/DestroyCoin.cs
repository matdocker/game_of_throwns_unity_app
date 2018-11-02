using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyCoin : MonoBehaviour
{

  // Use this for initialization
  private List<string> specialCoins = new List<string>();
  void Start()
  {
    specialCoins.Add("CoinWall");
    specialCoins.Add("CoinPresent");
    specialCoins.Add("SilverCoin_G");
    specialCoins.Add("SilverCoin_I");
  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.name.Contains("Coin"))
    {
      if (specialCoins.Contains(col.gameObject.name))
      {
        //				Debug.Log ("agggrhhh...[sfx]");
      }
      col.gameObject.SetActive(false);
      col.gameObject.GetComponent<CoinScript>().enabled = false;
    }
    if (col.gameObject.tag == "Gift")
    {
      col.gameObject.SetActive(false);
    }
  }
}
