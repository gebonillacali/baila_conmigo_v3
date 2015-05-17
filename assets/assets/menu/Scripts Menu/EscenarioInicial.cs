using UnityEngine;
using System.Collections;

public class EscenarioInicial : MonoBehaviour {


	private ArrayList limpiar;
	public string url="openclassmedia.org/bailaconmigo/check.php"; 
	private string mensaje="";
	
	public static string codigonino = "";
	public static string cod_nino = "";
	//private bool pausado = false;
	private Rect windowRect;

	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
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
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}
	
		GUI.Box( new Rect(400,200,200,240), mensaje);
		codigonino = GUI.TextArea (new Rect (760, 200, 130, 20), codigonino);
		if(GUI.Button (new Rect (775,270,100,25), "Aceptar")){
			cod_nino = codigonino;
			if(cod_nino!= null){
				Application.LoadLevel("ok3");}
		}

		if(GUI.Button (new Rect (900,450,200,50), "Jugar sin conexion")){
			cod_nino = codigonino;
			Application.LoadLevel("ok3");
		}

		if(GUI.Button(new Rect(1050,510,50,40),"Salir")){
			
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
	
	
	

