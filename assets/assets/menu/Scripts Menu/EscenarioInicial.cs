using UnityEngine;
using System.Collections;
using SimpleJSON;

public class EscenarioInicial : MonoBehaviour {


	private ArrayList limpiar;
	public string url="openclassmedia.org/bailaconmigo/check_service.php"; 
	private string mensaje="";
	
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

	void Start(){
		WWW www = new WWW(url);
		Debug.Log ("entra al start");
		StartCoroutine(WaitForRequest(www));
		//Screen.lockCursor = true;
		Time.timeScale = 1;	
		initiated = true;
	}
	
	void OnGUI () {
		 
		limpiar = EscenarioRutinaManual.movimientosSeleccionados;

		if (paused){
			
			GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
			if (GUI.Button(new Rect(0,0,bW,bH),"Menu Principal"))
			{
				limpiar.Clear();
				Application.LoadLevel("ok");
				initiated = true;
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}

		if (mensaje.Length > 0) {
			GUIStyle usersStyle = new GUIStyle(GUI.skin.button);
			usersStyle.fontSize = 35;

			usersStyle.hover.background = Texture2D.whiteTexture;
			usersStyle.hover.textColor = Color.black;

			var dataUsuarios = JSON.JsonDecode (mensaje);
			ArrayList dataUsuarioAL = (ArrayList)dataUsuarios;
			scrollPosition = GUI.BeginScrollView (new Rect (175,150,500,300),scrollPosition,new Rect (0, 0, 200, 102*dataUsuarioAL.Count)); 
			for (int i=0; i<dataUsuarioAL.Count; i++) {
				Hashtable dataUsuario = (Hashtable) dataUsuarioAL[i];
				if(GUI.Button (new Rect (50,(i*102),400,100),(string) dataUsuario["nom_nino"], usersStyle)) {
					cod_nino = (string)dataUsuario["cod_nino"];
					initiated =false;
					Application.LoadLevel("ok3");
				}
			}

			GUI.EndScrollView();
		}

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.fontSize = 20;

		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = 20;

		GUI.Box(new Rect(750,150,400,300), "Menu Adicionales", boxStyle);

		if(GUI.Button (new Rect (850,230,200,50), "Crear nueva rutina", buttonsStyle)){
			initiated = false;
			Application.LoadLevel("3.EscenarioInterfazdeGrabacion");
		}

		if(GUI.Button (new Rect (850,300,200,50), "Jugar sin conexion", buttonsStyle)){
			cod_nino = codigonino;
			initiated = false;
			Application.LoadLevel("ok3");
		}

		if(GUI.Button(new Rect(850,370,200,50),"Salir",buttonsStyle)){
			
			Application.Quit();
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
	
	
	

