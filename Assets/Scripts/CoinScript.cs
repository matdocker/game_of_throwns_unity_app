using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{

  // Use this for initialization
  public bool isLanded = false;
  public bool giantShake = false;
  //	public bool _isScale = false;
  //	private Vector3 _localScale;
  //	private float _duration = 70f;
  void Start()
  {
  }
  /*
	public void Scale(float delay, Vector3 scale) {
		transform.localScale = Vector3.zero;
		_localScale = scale;
		_isScale = true;
	}
*/
  // Update is called once per frame
  void FixedUpdate()
  {
    /*
		if(_isScale && gameObject.activeInHierarchy) {
			if(transform.localScale != _localScale) {
				Vector3 _curr = transform.localScale;
				if(_curr.x < _localScale.x) {
					_curr.x += _duration*Time.fixedDeltaTime;
				} else {
					_curr.x = _localScale.x;
				}
				if(_curr.z < _localScale.z) {
					_curr.z += _duration*Time.fixedDeltaTime;
				} else {
					_curr.z = _localScale.z;
				}
				if(_curr.y < _localScale.y) {
					_curr.y += _duration*Time.fixedDeltaTime;
				} else {
					_curr.y = _localScale.y;
				}
				transform.localScale = _curr;
			} else {
				_isScale = false;
				rigidbody.freezeRotation = false;
				isLanded = true;
				this.enabled = false;
			}
		} */
  }
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
        rigidbody.freezeRotation = false;
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
           col.gameObject.tag == "Gift" || col.gameObject.tag == "CoinPresent"))
      {
        if (!giantShake)
        {
          giantShake = true;
          rigidbody.freezeRotation = false;
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
        if (rigidbody.freezeRotation)
        {
          rigidbody.freezeRotation = false;
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
    rigidbody.velocity = Vector3.zero;
  }
  void OnDisable()
  {
    rigidbody.velocity = Vector3.zero;
  }
}
