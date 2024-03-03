using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private List<TargetController.TargetFace[]> totalScores;
    private TargetController.TargetFace[] scores;
    // Start is called before the first frame update
    void Start()
    {
        totalScores = new List<TargetController.TargetFace[]>();
        instance = this;
        DontDestroyOnLoad(gameObject);
        scores = new TargetController.TargetFace[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Score(TargetController.TargetFace score){
        scores[BowController.numShot-1] = score;
        if(BowController.numShot >= 4){
            totalScores.Add(scores);
            //Load next scene.
        }
    }

}
