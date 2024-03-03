using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private TargetController.TargetFace[] scores;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scores = new TargetController.TargetFace[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Score(TargetController.TargetFace score){
        scores[BowController.numShot-1] = score;
        Debug.Log("You got " + score);
        for(int i = 0;i<scores.Length;i++){
            Debug.Log(scores[i]);
        }
    }

}
