using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // ���� ���¸� ��Ÿ���� enum
    public enum SlotState { Empty, Full }
    public SlotState state = SlotState.Empty;

    // ���� ������ ��Ÿ���� enum
    public enum SlotType { ItemSlot, WeaponSlot }
    public SlotType slotType;

    public Image itemImage;        // ���Կ� ǥ���� ������ �̹���
    public Text levelText;         // ���Կ� ǥ���� ���� �ؽ�Ʈ

    

    //public void SetItem(int Level)
    //{
    //    if (newItemData != null)
    //    {
    //        // ������ �����Ͱ� ���� ��� ���Կ� ������ ��������Ʈ ����
    //        levelText.text = $"{Level}";
    //        itemImage.sprite = newItemData.Icon;
    //        itemImage.color = new Color(1, 1, 1, 1); // ���İ��� 1�� ����
    //        levelText.gameObject.SetActive(true); // ���� �ؽ�Ʈ Ȱ��ȭ
    //    }
    //    else
    //    {
    //        // ������ �����Ͱ� ���� ��� ��������Ʈ�� ���İ��� 0���� ����
    //        itemImage.color = new Color(1, 1, 1, 0); // ���İ��� 0���� ����
    //        levelText.gameObject.SetActive(false); // ���� �ؽ�Ʈ ��Ȱ��ȭ
    //    }
    //}

    // ������ ������ �������� ���� �������� Ȯ���ϴ� �Լ�
    public bool IsItemSlot()
    {
        return slotType == SlotType.ItemSlot;
    }

    public bool IsWeaponSlot()
    {
        return slotType == SlotType.WeaponSlot;
    }
}
