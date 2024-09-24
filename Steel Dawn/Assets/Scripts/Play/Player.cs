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
    public GameObject[] expSprite; // 경험치에 따른 스프라이트 배열
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 초기화
        levelUpManager = GetComponent<LevelUpManager>();
        UpdateExpSprites(); // 초기 스프라이트 설정
    }

    private void Update()
    {
        // 경험치에 따라 스프라이트 업데이트
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

    // 경험치에 따라 스프라이트를 업데이트하는 메서드
    private void UpdateExpSprites()
    {
        // 모든 스프라이트 비활성화
        foreach (GameObject sprite in expSprite)
        {
            sprite.SetActive(false);
        }

        // 경험치에 따라 스프라이트 활성화 (10 단위로 증가)
        int numSpritesToActivate = (int)exp / 10;

        // 활성화할 스프라이트 개수만큼 반복
        for (int i = 0; i < numSpritesToActivate && i < expSprite.Length; i++)
        {
            expSprite[i].SetActive(true);
        }
    }

}
