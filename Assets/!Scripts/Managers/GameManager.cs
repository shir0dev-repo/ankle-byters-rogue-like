using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    
    private Transform _playerTransform;

    public Vector3 PlayerPosition => _playerTransform.position;

    protected override void Awake()
    {
        base.Awake();
        
        _playerTransform = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity).transform;
        _playerTransform.gameObject.name = "Player";
    }
}
