using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GUIStyle progressBarEmpty;
	public GUIStyle progressBarFull;
	private Vector2 pos = new Vector2(Screen.width - (Screen.width/9), Screen.height/5);
	public Vector2 size = new Vector2(20, 60);
	public float speed = 0.5f;
	private float limit;
	private float tempBar = 1;

	private float barDisplay = 1;
	
	void OnGUI() {
		// draw the background:
		GUI.BeginGroup(new Rect (pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0, 0, size.x, size.y), "", progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup(new Rect (0, (size.y - (size.y  * barDisplay)), size.x, size.y  * barDisplay));
		GUI.Box(new Rect (0, -size.y + (size.y * barDisplay), size.x, size.y), "", progressBarFull);
		GUI.EndGroup();
		GUI.EndGroup ();
	}
	
	void Update() {        

		if (barDisplay > tempBar) {
			barDisplay -= speed * Time.deltaTime;
		}
	}

	public void setSpeed(int number){
		limit = 1.0f / number;
		speed = 1.0f / (number * .4f);
	}

	public void newLimit(){
		tempBar -= limit;
	}

	void reset(){
		barDisplay = 1;
		tempBar = 1;
	}
}