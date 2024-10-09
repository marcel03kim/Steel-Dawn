using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Data", menuName = "ScriptableObjects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public enum MonsterState
    {
        Move,
        Attack,
        Die
    }

    public MonsterState initialState; // �ʱ� ���¸� ������ �� �ִ� ����
    public float hp;       // ���� ü��
    public float speed;    // ���� �ӵ�
    public float power;    // ���� ���ݷ�
    public float attackInterval; // ���� �ӵ�
    public GameObject expPrefab; // ����ġ ������
    public GameObject goldPrefab; // ��� ������
}
