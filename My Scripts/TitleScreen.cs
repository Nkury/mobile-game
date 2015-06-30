using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
	public Texture image;
	public GUIStyle textStyle;

	void OnGUI(){
		Rect titleRect = GUILayoutUtility.GetRect (new GUIContent("ENDUROLL"), "ENDUROLL");

		GUI.DrawTexture(new Rect((Screen.width/2)-(image.width/2), (Screen.height/2)-(image.width/2), image.width, image.height), image);	
		GUI.Label (titleRect, "ENDUROLL", textStyle);
	}
}
