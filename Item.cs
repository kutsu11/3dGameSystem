
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
	public enum KindOfItem
	{
		KeyItem,
		UseItem
	}

	//　アイテムの種類
	[SerializeField]
	private KindOfItem kindOfItem;
	//　アイテムのアイコン
	[SerializeField]
	private Sprite icon;
	//　アイテムの名前
	[SerializeField]
	private string itemName;
	[SerializeField]
	private string jpitemName;
	//　アイテムの情報
	[SerializeField]
	private string information;
	[SerializeField]
	private string enitemName;
	//　アイテムの情報
	[SerializeField]
	private string eninformation;
	//　アイテムのID
	[SerializeField]
	private int itemId;

	public KindOfItem GetKindOfItem()
	{
		return kindOfItem;
	}

	public Sprite GetIcon()
	{
		return icon;
	}

	public string GetItemName()
	{
		return itemName;
	}

	public string GetjpItemName()
	{
		return jpitemName;
	}

	public string GetenItemName()
	{
		return enitemName;
	}

	public string GetInformation()
	{
		return information;
	}

	public string GetenInformation()
	{
		return eninformation;
	}

}

