using UnityEngine;

public class PaterneSpawn : MonoBehaviour
{
    [SerializeField] private Transform _positifBallTransform;
    [SerializeField] private Transform _negatifBallTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _zonePrefab;
    [SerializeField] private float _zoneSpawnChance = .2f;
    [SerializeField] private float _bulletSpeed = 1.5f;

    [Header("Scale on Player Score :")]
    [SerializeField] private float _minScore = 50;
    [SerializeField] private float _maxScore = 500;
    [Space]
    [SerializeField] private float _minSpeed = .5f;
    [SerializeField] private float _maxSpeed = 2;
    [Space]
    [SerializeField] private float _minSpawnRate = .5f;
    [SerializeField] private float _maxSpawnRate = 2;
    private float _currentSpeedMult;
    private float _currentSpawnPerSecond;
    private float _spawnTime = 0;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

    }

    private void Update()
    {
        ComputeRemapOnScore();

        _spawnTime += Time.deltaTime;
        if (_spawnTime >= 1 / _currentSpawnPerSecond)
        {
            _spawnTime = 0;
            SpawnElement(Random.value < _zoneSpawnChance ? _zonePrefab : _bulletPrefab);
        }
    }

    public void ComputeRemapOnScore()
    {
        float currentScore = GameManager.instance.Score;
        float scoreTime = Mathf.InverseLerp(_minScore, _maxScore, currentScore);
        // float scoreTime = (currentScore - _minScore) / (_maxScore - _minScore);

        _currentSpawnPerSecond = Mathf.LerpUnclamped(_minSpawnRate, _maxSpawnRate, scoreTime);
        _currentSpeedMult = Mathf.LerpUnclamped(_minSpeed, _maxSpeed, scoreTime);
    }

    private void SpawnElement(GameObject element)
    {
        Bullet bullet = element.GetComponent<Bullet>();
        if (bullet)
        {
            Bullet newBullet = Instantiate(bullet, GetRandomPosAroundCamera(), Quaternion.identity);
            Destroy(newBullet.Initialize(_positifBallTransform, _bulletSpeed * _currentSpeedMult).gameObject, 10f);
            return;
        }

        Zone zone = element.GetComponent<Zone>();
        if (zone)
        {
            Vector2 spawnPoint = Random.value > .5f ? _positifBallTransform.position : _negatifBallTransform.position;
            Zone newZone = Instantiate(zone, spawnPoint, Quaternion.identity);
            newZone.Initialize(_bulletSpeed * _currentSpeedMult);
            return;
        }
    }

    private Vector2 GetRandomPosAroundCamera()
    {
        float distance = Vector2.Distance(_camera.ScreenToWorldPoint(Vector3.zero), Vector2.zero) * 1.1f;
        return Random.insideUnitCircle.normalized * distance;
    }


}
