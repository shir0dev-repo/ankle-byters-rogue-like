using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    private Transform _playerTransform;

    public Vector3 PlayerPosition => _playerTransform.position;

    protected override void Awake()
    {
        _playerTransform = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity).transform;
    }
}
