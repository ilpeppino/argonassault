using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Local variables

    private float _sceneTransitionTime = 5f;
    private int _sceneIndex;

    #endregion

    #region Singleton construction

    private static LevelManager _levelManager;

    public static LevelManager Instance
    {
        get
        {
            if (_levelManager == null)
            {
                _levelManager = new GameObject().AddComponent<LevelManager>();
            }

            return _levelManager;
        }
    }

    private void Awake()
    {
        
        if (_levelManager != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

    }

    #endregion

    void Start()
    {

        // Initializes index on loaded scene
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene index: " + _sceneIndex);

        // Only for splash screen, next scene is loaded after a delay
        if (_sceneIndex == 0 )
        {
            Invoke("LoadNextScene", _sceneTransitionTime);
        }
    }

    private void LoadNextScene()
    {
        // Check if the following scene exists
        if(SceneManager.GetSceneByBuildIndex(_sceneIndex + 1) == null)
        {
            Debug.Log("Scene loading error with build index " + _sceneIndex + 1);
            return;
        }

        // Loads the following the scene
        _sceneIndex++;
        SceneManager.LoadScene(_sceneIndex);
        Debug.Log("Scene index: " + _sceneIndex);
        
    }



}
