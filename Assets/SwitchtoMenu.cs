using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class SwitchtoMenu : MonoBehaviour
{
	public void loadMenu()
	{
		//Application.LoadLevel ("MapGen"); 
		SceneManager.LoadScene("Main-menu"); 
		//EditorSceneManager.LoadSceneAsync("MapGen");  
	}

}
