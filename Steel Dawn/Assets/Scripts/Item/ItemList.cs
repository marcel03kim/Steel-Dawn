using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public Sprite[] itemSprites; // ����Ƽ �����Ϳ��� ������ ��������Ʈ �迭
    private List<ItemData> itemLists;

    private void Awake()
    {
        itemLists = new List<ItemData>
        {
            new ItemData(0, "Į", "���� ����", itemSprites.Length > 0 ? itemSprites[0] : null),
            new ItemData(1, "��", "���Ÿ� ����.", itemSprites.Length > 1 ? itemSprites[1] : null),
            new ItemData(2, "ĳ��", "���� ���Ÿ� ����", itemSprites.Length > 2 ? itemSprites[2] : null),
            new ItemData(3, "��ü", "�� ���� �ɵ���", itemSprites.Length > 3 ? itemSprites[3] : null),
            new ItemData(4, "����", "�� �ֺ��� �Ӿ��� �ž�", itemSprites.Length > 4 ? itemSprites[4] : null),
            new ItemData(5, "ü��", "�� ���� ���� �� �ְ� �ȴ�", itemSprites.Length > 5 ? itemSprites[5] : null),
            new ItemData(6, "����", "�� Ŀ�� �ž�", itemSprites.Length > 6 ? itemSprites[6] : null),
            new ItemData(7, "��ų ����", "�� ������, �� ����", itemSprites.Length > 7 ? itemSprites[7] : null),
            new ItemData(8, "����", "�� �ܴ������ڱ�", itemSprites.Length > 8 ? itemSprites[8] : null),
            new ItemData(9, "���ݷ�", "�� ����... �ƴ�, ���� ������ ������ �ϴ±�", itemSprites.Length > 9 ? itemSprites[9] : null)
        };
    }

    public ItemData GetItemData(int index)
    {
        return itemLists.Find(item => item.Id == index) ?? new ItemData(-1, "Unknown", "Item not found", null);
    }
}