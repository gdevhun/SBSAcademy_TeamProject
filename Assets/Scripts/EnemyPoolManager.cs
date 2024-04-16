using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;

public class EnemyPoolManager : SingletonBehaviour<EnemyPoolManager>
{
	#region ���׹� ���� ������ ���� Ŭ����
	public enum EnemyType
	{
		Red, Green, Skull
	}
	[System.Serializable]
	public class EnemyPrefab //���׹��������� ���׹�Ÿ���� ������ ����
	{
		public EnemyType type;
		public GameObject prefab;
	}
	#endregion
	
	
	public List<EnemyPrefab> enemyPrefabs; //���׹������� ����Ʈ
	private Dictionary<EnemyType, List<GameObject>> _enemiesPool; //���׹� ���� ��ųʸ�
	protected override void Awake()
	{
		base.Awake();

		_enemiesPool = new Dictionary<EnemyType, List<GameObject>>(); //��ųʸ� �ʱ�ȭ
		
		foreach (EnemyPrefab enemyPrefab in enemyPrefabs)
		{
			List<GameObject> pool = new List<GameObject>();
			
			for (int i = 0; i < 20; i++) //���Ƿ� 20�� Ǯ��
			{
				GameObject enemyObj = Instantiate(enemyPrefab.prefab);
				enemyObj.SetActive(false);
				pool.Add(enemyObj);
			}
			_enemiesPool.Add(enemyPrefab.type,pool);
		}

	}

	private void Start()
	{

	}

	public GameObject GetEnemy(EnemyType enemyType)
	{
		GameObject selectedEnemy = null;
		List<GameObject> enemyList;
		if (_enemiesPool.TryGetValue(enemyType, out enemyList))
		{
			foreach (var enemy in enemyList)
			{
				if (!enemy.activeSelf)
				{
					selectedEnemy = enemy;
					selectedEnemy.SetActive(true);
					break;
				}
			}
		}
		else  //��Ȱ��ȭ������ ���׹̰� ���� ���
		{
			enemyList = new List<GameObject>();
			_enemiesPool.Add(enemyType,enemyList);
		}

		if (selectedEnemy == null)
		{
			selectedEnemy = Instantiate(GetEnemyPrefab(enemyType), transform);
			selectedEnemy.transform.parent = this.transform;
			enemyList.Add(selectedEnemy);
		}
		return selectedEnemy;
	}
	
	private GameObject GetEnemyPrefab(EnemyType type)
	{
		foreach (EnemyPrefab enemyPrefab in enemyPrefabs)
		{
			if (enemyPrefab.type == type)
			{
				return enemyPrefab.prefab;
			}
		}
		return null;
	}
	
}
