using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemyHealthy;

    [SerializeField]
    private float movementSpeed;

    private int killReward;
    private int damage;

    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }
    void Start()
    {
        initializeEnemy();
    }

    public void takeDamage(float amount)
    {
        enemyHealthy -= amount;
        if(enemyHealthy <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }
    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetTile.transform.position,movementSpeed*Time.deltaTime);
    }

    private void checkPosition()
    {
        if(targetTile != null && targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if(distance < 0.001f)
            {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);
                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
        }
    }
    
    void Update()
    {
        checkPosition();
        moveEnemy();
    }
}
