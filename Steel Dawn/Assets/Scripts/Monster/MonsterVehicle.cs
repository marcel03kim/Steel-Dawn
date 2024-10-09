using System.Collections;
using UnityEngine;

public abstract class MonsterVehicle : MonoBehaviour
{
    public MonsterData monsterData; // ���� ������

    // ����Ŭ�� �־�� �ϴ� ������
    protected float hp;              // ���� ü��
    protected float speed;           // ���� �ӵ�
    protected float power;           // ���� ���ݷ�
    protected MonsterData.MonsterState currentState; // ������ ���� ����

    protected Rigidbody2D rb;        // ������ٵ� ����� ���� ����
    protected Transform target;      // �÷��̾� ��ġ�� �������� ���� ����
    protected Animator anim;         // �ִϸ��̼� ������ ���� ����
    protected SpriteRenderer spriteRenderer; // ��������Ʈ�� �����ϱ� ���� ����

    private bool isDropped = false;  // ��� ���� ���� �߰�


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D �ʱ�ȭ
        anim = GetComponent<Animator>();   // Animator �ʱ�ȭ
        target = GameObject.FindWithTag("Player").transform;  // Player �±׸� ���� ������Ʈ ã��
        spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer �ʱ�ȭ

        // ���� �����Ϳ��� �Ӽ� ��������
        if (monsterData != null)
        {
            hp = monsterData.hp;
            speed = monsterData.speed;
            power = monsterData.power;
            currentState = monsterData.initialState; // �ʱ� ���� ����
        }
        else
        {
            Debug.LogWarning("MonsterData is not assigned for " + gameObject.name);
        }

    }

    private void Update()
    {
        if (hp <= 0)
        {
            Die(); // ���� ����
        }
    }

    protected void FixedUpdate()
    {
        switch (currentState)
        {
            case MonsterData.MonsterState.Move:
                Move();  // Move �޼��忡 �̵� ���� �ۼ�
                break;
            case MonsterData.MonsterState.Die:
                Die(); // Die �޼��忡 ���� ���� �ۼ�
                break;
        }
    }

    // Player�� ���� �̵��ϴ� �޼���
    protected virtual void Move()
    {
        if (target != null)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

            // �̵� ���⿡ ���� ��������Ʈ ����
            if (target.position.x > rb.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.MovePosition(newPosition);  // Rigidbody2D�� ����� �̵�

            anim.SetBool("move", true); 

        }
    }


    protected virtual void Die()
    {
        anim.SetBool("move", false); // Move �ִϸ��̼��� ����
        anim.SetTrigger("die");

        int lastClipIndex = anim.runtimeAnimatorController.animationClips.Length - 1;
        
        // ������ Ŭ���� ���̸� ��������
        float dieAnimationLength = anim.runtimeAnimatorController.animationClips[lastClipIndex].length;

        // �ִϸ��̼� Ŭ���� ���̸�ŭ ���
        StartCoroutine(FadeOutCoroutine(dieAnimationLength));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        yield return new WaitForSeconds(1.0f);

        float elapsedTime = 0f;
        Color color = spriteRenderer.color;  // ���� ��������Ʈ ���� ��������
        float startAlpha = color.a;  // ������ ���� ���� ��

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;  // ��� �ð� ������Ʈ
            color.a = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);  // ���� ���� ���� �����Ͽ� ���� 0����
            spriteRenderer.color = color;  // ��������Ʈ ���� ������Ʈ
            yield return null;  // ���� �����ӱ��� ���
        }

        // ���� ����ġ ��� ����
        DropItems();

        // ���������� ���� ���� 0���� ����
        color.a = 0f;
        spriteRenderer.color = color;

        // ���������� ������Ʈ�� ����
        Destroy(gameObject);
    }

    protected virtual void DropItems()
    {
        if (!isDropped)
        {
            int dropItem = Random.Range(0, 2); // 0 �Ǵ� 1�� ��ȯ
            if (dropItem == 0)
            {
                GameObject exp = Instantiate(monsterData.expPrefab, transform.position, transform.rotation);
                isDropped = true;  // ��� �Ϸ� ���·� ����
                exp.GetComponent<GetExp>().getExp = 10;
            }
            else if (dropItem == 1)
            {
                GameObject exp = Instantiate(monsterData.expPrefab, transform.position, transform.rotation);
                GameObject gold = Instantiate(monsterData.goldPrefab, transform.position, transform.rotation);
                isDropped = true;  // ��� �Ϸ� ���·� ����
                exp.GetComponent<GetExp>().getExp = 10;
                gold.GetComponent<GetGold>().getGold = 10;
            }
        }
    }
}
