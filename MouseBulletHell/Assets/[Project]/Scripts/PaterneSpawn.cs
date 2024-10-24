using UnityEngine;

public class PaterneSpawn : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _spawnPerSecond = 1.5f;
    [SerializeField] private float _bulletSpeed = 1.5f;
    private float _spawnTime = 0;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;
        if (_spawnTime >= 1 / _spawnPerSecond)
        {
            _spawnTime = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, GetRandomPosAroundCamera(), Quaternion.identity);
        Destroy(newBullet.Initialize(_playerTransform, _bulletSpeed), 10f);
    }

    private Vector2 GetRandomPosAroundCamera()
    {
        float distance = Vector2.Distance(_camera.ScreenToWorldPoint(Vector3.zero), Vector2.zero) * 1.1f;
        return Random.insideUnitCircle.normalized * distance;
    }
}
