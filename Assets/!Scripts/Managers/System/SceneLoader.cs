using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
  public const int TITLE_SCENE_INDEX = 0;
  public const int PLAY_SCENE_INDEX = 1;
  public const int GAME_OVER_SCENE_INDEX = 2;

  public int CurrentSceneIndex { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
  }

  public void LoadScene(int index)
  {
    SceneManager.LoadScene(index);
    CurrentSceneIndex = index;
  }

}