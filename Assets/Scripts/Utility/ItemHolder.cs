
using Unity.VisualScripting;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
	Item item;
	private void Start()
	{
		item = GetComponentInChildren<Item>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//TODO: add to player inventory
		if (collision.CompareTag("Player"))
		{
			PlayerManager.Manager.PlayerInventory.AddItem(item);
			Destroy(transform.gameObject);
		}
	}

}
