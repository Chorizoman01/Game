using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int deaths { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }


    private void NewGame()
    {
        deaths = 0;
        LoadLevel(2);
    }

    public void LoadLevel(int world)
    {
        this.world = world;
        SceneManager.LoadScene($"{world}"); // Load by index
    }

    //public void Resetlvl(float delay)
    //{
    //    Invoke(nameof(Resetlvl), delay);
    //}

    public void Resetlvl()
    {
        
        LoadLevel(world);
        deaths+=1;
    }

    public void Nextlvl()
    {
        LoadLevel(world + 1);
    }
}
