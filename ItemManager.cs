using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemManager : MonoBehaviour
{
	//　ポーズした時に表示するUI
	[SerializeField]
	public GameObject inventory;
	//　アイテムデータベース
	[SerializeField]
	private ItemDataBase itemDataBase;

	static ItemManager instance;
	public static ItemManager GetInstance()
	{
		return instance;
	}
	private void Awake()//startメソッドとほぼ同じ
	{
		instance = this;
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			//　ポーズUIのアクティブ、非アクティブを切り替え
			inventory.SetActive(!inventory.activeSelf);

			//　ポーズUIが表示されてる時は停止
			if (inventory.activeSelf)
			{
				Time.timeScale = 0f;
				//　ポーズUIが表示されてなければ通常通り進行
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}
    public bool HasItem(string searchName)//データベース上に存在するか否か
	{
		return itemDataBase.GetItemLists()
			   .Exists(item => item.GetItemName() == searchName);
	}
	public Item GetItem(string searchName)//Goodsに取得アイテム情報を渡す
	{
		return itemDataBase.GetItemLists()
			   .Find(itemName => itemName.GetItemName() == searchName);
	}
}