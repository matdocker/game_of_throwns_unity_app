using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class CoinPusher : MonoBehaviour
{

  // Use this for initialization
  Vector3 origin;
  public float move1 = 2.2f;
  public float move2 = 1.8f;
  public float speed = 1.3f;
  string dozer_state = "";
  void Awake()
  {
    origin = rigidbody.position;
    origin.z += move1;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    Vector3 offs = new Vector3(0f, 0f, Mathf.Sin(Time.time * speed));
    rigidbody.MovePosition(origin + offs * move2);
  }
}
