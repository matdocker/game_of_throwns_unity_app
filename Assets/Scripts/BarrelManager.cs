using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{

  public GameObject[] barrels;
  private GameObject[] gifts;
  public GameObject miniGameParent;
  private Rewardmanager rm;

  void Awake()
  {
    rm = Transform.FindObjectOfType<Rewardmanager>();
  }

  // Use this for initialization
  public void barrelsActive()
  {
    print("barrel active 1");
    if (MiniGameManager.IfMiniGame == false)
    {
      print("barrel active 2");
      // Reset barrels after each mini game
      for (int i = 0; i < barrels.Length; i++)
      {
        print("barrel active 3");
        barrels[i].SetActive(true);

        var children = miniGameParent.GetComponentsInChildren<Transform>();
        foreach (var child in children)

          if (child.tag == "Rewards")
          {
            child.GetComponent<Transform>().position = GameObject.FindWithTag("coinDestroy").transform.position;

            Destroy(child.gameObject);
          }
        // do something;
      }

    }
  }


}
