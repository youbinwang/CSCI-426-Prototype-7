using System.Collections;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField] private float _shakeAmount = 0.1f;
    [SerializeField] private float _decreaseFactor = 1.0f;
    private Vector3 _initial;

    void Start()
    {
        _initial = transform.localPosition;
    }

    public void Shake(float shakeTime, float shakeAmount = 0f)
    {
        _initial = transform.localPosition;
        StartCoroutine(Shaking(shakeTime, 
            shakeAmount == 0 ? _shakeAmount : shakeAmount));
    }

    private IEnumerator Shaking(float shakeTime, float shakeAmount)
    {
        while (shakeTime > 0)
        {
            Vector3 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.localPosition = shakePos + _initial;
            shakeTime -= Time.deltaTime * _decreaseFactor;
            yield return null;
        }

        gameObject.transform.localPosition = _initial;
    }
}
