using UnityEngine;

public class GameValues : MonoBehaviour
{
    public GameModes GameMode = GameModes.Playing;

#if UNITY_EDITOR

    private GameModes oldMode;

    private void Start()
    {
        GameMode = GameModes.Playing;

        EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);
        EventHandler.OnGameModeChanged?.Invoke(GameMode);
    }

    private void Update()
    {
        if (GameMode != oldMode)
        {
            GameMode = oldMode;

            //ChangeGameMode(GameMode);

            EventHandler.OnGameModeChanged?.Invoke(GameMode);
        }
    }

#endif

    public void ChangeGameMode(GameModes mode)
    {
        switch (mode)
        {
            case GameModes.Playing:
                Time.timeScale = 1;
                break;
            case GameModes.Pause:
                Time.timeScale = 0;
                break;
        }
    }
}

public enum GameModes
{
    Playing, Pause
}