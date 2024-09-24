using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRobot : MonoBehaviour
{
    public float Hp = 1;       //���� ü�� ���� ����
    private float speed = 1.0f; //�����̴� �ӵ� ���� ����
    public float power = 2;       //���� ���ݷ� ���� ����

    public GameObject bulletPrefab;
    public Transform firePos;
    private float attackInterval = 3.0f; //���ݼӵ� ���� ����
    private bool canAttack = true;  // ���� ������ �������� ����
    public GameObject expPrefab;
    public GameObject goldPrefab;
    private bool isDropped = false;

    Rigidbody2D rb;        //������ٵ� ����� ���� ���� ����
    private Transform target;   //�÷��̾� ��ġ�� �������� ���� ���� ����
    private Animator anim;    //�ִϸ��̼� ������ ���� ���� ����
    SpriteRenderer spriteRenderer; //������Ʈ ��������Ʈ�� �����ϱ� ���� ���� ����

    public enum State            //������ ���¸� �����Ͽ� ������Ʈ �޼��忡�� ����ġ������ �޾� ��
    {
        Move,
        Attack,
        Die
    }

    public State currentState = State.Move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D �ʱ�ȭ
        anim = GetComponent<Animator>();   // Animator �ʱ�ȭ
        target = GameObject.FindWithTag("Player").transform;  // Player �±׸� ���� ������Ʈ ã��
        spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer �ʱ�ȭ
    }

    private void Update()
    {
        if (Hp <= 0)
        {
            currentState = State.Die;
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Move:
                Move();  // Move �޼��忡 �̵� ���� �ۼ�
                break;
            case State.Attack:
                attack();
                break;
            case State.Die:
                Die(); // Die �޼��忡 ���� ���� �ۼ�
                break;
        }
    }

    // Player�� ���� �̵��ϴ� �޼���
    void Move()
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
            anim.SetInteger("anim", 1);

            // ��ǥ ������ ���� �� ���¸� Attack���� ����
            if (Vector2.Distance(rb.position, target.position) < 3.0f)
            {
                currentState = State.Attack;
                anim.SetInteger("anim", 2);
            }
        }
    }

    void attack()
    {
        if (target != null && canAttack)
        {
            speed = 0;
            StartCoroutine(AttackCoroutine());

            if (Vector2.Distance(rb.position, target.position) > 4.0f)
            {
                speed = 1.0f;
                currentState = State.Move;
            }
        }
    }
    private IEnumerator AttackCoroutine()
    {
        canAttack = false;  // ���� ���� �� ������ ���߰� ��
        anim.SetInteger("anim", 0);

        yield return new WaitForSeconds(0.4f);

        Player player = target.GetComponent<Player>();  // PlayerMove ������Ʈ ��������
        if (player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);
        }

        yield return new WaitForSeconds(attackInterval);  // ���� ���� ���
        canAttack = true;  // �ٽ� ���� �����ϰ� ����
        anim.SetInteger("anim", 2);
    }

    // ���� ó�� ���� �ۼ�
    void Die()
    {
        anim.SetTrigger("die");
        StartCoroutine(FadeOutCoroutine(1.5f));  // ���İ� ���� �ڷ�ƾ ����
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

        if (!isDropped)
        {
            int dropItem = Random.Range(0, 2); // 0 �Ǵ� 1�� ��ȯ
            if (dropItem == 0)
            {
                GameObject exp = Instantiate(expPrefab, transform.position, transform.rotation);
                isDropped = true;  // ��� �Ϸ� ���·� ����
                exp.GetComponent<GetExp>().getExp = 10;
            }
            else if (dropItem == 1)
            {
                GameObject exp = Instantiate(expPrefab, transform.position, transform.rotation);
                GameObject gold = Instantiate(goldPrefab, transform.position, transform.rotation);
                isDropped = true;  // ��� �Ϸ� ���·� ����
                exp.GetComponent<GetExp>().getExp = 10;
                gold.GetComponent<GetGold>().getGold = 10;
            }
        }

        // ���������� ���� ���� 0���� ����
        color.a = 0f;
        spriteRenderer.color = color;

        // ���������� ������Ʈ�� ����
        Destroy(gameObject);
    }

}