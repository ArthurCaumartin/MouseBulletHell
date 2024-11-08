using Unity.VisualScripting;
using UnityEngine;

public class PaterneSpawn : MonoBehaviour
{
    [SerializeField] private Transform _positifBallTransform;
    [SerializeField] private Transform _negatifBallTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _zonePrefab;
    [SerializeField] private float _zoneSpawnChance = .2f;
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
            SpawnElement(Random.value < _zoneSpawnChance ? _zonePrefab : _bulletPrefab);
        }
    }

    private void SpawnElement(GameObject element)
    {
        Bullet bullet = element.GetComponent<Bullet>();
        if (bullet)
        {
            Bullet newBullet = Instantiate(bullet, GetRandomPosAroundCamera(), Quaternion.identity);
            Destroy(newBullet.Initialize(_positifBallTransform, _bulletSpeed).gameObject, 10f);
            return;
        }

        Zone zone = element.GetComponent<Zone>();
        if (zone)
        {
            Vector2 spawnPoint = Random.value > .5f ? _positifBallTransform.position : _negatifBallTransform.position;
            Zone newZone = Instantiate(zone, spawnPoint, Quaternion.identity);
            newZone.Initialize(_bulletSpeed);
            return;
        }
    }

    private Vector2 GetRandomPosAroundCamera()
    {
        float distance = Vector2.Distance(_camera.ScreenToWorldPoint(Vector3.zero), Vector2.zero) * 1.1f;
        return Random.insideUnitCircle.normalized * distance;
    }
}
