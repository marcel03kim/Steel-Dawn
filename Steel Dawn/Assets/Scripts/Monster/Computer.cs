using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float Hp = 1;       //���� ü�� ���� ����
    private float speed = 0.75f; //�����̴� �ӵ� ���� ����
    public float power = 2;       //���� ���ݷ� ���� ����

    private float attackInterval = 3.0f; //���ݼӵ� ���� ����
    private bool canAttack = true;  // ���� ������ �������� ����

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
            anim.SetBool("move", true);

            // ��ǥ ������ ���� �� ���¸� Attack���� ����
            if (Vector2.Distance(rb.position, target.position) < 0.3f)
            {
                currentState = State.Attack;
                anim.SetBool("move", false);
            }
        }
    }

    void attack()
    {
        if (target != null && canAttack)
        {
            StartCoroutine(AttackCoroutine());

            if (Vector2.Distance(rb.position, target.position) > 0.5f)
            {
                currentState = State.Move;
                anim.SetBool("move", true);
            }
        }
    }
    private IEnumerator AttackCoroutine()
    {
        canAttack = false;  // ���� ���� �� ������ ���߰� ��

        Player player = target.GetComponent<Player>();  // PlayerMove ������Ʈ ��������
        if (player != null)
        {
            player.Hp -= power;  // �÷��̾��� ü���� power��ŭ ����
        }

        yield return new WaitForSeconds(attackInterval);  // ���� ���� ���
        canAttack = true;  // �ٽ� ���� �����ϰ� ����
    }

    // ���� ó�� ���� �ۼ�
    void Die()
    {
        anim.SetTrigger("die");
        StartCoroutine(FadeOutCoroutine(2.0f));  // ���İ� ���� �ڷ�ƾ ����
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

        // ���������� ���� ���� 0���� ����
        color.a = 0f;
        spriteRenderer.color = color;

        // ���������� ������Ʈ�� ����
        Destroy(gameObject);
    }
}
