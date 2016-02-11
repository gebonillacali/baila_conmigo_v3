using UnityEngine;
using System.Collections;

/**
 * Clase que permite gestionar la grabacion de los movimientos que se realizan en el Kinect
 * 
 * @author gbonilla@unbosque.edu.co - efrancor@unbosque.edu.co
 */
public class GrabacionManager : MonoBehaviour {
	
	private Rect windowRect;
	
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;

	private GrabadorMovimiento grabadorMovimiento;
	
	public GUIText CalibrationText;
	
	// Use this for initialization
	void Start () {
		grabadorMovimiento = new GrabadorMovimiento ();
	}
	
	void OnGUI () {
		GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
		if (GUI.Button(new Rect(0,0,bW,bH),"Iniciar Grabacion")) {
			grabadorMovimiento.StartRecord();
		}		
		
		if(GUI.Button(new Rect(0,120,bW,bH),"Detener Grabacion")){		
			grabadorMovimiento.StopRecord();
		}
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Running");
		if (grabadorMovimiento.isRecording()) {
			grabadorMovimiento.record();
		}
	}
}

