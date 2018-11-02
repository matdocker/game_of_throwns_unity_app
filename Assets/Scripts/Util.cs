using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour
{

  // Use this for initialization

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void LoadScene(string scene)
  {
    Application.LoadLevel(scene);
  }
  public void LoadScene(int scene)
  {
    Application.LoadLevel(scene);
  }
}