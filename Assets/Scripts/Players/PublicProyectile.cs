using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicProyectile : MonoBehaviour
{

    public AnimationCurve curve;

    [SerializeField]
    private float _duration = 1.0f;

    [SerializeField]
    private float _heightY = 3.0f;

    private void Start()
    {
        EventManager.OnPlayerHit += ShootFinish;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHit -= ShootFinish;
    }

    public void ShootProyectile(Vector3 start, Vector2 target)
    {
        StartCoroutine(CurveMovement(start, target));
    }

    public IEnumerator CurveMovement(Vector3 start, Vector2 target)
    {
        float timePassed = 0f;

        Vector2 end = target;

        while (timePassed < _duration)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / _duration; //0 to 1 time
            float heighT = curve.Evaluate(linearT); //Value from curve

            float height = Mathf.Lerp(0f, _heightY, heighT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

    public void ShootFinish()
    {
        this.gameObject.SetActive(false);
    }
}
