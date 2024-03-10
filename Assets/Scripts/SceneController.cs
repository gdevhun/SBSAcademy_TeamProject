using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : SingletonBehaviour<SceneController>
{
    public void StartGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void BackToLobbyScene()
    {
		SceneManager.LoadScene("LobbyScene");
	}
}
