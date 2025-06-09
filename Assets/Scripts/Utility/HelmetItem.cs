
using UnityEngine;

public class HelmetItem : Item
{
	public override void Use()
	{
		PlayerManager.Manager.PlayerHealth.MaxHealthUpgrade(1);
		PlayerManager.Manager.PlayerHealth.Heal(1);
		base.Use();
	}
}