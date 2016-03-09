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
	private int GH = 260;

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
		Debug.Log (Application.loadedLevelName);
		if (Application.loadedLevelName != "3.EscenarioInterfazdeGrabacion") {
			return;
		}
		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = 20;

		GUIStyle labelsStyle = new GUIStyle(GUI.skin.label);
		labelsStyle.fontSize = 20;

		GUIStyle textFileldsStyle = new GUIStyle(GUI.skin.textField);
		textFileldsStyle.fontSize = 20;

		GUIStyle labelsStyleTitle = new GUIStyle(GUI.skin.label);
		labelsStyleTitle.fontSize = 35;
		labelsStyleTitle.fontStyle = FontStyle.Bold;
		GUI.Label(new Rect(440,50,600,bH),"Grabacion de Rutinas FNP", labelsStyleTitle);

		GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
		if (!grabadorMovimiento.isRecording ()) {

			if (GUI.Button (new Rect (0, 120, bW, bH), "Iniciar Grabacion", buttonsStyle)) {
					grabadorMovimiento.StartRecord ();
			}
			if (GUI.Button (new Rect (0, 200, bW, bH), "Volver", buttonsStyle)) {
				Application.LoadLevel("ok");
			}
		} else {
			if (grabadorMovimiento.isWaitingForRoutineName()) {
				GUI.Label(new Rect (0, 0, bW, bH), "Ingrese el nombre de la rutina", labelsStyle);
				nombreRutina = GUI.TextField(new Rect (0, 60, bW, bH), nombreRutina, textFileldsStyle);
				if (GUI.Button(new Rect (0, 120, bW, bH), "Ingresar", buttonsStyle)) {
					grabadorMovimiento.setRoutineName(nombreRutina.Replace(" ", ""));
				}
			} else {
			if (GUI.Button (new Rect (0, 120, bW, bH), "Detener Grabacion", buttonsStyle)) {		
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
		JSONArray rutinasData = JSONArray.Parse(rutinasJsonStr).AsArray;

		JSONClass rutina = new JSONClass();
		rutina["nombreRutina"] = nombreRutina;
		rutina["pathRutina"] = pathFile;

		rutinasData.Add(rutina);

		rutinasJsonStr = JSON.JsonEncode(rutinasData.ToString()).Replace("\"[","[").Replace("]\"","]").Replace("\\","");
		Debug.Log(rutinasJsonStr);
		var sr = File.CreateText(@path + "rutinas.json");
		sr.WriteLine (rutinasJsonStr);
		sr.Close();
	}
}

