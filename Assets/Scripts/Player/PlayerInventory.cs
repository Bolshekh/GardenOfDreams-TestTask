using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
			ReloadInventoryVisualsOnAddition();
		}
		
	}
	public void ReloadInventoryVisualsOnAddition()
	{
		var _items = inventoryContent.GetComponentsInChildren<Item>().ToList();
		//foreach (Transform child in inventoryContent.transform)
		//{
		//	Destroy(child.gameObject, 0f);
		//}
		//addding items
		foreach (Item item in items)
		{
			var _found = _items.Where(i => i.ItemName == item.ItemName).DefaultIfEmpty(null).FirstOrDefault();
			if(_found == null)
			{
				var _item = Instantiate(menuItemPrefab, inventoryContent.transform);
				var _component = _item.AddComponent(item.GetType());
				((Item)_component).Instantiate();
				((Item)_component).CopyValues(item);
				((Item)_component).ResetUiVisuals();
				_item.GetComponent<Button>().onClick.AddListener(((Item)_component).Use);
			}
		}
		items = inventoryContent.GetComponentsInChildren<Item>().ToList();
	}
	public void ReloadInventoryVisualsOnRemoval()
	{
		var _items = inventoryContent.GetComponentsInChildren<Item>().ToList();
		foreach (Item item in _items)
		{
			var _found = items.Where(i => i.ItemName == item.ItemName).DefaultIfEmpty(null).FirstOrDefault();
			if (_found == null)
			{
				Destroy(item.gameObject);
			}
		}
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
				ReloadInventoryVisualsOnRemoval();
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
	//SAVE LOAD
	string savePath => Application.dataPath + "/saves/Save1.txt";
	private void Start()
	{
		Load();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			Save();
		}
	}
	public void Save()
	{
		var _save = new Save() { PlayerPosition = transform.position };
		string _jsonSave = JsonUtility.ToJson(_save);

		if (!Directory.Exists(Application.dataPath + "/saves/"))
		{
			Directory.CreateDirectory(Application.dataPath + "/saves/");
			File.Create(savePath);
		}

		File.WriteAllText(savePath, _jsonSave);
	}
	public void Load()
	{
		if (!File.Exists(savePath)) return;

		string _loadedSave = File.ReadAllText(savePath);

		Save _save = JsonUtility.FromJson<Save>(_loadedSave);

		transform.position = _save.PlayerPosition;
	}
}

class Save
{
	public Vector2 PlayerPosition;
}