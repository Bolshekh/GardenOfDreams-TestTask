using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float spawnDiameter;
	// Start is called before the first frame update
	void Start()
	{
		SpawnEnemy(3);
	}
	public void SpawnEnemy(int Amount)
	{
		for (int i = 0; i < Amount; i++)
		{
			Instantiate(enemyPrefab, GetSpawnPointAroundPoint(PlayerManager.Player.transform.position, spawnDiameter), Quaternion.identity);
		}

	}
	Vector2 GetSpawnPointAroundPoint(Vector2 Point, float Diameter)
	{
		float _x = Random.Range(0, Diameter);
		int _coin = Random.Range(0, 2);
		float _y = Mathf.Pow((Diameter * _x) - (_x * _x), 0.5f);
		_y *= _coin == 0 ? 1 : -1;

		return new Vector2(_x - (Diameter * 0.5f) + Point.x, _y + Point.y);
	}
}
