using UnityEngine;

public class GameValues : MonoBehaviour
{
    public GameModes GameMode = GameModes.Playing;

#if UNITY_EDITOR

    private GameModes oldMode = GameModes.Pause;

    private void Start()
    {
        oldMode = GameModes.Playing;

        EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);
        EventHandler.OnGameModeChanged?.Invoke(GameMode);
    }

    private void Update()
    {
        if (GameMode != oldMode)
        {
            GameMode = oldMode;

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