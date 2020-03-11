using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    [SerializeField] GameObject enemyExplosion;

    ScoreHandler scoreHandler;

    private void Awake()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particles hit " + other.name);
        scoreHandler.OnEnemyDestroyed(5);
        GameObject explosion = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
