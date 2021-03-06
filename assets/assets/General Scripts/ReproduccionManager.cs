﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using System;

public class ReproduccionManager : MonoBehaviour, ReproductorArchivoLector.ReproductorNotifier, AudioMp3Manager.AudioMp3Listener, ReproductorMovimiento.ReproduccionActions {

	enum ReproductionState {
		MODALIDAD_SELECCION,
		GENERACION_RUTINA,
		RUTINA_SELECCION,
		SELECCION_CANCION_RUTINA_GENERADA,
		MOSTRAR_CARGANDO,
		MOSTRAR_INSTRUCCIONES,
		RUTINA_EJECUCION,
		RUTINA_FINALIZADA,
		MOSTRAR_PUNTAJE,
		PAUSA
	}

	/// <summary>
	/// Atributos privados de logica privados
	/// </summary>
	private string path = "Recordings/playback/";
	private JSONArray rutinasData = null;
	private Vector2 scrollPosition = new Vector2();
	private int bW = ScreenUtil.getFontFixedSize(200);
	private int bH = ScreenUtil.getFontFixedSize(50);
	private int gW = ScreenUtil.getFontFixedSize(400);
	private int GH = ScreenUtil.getFontFixedSize(170);
	private string nombreRutinaSelected = "";
	private string pathRutinaSelected = "";
	private bool[] movimientos;
	private int repeticionesMovimientos = 1;
	private string cancionRutinaSelected = "";
	private float timer;
	private ReproductionState state;
	private float percent;
	private bool rutinaLoaded = false;
	private bool cancionLoaded = false;
	private bool rutinaGenerada = false;
	private List<MovimientoFrameRange> movimientosFrameRange;
	private GUIStyle labelsStyleTitle = new GUIStyle(GUI.skin.label);
	private GUIStyle labelsStyle = new GUIStyle(GUI.skin.label);
	private GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
	
	public Texture2D AjInstrucciones;
	//private float framerate = 1;
	float deltaTimeFR = 0.0f;
	public AudioSource empecemosAudio;

	/// <summary>
	/// Atributos publicos objectos entrada.
	/// </summary>
	public ReproductorMovimiento reproductorMovimiento;
	public KinectManager kinect;
	public AudioMp3Manager audioManagerMp3;

	// Use this for initialization
	void Start () {
		rutinaLoaded = false;
		cancionLoaded = false;
		state = ReproductionState.MODALIDAD_SELECCION;

		//Registrando listeners
		audioManagerMp3.audioListener = this;
		reproductorMovimiento.reproductorNotifier = this;
		reproductorMovimiento.actions = this;
		//reproductorMovimiento.gameObject.SetActive(false);
		getRutinas ();
	}

	void OnGUI () {

		//Estableciendo styles
		labelsStyleTitle = new GUIStyle(GUI.skin.label);
		labelsStyle = new GUIStyle(GUI.skin.label);
		buttonsStyle = new GUIStyle(GUI.skin.button);

		labelsStyleTitle.fontSize = ScreenUtil.getFontFixedSize(35);
		labelsStyleTitle.fontStyle = FontStyle.Bold;
		//GUI.Label(ScreenUtil.getPosElement(new Rect(490,50,600,bH)),"Rutinas FNP Creadas", labelsStyleTitle);

		labelsStyle.fontSize = ScreenUtil.getFontFixedSize(20);
		buttonsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		switch (state) {
			case ReproductionState.MODALIDAD_SELECCION:
				modalidadSeleccionReproduccion();			
				break;
			case ReproductionState.GENERACION_RUTINA:
				generacionRutina();			
			break;
			case ReproductionState.SELECCION_CANCION_RUTINA_GENERADA:
				seleccionCancionRutinaGenerada();
			break;
			case ReproductionState.RUTINA_SELECCION:
					rutinaSeleccion();
				break;
			case ReproductionState.MOSTRAR_CARGANDO:
					mostrarCargando();
				break;
			case ReproductionState.MOSTRAR_INSTRUCCIONES:
					mostrarInstrucciones();
				break;
			case ReproductionState.RUTINA_EJECUCION:
				rutinaEjecucion();		
			break;
			case ReproductionState.MOSTRAR_PUNTAJE:
				mostrarPuntaje();		
			break;
			case ReproductionState.PAUSA:
				pausaJuego();
			break;
		}
	}

	/* INIT GUI ELEMENTS*/

	private void modalidadSeleccionReproduccion() {
		labelsStyle.fontSize = ScreenUtil.getFontFixedSize(40);
		rutinaGenerada = false;
		SessionJuego.setRutinaGenerada(false);
		GUI.Label (ScreenUtil.getPosElement (new Rect (510, 130, 400, 100)), "Como deseas Jugar?", labelsStyle);
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 250, 300, 100)), "Generar Rutina con Movimientos", buttonsStyle)) {
			state = ReproductionState.GENERACION_RUTINA;
			rutinaGenerada = true;
			SessionJuego.setRutinaGenerada(true);
		}
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 360, 300, 100)), "Rutinas grabadas", buttonsStyle)) {
			state = ReproductionState.RUTINA_SELECCION;
			rutinaGenerada = false;
			SessionJuego.setRutinaGenerada(false);
		}

		if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 500, 300, 50)), "Volver", buttonsStyle)) {
			Application.LoadLevel ("ok3");
			KinectWrapper.NuiShutdown ();
		}
	}

	private void mostrarInstrucciones() {
		GUI.DrawTexture (ScreenUtil.getPosElement (new Rect (300, 150, 800, 600)), AjInstrucciones);
		if (rutinaLoaded && cancionLoaded && GUI.Button (ScreenUtil.getPosElement (new Rect (510, 550, 100, 50)), "Empezar")) {
			Debug.Log ("Empezar");
			reproductorMovimiento.setAllowToPlay (true);
			empecemosAudio.Play ();
			audioManagerMp3.play ();
			state = ReproductionState.RUTINA_EJECUCION;
			timer = 0;
		}
	}

	private void mostrarCargando() {
		GUI.Label (ScreenUtil.getPosElement (new Rect (490, 300, 500, 600)), "Cargando...  Espere por favor", labelsStyleTitle);
		if (timer > 3) {
			reproductorMovimiento.reproductorNotifier = this;
			reproductorMovimiento.setPathFile (pathRutinaSelected);
			state = ReproductionState.MOSTRAR_INSTRUCCIONES;
		}
	}

	private void noHayRutinas() {
		
		GUI.Label (ScreenUtil.getPosElement (new Rect (490, 150, 400, 50)), "No hay rutinas grabadas", labelsStyle);
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 400, 300, 50)), "Volver", buttonsStyle)) {
			Application.LoadLevel ("ok3");
			KinectWrapper.NuiShutdown ();
		}
	}

	private void generacionRutina() {
		if (rutinasData != null && rutinasData.Count > 0) {

			nombreRutinaSelected = "";
			pathRutinaSelected = "";
			cancionRutinaSelected = "";
			rutinaLoaded = false;
			cancionLoaded = false;
			if (audioManagerMp3.isPlaying) {
				audioManagerMp3.stop ();
			}
			labelsStyle.fontSize = ScreenUtil.getFontFixedSize(40);
			GUI.Label (ScreenUtil.getPosElement (new Rect (400, 130, 700, 100)), "Selecciona los movimientos de tu rutina", labelsStyle);
			GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
			usersStyle.fontSize = 20;			
			usersStyle.hover.background = Texture2D.whiteTexture;
			usersStyle.hover.textColor = Color.black;
			
			scrollPosition = GUI.BeginScrollView (new Rect (((Screen.width / 2) - (gW / 2)), ((Screen.height / 2) - (gW / 2))+50, gW + 12, GH), scrollPosition, new Rect (0, 0, gW, rutinasData.Count * 51)); 
			if (movimientos == null) {
				setMovimientos(false);
			}
			int posy=0;
			for (int i = 0; i < rutinasData.Count; i++) {
				string nombreRutina = rutinasData [i] ["nombreRutina"];
				string pathFile = rutinasData [i] ["pathRutina"];
				string codigoRutina = rutinasData [i] ["codigoRutina"];
				string cancionRutina = rutinasData [i] ["cancionRutina"];
				string isMovimiento = rutinasData [i] ["isMovimiento"];
				
				if (isMovimiento == "no") {
					continue;
				}

				movimientos[i] = GUI.Toggle(new Rect (0, posy * 51, bW * 2, bH), movimientos[i] , nombreRutina);

				posy++;
			}
			GUI.EndScrollView ();
			GUI.Label(ScreenUtil.getPosElement (new Rect (500, 450, 300, 50)), "Repeticiones " + repeticionesMovimientos);
			repeticionesMovimientos = (int) GUI.HorizontalSlider (ScreenUtil.getPosElement (new Rect (500, 500, 300, 50)),(float) repeticionesMovimientos, 1, 20);

			if (GUI.Button (ScreenUtil.getPosElement (new Rect (900, 300, 250, 50)), "Seleccionar Todos", buttonsStyle)) {
				setMovimientos(true);
			}
			if (GUI.Button (ScreenUtil.getPosElement (new Rect (900, 350, 250, 50)), "Quitar Seleccion", buttonsStyle)) {
				setMovimientos(false);
			}
			if (GUI.Button (ScreenUtil.getPosElement (new Rect (550, 530, 300, 50)), "Generar Rutina", buttonsStyle)) {
				state = ReproductionState.SELECCION_CANCION_RUTINA_GENERADA;
			}
			if (GUI.Button (ScreenUtil.getPosElement (new Rect (550, 600, 300, 50)), "Volver", buttonsStyle)) {
				state = ReproductionState.MODALIDAD_SELECCION;
			}
		}
	}

	private void seleccionCancionRutinaGenerada() {
		GUI.Label (ScreenUtil.getPosElement (new Rect (510, 130, 400, 100)), "Selecciona la cancion que deseas para jugar", labelsStyle);
		scrollPosition = GUI.BeginScrollView (new Rect (((Screen.width / 2) - (gW / 2)), ((Screen.height / 2) - (gW / 2)) + 100, gW*2, GH), scrollPosition, new Rect (0, 0, gW, rutinasData.Count * 51)); 
		GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
		usersStyle.fontSize = 20;			
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		int posy = 0;
		List<string> canciones = new List<string>();
		for (int i = 0; i < rutinasData.Count; i++) {
				string nombreRutina = rutinasData [i] ["nombreRutina"];
				string pathFile = rutinasData [i] ["pathRutina"];
				string codigoRutina = rutinasData [i] ["codigoRutina"];
				string cancionRutina = rutinasData [i] ["cancionRutina"];
				string isMovimiento = rutinasData [i] ["isMovimiento"];

				string cancionRutinaStrip = cancionRutina.Split('/')[cancionRutina.Split('/').Length-1];	
				if (isMovimiento == "si" || canciones.Contains(cancionRutinaStrip)) {
						continue;
				}
				
				canciones.Add(cancionRutinaStrip);
				if (GUI.Button (new Rect (0, posy * 51, bW * 2, bH), cancionRutinaStrip, usersStyle)) {
					state = ReproductionState.MOSTRAR_CARGANDO;
					timer = 0;
					GUI.Label (new Rect (0, 50, bW * 2, bH), "Cargando... Espere por favor ;)", labelsStyleTitle);
					nombreRutinaSelected = "Rutina Generada";
					pathRutinaSelected = getMovimientosPath();
					cancionRutinaSelected = cancionRutina;
					Debug.Log ("Nombre Rutina:" + nombreRutinaSelected);
					Debug.Log ("cancion Rutina:" + cancionRutinaSelected);
	
				}
			posy++;
		}
		GUI.EndScrollView ();
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 500, 300, 50)), "Volver", buttonsStyle)) {
			state = ReproductionState.GENERACION_RUTINA;
		}
	}

	private void rutinaSeleccion() {
		if (rutinasData != null && rutinasData.Count > 0) {
			nombreRutinaSelected = "";
			pathRutinaSelected = "";
			cancionRutinaSelected = "";
			rutinaLoaded = false;
			cancionLoaded = false;
			if (audioManagerMp3.isPlaying) {
					audioManagerMp3.stop ();
			}
			labelsStyle.fontSize = ScreenUtil.getFontFixedSize(40);
			GUI.Label (ScreenUtil.getPosElement (new Rect (400, 130, 700, 100)), "Selecciona la rutina que deseas jugar", labelsStyle);

			GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
			usersStyle.fontSize = 20;			
			usersStyle.hover.background = Texture2D.whiteTexture;
			usersStyle.hover.textColor = Color.black;

			scrollPosition = GUI.BeginScrollView (new Rect (((Screen.width / 2) - (gW / 2)), ((Screen.height / 2) - (gW / 2)) + 50, gW*2, GH), scrollPosition, new Rect (0, 0, gW, rutinasData.Count * 51)); 
			int posy = 0;
			for (int i = 0; i < rutinasData.Count; i++) {
					string nombreRutina = rutinasData [i] ["nombreRutina"];
					string pathFile = rutinasData [i] ["pathRutina"];
					string codigoRutina = rutinasData [i] ["codigoRutina"];
					string cancionRutina = rutinasData [i] ["cancionRutina"];
					string isMovimiento = rutinasData [i] ["isMovimiento"];
					
					if (isMovimiento == "si") {
						continue;
					}
					//Debug.Log(nombreRutina);
					if (GUI.Button (new Rect (0, posy * 51, bW * 2, bH), nombreRutina, usersStyle)) {
							Debug.Log ("Rutina:" + nombreRutina + " seleccionada");
							Debug.Log ("Rutina:" + codigoRutina + " seleccionada");
							SessionJuego.setCodigoRutina (int.Parse (codigoRutina));
							SessionJuego.setRepeticionesMovimientos(repeticionesMovimientos);
							if (reproductorMovimiento != null) {
									state = ReproductionState.MOSTRAR_CARGANDO;
									timer = 0;
									GUI.Label (new Rect (0, 50, bW * 2, bH), "Cargando... Espere por favor ;)", labelsStyleTitle);
									nombreRutinaSelected = nombreRutina;
									pathRutinaSelected = pathFile;
									cancionRutinaSelected = cancionRutina;
									Debug.Log ("Nombre Rutina:" + nombreRutinaSelected);
									Debug.Log ("cancion Rutina:" + cancionRutinaSelected);
		
							}
							//nombreRutinaSelected = nombreRutina;
							//KinectWrapper.setInputFile(pathFile);
					}
				posy++;
			}
			GUI.EndScrollView ();
			GUI.Label(ScreenUtil.getPosElement (new Rect (500, 450, 300, 50)), "Repeticiones " + repeticionesMovimientos);
			repeticionesMovimientos = (int) GUI.HorizontalSlider (ScreenUtil.getPosElement (new Rect (500, 500, 300, 50)),(float) repeticionesMovimientos, 1, 20);
			if (GUI.Button (ScreenUtil.getPosElement (new Rect (530, 550, 300, 50)), "Volver", buttonsStyle)) {
				state = ReproductionState.MODALIDAD_SELECCION;
			}
		} else {
			noHayRutinas();
		}
	}

	private void rutinaEjecucion() {
		GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
		usersStyle.fontSize = 20;			
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (900, 700, 400, 100)), "Pausar", usersStyle)) {
			pausar();
		}
	}

	private void pausaJuego() {
		GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
		usersStyle.fontSize = 20;			
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (900, 700, 400, 100)), "Continuar", usersStyle)) {
			continuarJuego();
		}
	}

	private void mostrarPuntaje() {

		GUI.Label (ScreenUtil.getPosElement (new Rect (510, 130, 400, 100)), "Tu puntaje fue de " + SessionJuego.getAcumuladoJugador().ToString(),labelsStyleTitle);
		GUIStyle usersStyle = new GUIStyle (GUI.skin.button);
		usersStyle.fontSize = 20;			
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (510, 300, 400, 100)), "Continuar", usersStyle)) {
			state = ReproductionState.MODALIDAD_SELECCION;
			SessionJuego.prepareForNextGame();
		}
	}

	/* END GUI ELEMENTS  */

	private void continuarJuego() {
		state = ReproductionState.RUTINA_EJECUCION;
		reproductorMovimiento.setAllowToPlay (true);
		audioManagerMp3.play ();
	}

	private void pausar() {
		state = ReproductionState.PAUSA;
		reproductorMovimiento.setAllowToPlay (false);
		audioManagerMp3.pause ();
	}

	private void setMovimientos(bool value) {

		if (movimientos == null) {
			movimientos = new bool[rutinasData.Count];
		}
		
		for (int i = 0; i < rutinasData.Count; i++) {
			movimientos[i] = value;
		}
	}

	private string getMovimientosPath() {
		List<string> paths = new List<string> ();
		SessionJuego.setCodigoRutina (128);
		for (int i = 0; i < rutinasData.Count; i++) {			
			string pathFile = rutinasData [i] ["pathRutina"];
			string isMovimiento = rutinasData [i] ["isMovimiento"];
			if (isMovimiento == "si" && movimientos[i]) {				
				paths.Add(pathFile);
			}
		}
		for(int numRepeticiones = 0 ; numRepeticiones < repeticionesMovimientos ; numRepeticiones++) {
			for (int i = 0; i < rutinasData.Count; i++) {
				string codigoRutina = rutinasData [i] ["codigoRutina"];
				string isMovimiento = rutinasData [i] ["isMovimiento"];
				if (isMovimiento == "si" && movimientos[i]) {
					SessionJuego.getCodigoMovimientos().Add(codigoRutina);
				}
			}
		}
		SessionJuego.setRepeticionesMovimientos (repeticionesMovimientos);
		string finalPaths = string.Join (",", paths.ToArray ());
		return finalPaths;
	}



	/* Implementaciones de Interfaces */

	public void rutinaCargada (int numFrames, List<MovimientoFrameRange> movimientosFrameRange)
	{
		this.movimientosFrameRange = movimientosFrameRange;
		for (int i=0; i < movimientosFrameRange.Count; i++) {
			SessionJuego.getMovimientoFrameRange().Add(movimientosFrameRange[i]);
		}
						
		rutinaLoaded = true;

		cancionLoaded = false;
		audioManagerMp3.loadAudio (cancionRutinaSelected);
	}

	public List<MovimientoFrameRange> getRangoMovimientos ()
	{
		return movimientosFrameRange;
	}

	public bool isRutinaGenerada ()
	{
		return rutinaGenerada;
	}

	public void audioLoaded (string fileSelected)
	{
		cancionLoaded = true;
	}

	public void juegoTerminado ()
	{
		state = ReproductionState.MOSTRAR_PUNTAJE;
		audioManagerMp3.UnloadAudio ();
	}

	// Update is called once per frame
	void Update () {
		if (state != ReproductionState.MOSTRAR_CARGANDO) {
			timer = 0;
		} else {
			timer += Time.deltaTime;
		}


	}

	private void getRutinas() {
		string rutinasJsonStr = File.ReadAllText (@path + "rutinas.json");
		Debug.Log(rutinasJsonStr);
		this.rutinasData = JSONArray.Parse(rutinasJsonStr).AsArray;
		this.rutinasData.reverse();
	}



}
