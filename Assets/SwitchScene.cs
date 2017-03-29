using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class SwitchScene : MonoBehaviour
{
	public void loadGame()
	{
		//Application.LoadLevel ("MapGen"); 
		SceneManager.LoadScene("MapGen"); 
		//EditorSceneManager.LoadSceneAsync("MapGen");  
	}

}

