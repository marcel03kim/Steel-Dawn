using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public GameObject LevelUpCanvas;

    public Button[] itemButtons;  // 각 아이템에 대한 버튼 배열
    public Text[] nameText;       // 이름 텍스트 UI 배열
    public Text[] descText;       // 설명 텍스트 UI 배열
    public Image[] itemImage;     // 아이템 이미지 UI 배열

    public Slot[] itemSlots;
    private ItemList itemList;
    private List<int> randomIndices; // 랜덤 인덱스를 저장할 리스트

    public void PlayerLevelUp()
    {
        Time.timeScale = 0f;
        LevelUpCanvas.SetActive(true);

        itemList = FindObjectOfType<ItemList>();

        if (itemList == null)
        {
            Debug.LogError("ItemList가 씬에 없습니다. ItemList 오브젝트가 있는지 확인하세요.");
            return;
        }

        UpdateRandomItemDetails();
    }

    // 3개의 랜덤한 아이템을 선택하여 UI에 표시하는 함수
    public void UpdateRandomItemDetails()
    {
        randomIndices = GetUniqueRandomIndices(3, 10);  // 아이템 개수가 10으로 고정

        for (int i = 0; i < randomIndices.Count; i++)
        {
            int index = randomIndices[i];
            ItemData itemData = itemList.GetItemData(index);

            if (itemData != null)
            {
                // 같은 인덱스의 텍스트, 설명, 이미지에 맞춰서 설정
                nameText[i].text = itemData.Name;
                descText[i].text = itemData.Description;
                itemImage[i].sprite = itemData.Icon;

                // 버튼 클릭 시 인덱스를 전달하도록 설정
                int buttonIndex = i; // 클로저 문제를 피하기 위해 새 변수로 저장
                itemButtons[i].onClick.AddListener(() => OnItemButtonClick(buttonIndex));
            }
        }
    }

    // 버튼 클릭 시 호출되는 함수
    private void OnItemButtonClick(int index)
    {
        // 선택된 아이템의 인덱스에 따라 필요한 작업 수행
        Debug.Log($"Selected Item Index: {randomIndices[index]}");
        Time.timeScale = 1.0f;
        LevelUpCanvas.SetActive(false);
        // 여기에 추가적인 로직을 넣을 수 있음
    }

    // 중복되지 않는 랜덤 인덱스 목록을 반환하는 함수
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