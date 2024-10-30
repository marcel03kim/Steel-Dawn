using UnityEngine;

public class Computer : MonsterVehicle
{
    private float attackTimer; // ���� Ÿ�̸� ����

    protected override void Start()
    {
        base.Start(); // �θ� Ŭ������ Start �޼��带 ȣ��
        attackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
    }

    protected override void Move()
    {
        base.Move(); // �θ� Ŭ������ Move �޼��� ȣ��
    }

    protected override void Die()
    {
        base.Die(); // �θ� Ŭ������ Die �޼��� ȣ��
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();  // Player ������Ʈ ��������
            if (player != null)
            {
                attackTimer += Time.deltaTime; // Ÿ�̸� ������Ʈ
                // attackInterval���� �÷��̾�� ���ظ� ����
                if (attackTimer >= monsterData.attackInterval)
                {
                    player.currentHp -= power; // �÷��̾��� ü���� power��ŭ ����
                    attackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
                }
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackTimer = 0f; // �÷��̾���� �浹�� ������ Ÿ�̸� �ʱ�ȭ
        }
    }
}
