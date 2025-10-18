using UnityEngine;
using System.Collections.Generic;

public class SpawnTrigger : MonoBehaviour
{
    [Tooltip("Список спавнеров, которые запустятся при входе игрока в триггер")]
    public List<EnemySpawner> spawners = new List<EnemySpawner>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (EnemySpawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.StartSpawn();
                }
            }
        }
    }
}
