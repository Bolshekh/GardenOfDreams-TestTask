using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
	public HitResponse Hit(HitInfo hitInfo);
}
