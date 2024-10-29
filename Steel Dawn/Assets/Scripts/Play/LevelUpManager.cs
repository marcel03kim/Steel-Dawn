using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public GameObject LevelUpCanvas;

    public Button[] itemButtons;  // �� �����ۿ� ���� ��ư �迭
    public Text[] nameText;       // �̸� �ؽ�Ʈ UI �迭
    public Text[] descText;       // ���� �ؽ�Ʈ UI �迭
    public Image[] itemImage;     // ������ �̹��� UI �迭

    public Slot[] itemSlots;
    private ItemList itemList;
    private List<int> randomIndices; // ���� �ε����� ������ ����Ʈ

    public void PlayerLevelUp()
    {
        Time.timeScale = 0f;
        LevelUpCanvas.SetActive(true);

        itemList = FindObjectOfType<ItemList>();

        if (itemList == null)
        {
            Debug.LogError("ItemList�� ���� �����ϴ�. ItemList ������Ʈ�� �ִ��� Ȯ���ϼ���.");
            return;
        }

        UpdateRandomItemDetails();
    }

    // 3���� ������ �������� �����Ͽ� UI�� ǥ���ϴ� �Լ�
    public void UpdateRandomItemDetails()
    {
        randomIndices = GetUniqueRandomIndices(3, 10);  // ������ ������ 10���� ����

        for (int i = 0; i < randomIndices.Count; i++)
        {
            int index = randomIndices[i];
            ItemData itemData = itemList.GetItemData(index);

            if (itemData != null)
            {
                // ���� �ε����� �ؽ�Ʈ, ����, �̹����� ���缭 ����
                nameText[i].text = itemData.Name;
                descText[i].text = itemData.Description;
                itemImage[i].sprite = itemData.Icon;

                // ��ư Ŭ�� �� �ε����� �����ϵ��� ����
                int buttonIndex = i; // Ŭ���� ������ ���ϱ� ���� �� ������ ����
                itemButtons[i].onClick.AddListener(() => OnItemButtonClick(buttonIndex));
            }
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    private void OnItemButtonClick(int index)
    {
        // ���õ� �������� �ε����� ���� �ʿ��� �۾� ����
        Debug.Log($"Selected Item Index: {randomIndices[index]}");
        Time.timeScale = 1.0f;
        LevelUpCanvas.SetActive(false);
        // ���⿡ �߰����� ������ ���� �� ����
    }

    // �ߺ����� �ʴ� ���� �ε��� ����� ��ȯ�ϴ� �Լ�
    private List<int> GetUniqueRandomIndices(int count, int maxRange)
    {
        HashSet<int> uniqueIndices = new HashSet<int>();

        while (uniqueIndices.Count < count)
        {
            int randomIndex = Random.Range(0, maxRange);
            uniqueIndices.Add(randomIndex);
        }

        return new List<int>(uniqueIndices);
    }

}