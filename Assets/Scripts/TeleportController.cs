using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector2 targetRotation;

    void Start(){
        float degree = Mathf.Deg2Rad * Quaternion.Angle(target.rotation, transform.rotation);
        targetRotation = new Vector2(Mathf.Cos(degree), Mathf.Sin(degree));
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Arrow"){
            collision.gameObject.transform.position = target.position;
            collision.rigidbody.velocity *= targetRotation;
        }
    }
}
