using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;  // 몬스터 프리팹 리스트
    public GameObject player;  // 플레이어 오브젝트
    public float spawnInterval = 5f;  // 스폰 인터벌 (초 단위)
    private float spawnRadius = 20f;  // 플레이어 주변 반경
    private float spawnDistance = 30f;  // 몬스터 생성 위치(플레이어와의 거리)
    public float playTime;  // PlayTime 꼐산을 위한 변수

    private void Start()
    {
        // 스폰 반복 시작
        StartCoroutine(SpawnMonsters());
    }

    private void Update()
    {
        // PlayTime을 매 프레임 증가시키기 (게임 시간에 비례)
        playTime += Time.deltaTime;
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);  // 스폰 인터벌 대기

            // 플레이 타임에 따른 몬스터 개수 결정
            int monsterCount = GetMonsterCountBasedOnPlayTime(playTime);

            // 몬스터 스폰
            SpawnRandomMonsters(monsterCount);
        }
    }

    // 플레이 타임에 따라 스폰할 몬스터 개수 결정
    private int GetMonsterCountBasedOnPlayTime(float playTime)
    {
        if (playTime < 60f)  // 1분 미만
        {
            return 10;
        }
        else if (playTime >= 60f && playTime < 180f)  // 1분 이상, 3분 미만
        {
            return 20;
        }
        else  // 3분 이상
        {
            return 50;
        }
    }

    // 몬스터 리스트에서 랜덤한 비율로 몬스터를 생성하는 함수
    private void SpawnRandomMonsters(int totalCount)
    {
        if (monsterPrefabs.Count == 0 || player == null) return;

        // 몬스터별 스폰 수 저장을 위한 리스트
        int[] monsterCounts = new int[monsterPrefabs.Count];

        // 랜덤하게 각 몬스터별 개수를 결정
        for (int i = 0; i < totalCount; i++)
        {
            int randomMonsterIndex = Random.Range(0, monsterPrefabs.Count);
            monsterCounts[randomMonsterIndex]++;
        }

        // 계산된 개수만큼 몬스터를 스폰
        for (int i = 0; i < monsterCounts.Length; i++)
        {
            for (int j = 0; j < monsterCounts[i]; j++)
            {
                // 무작위 스폰 위치 계산
                Vector2 spawnPosition = GetRandomSpawnPosition();

                // 몬스터 생성
                Instantiate(monsterPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // 플레이어 위치 가져오기
        Vector2 playerPosition = player.transform.position;

        // 최대 거리로 설정
        float distance = Mathf.Min(spawnRadius, spawnDistance);  // 반경과 최대 거리 중 최소값 사용

        // 반경 내에서 랜덤 방향 생성
        float angle = Random.Range(0f, Mathf.PI * 2);  // 360도 무작위 방향

        // 랜덤 위치 계산 (Polar to Cartesian 변환)
        Vector2 spawnPosition = new Vector2(
            playerPosition.x + Mathf.Cos(angle) * distance,
            playerPosition.y + Mathf.Sin(angle) * distance
        );

        return spawnPosition;
    }
}
