using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float Hp;
    public float currentHp;
    public float speed;
    public float power;
    public float defense;
    public int level = 0;
    public float exp = 0;
    public Image healthBar; 

    public GameObject playerStartSetting;

    public GameObject gameOver;
    public Camera cam;

    private bool isLevelUp = false;
    private Animator anim;
    private LevelUpManager levelUpManager;
    public GameObject[] expSprite; // ����ġ�� ���� ��������Ʈ �迭
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    Vector2 movement = new Vector2();
    Rigidbody2D rb;

    private void Start()
    {
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
        currentHp = Hp;
    }

    private void Update()
    {
        // ����ġ�� ���� ��������Ʈ ������Ʈ
        UpdateExpSprites();
        UpdateHealthBar();

        if (!isLevelUp && exp >= 190)
        {
            isLevelUp = true;
            LevelUp();
        }

        if(currentHp <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHp / Hp;
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

        if (movement != Vector2.zero)
        {
            // ĳ������ ������ ����
            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (movement.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // �̵� ���̸� ���� �ִϸ��̼��� ���
            anim.SetBool("move", true);
        }
        else
        {
            // �̵��� ���߸� idle �ִϸ��̼����� ��ȯ
            anim.SetBool("move", false);
        }

        // ���͸� ����ȭ�ϰ� �ӵ��� ���ؼ� �̵� ����
        movement.Normalize();
        rb.velocity = movement * speed;
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
        anim.SetTrigger("die");

        yield return new WaitForSeconds(3f);

        gameOver.SetActive(true);  
    }

    public void GoMain()
    {
        Loading.LoadScene("MainScene");
    }
}
