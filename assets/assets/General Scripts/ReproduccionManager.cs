﻿using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class ReproduccionManager : MonoBehaviour {

	/// <summary>
	/// Atributos privados de logica privados
	/// </summary>
	private string path = "Recordings/playback/";
	private JSONArray rutinasData = null;
	private Vector2 scrollPosition = new Vector2();
	private int bW = 200;
	private int bH = 50;
	private int gW = 400;
	private int GH = 170;
	private string nombreRutinaSelected = "";
	private string pathRutinaSelected = "";
	private float timer;

	/// <summary>
	/// Atributos publicos objectos entrada.
	/// </summary>
	public ReproductorMovimiento reproductorMovimiento;
	public KinectManager kinect;


	// Use this for initialization
	void Start () {
		getRutinas ();
	}

	void OnGUI () {

		GUIStyle labelsStyleTitle = new GUIStyle(GUI.skin.label);
		labelsStyleTitle.fontSize = 35;
		labelsStyleTitle.fontStyle = FontStyle.Bold;

		GUI.Label(new Rect(490,50,600,bH),"Rutinas FNP Creadas", labelsStyleTitle);

		GUIStyle labelsStyle = new GUIStyle(GUI.skin.label);
		labelsStyle.fontSize = 20;

		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = 20;

		if (rutinasData != null && rutinasData.Count > 0 && reproductorMovimiento != null && !reproductorMovimiento.isPlaying() && nombreRutinaSelected.Length <= 0) {		

			nombreRutinaSelected = "";
			pathRutinaSelected = "";
			GUI.Label(new Rect(510,130,400,100),"Selecciona la rutina que deseas jugar",labelsStyle);

			GUIStyle usersStyle = new GUIStyle(GUI.skin.button);
			usersStyle.fontSize = 20;			
			usersStyle.hover.background = Texture2D.whiteTexture;
			usersStyle.hover.textColor = Color.black;

			scrollPosition = GUI.BeginScrollView (new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)) + 100, gW, GH),scrollPosition,new Rect (0, 0, gW, rutinasData.Count * 51)); 

			kinect.ControlMouseCursor = true;

			for (int i = 0; i < rutinasData.Count ; i++) {
				string nombreRutina = rutinasData[i]["nombreRutina"];
				string pathFile = rutinasData[i]["pathRutina"];
				//Debug.Log(nombreRutina);
				if (GUI.Button (new Rect (0, i * 51, bW * 2	, bH), nombreRutina, usersStyle)) {
					Debug.Log("Rutina:" + nombreRutina + " seleccionada");
					if (reproductorMovimiento != null) {
						GUI.Label(new Rect(0,50,bW * 2	, bH),"Cargando... Espere por favor ;)", labelsStyleTitle);
						nombreRutinaSelected = nombreRutina;
						pathRutinaSelected = pathFile;
					}
					//nombreRutinaSelected = nombreRutina;
					//KinectWrapper.setInputFile(pathFile);
				}
			}
			GUI.EndScrollView();
			if (GUI.Button (new Rect (530, 400, 300, 50), "Volver", buttonsStyle)) {
				Application.LoadLevel("ok3");
			}
		} else {
			if (reproductorMovimiento != null && !reproductorMovimiento.isPlaying() && nombreRutinaSelected.Length <= 0) {
			//if (!KinectWrapper.isPlaying()) {
				//GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
				GUI.Label(new Rect (490, 150, 400, bH), "No hay rutinas grabadas", labelsStyle);
				if (GUI.Button (new Rect (530, 400, 300, 50), "Volver", buttonsStyle)) {
					Application.LoadLevel("ok3");
				}
				//GUI.EndGroup();
			} else {
				if (reproductorMovimiento != null && reproductorMovimiento.isPlaying()) {
				//if (KinectWrapper.isPlaying()) {
					GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
					string minutes = Mathf.Floor(timer / 60) < 10 ? "0" + (Mathf.Floor(timer / 60)) : (Mathf.Floor(timer / 60)) + "";
					string seconds = Mathf.RoundToInt(timer % 60) < 10 ? "0" + Mathf.RoundToInt((timer % 60)).ToString() : Mathf.RoundToInt((timer % 60)).ToString() + "";
					GUI.Label(new Rect (0, 0, bW, bH), nombreRutinaSelected + " - " + minutes + ":" + seconds);
					GUI.EndGroup();
					kinect.ControlMouseCursor = false;
				} else {
					if (nombreRutinaSelected.Length > 0) {
						GUI.Label(new Rect(0,50,bW * 2	, bH),"Cargando... Espere por favor ;)", labelsStyleTitle);
						timer = 0;
						reproductorMovimiento.setPathFile(pathRutinaSelected);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (reproductorMovimiento != null && reproductorMovimiento.isPlaying ()) {
		//if (KinectWrapper.isPlaying ()) {
			timer += Time.deltaTime;
		}
	}

	private void getRutinas() {
		string rutinasJsonStr = File.ReadAllText (@path + "rutinas.json");
		Debug.Log(rutinasJsonStr);
		this.rutinasData = JSONArray.Parse(rutinasJsonStr).AsArray;
	}
}
