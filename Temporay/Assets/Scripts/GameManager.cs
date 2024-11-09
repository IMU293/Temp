using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject monsterPrefab;
    public GameObject monsterPrefab2;
    public GameObject bossPrefab; // 보스 몬스터 프리팹

    public int numberOfMonsters = 5; // 한 번에 생성할 몬스터 수
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 20f;
    public float spawnInterval = 5f; // 몬스터 생성 주기
    public float spawnInterval_Boss = 300f;

    private float spawnTimer = 0f; // 경과 시간 추적
    private float spawnTimer_Boss = 0f;//보스 몬스터 출현시간
    private bool bossSpawned = false; // 보스 생성 여부 확인

    public string nextSceneName;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 경과 시간을 추적하고 spawnInterval에 도달하면 몬스터 소환
        spawnTimer += Time.deltaTime;
        spawnTimer_Boss += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnMonsters();
            spawnTimer = 0f; // 타이머 초기화
        }
        if (!bossSpawned)
        {
            spawnTimer_Boss += Time.deltaTime;

            if (spawnTimer_Boss >= spawnInterval_Boss)
            {
                SpawnBoss();
                bossSpawned = true; // 보스가 한 번만 생성되도록 설정
            }
        }
    }
    // 여러 스폰 포인트 생성 후 몬스터 생성
    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            Vector3 spawnPosition = GenerateRandomSpawnPoint();
            int monsterType = Random.Range(0, 2);
            if (monsterType == 0)
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            if (monsterType == 1)
                Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
        }
    }
    void SpawnBoss()
    {
        Vector3 spawnPosition = GenerateRandomSpawnPoint();
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Boss Monster Spawned!");
    }
    // 플레이어로부터 일정 거리 떨어진 랜덤 스폰 포인트 생성
    Vector3 GenerateRandomSpawnPoint()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        return player.transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * randomDistance;
    }
    // 조건을 만족할 때 다음 씬으로 전환
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
