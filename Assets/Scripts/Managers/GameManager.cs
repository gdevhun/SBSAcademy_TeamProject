using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
	public GameObject Player;
	private string selectedStageName; // ����ڰ� ���� �������� ��� ����
	public StageData stageData;
	public bool isGameOver;
	void Start()
	{
		
	}
	public void FindPlayer()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	public void OnStageButtonClick(string stageName)
	{
		selectedStageName = stageName;
		LoadSelectedStage();
	}

	private void LoadSelectedStage()
	{
		stageData = Resources.Load<StageData>(selectedStageName);

		if (stageData != null)
		{
			// �ε� ����
			Debug.Log("StageData loaded successfully.");
			Debug.Log(stageData.stageSpawnNum);
		}
		else
		{
			// �ε� ����
			Debug.LogError($"Failed to load StageData for {selectedStageName}.");
		}
	}
}


