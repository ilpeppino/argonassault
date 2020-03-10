using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{

    #region Singleton 

    // Instance variable of the class
    private static AudioManager _audioManager;

    // Constructor 
    public static AudioManager Instance
    {
        get
        {
            // If there is no instance of Audio Manager, then it creates one 
            if (_audioManager == null)
            {

                Debug.Log("New AudioManager instance being created");
                _audioManager = new GameObject().AddComponent<AudioManager>();
                
            }

            return _audioManager;
        }
    }

    private void Awake()
    {

        // Destroy the "new" instance if one is already present
        if (_audioManager != null)
        {
            Debug.Log("Destroying new AudioManager");
            Destroy(this);
        } else
        
        Debug.Log("This: " + this);
        DontDestroyOnLoad(this);
        


    }

    #endregion


    private void Start()
    {
        
        // Singleton for Audio Manager

        if (this != null)
        {
            Destroy(this);
            return;
        }  
        
        DontDestroyOnLoad(this);

    }


}
