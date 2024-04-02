using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    public bool InGame;
    public bool GamePaused;
    public bool InCombat;

    public InsanityManager InsanityManager { get; private set; }
    public TimeTickManager TimeTickManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }

    public GameObject Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        //PlayerManager = gameObject.AddComponent<PlayerManager>();
        //TimeTickManager = gameObject.AddComponent<TimeTickManager>();
        //InsanityManager = gameObject.AddComponent<InsanityManager>();
        //CameraManager = gameObject.AddComponent<CameraManager>();

        //Player = PlayerManager.SpawnPlayer(Vector3.zero);
        //TimeTickManager.Create();
    }

    public void StartGame()
    {
        if (SceneLoader.Instance.CurrentSceneIndex == SceneLoader.PLAY_SCENE_INDEX || InGame)
            return;

        InGame = true;
        UIManager.Instance.SetUI(false, false, false);

        SceneLoader.Instance.LoadScene(SceneLoader.PLAY_SCENE_INDEX);

        //Player manager starts disabled and is awoken by gamemanger
        PlayerManager.Instance.StartGame();
        //See insanity manager
        InsanityManager.Instance.IncrementOverTime();
        //Added Call to ShadowManager's PlaceShadows() function
        ShadowManager.Instance.PlaceShadows();
    }

    public void PauseGame()
    {
        if (SceneLoader.Instance.CurrentSceneIndex != SceneLoader.PLAY_SCENE_INDEX || !InGame)
            return;

        GamePaused = !GamePaused;
        Time.timeScale = GamePaused ? 0 : 1; // currently paused : currently playing
        UIManager.Instance.DisplaySettingsMenu(GamePaused);
    }

    public void GameOver()
    {
        if (SceneLoader.Instance.CurrentSceneIndex == SceneLoader.GAME_OVER_SCENE_INDEX)
            return;

        InGame = false;
        GamePaused = false;

        UIManager.Instance.DisplayGameOverMenu(true);
        SceneLoader.Instance.LoadScene(SceneLoader.GAME_OVER_SCENE_INDEX);
    }
    
    // setter for GameManagers player which is used by ShadowManager
    public void SetPlayer(GameObject player) { Player = player; }

    public static void Quit(bool toMenu)
    {
        if (toMenu)
        {
            SceneLoader.Instance.LoadScene(SceneLoader.TITLE_SCENE_INDEX);
            UIManager.Instance.SetUI(true, false, false);

            return;
        }
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
