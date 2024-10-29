using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject SettingCanvas;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingCanvas.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        switch(sceneName)
        {
            case "shop": Loading.LoadScene("ShopScene");
                break;
            case "set": SettingCanvas.SetActive(true);
                break;
            case "x": SettingCanvas.SetActive(false);
                break;
            case "quit": Application.Quit(); 
                break;
        }
    }
}
