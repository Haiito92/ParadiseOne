using UnityEngine;

public class BoatTrailSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _particleLifetime;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnParticle();
            _timer = 0f;
        }
    }

    private void SpawnParticle()
    {
        Vector3 spawnPos = transform.position - transform.forward * _spawnDistance;

        GameObject instance = Instantiate(_particlePrefab, spawnPos, transform.rotation);

        Destroy(instance, _particleLifetime);
    }

}
