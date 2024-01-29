using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;

    public bool InCombat;

    public InsanityManager InsanityManager { get; private set; }
    public TimeTickManager TimeTickManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }

    public GameObject Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        TimeTickManager = gameObject.AddComponent<TimeTickManager>();
        InsanityManager = gameObject.AddComponent<InsanityManager>();
        CameraManager = gameObject.AddComponent<CameraManager>();

        Player = PlayerManager.SpawnPlayer(Vector3.zero);
        TimeTickManager.Create();
    }
}
