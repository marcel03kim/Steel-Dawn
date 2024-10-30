using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image[] tutorialimage;
    private List<TutorialTextData> tutorialText;
    public int currentTextIndex = 0;
    private Coroutine typingCoroutine;
    private bool isMoving;
    private bool isAttacked;
    private bool isLevelUp;
    private bool isSpawned = false;

    public Button skipButton;
    public GameObject player;
    public GameObject monster;
    public GameObject exp;
    public GameObject playerStartSetting;
    public GameObject stage;
    public Text tx;
    private Player playerScript; // Player ��ũ��Ʈ�� �����ϱ� ���� ����

    private void Awake()
    {
        tutorialText = new List<TutorialTextData>
        {
            new TutorialTextData(0, "Steel Dawn�� ���� �� ȯ���մϴ�"),
            new TutorialTextData(1, "�⺻ ������ WASD�� �̵��ϸ� ���͸� ���� �ڵ����� �����մϴ�"),
            new TutorialTextData(2, "�� �� �����������?"),
            new TutorialTextData(3, "���ϼ̽��ϴ�"),
            new TutorialTextData(4, "���� ���̴� ������ �����۰� ����â�Դϴ�"),
            new TutorialTextData(5, "������ �� ������ �� ���� ī�� �� ���ϴ� ���� ���� �̰��� �߰��˴ϴ�"),
            new TutorialTextData(6, "��ħ ���� ����ġ�� �ֱ���"),
            new TutorialTextData(7, "�� �� �ٰ��������?"),
            new TutorialTextData(8, "�� �ϼ̽��ϴ�"),
            new TutorialTextData(9, "���ʹ� ���� ������ �����ϰ� ����ġ�� ��带 ����߸��ϴ�"),
            new TutorialTextData(10, "����ġ�� ȹ���ϸ� �ش� �������� �������ϴ�"),
            new TutorialTextData(11, "���� ���Ͱ� ����ϱ� ������ �� ���׿�"),
            new TutorialTextData(12, "�� �� ���غ����?"),
            new TutorialTextData(13, "����, ���� ������ �� ���׿�"),
            new TutorialTextData(14, "���� ���̴� �������� ����� ü���Դϴ�"),
            new TutorialTextData(15, "ü���� ��� �Ҹ�Ǹ� ��,"),
            new TutorialTextData(16, "��ŵ� �� �ƽ� �Ŷ�� �����մϴ�"),
            new TutorialTextData(17, "�׷� �ε� ��Ż�Ͻñ�")
        };
    }


    private void Start()
    {
        // Player ��ũ��Ʈ ������Ʈ ��������
        playerScript = player.GetComponent<Player>();
        currentTextIndex = -1;
        monster.gameObject.SetActive(false);
        exp.gameObject.SetActive(false);
        ButtonClick();
    }

    public void Update()
    {
        // �÷��̾ ���������� üũ
        if (playerScript != null)
        {
            CheckPlayerMovement();
            CheckPlayerLevelUp();
            CheckPlayerAttack();
        }

        switch(currentTextIndex)
        {
            case 2:
                if (Input.anyKeyDown)
                {
                    foreach (Image img in tutorialimage)
                    {
                        img.gameObject.SetActive(false);
                    }
                }
                break;
            case 6:
                if (!isSpawned)
                {
                    Vector3 expPosition = new Vector3(player.transform.position.x + 12f, player.transform.position.y - 1.5f, player.transform.position.z);
                    exp.transform.position = expPosition;
                    exp.SetActive(true);
                }
                break;

            case 7:
                if (Input.anyKeyDown)
                {
                    foreach (Image img in tutorialimage)
                    {
                        img.gameObject.SetActive(false);
                    }
                }
                break;
            case 12:
                if (Input.anyKeyDown)
                {
                    foreach (Image img in tutorialimage)
                    {
                        img.gameObject.SetActive(false);
                    }

                    if (!isSpawned)
                    {
                        // ���ο� Vector3 ��ġ �����Ͽ� ���� ��ġ ����
                        Vector3 spawnPosition = new Vector3(player.transform.position.x + 12f, player.transform.position.y - 0.5f, player.transform.position.z);
                        monster.transform.position = spawnPosition;
                        monster.SetActive(true);
                        isSpawned = true;
                    }
                }
                break;
            case 18:
            case 19:
                stage.GetComponent<StageData>().isTutorialClear = true;
                Loading.LoadScene("MainScene");
                break;
        }
    }
    public void ButtonClick()
    {
        ++currentTextIndex;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        foreach (Image img in tutorialimage)
        {
            img.gameObject.SetActive(false);
        }

        if (currentTextIndex <= 17)
        {
            typingCoroutine = StartCoroutine(typing(currentTextIndex));
            switch (currentTextIndex)
            {
                case 0:
                case 1:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 2:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    skipButton.gameObject.SetActive(false);
                    break;
                case 3:
                    skipButton.gameObject.SetActive(true);
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 4:
                case 5:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    tutorialimage[2].gameObject.SetActive(true);
                    break;
                case 6:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    tutorialimage[3].gameObject.SetActive(true);
                    break;
                case 7:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    tutorialimage[3].gameObject.SetActive(true);
                    skipButton.gameObject.SetActive(false);
                    break;
                case 8:
                    skipButton.gameObject.SetActive(true);
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 9:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 10:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    tutorialimage[4].gameObject.SetActive(true);
                break;
                case 11:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 12:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    skipButton.gameObject.SetActive(false);
                    break;
                case 13:
                    skipButton.gameObject.SetActive(true);
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 14:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    tutorialimage[5].gameObject.SetActive(true);
                    break;
                case 15:
                case 16:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                case 17:
                    tutorialimage[0].gameObject.SetActive(true);
                    tutorialimage[1].gameObject.SetActive(true);
                    break;
                
                default:
                    foreach (Image img in tutorialimage)
                    {
                        img.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    IEnumerator typing(int textNum)
    {
        string selectedText = tutorialText[textNum].tutorialText;
        tx.text = "";  // �ʱ�ȭ

        for (int i = 0; i <= selectedText.Length; i++)
        {
            tx.text = selectedText.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void CheckPlayerMovement()
    {
        
        if (playerScript.rb.velocity.magnitude > 0 && !isMoving)
        {
            ButtonClick();
            isMoving = true;
        }
    }

    private void CheckPlayerLevelUp()
    {
        // ������ ���� üũ
        if (playerScript.isLevelUp && !isLevelUp)
        {
            ButtonClick();
            isLevelUp = true;
        }
    }

    private void CheckPlayerAttack()
    {
        // ���� HP�� �ִ� HP���� ���� ��� ���� �������� ����
        if (playerScript.currentHp < playerStartSetting.GetComponent<PlayerUpgrade>().StartHp && !isAttacked)
        {
            ButtonClick();
            monster.gameObject.SetActive(false);
            isAttacked = true;
        }
    }

}
