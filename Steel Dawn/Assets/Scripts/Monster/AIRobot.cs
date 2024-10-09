using System.Collections;
using UnityEngine;

public class AIRobot : MonsterVehicle
{
    private bool canAttack = true; // ���� ���� ����
    public Transform firePos; // �Ѿ� �߻� ��ġ
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������

    protected override void Move()
    {
        base.Move();

        if (target != null)
        {
            // ���� ������ ���� �� ���� ����
            if (Vector2.Distance(transform.position, target.position) < 10.0f)
            {
                currentState = MonsterData.MonsterState.Attack;
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                currentState = MonsterData.MonsterState.Move; // ���� ���� �ܷ� ������ �̵� ���·� ����
            }
        }

        // �̵� �ִϸ��̼� ���� ����
        anim.SetBool("move", currentState == MonsterData.MonsterState.Move);
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false; // ���� �Ұ� ���·� ����
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.4f); // ���� �ִϸ��̼� ���

        // �Ѿ� �ν��Ͻ�ȭ
        GameObject bulletObject = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            Vector2 direction = (target.position - firePos.position).normalized; // Ÿ�� ���� ���
            bullet.SetDirection(direction); // ���� ����
            bullet.power = monsterData.power; // �Ѿ��� ���ݷ� ����
        }

        yield return new WaitForSeconds(monsterData.attackInterval); // ���� ���ݱ��� ���
        canAttack = true; // ���� ���� ���·� ����
        anim.SetBool("move", false);
    }
}
