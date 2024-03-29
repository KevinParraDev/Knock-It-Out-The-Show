using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private int _currentLevel = 1;

    public static GameManager Instance;

    public bool twoPlayers = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // No destruir el GM durante el cambio de escenas
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

     private void Update()
     {
          Cursor.visible = false;

          if(Input.GetButtonDown("Fire2"))
               twoPlayers = !twoPlayers;
     }

     private void Start()
    {
        AudioManager.Instance.SetVolume(0.2f, AudioChannel.Sfx);
        AudioManager.Instance.SetVolume(0.8f, AudioChannel.Music);
        AudioManager.Instance.PlayMusic(AudioMusicType.Menu);
    }

    public void GameStart()
    {
        
        LoadLevel(1);
    }

    public void HandleMenu()
    {
        // El nivel 0 es el men�
        HandleLevelChange(0);
            
    }

    public void HandleLevelChange(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void HandleLevelCompleted()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        _currentLevel++;

        if (_currentLevel <= SceneManager.sceneCountInBuildSettings - 1)
        {
            // Se carga la escena del siguiente nivel
            HandleLevelChange(_currentLevel);
        }

        else
        {
            HandleMenu();
            _currentLevel = 1;
        }
    }

    public void LoadLevel(int level)
    {
        HandleLevelChange(level);
    }

    public void ReloadLevel()
    {
        HandleLevelChange(_currentLevel);
    }

    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    
}
