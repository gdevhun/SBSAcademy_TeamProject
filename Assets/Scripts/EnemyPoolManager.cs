using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
	public GameObject[] enemiesPrefabs; 
	List<GameObject>[] enemiesPool;
	public static EnemyPoolManager _instance;
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		enemiesPool = new List<GameObject>[enemiesPrefabs.Length];
		for (int i = 0; i < enemiesPool.Length; i++)
		{
			enemiesPool[i] = new List<GameObject>();
		}

	}

	private void Start()
	{

		for (int i = 0; i < 20; i++)
		{   //일반 에네미들 풀링
			for (int j = 3; j < enemiesPrefabs.Length; j++)
			{
				GameObject enemy = Instantiate(enemiesPrefabs[j], transform);
				enemy.transform.parent = this.transform;
				enemy.SetActive(false);
				enemiesPool[j].Add(enemy);
			}
		}
	}
	//0~1 에네미
	public GameObject GetEnemies(int enemyindex)
	{
		GameObject selectedEnemy = null;
		foreach (GameObject enemy in enemiesPool[enemyindex])
		{
			if (!enemy.activeSelf)
			{
				selectedEnemy = enemy;
				selectedEnemy.SetActive(true);
				break;
			}
		}
		if (!selectedEnemy)
		{
			selectedEnemy = Instantiate(enemiesPrefabs[enemyindex], transform);
			selectedEnemy.transform.parent = this.transform;
			enemiesPool[enemyindex].Add(selectedEnemy);
		}

		return selectedEnemy;
	}
}
