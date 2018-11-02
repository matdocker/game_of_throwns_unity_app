using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{

  // Use this for initialization
  public string scene;
  public float delay;

  public bool isFaded;
  public bool isDoNothing;
  void Start()
  {
    if (isFaded)
    {
      GetComponent<Image>().CrossFadeAlpha(0f, delay, true);
    }
    StartCoroutine(LoadScene(scene, delay));
  }
  // Update is called once per frame
  void Update()
  {
  }
  void load()
  {
    Application.LoadLevel(scene);
  }
  IEnumerator LoadScene(string scene, float delay)
  {
    yield return new WaitForSeconds(delay);
    if (isDoNothing)
    {
      DestroyObject(gameObject);
    }
    else
    {
      Application.LoadLevel(scene);
    }
  }
}
