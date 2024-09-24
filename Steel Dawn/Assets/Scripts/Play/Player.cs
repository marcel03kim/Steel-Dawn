using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp;
    private float speed = 1.5f;
    public int level = 0;
    public float exp = 0;
    public float power;
    public float defense;
    private bool isLevelUp = false;

    private LevelUpManager levelUpManager;
    public GameObject[] expSprite; // ����ġ�� ���� ��������Ʈ �迭
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ �ʱ�ȭ
        levelUpManager = GetComponent<LevelUpManager>();
        UpdateExpSprites(); // �ʱ� ��������Ʈ ����
    }

    private void Update()
    {
        // ����ġ�� ���� ��������Ʈ ������Ʈ
        UpdateExpSprites();
        if(!isLevelUp && exp >= 190)
        {
            isLevelUp = true;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level += 1;
        exp = 0;
        isLevelUp = false;
        levelUpManager.GetComponent<LevelUpManager>().PlayerLevelUp();
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
        foreach (GameObject sprite in expSprite)
        {
            sprite.SetActive(false);
        }

        // ����ġ�� ���� ��������Ʈ Ȱ��ȭ (10 ������ ����)
        int numSpritesToActivate = (int)exp / 10;

        // Ȱ��ȭ�� ��������Ʈ ������ŭ �ݺ�
        for (int i = 0; i < numSpritesToActivate && i < expSprite.Length; i++)
        {
            expSprite[i].SetActive(true);
        }
    }

}
