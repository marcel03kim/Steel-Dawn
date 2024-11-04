using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typingEffect : MonoBehaviour
{
    public Text tx;
    public Image[] storyImages;
    private List<StoryTextData> storyText;
    public int currentTextIndex = 0;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        storyText = new List<StoryTextData>
        {
            new StoryTextData(0, "���� 205X��, ������ ���� �����ߴ�."),
            new StoryTextData(1, "���� ����� ��� ������ �߽����� ������ ���� ū �̽��� ����� �ߴ��� �Ǿ���."),
            new StoryTextData(2, "���� ���¿��� ������ ��ġ�� ��� ���߿� ū ���� ���� �� ���� ������ �� �� �־���."),
            new StoryTextData(3, "������ ������ ���⼭ ��������."),
            new StoryTextData(4, "����� ������� ���۵� �ɰ��� ������ �η����� ū ������ ���ư�"),
            new StoryTextData(5, "�ش� ������ ���� ������ �ھƸ� ������ �����Ͽ���."),
            new StoryTextData(6, "��� ��������� �� �� ���� ������ ��ġ���� ���� ���� ����������"),
            new StoryTextData(7, "�ᱹ �η��� ��迡�� ���� ���ϰ� ���Ҵ�."),
            new StoryTextData(8, "�ű������ ���� ��� ���� ���̾���."),
            new StoryTextData(9, "���� ū ������ . . ."),
            new StoryTextData(10, "�� ���� �������."),
            new StoryTextData(11, "������ �� ä ���� �� �� ���� ã�ٺ��� ������ ����"),
            new StoryTextData(12, "����, ���� �� ��Ҵ� �� ����̾���."),
            new StoryTextData(13, "����ġ�� ������ �Ѿ� ���� �� ������ ��������"),
            new StoryTextData(14, "���� ȸ�� ���� �ö���־���.")
        };
    }

    private void Start()
    {
        currentTextIndex = -1;  // Start with the first text index
        ButtonClick();          // Immediately display the first text
    }

    private void Update()
    {
        switch (currentTextIndex)
        {
            case 0:
            case 1:
                storyImages[0].gameObject.SetActive(true); 
                break;
            case 2:
                storyImages[1].gameObject.SetActive(true);
                break;
            case 3:
                foreach (Image img in storyImages)
                {
                    img.gameObject.SetActive(false);
                }
                break;
            case 4:
            case 5:
                storyImages[2].gameObject.SetActive(true);
                break;
            case 6:
                storyImages[3].gameObject.SetActive(true);
                break;
            case 7:
                storyImages[4].gameObject.SetActive(true);
                break;
            case 8:
                storyImages[5].gameObject.SetActive(true);
                break;
            case 9:
                foreach (Image img in storyImages)
                {
                    img.gameObject.SetActive(false);
                }
                break;
            case 10:
                storyImages[6].gameObject.SetActive(true);
                break;
            case 11:
                storyImages[7].gameObject.SetActive(true);
                break;
            case 12:
                storyImages[8].gameObject.SetActive(true);
                break;
            case 13:
                storyImages[9].gameObject.SetActive(true);
                break;
            case 14:
            case 15:
                storyImages[9].gameObject.SetActive(true);
                break;
            default :
                foreach (Image img in storyImages)
                {
                    img.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void ButtonClick()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (currentTextIndex <= 14)
        {
            ++currentTextIndex;
            typingCoroutine = StartCoroutine(typing(currentTextIndex));
        }
        if(currentTextIndex == 15)
        {
            Loading.LoadScene("TutorialScene");
        }
    }

    IEnumerator typing(int textNum)
    {
        string selectedText = storyText[textNum].Text;
        tx.text = "";  // �ʱ�ȭ

        for (int i = 0; i <= selectedText.Length; i++)
        {
            tx.text = selectedText.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
