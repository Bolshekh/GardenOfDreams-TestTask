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
		private set => itemName = value;
	}
	[SerializeField] string itemName;
	public int ItemAmount
	{
		get => itemAmount;
		set => itemAmount = value;
	}
	[SerializeField] int itemAmount;
	[SerializeField] Sprite itemIcon;
	[SerializeField] Image itemIconComponent;
	[SerializeField] TMP_Text itemNameComponent;
	[SerializeField] TMP_Text itemAmountComponent;
	bool IsOnGround = true;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void Use()
	{

	}
	public void ResetVisuals()
	{
		itemIconComponent.sprite = itemIcon;
		itemNameComponent.text = itemName;
		itemAmountComponent.text = itemAmount.ToString();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//TODO: add to player inventory
		if(collision.CompareTag("Player"))
		{
			PlayerManager.Manager.PlayerInventory.AddItem(this); 
			Destroy(transform.gameObject);
		}
	}

}
