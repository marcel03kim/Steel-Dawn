using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float Hp;
    public float speed;
    public float power;
    public float defense;
    public int level = 0;
    public float exp = 0;

    public GameObject playerStartSetting;

    public GameObject gameOver;
    public Camera cam;

    private bool isMove;
    private bool isLevelUp = false;
    private Animator anim;
    private LevelUpManager levelUpManager;
    public GameObject[] expSprite; // 경험치에 따른 스프라이트 배열
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        isMove = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 초기화
        levelUpManager = GetComponent<LevelUpManager>();
        UpdateExpSprites(); // 초기 스프라이트 설정
        gameOver.SetActive(false);

        Hp = playerStartSetting.GetComponent<PlayerUpgrade>().StartHp;
        speed = playerStartSetting.GetComponent<PlayerUpgrade>().StartSpeed;
        power = playerStartSetting.GetComponent<PlayerUpgrade>().StartPower;
        defense = playerStartSetting.GetComponent<PlayerUpgrade>().StartDefense;
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

        if(Hp <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }


    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float currentPosition = gameObject.transform.position.x;
        if (currentPosition > gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        movement.Normalize();

        rb.velocity = movement * speed;

        anim.SetBool("move", true);
        isMove = true;
    }


    void LevelUp()
    {
        level += 1;
        exp = 0;
        isLevelUp = false;
        levelUpManager.GetComponent<LevelUpManager>().PlayerLevelUp();
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

    private IEnumerator DieCoroutine()
    {
        Time.timeScale = 0;
        anim.SetTrigger("die");

        yield return new WaitForSeconds(3f);

        gameOver.SetActive(true);  
    }

    public void GoMain()
    {
        Loading.LoadScene("MainScene");
    }
}
