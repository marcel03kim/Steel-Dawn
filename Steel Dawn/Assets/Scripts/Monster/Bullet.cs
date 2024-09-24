using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2.0f; //탄알 이동 속력
    public int power;
    private Rigidbody2D rb;      //이동에 사용할 리지드바디 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        //게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 rb에 할당
        rb = GetComponent<Rigidbody2D>();
        // 리지드바디의 속도 = 오브젝트의 기준으로 앞쪽 방향 * 이동속력
        rb.velocity = transform.forward * speed;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        //3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 5f);
    }

    //트리거 충돌 시 자동으로 실행되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            gameObject.SetActive(false);
            //상대방 게임 오브젝트에서 Player_controller 컴포넌트 가져오기
            Player player = collision.GetComponent<Player>();

            //상대방으로부터 Player_controller 컴포넌트를 가져오는 데 성공했다면
            if (player != null)
            {
                //상대방 Player_controller 컴포넌트의 Die() 메서드 실행
                player.Hp -= power;
            }
        }
    }
}
