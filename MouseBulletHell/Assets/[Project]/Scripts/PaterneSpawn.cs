using Unity.VisualScripting;
using UnityEngine;

public class PaterneSpawn : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _laserPrefab;
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
            SpawnElement(Random.value > .5f ? _bulletPrefab : _laserPrefab);
        }
    }

    private void SpawnElement(GameObject element)
    {
        Bullet bullet = element.GetComponent<Bullet>();
        if (bullet)
        {
            Bullet newBullet = Instantiate(bullet, GetRandomPosAroundCamera(), Quaternion.identity);
            Destroy(newBullet.Initialize(_playerTransform, _bulletSpeed).gameObject, 10f);
            return;
        }

        Laser laser = element.GetComponent<Laser>();
        if (laser)
        {
            Laser newLaser = Instantiate(laser, GetRandomPosAroundCamera(), Quaternion.identity);
            newLaser.Initialize(_playerTransform, _bulletSpeed);
            return;
        }
    }

    private Vector2 GetRandomPosAroundCamera()
    {
        float distance = Vector2.Distance(_camera.ScreenToWorldPoint(Vector3.zero), Vector2.zero) * 1.1f;
        return Random.insideUnitCircle.normalized * distance;
    }
}
