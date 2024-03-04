using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public enum TargetFace{Miss, Gold, Red, Blue, Black, White}
    [SerializeField]
    private TargetFace myScore;
    private SpriteRenderer myRenderer;
    private Color myColor;
    void Start(){
        myRenderer = GetComponent<SpriteRenderer>();
        myColor = myRenderer.color;
    }
    void OnCollisionEnter2D(Collision2D collision){
        StartCoroutine(Flash());
        ScoreManager.instance.Score(myScore);
    }
    IEnumerator Flash(){
        for(int i = 0;i<3;i++){
            myRenderer.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            myRenderer.color = myColor;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
