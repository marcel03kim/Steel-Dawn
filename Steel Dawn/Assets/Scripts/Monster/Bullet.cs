using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2.0f; //ź�� �̵� �ӷ�
    public int power;
    private Rigidbody2D rb;      //�̵��� ����� ������ٵ� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        //���� ������Ʈ���� Rigidbody ������Ʈ�� ã�� rb�� �Ҵ�
        rb = GetComponent<Rigidbody2D>();
        // ������ٵ��� �ӵ� = ������Ʈ�� �������� ���� ���� * �̵��ӷ�
        rb.velocity = transform.forward * speed;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        //3�� �ڿ� �ڽ��� ���� ������Ʈ �ı�
        Destroy(gameObject, 5f);
    }

    //Ʈ���� �浹 �� �ڵ����� ����Ǵ� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            gameObject.SetActive(false);
            //���� ���� ������Ʈ���� Player_controller ������Ʈ ��������
            Player player = collision.GetComponent<Player>();

            //�������κ��� Player_controller ������Ʈ�� �������� �� �����ߴٸ�
            if (player != null)
            {
                //���� Player_controller ������Ʈ�� Die() �޼��� ����
                player.Hp -= power;
            }
        }
    }
}
