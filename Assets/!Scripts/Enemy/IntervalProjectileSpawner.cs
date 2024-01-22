using System.Collections;
using UnityEngine;

public class IntervalProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _interval = 1.0f;

    private void Start()
    {
        StartCoroutine(InstantiateInterval());
    }

    IEnumerator InstantiateInterval()
    {
        float elapsedTime = 0.0f;

        while (true)
        {
            while (elapsedTime <= _interval)
            {
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0.0f;
            Destroy(Instantiate(_projectilePrefab, transform.position, transform.rotation), 3f);
            yield return null;
        }
    }
}
