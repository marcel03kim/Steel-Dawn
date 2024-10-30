using UnityEngine;

public class TutorialMonster : MonsterVehicle
{
    protected override void Start()
    {
        base.Start(); // �θ� Ŭ������ Start �޼��带 ȣ��
    }

    protected override void Move()
    {
        base.Move(); // �θ� Ŭ������ Move �޼��� ȣ��
    }

    protected override void Die()
    {
        base.Die(); // �θ� Ŭ������ Die �޼��� ȣ��
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();  // Player ������Ʈ ��������
            if (player != null)
            {
                player.currentHp -= power; // �÷��̾��� ü���� power��ŭ ����
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();  // Player ������Ʈ ��������
            if (player != null)
            {
                player.currentHp -= power; // �÷��̾��� ü���� power��ŭ ����
            }
        }
    }

}
