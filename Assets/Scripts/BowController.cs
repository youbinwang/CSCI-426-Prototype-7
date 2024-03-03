using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrow;
    public float launchForce;
    public float maxLaunchForce = 20f;
    private float currentLaunchForce;
    public Transform shotPoint;

    public GameObject point;
    private List<GameObject> points = new List<GameObject>();
    public int maxNumberOfPoints = 25;
    public float spaceBetweenPoints;
    private float maxSpaceBetweenPoints = 0.1f;
    private Vector2 direction;

    private bool isPressing = false;

    public static int numShot;

    private void Start()
    {
        numShot = 0;
        for (int i = 0; i < maxNumberOfPoints; i++)
        {
            GameObject newPoint = Instantiate(point, shotPoint.position, Quaternion.identity);
            newPoint.SetActive(false);
            points.Add(newPoint);
        }
    }

    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            currentLaunchForce = launchForce;
        }

        if (isPressing)
        {
            if (currentLaunchForce < maxLaunchForce)
            {
                currentLaunchForce += Time.deltaTime * 10;
                spaceBetweenPoints = maxSpaceBetweenPoints * (1 - (currentLaunchForce / maxLaunchForce)) + 0.05f;
            }

            int activePoints = Mathf.RoundToInt(currentLaunchForce / maxLaunchForce * maxNumberOfPoints);
            for (int i = 0; i < maxNumberOfPoints; i++)
            {
                if (i < activePoints)
                {
                    points[i].SetActive(true);
                    points[i].transform.position = PointPosition(i * spaceBetweenPoints);
                }
                else
                {
                    points[i].SetActive(false);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isPressing)
        {
            Shoot();
            isPressing = false;
            foreach (var point in points)
            {
                point.SetActive(false);
            }
        }
    }

    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * currentLaunchForce;
        currentLaunchForce = launchForce;
        numShot++;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 startPosition = (Vector2)shotPoint.position + (direction.normalized * currentLaunchForce * t) + 0.5f * Physics2D.gravity * Mathf.Pow(t, 2);
        return startPosition;
    }
}