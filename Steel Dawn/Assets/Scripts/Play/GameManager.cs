using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int gold;


    private void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϴ��� Ȯ���ϰ�, �������� ������ �� ������Ʈ�� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���� ����Ǿ ������Ʈ�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ��� GameManager�� ������ �ı�
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
