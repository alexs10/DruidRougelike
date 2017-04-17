using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
	public void loadGame()
	{
		//Application.LoadLevel ("MapGen"); 
		SceneManager.LoadScene("MapGen");
		GameManager.instance = null; 
		//SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
		//GameManager.CallbackInitialization(); 
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
		//EditorSceneManager.LoadSceneAsync("MapGen");  
	}

}

