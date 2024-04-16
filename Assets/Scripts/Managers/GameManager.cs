using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
	public GameObject player;
	private string _selectedStageName; // 사용자가 누른 스테이지 담는 변수
	public StageData stageData;
	public bool isGameOver;
	void Start()
	{
		
	}
	public void FindPlayer()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	public void OnStageButtonClick(string stageName)
	{
		_selectedStageName = stageName;
		LoadSelectedStage();
	}

	private void LoadSelectedStage()
	{
		stageData = Resources.Load<StageData>(_selectedStageName);

		if (stageData != null)
		{
			// 로드 성공
			Debug.Log("StageData loaded successfully.");
			Debug.Log(stageData.stageSpawnNum);
		}
		else
		{
			// 로드 실패
			Debug.LogError($"Failed to load StageData for {_selectedStageName}.");
		}
	}
}


