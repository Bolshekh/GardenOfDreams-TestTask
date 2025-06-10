using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour, IUsable
{
	public string ItemName
	{
		get => itemName;
		set => itemName = value;
	}
	[SerializeField] string itemName;
	public int ItemAmount
	{
		get => itemAmount;
		set => itemAmount = value;
	}
	[SerializeField] int itemAmount;

	[SerializeField] Sprite itemIcon;

	Image itemIconComponent;
	TMP_Text itemNameComponent;
	TMP_Text itemAmountComponent;

	public virtual void Use()
	{
		PlayerManager.Manager.PlayerInventory.RemoveItem(this);
	}
	public virtual void Instantiate()
	{
		itemIconComponent = transform.GetChild(0).GetComponent<Image>();
		itemNameComponent = transform.GetChild(1).GetComponent<TMP_Text>();
		itemAmountComponent = transform.GetChild(2).GetComponent<TMP_Text>();
	}
	public virtual void ResetUiVisuals()
	{
		Instantiate();
		itemIconComponent.sprite = itemIcon;
		itemNameComponent.text = itemName;
		itemAmountComponent.text = itemAmount == 1? "" : itemAmount.ToString();
	} 
	public virtual void CopyValues(Item Item)
	{
		itemName = Item.itemName;
		itemAmount = Item.itemAmount;
		itemIcon = Item.itemIcon;
	}
	public Item GetCopy()
	{
		return new Item() { itemName = this.itemName, itemAmount = this.itemAmount, itemIcon = this.itemIcon };
	}
}