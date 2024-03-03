using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public enum TargetFace{Gold, Red, Blue, Black, White}
    [SerializeField]
    private TargetFace myScore;

    void OnCollisionEnter2D(Collision2D collision){

        ScoreManager.instance.Score(myScore);
    }
}
