    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using System;

public class EnemyWaweManager : MonoBehaviour
    {
        public GameObject [] enemyTypes;
        private List<Vector2Int> pathCells;
        public List<GameObject> enemyInstances;
        private Dictionary<GameObject, int> enemyPathIndices;
        public int enemiesPerWave;
        public int waveNumber=0;
        private bool allEnemiesSpawned;
        public TextMeshProUGUI waveText;
        public bool gameStarted;
        public GameObject bossPrefab;
        public GameObject button;

        // Start is called before the first frame update
    void Start()
    {
        waveText.enabled=false;
        enemyInstances = new List<GameObject>();
        enemyPathIndices = new Dictionary<GameObject, int>();
        
    }



        // Update is called once per frame
    private int nextPathCellIndex = 0;
    void Update()
    {
        if (enemyInstances.Count!=0&&pathCells != null && pathCells.Count > 0)
        {
            MoveEnemy();
        }
        if(enemyInstances.Count==0 && gameStarted)
        {
            WaweEnd();
        }
    }


    void MoveEnemy()
    {
        
    for (int i = 0; i < enemyInstances.Count; i++)
    {
        GameObject enemy = enemyInstances[i];
        int pathIndex = enemyPathIndices[enemy];
        Vector3 currentPos = enemy.transform.position;
        Vector3 nextPos = new Vector3(pathCells[pathIndex].x, 0.6f, pathCells[pathIndex].y);
        enemy.transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * enemy.GetComponent<Enemy>().speed);
        if (Vector3.Distance(currentPos, nextPos) < 0.05f)
        {
            pathIndex++;
            if (pathIndex >= pathCells.Count)
            {
                HealthManager playerHealth = GetComponent<HealthManager>();
                playerHealth.TakeDamage(10);
                Debug.Log("can azaldı");
                // The enemy has reached the end of the path, so destroy the enemy object
                Destroy(enemy);
                // Remove the enemy from the enemyInstances list and enemyPathIndices dictionary
                enemyInstances.RemoveAt(i);
                enemyPathIndices.Remove(enemy);
                // Decrement the loop counter to compensate for the removed element
                i--;
            }
            else
            {
                enemyPathIndices[enemy] = pathIndex;
            }
        }
    }
}

public void StartWave()
{
        button.SetActive(false);
        waveText.enabled=true;
        gameStarted = true;
        StartCoroutine(SpawnWave());

    
    
}

public IEnumerator SpawnWave()
{

    waveNumber++;
    waveText.text="Wave : "+(waveNumber);
    yield return new WaitForSeconds(0.6f);
    waveText.enabled=false;
    enemiesPerWave=waveNumber*2+1;
    int waveEnemyCount = enemiesPerWave;
    if (waveNumber % 5 == 0)
    {
        for (int i = 0; i < waveNumber/5; i++)
        {
            SpawnBoss();
        }
    }
    else
    {
        while (waveEnemyCount > 0)
        {
            SpawnEnemy();
            waveEnemyCount--;
            yield return new WaitForSeconds(0.75f);
        }
    }
}

void SpawnBoss()
{
    GameObject boss = Instantiate(bossPrefab, new Vector3(0, 0.8f, 5), Quaternion.identity);
    enemyInstances.Add(boss);
    enemyPathIndices[boss] = 0;
}


    void WaweEnd()
    {
        MoneyManager moneyManager=GetComponent<MoneyManager>();
        moneyManager.EarnMoney(waveNumber*25);
        gameStarted=false;
        button.SetActive(true);
        
        
    }

    void SpawnEnemy()
    {
        int randomEnemyIndex = UnityEngine.Random.Range(0, enemyTypes.Length);
        GameObject enemy = Instantiate(enemyTypes[randomEnemyIndex], new Vector3(0, 0.6f, 5), Quaternion.identity);
        enemyInstances.Add(enemy);
        enemyPathIndices[enemy] = 0;
        // Set the allEnemiesSpawned flag to true if the required number of enemies has been spawned
        if (enemyInstances.Count == enemiesPerWave)
        {
            allEnemiesSpawned = true;
        }
    }

    public void SetPathCells(List<Vector2Int> pathCells)
        {
            this.pathCells = pathCells;
        }
}
