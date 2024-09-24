using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int gold;


    private void Awake()
    {
        // 인스턴스가 이미 존재하는지 확인하고, 존재하지 않으면 이 오브젝트로 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 변경되어도 오브젝트가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 중복된 GameManager가 있으면 파괴
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
