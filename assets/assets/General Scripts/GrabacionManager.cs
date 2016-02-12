using UnityEngine;
using System.Collections;
using SimpleJSON;

using System.IO;

/// <summary>
/// Grabacion manager.
/// Clase que permite gestionar la grabacion de los movimientos que se realizan en el Kinect
/// 
/// </summary>
public class GrabacionManager : MonoBehaviour, GrabadorMovimiento.GrabacionStatus {

	private Rect windowRect;
	
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;

	private GrabadorMovimiento grabadorMovimiento;
	private string nombreRutina = "";
	private string path = "Recordings/playback/";
	
	public GUIText CalibrationText;
	public KinectManager kinectManager;
	
	// Use this for initialization
	void Start () {
		grabadorMovimiento = new GrabadorMovimiento (kinectManager, this);
	}
	
	void OnGUI () {
		GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
		if (!grabadorMovimiento.isRecording ()) {
			if (GUI.Button (new Rect (0, 0, bW, bH), "Iniciar Grabacion")) {
					grabadorMovimiento.StartRecord ();
			}		
		} else {
			if (grabadorMovimiento.isWaitingForRoutineName()) {
				GUI.Label(new Rect (0, 0, bW, bH), "Ingrese el nombre de la rutina");
				nombreRutina = GUI.TextField(new Rect (0, 60, bW, bH), nombreRutina);
				if (GUI.Button(new Rect (0, 120, bW, bH), "Ingresar")) {
					grabadorMovimiento.setRoutineName(nombreRutina);
				}
			} else {
			if (GUI.Button (new Rect (0, 120, bW, bH), "Detener Grabacion")) {		
					grabadorMovimiento.StopRecord ();
				}
			}
		}
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {

		if (grabadorMovimiento.isRecording() && !grabadorMovimiento.isWaitingForRoutineName()) {
			if (grabadorMovimiento.record().Length > 0) {
				Debug.Log(grabadorMovimiento.record());
			}
		}
	}

	/// <summary>
	/// Metodo que indica cuando se ha completado una grabacion.
	/// </summary>
	/// <param name="pathFile">Ruta del Archivo</param>
	public void grabacionCompletada(string pathFile) {
		string rutinasJsonStr = File.ReadAllText (@path + "rutinas.json");
		Debug.Log(rutinasJsonStr);
		ArrayList rutinasData = (ArrayList)JSON.JsonDecode(rutinasJsonStr);
		rutinasData.Add(pathFile);
		rutinasJsonStr = JSON.JsonEncode(rutinasData);
		var sr = File.CreateText(@path + "rutinas.json");
		sr.WriteLine (rutinasJsonStr);
		sr.Close();
	}
}

