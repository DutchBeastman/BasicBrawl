using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void OnGUI () {
		// Making a background box, where in the buttons are placed
		GUI.Box(new Rect(Screen.width / 3,Screen.height / 4,Screen.width / 2.5f,Screen.height / 2f), "Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(Screen.width / 2.5f,Screen.height / 3f,Screen.width / 4f,Screen.height / 7f), "Level 1")) {
			Application.LoadLevel(1);
		}
		
		// Make the button. If it is pressed, Application.Quit will be executed.
		if(GUI.Button(new Rect(Screen.width / 2.5f,Screen.height / 2f,Screen.width / 4f,Screen.height / 7f), "Exit")) {
			Application.Quit();
		}
	}
}
