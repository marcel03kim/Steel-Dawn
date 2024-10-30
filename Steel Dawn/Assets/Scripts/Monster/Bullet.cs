using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // �Ѿ� �ӵ�
    public float power; // �Ѿ��� ���ݷ�
    private Vector2 direction; // �Ѿ��� ����

    private void Update()
    {
        // �Ѿ��� �߻�� �������� �̵�
        transform.Translate(direction * speed * Time.deltaTime);
        Destroy(gameObject, 10f); // 10�� �Ŀ� �Ѿ� �ı�
    }

    // ���� ���� �޼���
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾�� �������� �ִ� ���� �߰�
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.currentHp -= power; // �÷��̾� ü�� ����
            }

            // �Ѿ��� �浹�� �� �ı�
            Destroy(gameObject);
        }
    }
}
