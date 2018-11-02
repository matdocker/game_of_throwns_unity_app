using UnityEngine;
using System.Collections;

public class destroyBall : MonoBehaviour
{

  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "ShootedBall")
    {
      print("BallDestroyArea");
      Destroy(col.gameObject);

    }


  }

}

