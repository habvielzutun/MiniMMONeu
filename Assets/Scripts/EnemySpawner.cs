using Unity.Netcode;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyOne;
    [SerializeField] private Enemy enemyTwo;

    bool one = false;

    void Start()
    {
        
        InvokeRepeating(nameof(SpawnEnemy), 2f, 6f);
    }
    private void SpawnEnemy()
    {
        if (one == true)
        {
            Enemy instantiatedEnemy = Instantiate(enemyOne);
            instantiatedEnemy.GetComponent<NetworkObject>().Spawn();
            one = false;
        }
        else
        {
            Enemy instantiatedEnemy = Instantiate(enemyTwo);
            instantiatedEnemy.GetComponent<NetworkObject>().Spawn();
            one = true;
        }
        
    }
}