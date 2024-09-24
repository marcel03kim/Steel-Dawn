using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float Hp = 1;       //몬스터 체력 변수 선언
    private float speed = 0.75f; //움직이는 속도 변수 선언
    public float power = 2;       //몬스터 공격력 변수 선언

    private float attackInterval = 3.0f; //공격속도 변수 선언
    private bool canAttack = true;  // 공격 가능한 상태인지 여부

    Rigidbody2D rb;        //리지드바디 사용을 위한 변수 선언
    private Transform target;   //플레이어 위치를 가져오기 위한 변수 선언
    private Animator anim;    //애니메이션 적용을 위한 변수 선언
    SpriteRenderer spriteRenderer; //오브젝트 스프라이트에 접근하기 위한 변수 선언

    public enum State            //각각의 상태를 지정하여 업데이트 메서드에서 스위치문으로 받아 옴
    {
        Move,
        Attack,
        Die
    }

    public State currentState = State.Move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 초기화
        anim = GetComponent<Animator>();   // Animator 초기화
        target = GameObject.FindWithTag("Player").transform;  // Player 태그를 가진 오브젝트 찾기
        spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer 초기화
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
                Move();  // Move 메서드에 이동 로직 작성
                break;
            case State.Attack:
                attack();
                break;
            case State.Die:
                Die(); // Die 메서드에 죽음 로직 작성
                break;
        }
    }

    // Player를 향해 이동하는 메서드
    void Move()
    {
        if (target != null)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

            // 이동 방향에 따른 스프라이트 반전
            if (target.position.x > rb.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.MovePosition(newPosition);  // Rigidbody2D를 사용한 이동
            anim.SetBool("move", true);

            // 목표 지점에 도착 시 상태를 Attack으로 변경
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
        canAttack = false;  // 공격 시작 시 공격을 멈추게 함

        Player player = target.GetComponent<Player>();  // PlayerMove 컴포넌트 가져오기
        if (player != null)
        {
            player.Hp -= power;  // 플레이어의 체력을 power만큼 감소
        }

        yield return new WaitForSeconds(attackInterval);  // 공격 간격 대기
        canAttack = true;  // 다시 공격 가능하게 설정
    }

    // 죽음 처리 로직 작성
    void Die()
    {
        anim.SetTrigger("die");
        StartCoroutine(FadeOutCoroutine(2.0f));  // 알파값 조정 코루틴 실행
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        yield return new WaitForSeconds(1.0f);

        float elapsedTime = 0f;
        Color color = spriteRenderer.color;  // 현재 스프라이트 색상 가져오기
        float startAlpha = color.a;  // 시작할 때의 알파 값

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;  // 경과 시간 업데이트
            color.a = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);  // 알파 값을 선형 보간하여 점차 0으로
            spriteRenderer.color = color;  // 스프라이트 색상 업데이트
            yield return null;  // 다음 프레임까지 대기
        }

        // 마지막으로 알파 값을 0으로 설정
        color.a = 0f;
        spriteRenderer.color = color;

        // 최종적으로 오브젝트를 삭제
        Destroy(gameObject);
    }
}
