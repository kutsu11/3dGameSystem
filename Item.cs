
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

	//�@�A�C�e���̎��
	[SerializeField]
	private KindOfItem kindOfItem;
	//�@�A�C�e���̃A�C�R��
	[SerializeField]
	private Sprite icon;
	//�@�A�C�e���̖��O
	[SerializeField]
	private string itemName;
	[SerializeField]
	private string jpitemName;
	//�@�A�C�e���̏��
	[SerializeField]
	private string information;
	[SerializeField]
	private string enitemName;
	//�@�A�C�e���̏��
	[SerializeField]
	private string eninformation;
	//�@�A�C�e����ID
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

