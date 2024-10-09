using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;  // ���� ������ ����Ʈ
    public GameObject player;  // �÷��̾� ������Ʈ
    public float spawnInterval = 5f;  // ���� ���͹� (�� ����)
    private float spawnRadius = 20f;  // �÷��̾� �ֺ� �ݰ�
    private float spawnDistance = 30f;  // ���� ���� ��ġ(�÷��̾���� �Ÿ�)
    public float playTime;  // PlayTime ������ ���� ����

    private void Start()
    {
        // ���� �ݺ� ����
        StartCoroutine(SpawnMonsters());
    }

    private void Update()
    {
        // PlayTime�� �� ������ ������Ű�� (���� �ð��� ���)
        playTime += Time.deltaTime;
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);  // ���� ���͹� ���

            // �÷��� Ÿ�ӿ� ���� ���� ���� ����
            int monsterCount = GetMonsterCountBasedOnPlayTime(playTime);

            // ���� ����
            SpawnRandomMonsters(monsterCount);
        }
    }

    // �÷��� Ÿ�ӿ� ���� ������ ���� ���� ����
    private int GetMonsterCountBasedOnPlayTime(float playTime)
    {
        if (playTime < 60f)  // 1�� �̸�
        {
            return 10;
        }
        else if (playTime >= 60f && playTime < 180f)  // 1�� �̻�, 3�� �̸�
        {
            return 20;
        }
        else  // 3�� �̻�
        {
            return 50;
        }
    }

    // ���� ����Ʈ���� ������ ������ ���͸� �����ϴ� �Լ�
    private void SpawnRandomMonsters(int totalCount)
    {
        if (monsterPrefabs.Count == 0 || player == null) return;

        // ���ͺ� ���� �� ������ ���� ����Ʈ
        int[] monsterCounts = new int[monsterPrefabs.Count];

        // �����ϰ� �� ���ͺ� ������ ����
        for (int i = 0; i < totalCount; i++)
        {
            int randomMonsterIndex = Random.Range(0, monsterPrefabs.Count);
            monsterCounts[randomMonsterIndex]++;
        }

        // ���� ������ŭ ���͸� ����
        for (int i = 0; i < monsterCounts.Length; i++)
        {
            for (int j = 0; j < monsterCounts[i]; j++)
            {
                // ������ ���� ��ġ ���
                Vector2 spawnPosition = GetRandomSpawnPosition();

                // ���� ����
                Instantiate(monsterPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // �÷��̾� ��ġ ��������
        Vector2 playerPosition = player.transform.position;

        // �ִ� �Ÿ��� ����
        float distance = Mathf.Min(spawnRadius, spawnDistance);  // �ݰ�� �ִ� �Ÿ� �� �ּҰ� ���

        // �ݰ� ������ ���� ���� ����
        float angle = Random.Range(0f, Mathf.PI * 2);  // 360�� ������ ����

        // ���� ��ġ ��� (Polar to Cartesian ��ȯ)
        Vector2 spawnPosition = new Vector2(
            playerPosition.x + Mathf.Cos(angle) * distance,
            playerPosition.y + Mathf.Sin(angle) * distance
        );

        return spawnPosition;
    }
}
