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
    public GameObject[] expSprite; // ����ġ�� ���� ��������Ʈ �迭
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
        isMove = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ �ʱ�ȭ
        levelUpManager = GetComponent<LevelUpManager>();
        UpdateExpSprites(); // �ʱ� ��������Ʈ ����
        gameOver.SetActive(false);

        Hp = playerStartSetting.GetComponent<PlayerUpgrade>().StartHp;
        speed = playerStartSetting.GetComponent<PlayerUpgrade>().StartSpeed;
        power = playerStartSetting.GetComponent<PlayerUpgrade>().StartPower;
        defense = playerStartSetting.GetComponent<PlayerUpgrade>().StartDefense;
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
