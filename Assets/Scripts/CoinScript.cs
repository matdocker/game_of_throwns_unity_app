using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{

  // Use this for initialization
  public bool isLanded = false;
  public bool giantShake = false;
  public bool _isScale = false;
  private Vector3 _localScale;

  void invokeDisabler()
  {
    this.enabled = false;
  }
  void OnCollisionEnter(Collision col)
  {
    if (!isLanded)
    {
      isLanded = true;
      if (gameObject.tag == "NormalCoin")
      {
        SoundManager sm = GameObject.FindObjectOfType<SoundManager>();
        sm.Play(SoundManager.SFX.normal);
        GetComponent<Rigidbody>().freezeRotation = false;
        if (!IsInvoking("invokeDisabler"))
        {
          Invoke("invokeDisabler", 0.5f);
        }
      }
      else if (gameObject.tag == "summoned_GiantCoin" &&
              (col.gameObject.tag == "Platform" || col.gameObject.tag == "NormalCoin" ||
           col.gameObject.tag == "TapArea" || col.gameObject.tag == "SilverCoin" ||
           col.gameObject.tag == "XPCoin" || col.gameObject.tag == "GiantCoin" ||
           col.gameObject.tag == "CoinWall" || col.gameObject.tag == "CoinShower" ||
       col.gameObject.tag == "Gift" || col.gameObject.tag == "CoinPresent" || col.gameObject.tag == "MiniGame"))
      {
        if (!giantShake)
        {
          giantShake = true;
          GetComponent<Rigidbody>().freezeRotation = false;
          GameScene gs = FindObjectOfType<GameScene>();
          gs.ShakeCastle();
          SoundManager sm = GameObject.FindObjectOfType<SoundManager>();
          sm.Play(SoundManager.SFX.shake);
        }
        if (!IsInvoking("invokeDisabler"))
        {
          Invoke("invokeDisabler", 0.5f);
        }
      }
    }
    else
    {
      if (this.enabled && gameObject.tag == "NormalCoin")
      {
        if (GetComponent<Rigidbody>().freezeRotation)
        {
          GetComponent<Rigidbody>().freezeRotation = false;
        }
        if (!IsInvoking("invokeDisabler"))
        {
          Invoke("invokeDisabler", 0.25f);
        }
      }
    }
  }
  void OnEnable()
  {
    GetComponent<Rigidbody>().velocity = Vector3.zero;
    GetComponent<Rigidbody>().AddForce(transform.up * -10f);
  }
  void OnDisable()
  {
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
}
