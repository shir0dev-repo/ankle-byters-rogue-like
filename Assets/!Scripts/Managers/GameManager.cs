using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    
    public InsanityManager InsanityManager { get; private set; }
    public TimeTickManager TimeTickManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        TimeTickManager = gameObject.AddComponent<TimeTickManager>();
        InsanityManager = gameObject.AddComponent<InsanityManager>();

        PlayerManager.SpawnPlayer(Vector3.zero);
        TimeTickManager.Create();
    }
}
