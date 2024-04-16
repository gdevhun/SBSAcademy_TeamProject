using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class EnemySpawner : SingletonBehaviour<EnemySpawner>
{
    //게임메니저로부터 스테이지의 정보를 얻어온다.
	[SerializeField] private int spawnInterval;
    [SerializeField] private int spawnEnemyCntPerInterval;
	[SerializeField] private Transform[] spawnPoints;
	private CancellationTokenSource _spawnCancelToken; //스폰중지토큰
	
	protected override void Awake()
	{
		base.Awake();
		
		//각스테이지별로 에니미스포너 생성, 초기화
		spawnInterval = GameManager.Instance.stageData.stageSpawnInteval;
		//한 스테이지에 호출될 에네미 인터벌 총 횟수.
		spawnEnemyCntPerInterval = GameManager.Instance.stageData.stageSpawnInteval;
		//한 스테이지 한 인터벌에 스폰하는 에네미들의 수.
	}
	private void Start()
	{
		StartEnemySpawning();
	}

	private void StartEnemySpawning()
	{
		_spawnCancelToken = new CancellationTokenSource();
		SpawnEnemyRoutine().Forget();
		// UniTask를 반환하는 메서드는 굳이 StartCoroutine를 사용하지 않고도 바로 호출 가능
	}
	
	private async UniTask SpawnEnemyRoutine()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(2));

		for (int i = 0; i < spawnInterval; i++)
		{
			for (int j = 0; j < spawnEnemyCntPerInterval; j++)
			{
				SpawnEnemy();  //spawnEnemyCntPerInterval 만큼 에네미 스폰
			}

			await UniTask.Delay(TimeSpan.FromSeconds(3),
				cancellationToken:this.GetCancellationTokenOnDestroy());
			//GetCancellationTokenOnDestroy ->    
			//플레이어가 죽으면 토큰을 꼭 삭제해야함. 그래야 스폰이중지됨.
		}

		await UniTask.Delay(TimeSpan.FromSeconds(5));
	}

	private void SpawnEnemy()
	{
		int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length); //임의 에네미 스폰포인트
		//int enemyIndex = UnityEngine.Random.Range(0, 2); //0~1
		GameObject enemy = EnemyPoolManager.Instance.GetEnemy(EnemyPoolManager.EnemyType.Green);
		enemy.gameObject.transform.SetPositionAndRotation(spawnPoints[spawnPointIndex].position,
			Quaternion.identity);
	}
}
