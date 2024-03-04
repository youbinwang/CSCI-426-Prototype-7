using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    public float gravityStrength = 2.0f;
    private List<Collider2D> arrows;

    private AudioSource audioSource;
    public AudioClip gravityClip;
    
    void Start(){
        arrows = new List<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Arrow")
        {
            audioSource.PlayOneShot(gravityClip);
            arrows.Add(collider);
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Arrow"){
            arrows.Remove(collider);
        }
    }
    void Update(){
        foreach(Collider2D collider in arrows){
            Vector2 force = transform.position - collider.gameObject.transform.position;
            float distance = (origin.position - collider.gameObject.transform.position).magnitude;
            collider.attachedRigidbody.AddForce(force*distance*gravityStrength*Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
