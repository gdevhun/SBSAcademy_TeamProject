using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;

public class EnemyPoolManager : SingletonBehaviour<EnemyPoolManager>
{
	#region 에네미 관련 매핑을 위한 클래스
	public enum EnemyType
	{
		Red, Green, Skull
	}
	[System.Serializable]
	public class EnemyPrefab //에네미프리팹은 에네미타입을 변수로 소유
	{
		public EnemyType type;
		public GameObject prefab;
	}
	#endregion
	
	
	public List<EnemyPrefab> enemyPrefabs; //에네미프리팹 리스트
	private Dictionary<EnemyType, List<GameObject>> _enemiesPool; //에네미 매핑 딕셔너리
	protected override void Awake()
	{
		base.Awake();

		_enemiesPool = new Dictionary<EnemyType, List<GameObject>>(); //딕셔너리 초기화
		
		foreach (EnemyPrefab enemyPrefab in enemyPrefabs)
		{
			List<GameObject> pool = new List<GameObject>();
			
			for (int i = 0; i < 20; i++) //임의로 20개 풀링
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
		else  //비활성화상태의 에네미가 없는 경우
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
