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
    public Sprite[] expSprite; // 경험치에 따른 스프라이트 배열
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 초기화
        UpdateExpSprites(); // 초기 스프라이트 설정
    }

    private void Update()
    {
        // 경험치에 따라 스프라이트 업데이트
        UpdateExpSprites();
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
        spriteRenderer.sprite = null; // 기본적으로 스프라이트를 비활성화

        // 경험치에 따라 특정 스프라이트 활성화
        switch ((int)exp)
        {
            case 0:
                // 아무것도 하지 않음
                break;
            case 10:
                spriteRenderer.sprite = expSprite[0]; // 첫 번째 스프라이트 활성화
                break;
            case 20:
                spriteRenderer.sprite = expSprite[1]; // 두 번째 스프라이트 활성화
                break;
            case 30:
                spriteRenderer.sprite = expSprite[2]; // 세 번째 스프라이트 활성화
                break;
            case 40:
                spriteRenderer.sprite = expSprite[3]; // 네 번째 스프라이트 활성화
                break;
            case 50:
                spriteRenderer.sprite = expSprite[4]; // 다섯 번째 스프라이트 활성화
                break;
            case 60:
                spriteRenderer.sprite = expSprite[5]; // 여섯 번째 스프라이트 활성화
                break;
            case 70:
                spriteRenderer.sprite = expSprite[6]; // 일곱 번째 스프라이트 활성화
                break;
            case 80:
                spriteRenderer.sprite = expSprite[7]; // 여덟 번째 스프라이트 활성화
                break;
            case 90:
                spriteRenderer.sprite = expSprite[8]; // 아홉 번째 스프라이트 활성화
                break;
            case 100:
                spriteRenderer.sprite = expSprite[9]; // 열 번째 스프라이트 활성화
                break;
            case 110:
                spriteRenderer.sprite = expSprite[10]; // 열한 번째 스프라이트 활성화
                break;
            case 120:
                spriteRenderer.sprite = expSprite[11]; // 열두 번째 스프라이트 활성화
                break;
            case 130:
                spriteRenderer.sprite = expSprite[12]; // 열세 번째 스프라이트 활성화
                break;
            case 140:
                spriteRenderer.sprite = expSprite[13]; // 열네 번째 스프라이트 활성화
                break;
            case 150:
                spriteRenderer.sprite = expSprite[14]; // 열다섯 번째 스프라이트 활성화
                break;
            case 160:
                spriteRenderer.sprite = expSprite[15]; // 열여섯 번째 스프라이트 활성화
                break;
            case 170:
                spriteRenderer.sprite = expSprite[16]; // 열일곱 번째 스프라이트 활성화
                break;
            case 180:
                spriteRenderer.sprite = expSprite[17]; // 열여덟 번째 스프라이트 활성화
                break;
            default:
                // 180 이상의 경우에는 스프라이트를 설정하지 않음
                break;
        }
    }
}
