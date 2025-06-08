using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] GameObject menuItemPrefab;
	[SerializeField] GameObject inventoryContent;


	List<Item> items = new List<Item>();

	public void AddItem(Item item)
	{
		var _item = items.Where(i => i.ItemName == item.ItemName).DefaultIfEmpty(null).FirstOrDefault();
		if (_item != null)
		{
			_item.ItemAmount++;
			_item.ResetVisuals();
		}
		else
		{
			items.Add(item);
		}
	}
	public void ReloadInventoryVisuals()
	{
		foreach(Transform child in inventoryContent.transform)
		{
			Destroy(child.gameObject);
		}
		foreach(Item item in items)
		{
			var _item = Instantiate(menuItemPrefab, inventoryContent.transform);
			_item.GetComponent<Item>().ResetVisuals();
		}
	}
	public void RemoveItem(Item item)
	{
		items.Remove(item);
	}
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
