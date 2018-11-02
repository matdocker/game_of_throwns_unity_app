using UnityEngine;
using System.Collections;

public class DropCoin : MonoBehaviour
{

  // Use this for initialization
  public GameObject coin;
  void Start()
  {
    coin = (GameObject)Resources.Load("Prefabs/sample") as GameObject;
  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnMouseDown()
  {
    GameObject newCoin = Instantiate(coin) as GameObject;
    newCoin.transform.position = new Vector3(transform.position.x, 2.73f, 1.95f);
  }
}
