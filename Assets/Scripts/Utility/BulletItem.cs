
using UnityEngine;

public class BulletItem : Item
{
	public override void Use()
	{
		Debug.Log("shot");
		base.Use();
	}
}