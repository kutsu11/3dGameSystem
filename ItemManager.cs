using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemManager : MonoBehaviour
{
	//�@�|�[�Y�������ɕ\������UI
	[SerializeField]
	public GameObject inventory;
	//�@�A�C�e���f�[�^�x�[�X
	[SerializeField]
	private ItemDataBase itemDataBase;

	static ItemManager instance;
	public static ItemManager GetInstance()
	{
		return instance;
	}
	private void Awake()//start���\�b�h�Ƃقړ���
	{
		instance = this;
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			//�@�|�[�YUI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
			inventory.SetActive(!inventory.activeSelf);

			//�@�|�[�YUI���\������Ă鎞�͒�~
			if (inventory.activeSelf)
			{
				Time.timeScale = 0f;
				//�@�|�[�YUI���\������ĂȂ���Βʏ�ʂ�i�s
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}
    public bool HasItem(string searchName)//�f�[�^�x�[�X��ɑ��݂��邩�ۂ�
	{
		return itemDataBase.GetItemLists()
			   .Exists(item => item.GetItemName() == searchName);
	}
	public Item GetItem(string searchName)//Goods�Ɏ擾�A�C�e������n��
	{
		return itemDataBase.GetItemLists()
			   .Find(itemName => itemName.GetItemName() == searchName);
	}
}