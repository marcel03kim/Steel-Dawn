using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp;
    private float speed = 1.5f;
    public float exp;
    public float power;
    public float defense;
    public Sprite[] expSprite; // ����ġ�� ���� ��������Ʈ �迭
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ �ʱ�ȭ
        UpdateExpSprites(); // �ʱ� ��������Ʈ ����
    }

    private void Update()
    {
        // ����ġ�� ���� ��������Ʈ ������Ʈ
        UpdateExpSprites();
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb.velocity = movement * speed;
    }

    // ����ġ�� ���� ��������Ʈ�� ������Ʈ�ϴ� �޼���
    private void UpdateExpSprites()
    {
        // ��� ��������Ʈ ��Ȱ��ȭ
        spriteRenderer.sprite = null; // �⺻������ ��������Ʈ�� ��Ȱ��ȭ

        // ����ġ�� ���� Ư�� ��������Ʈ Ȱ��ȭ
        switch ((int)exp)
        {
            case 0:
                // �ƹ��͵� ���� ����
                break;
            case 10:
                spriteRenderer.sprite = expSprite[0]; // ù ��° ��������Ʈ Ȱ��ȭ
                break;
            case 20:
                spriteRenderer.sprite = expSprite[1]; // �� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 30:
                spriteRenderer.sprite = expSprite[2]; // �� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 40:
                spriteRenderer.sprite = expSprite[3]; // �� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 50:
                spriteRenderer.sprite = expSprite[4]; // �ټ� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 60:
                spriteRenderer.sprite = expSprite[5]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 70:
                spriteRenderer.sprite = expSprite[6]; // �ϰ� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 80:
                spriteRenderer.sprite = expSprite[7]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 90:
                spriteRenderer.sprite = expSprite[8]; // ��ȩ ��° ��������Ʈ Ȱ��ȭ
                break;
            case 100:
                spriteRenderer.sprite = expSprite[9]; // �� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 110:
                spriteRenderer.sprite = expSprite[10]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 120:
                spriteRenderer.sprite = expSprite[11]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 130:
                spriteRenderer.sprite = expSprite[12]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 140:
                spriteRenderer.sprite = expSprite[13]; // ���� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 150:
                spriteRenderer.sprite = expSprite[14]; // ���ټ� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 160:
                spriteRenderer.sprite = expSprite[15]; // ������ ��° ��������Ʈ Ȱ��ȭ
                break;
            case 170:
                spriteRenderer.sprite = expSprite[16]; // ���ϰ� ��° ��������Ʈ Ȱ��ȭ
                break;
            case 180:
                spriteRenderer.sprite = expSprite[17]; // ������ ��° ��������Ʈ Ȱ��ȭ
                break;
            default:
                // 180 �̻��� ��쿡�� ��������Ʈ�� �������� ����
                break;
        }
    }
}
