using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] GameObject menuItemPrefab;
	[SerializeField] GameObject inventoryContent;


	[SerializeField] List<Item> items = new List<Item>();

	public void AddItem(Item item)
	{
		var _item = items.Where(i => i.ItemName == item.ItemName).DefaultIfEmpty(null).FirstOrDefault();
		if (_item != null)
		{
			_item.ItemAmount+=item.ItemAmount;
			_item.ResetUiVisuals();
		}
		else
		{
			items.Add(item);
			ReloadInventoryVisuals();
		}
	}
	public void ReloadInventoryVisuals()
	{
		foreach(Transform child in inventoryContent.transform)
		{
			DestroyImmediate(child.gameObject);
		}
		foreach(Item item in items)
		{
			var _item = Instantiate(menuItemPrefab, inventoryContent.transform);
			var _component = _item.AddComponent(item.GetType());
			((Item)_component).Instantiate();
			((Item)_component).CopyValues(item);
			((Item)_component).ResetUiVisuals();
			_item.GetComponent<Button>().onClick.AddListener(((Item)_component).Use);
		}
		items = inventoryContent.GetComponentsInChildren<Item>().ToList();
	}
	public void RemoveItem(Item item)
	{
		var _item = items.Where(i => i.ItemName == item.ItemName).DefaultIfEmpty(null).FirstOrDefault();
		if (_item != null)
		{
			_item.ItemAmount--;

			if (_item.ItemAmount <= 0)
			{
				items.Remove(item);
				ReloadInventoryVisuals();
			}
			else
			{
				_item.ResetUiVisuals();
			}

		}
		//else
		//{
		//	items.Remove(item);
		//	ReloadInventoryVisuals();
		//}
	}
	/// <summary>
	/// Try to use item with specific name
	/// </summary>
	/// <param name="ItemName">Item name</param>
	/// <returns>Is item been used</returns>
	public bool UseItem(string ItemName)
	{
		var _item = items.Where(i => i.ItemName == ItemName).DefaultIfEmpty(null).FirstOrDefault();
		if (_item != null)
		{
			_item.Use();
			return true;
		}
		else
		{
			return false;
		}
	}
}
