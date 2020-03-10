using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject particleExplosion;

    float _loadLevelDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {

        HandleCollision(collision.gameObject.name);
        particleExplosion.SetActive(true);
        Invoke("ReloadScene", _loadLevelDelay);

    }

    private void HandleCollision(string collidedGameObjectName)
    {
        SendMessage("OnPlayerCollision");
        Debug.Log("Player collided with " + collidedGameObjectName);
        

    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
