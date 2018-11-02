using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRewards : MonoBehaviour
{

  static MiniGameRewards _instance;
  private SoundManager sm;
  public GameObject prefab;
  public GameObject coinAnim;

  private int[] SelectT1 = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 4, 5 };
  private int rewardSelect;
  private int coinReward = 0;

  int[] lvlOneOneCoin = { 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 4, 5 };
  // Level 1.2 Cont Winnings
  int[] lvlOneTwoCoin = { 8, 8, 10, 10, 10, 12, 12, 12, 15 };
  int[] lvlOneTwoCoinWall = { 1, 1, 2, 3, 2, 1, 1 };
  int[] lvlOneTwoMiniGame = { 1, 1, 2, 3, 2, 1, 1 };
  // Level 1.3 Cont Winnings
  int[] lvlOneThreeCoin = { 20, 20, 20, 20, 25, 25, 25, 50 };
  int[] lvlOneThreeCoinWall = { 1, 1, 2, 3, 3, 2, 1, 1 };
  int[] lvlOneThreeMiniGame = { 1, 1, 2, 3, 3, 2, 1, 1 };

  public GameObject[] prizeArr;
  public static int score = 0;
  public static int gift = 0;
  public static int coinMG = 0;
  public static int coinCW = 0;
  public static bool IsGoal = false;
  public bool prevGoal = false;
  public int Goal = 0;
  public static int goalSprite = 0;
  public GameObject barrelOne;
  public static int barCnt = 0;
  GameObject[] barrels;

  //public GameObject rewardCoins;
  public GameObject[] rewards;
  private Rewardmanager rm;
  private GameScene gs;


  void Awake()
  {

    if (MiniGameManager.IfMiniGame == true)
    {
      sm = Transform.FindObjectOfType<SoundManager>();
      gs = Transform.FindObjectOfType<GameScene>();

    }

  }
  void Update();
}
// When throwing the ball - on entry to barrel
void OnTriggerEnter(Collider other)
{

  if (MiniGameManager.IfMiniGame == true)
  {
    // Created to add the option to add additional rewards to each tier
    if (other.tag == "BarOne" || other.tag == "BarTwo" || other.tag == "BarThree")
    {
      Debug.Log("Tier One");
      DestroyBall();
      sm.Play(SoundManager.SFX.goal);
      sm.Play(SoundManager.SFX.barrel);
      rewardSelect = SelectT1[Random.Range(0, SelectT1.Length)];
      print(rewardSelect + "reward Select");
      RewardGen(other.tag, rewardSelect);

      IsGoal = true;
      prevGoal = true;
      CheckGoal();

    }

    if (other.tag == "BarFour" || other.tag == "BarFive" || other.tag == "BarSix")
    {
      Debug.Log("Tier Two");
      DestroyBall();
      sm.Play(SoundManager.SFX.goal);
      sm.Play(SoundManager.SFX.barrel);
      rewardSelect = SelectT1[Random.Range(0, SelectT1.Length)];
      print(rewardSelect + "reward Select");
      RewardGen(other.tag, rewardSelect);

      IsGoal = true;
      prevGoal = true;
      CheckGoal();


    }

    if (other.tag == "BarSeven" || other.tag == "BarEight" || other.tag == "BarNine")
    {
      Debug.Log("Tier Three");
      DestroyBall();
      sm.Play(SoundManager.SFX.goal);
      sm.Play(SoundManager.SFX.barrel);
      rewardSelect = SelectT1[Random.Range(0, SelectT1.Length)];
      print(rewardSelect + "reward Select");
      RewardGen(other.tag, rewardSelect);
      IsGoal = true;
      prevGoal = true;
      CheckGoal();

    }

  }

  else
  {
    print("else");
    prevGoal = false;
    CheckGoal();
  }
}

public void CheckGoal()
{

  if (prevGoal == true)
  {
    Goal++;
  }
  else
  {
    print("CheckGoal else");
    prevGoal = false;

  }
}
// Out of bounds Collider
void OnCollisionEnter(Collision col)
{
  //		print("BallCollider");

  if (this.gameObject.tag == "Ball" && col.gameObject.tag == "ShootedBall"
    || this.gameObject.tag == "ShootedBall" && col.gameObject.tag == "ShootedBall"
    || this.gameObject.tag == "ShootedBall" && col.gameObject.tag == "Ball"
    || this.gameObject.tag == "Ball" && col.gameObject.tag == "Ball")
  {
    print("BallHitedBall");
    Destroy(this.gameObject);

  }
}

public void DestroyBall()
{

  Destroy(this.gameObject, 0f);

}

// mini game reward generator

void RewardGen(string tag, int x)
{
  int Coin;

  if (x == 0)
  {
    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    Debug.Log("CoinGenFunc");

    Coin = lvlOneOneCoin[Random.Range(0, lvlOneOneCoin.Length)];
    Rewards(Coin, tag);
    print(Coin);
    GameObject.FindWithTag(tag).SetActive(false);

    for (int i = 0; i < Coin; i++)
    {
      score = score + 1;
    }

  }
  else if (x == 1)
  {
    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    Debug.Log("CoinGenFunc");

    Coin = lvlOneTwoCoin[Random.Range(0, lvlOneTwoCoin.Length)];
    Rewards(Coin, tag);
    GameObject.FindWithTag(tag).SetActive(false);
    print(Coin);

    for (int i = 0; i < Coin; i++)
    {
      score = score + 1;

    }

  }
  else if (x == 2)
  {
    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    Debug.Log("CoinGenFunc");

    Coin = lvlOneThreeCoin[Random.Range(0, lvlOneThreeCoin.Length)];
    Rewards(Coin, tag);
    GameObject.FindWithTag(tag).SetActive(false);
    print(Coin);

    for (int i = 0; i < Coin; i++)
    {
      score = score + 1;

    }

  }
  else if (x == 3)
  {

    print(prizeArr.Length + " PrizeArray length");
    print(tag + "Prize Gen tag");

    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    GameObject prize = prizeArr[Random.Range(0, prizeArr.Length - 1)];

    GameObject new_Gift = Instantiate(prize, pos, Quaternion.identity);
    new_Gift.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    new_Gift = giftRotation(new_Gift);
    new_Gift.GetComponent<Rigidbody>().useGravity = false;
    new_Gift.GetComponent<Rigidbody>().isKinematic = false;
    new_Gift.GetComponent<BoxCollider>().enabled = false;
    GameObject.FindWithTag(tag).SetActive(false);

    gift = gift + 1;
    gs.prizeCount(new_Gift);



  }
  else if (x == 4)
  {
    GameObject giftCoin = rewards[13];
    print(tag + "Prize Gen tag");

    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    GameObject coinRew = Instantiate(giftCoin, pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";
    GameObject.FindWithTag(tag).SetActive(false);
    coinMG = coinMG + 1;


  }
  else if (x == 5)
  {
    GameObject giftCoin = rewards[12];
    print(tag + "Prize Gen tag");

    Vector3 pos = GameObject.FindWithTag(tag).transform.position;
    GameObject coinRew = Instantiate(giftCoin, pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";
    GameObject.FindWithTag(tag).SetActive(false);
    coinCW = coinCW + 1;

  }
  return;
}

// Gift reward rotation set

GameObject giftRotation(GameObject gift)
{
  print("Gift Rotate");

  if (gift.name.Contains("Chest_"))
  {
    gift.transform.Rotate(10, 0, 90);
  }
  else if (gift.name.Contains("Dragon_"))
  {
    gift.transform.Rotate(-90, 180, 0);
  }
  else if (gift.name.Contains("Sword_"))
  {
    gift.transform.Rotate(0, 125, 0);
    gift.transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
  }
  else if (gift.name.Contains("Shield_"))
  {
    gift.transform.Rotate(25, 150, 0);
  }
  else if (gift.name.Contains("Staff_"))
  {
    gift.transform.Rotate(0, 180, 0);
    gift.transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
  }
  else if (gift.name.Contains("Crown_"))
  {
    gift.transform.Rotate(0, -40, 10);
  }
  return gift; ;
}

// instatiate mini game rewards
void Rewards(int coins, string tag)
{
  print("Coin Rewards");
  Vector3 pos = GameObject.FindWithTag(tag).transform.position;

  if (coins == 1)
  {
    GameObject coinRew = Instantiate(rewards[0], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 2)
  {
    GameObject coinRew = Instantiate(rewards[1], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 3)
  {
    GameObject coinRew = Instantiate(rewards[2], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 4)
  {
    GameObject coinRew = Instantiate(rewards[3], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 5)
  {
    GameObject coinRew = Instantiate(rewards[4], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 8)
  {
    GameObject coinRew = Instantiate(rewards[5], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 10)
  {
    GameObject coinRew = Instantiate(rewards[6], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 12)
  {
    GameObject coinRew = Instantiate(rewards[7], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 15)
  {
    GameObject coinRew = Instantiate(rewards[8], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 20)
  {
    GameObject coinRew = Instantiate(rewards[9], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 25)
  {
    GameObject coinRew = Instantiate(rewards[10], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";

  }
  else if (coins == 50)
  {
    GameObject coinRew = Instantiate(rewards[11], pos, Quaternion.identity);
    coinRew.transform.SetParent(GameObject.FindGameObjectWithTag("MiniGamePar").transform);
    coinRew.tag = "Rewards";
  }
  return;
}


    // Decided not to use code


    /*void CoinScore(int coinCount, string tag)
    {
        GameObject[] animCoin;
        Vector3 coinPos = new Vector3(-1.4f, -2.21f, 1.56f); //GameObject.FindWithTag("CoinCntMG").transform.localPosition;
        animCoin = new GameObject[coinCount];
        print(coinPos + "coin Pos");

        if (tag == "BarNine")
        {
            Vector3 pos = GameObject.Find("bar9").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar9").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarEight")
        {
            Vector3 pos = GameObject.Find("bar8").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar8").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarSeven")
        {
            Vector3 pos = GameObject.Find("bar7").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar7").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarSix")
        {
            Vector3 pos = GameObject.Find("bar6").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar6").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarFive")
        {
            Vector3 pos = GameObject.Find("bar5").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar5").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarFour")
        {
            Vector3 pos = GameObject.Find("bar4").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar4").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarThree")
        {
            Vector3 pos = GameObject.Find("bar3").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar3").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarTwo")
        {
            Vector3 pos = GameObject.Find("bar2").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar2").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);
            }

        }
        else if (tag == "BarOne")
        {
            Vector3 pos = GameObject.Find("bar1").transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                sm.Play(SoundManager.SFX.regen);
                animCoin[i] = Instantiate(coinAnim, pos, Quaternion.identity);
                //animCoin[i].transform.SetParent(GameObject.Find("bar1").transform);
                animCoin[i].transform.Translate(coinPos);
                score = score + 1;
                //Invoke("WaitForSeconds", 2);

            }

        }

    }

}*/
}
