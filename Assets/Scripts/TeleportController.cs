using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    // [SerializeField] private Transform target;
    // private Vector2 targetRotation;
    [SerializeField] private Transform teleportOrigin;
    [SerializeField] private Transform teleportTarget;
    private Transform sourceParticlesObject;
    private ParticleSystem sourceParticles;
    private ParticleSystem targetParticles;
    
    // void Start(){
    //     float degree = Mathf.Deg2Rad * Quaternion.Angle(target.rotation, transform.rotation);
    //     targetRotation = new Vector2(Mathf.Cos(degree), Mathf.Sin(degree));
    // }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Arrow")
    //     {
    //         collision.gameObject.transform.position = target.position;
    //         collision.rigidbody.velocity *= targetRotation;
    //     }
    // }
    void Start(){
        sourceParticlesObject = teleportOrigin.GetChild(1);
        sourceParticles = sourceParticlesObject.GetComponent<ParticleSystem>();
        targetParticles = teleportTarget.GetChild(1).GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D arrowRigidbody = collider.attachedRigidbody;

        if (arrowRigidbody != null)
        {
            sourceParticlesObject.position = collider.ClosestPoint(transform.position);
            sourceParticles.Play();
            float arrowVelocityAngle = Mathf.Atan2(arrowRigidbody.velocity.y, arrowRigidbody.velocity.x) * Mathf.Rad2Deg;
            float angleDifference = teleportTarget.eulerAngles.z - teleportOrigin.eulerAngles.z;
            float newAngle = arrowVelocityAngle + angleDifference;
            collider.transform.position = teleportTarget.position;
            Vector2 newVelocityDirection = Quaternion.Euler(0, 0, newAngle) * Vector2.right;
            StartCoroutine(TeleportDelay(collider.gameObject, newVelocityDirection.normalized * arrowRigidbody.velocity.magnitude));
        }
    }
    IEnumerator TeleportDelay(GameObject arrow, Vector2 velocity){
        arrow.SetActive(false);
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup-startTime < 0.2) {
            yield return null;
        }
        arrow.SetActive(true);
        arrow.GetComponent<Rigidbody2D>().velocity = velocity;
        targetParticles.Play();
    }
}
