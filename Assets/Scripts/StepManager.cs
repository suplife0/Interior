using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepManager : MonoBehaviour
{
    public static StepManager Instance;


    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
