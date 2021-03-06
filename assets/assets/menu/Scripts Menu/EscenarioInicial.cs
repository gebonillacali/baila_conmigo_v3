using UnityEngine;
using System.Collections;
using SimpleJSON;

public class EscenarioInicial : MonoBehaviour {


	private ArrayList limpiar;
	public string urlGrupos="openclassmedia.org/bailaconmigo/check_service_grupos.php"; 
	public string url="openclassmedia.org/bailaconmigo/check_service.php"; 
	private string mensaje="";
	private string mensajeGrupos="";
	
	public static string codigonino = "";
	public static string cod_nino = "";
	//private bool pausado = false;
	private Rect windowRect;

	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
	public static Vector2 scrollPosition = Vector2.zero; 
	
	public static bool paused = false;
	public static bool initiated = false;

	private float framerate = -1f;
	float deltaTimeFR = 0.0f;

	void Start(){
		updateGrupos ();
		Time.timeScale = 1;	
		initiated = true;
	}
	
	void OnGUI () {
		 
		limpiar = EscenarioRutinaManual.movimientosSeleccionados;

		//Framerate
		float msec = deltaTimeFR * 1000.0f;
		float fps = 1.0f / deltaTimeFR;		
		//GUI.Label(new Rect(0,0,100,100), string.Format("{0:0.0} ms ({1:0.} fps) ({1:0.} framerate)", msec, fps, framerate));

		//framerate = GUI.HorizontalSlider (new Rect (0, 100, 200, 20), framerate, 0, 100);
		//Time.timeScale = framerate;

		if (paused){


			GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
			if (GUI.Button(new Rect(0,0,bW,bH),"Menu Principal"))
			{
				limpiar.Clear();
				Application.LoadLevel("ok");
				KinectWrapper.NuiShutdown();
				initiated = true;
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}

		if (mensaje.Length > 0) {
			showJugadores();
		} else {
			if (mensajeGrupos.Length > 0) {
				showGrupos();
			} 
		}

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUI.Box(ScreenUtil.getPosElement(new Rect(750,150,400,300)), "Menu Adicionales", boxStyle);

		if(GUI.Button (ScreenUtil.getPosElement(new Rect (850,230,200,50)), "Crear nueva rutina", buttonsStyle)){
			initiated = false;
			Application.LoadLevel("3.EscenarioInterfazdeGrabacion");
			KinectWrapper.NuiShutdown();
		}

		if(GUI.Button (ScreenUtil.getPosElement(new Rect (850,300,200,50)), "Jugar sin conexion", buttonsStyle)){
			cod_nino = codigonino;
			initiated = false;
			SessionJuego.limpiarDatos();
			SessionJuego.setCodigoJugador(cod_nino);
			Application.LoadLevel("ok3");
			KinectWrapper.NuiShutdown();

		}

		if(GUI.Button(ScreenUtil.getPosElement(new Rect(850,370,200,50)),"Salir",buttonsStyle)){
			
			Application.Quit();
		}

	}

	private void showGrupos() {
		GUIStyle usersStyle = new GUIStyle(GUI.skin.button);
		usersStyle.fontSize = ScreenUtil.getFontFixedSize(35);
		
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		
		var dataUsuarios = JSON.JsonDecode (mensajeGrupos);
		ArrayList dataUsuarioAL = (ArrayList)dataUsuarios;
		scrollPosition = GUI.BeginScrollView (ScreenUtil.getPosElement(new Rect (175,150,500,300)),scrollPosition,ScreenUtil.getPosElement(new Rect (0, 0, 200, 102*dataUsuarioAL.Count))); 
		for (int i=0; i<dataUsuarioAL.Count; i++) {
			Hashtable dataUsuario = (Hashtable) dataUsuarioAL[i];
			if(GUI.Button (ScreenUtil.getPosElement(new Rect (50,(i*102),400,100)),(string) dataUsuario["nombreGrupo"], usersStyle)) {
				updateUsuarios((string)dataUsuario["idGrupo"]);
			}
		}
		
		GUI.EndScrollView();
	}

	private void showJugadores() {
		GUIStyle usersStyle = new GUIStyle(GUI.skin.button);
		usersStyle.fontSize = ScreenUtil.getFontFixedSize(35);
		
		usersStyle.hover.background = Texture2D.whiteTexture;
		usersStyle.hover.textColor = Color.black;
		
		var dataUsuarios = JSON.JsonDecode (mensaje);
		ArrayList dataUsuarioAL = (ArrayList)dataUsuarios;
		scrollPosition = GUI.BeginScrollView (ScreenUtil.getPosElement(new Rect (175,150,500,300)),scrollPosition,ScreenUtil.getPosElement(new Rect (0, 0, 200, 102*dataUsuarioAL.Count))); 

		for (int i=0; i<dataUsuarioAL.Count; i++) {
			Hashtable dataUsuario = (Hashtable) dataUsuarioAL[i];
			if(GUI.Button (ScreenUtil.getPosElement(new Rect (50,(i*102),400,100)),(string) dataUsuario["nom_nino"], usersStyle)) {
				cod_nino = (string)dataUsuario["cod_nino"];
				initiated =false;
				SessionJuego.limpiarDatos();
				SessionJuego.setCodigoJugador(cod_nino);
				SessionJuego.setNombreJugador((string)dataUsuario["nom_nino"]);
				Application.LoadLevel("ok3");
				KinectWrapper.NuiShutdown();
			}
		}

		GUI.EndScrollView();
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (225, 500, 400, 100)), "Volver")) {
			mensaje = "";
		}
	}

	private void updateGrupos() {
		WWW www = new WWW(urlGrupos);
		StartCoroutine(WaitForRequestGrupos(www));
	}

	private void updateUsuarios(string idGrupo) {
		WWW www = new WWW(url+"?idGrupo="+idGrupo);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequestGrupos(WWW hs_get){
		yield return hs_get;
		if(hs_get.error == null){
			mensajeGrupos = hs_get.text;
		} else {
			print("ERROR: "+hs_get.error);
		}
	}

	IEnumerator WaitForRequest(WWW hs_get){
		yield return hs_get;
		if(hs_get.error == null){
			mensaje = hs_get.text;
		} else {
			print("ERROR: "+hs_get.error);
		}
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			paused = tooglePause();
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if(mensaje.Length >0) {
				scrollControl(50);
			}
		}

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			if(mensaje.Length >0) {
				scrollControl(-50);
			}
		}

		deltaTimeFR += (Time.deltaTime - deltaTimeFR) * 0.1f;


	}

	public static void scrollControl(int value) { 
		scrollPosition = new Vector2(scrollPosition.x, scrollPosition.y+value);
	}

	bool tooglePause (){

		if (Time.timeScale == 0)
		{

			Screen.lockCursor = true;
			Time.timeScale = 1;
			return (false);

		}
		else
		{

			Screen.lockCursor = false;
			Time.timeScale = 0;
			return (true);
		}
	}
}
	
	
	

