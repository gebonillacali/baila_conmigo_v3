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
		if (rutinasData != null && rutinasData.Count > 0 && reproductorMovimiento != null && !reproductorMovimiento.isPlaying()) {
		//if (rutinasData != null && rutinasData.Count > 0 && !KinectWrapper.isPlaying()) {
			GUIStyle usersStyle = new GUIStyle(GUI.skin.button);
			usersStyle.fontSize = 20;
			
			usersStyle.hover.background = Texture2D.whiteTexture;
			usersStyle.hover.textColor = Color.black;

			scrollPosition = GUI.BeginScrollView (new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH),scrollPosition,new Rect (0, 0, gW, rutinasData.Count * 51)); 

			kinect.ControlMouseCursor = true;

			for (int i = 0; i < rutinasData.Count ; i++) {
				string nombreRutina = rutinasData[i]["nombreRutina"];
				string pathFile = rutinasData[i]["pathRutina"];
				Debug.Log(nombreRutina);
				if (GUI.Button (new Rect (0, i * 51, bW * 2	, bH), nombreRutina, usersStyle)) {
					Debug.Log("Rutina:" + nombreRutina + " seleccionada");
					if (reproductorMovimiento != null) {
						timer = 0;
						nombreRutinaSelected = nombreRutina;
						reproductorMovimiento.setPathFile(pathFile);
					}
					//nombreRutinaSelected = nombreRutina;
					//KinectWrapper.setInputFile(pathFile);
				}
			}
			GUI.EndScrollView();					
		} else {
			if (reproductorMovimiento != null && !reproductorMovimiento.isPlaying()) {
			//if (!KinectWrapper.isPlaying()) {
				GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
				GUI.Label(new Rect (0, 0, bW, bH), "No hay rutinas grabadas");
				GUI.EndGroup();
			} else {
				if (reproductorMovimiento != null && reproductorMovimiento.isPlaying()) {
				//if (KinectWrapper.isPlaying()) {
					GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
					string minutes = Mathf.Floor(timer / 60) < 10 ? "0" + (Mathf.Floor(timer / 60)) : (Mathf.Floor(timer / 60)) + "";
					string seconds = Mathf.RoundToInt(timer % 60) < 10 ? "0" + Mathf.RoundToInt((timer % 60)).ToString() : Mathf.RoundToInt((timer % 60)).ToString() + "";
					GUI.Label(new Rect (0, 0, bW, bH), nombreRutinaSelected + " - " + minutes + ":" + seconds);
					GUI.EndGroup();
					kinect.ControlMouseCursor = false;
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
