// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class EscenarioRutinaManual : MonoBehaviour {
	private ArrayList limpiar;
	public string url="openclassmedia.org/bailaconmigo/check2.php"; 
	public string url2="openclassmedia.org/bailaconmigo/consultar_cod_rutina.php"; 
	private string mensaje="";
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
	private bool paused = false;
	public static string nomrutina = "";
	public static string nom_rutina = "";
	string cod_rutina;
	int canti=0;

	public static ArrayList codigo_movimientos = new ArrayList();
	public static ArrayList arregloMovimientos = new ArrayList();
	public static ArrayList movimientosSeleccionados = new ArrayList();
	public static string [] animaciones = new string[18];
	
	Vector2 scrollViewVector = Vector2.zero;
	
	Vector3 scrollViewVector1 = Vector3.zero;
	
	Vector4 scrollViewVector2 = Vector4.zero;
	
	GUITexture textura1 ; 

	public Texture2D icon;
	public Texture2D icon2;
	public Texture2D icon3;
	public Texture2D icon4;
	public Texture2D icon5;
	public Texture2D icon6;
	public Texture2D icon7;
	public Texture2D icon8;
	public Texture2D icon9;
	public Texture2D icon10;
	public Texture2D icon11;
	public Texture2D icon12;
	public Texture2D icon13;
	public Texture2D icon14;
	public Texture2D icon15;
	public Texture2D icon16;
	public Texture2D icon17;
	public Texture2D icon18;
	public Texture2D icon19;
	public Texture2D icon20;
	public Texture2D icon21;
	AnimationClip walkClip;

	
	public static string animacion = "CodosArriba";
	public static string animacion1 = "BrazpDerPiernaIzq";
	public static string animacion2 = "BrazosArriba";
	public static string animacion3 = "BrazosAbiertosArriba";
	public static string animacion4 = "BrazosAbiertos";
	public static string animacion5 = "BrazoIzqPiernaDer";
	public static string animacion6 = "BrazoDerArriba";
	public static string animacion7 = "BrazoDerAdelante";
	public static string animacion8 = "AbduccionPiernasBrazoIzquierdo";
	public static string animacion9 = "AbduccionBrazosRodillaIzq";
	public static string animacion10 = "AbduccionBrazosRodillaDer";
	public static string animacion11 = "AbduccionBrazos";
	public static string animacion12 = "CodosArriba";
	public static string animacion13 = "CodosArriba";
	public static string animacion14 = "CodosArriba";
	public static string animacion15 = "CodosArriba";
	public static string animacion16 = "CodosArriba";
	public static string animacion17 = "CodosArriba";
	
	
	

	public static bool toggleBool= false;
	public static bool toggleBool1= false;
	public static bool toggleBool2= false;
	public static bool toggleBool3= false;
	public static bool toggleBool4= false;
	public static bool toggleBool5= false;
	public static bool toggleBool6= false;
	public static bool toggleBool7= false;
	public static bool toggleBool8= false;
	public static bool toggleBool9= false;
	public static bool toggleBool10= false;
	public static bool toggleBool11= false;
	public static bool toggleBool12= false;
	public static bool toggleBool13= false;
	public static bool toggleBool14= false;
	public static bool toggleBool15= false;
	public static bool toggleBool16= false;
	public static bool toggleBool17= false;

	void Start(){
		WWW www = new WWW(url);
		//Debug.Log ("entra al start");
		StartCoroutine(WaitForRequest(www));
		//Screen.lockCursor = true;
		Time.timeScale = 1;
	}
	
	void  OnGUI (){

	/* GUI.Box( new Rect(940,100,200,240), mensaje);
	 nomrutina = GUI.TextArea (new Rect (760, 250, 130, 20), nomrutina);
	if(GUI.Button (new Rect (775,350,100,25), "Guardar Rutina")){
		nom_rutina = nomrutina;
	    guardarRutina();
		}*/

		if (paused ){
			GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
			if (GUI.Button(new Rect(0,0,bW,bH),"Menu Principal"))
			{
				arregloMovimientos.Clear();

				vaciarArr();
				Application.LoadLevel("ok");
				KinectWrapper.NuiShutdown();
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}

		// esto es para Seleccionar alguna de las dos opciones
		
		// Make a background box
	
		// Boton Grabar rutina
		//if(GUI.Button ( new Rect(1000,500,100,50), "Grabar Rutina")){}
		
		// Boton jugar
		
		if(GUI.Button (ScreenUtil.getPosElement(new Rect(1050,200,100,50)), "Jugar")){
			llenarArreglo();
			seleccionarRutina();
			Application.LoadLevel ("1.EscenarioPrincipal1");
			KinectWrapper.NuiShutdown();
			
		}
		if(GUI.Button(ScreenUtil.getPosElement(new Rect(1150,500,50,40)),"Salir")){
			
			Application.Quit();
		}
		if(GUI.Button(ScreenUtil.getPosElement(new Rect(1000,450,200,40)), "Cambiar tipo de rutina")) {
			Application.LoadLevel("ok3");
			KinectWrapper.NuiShutdown();
		}
		if(GUI.Button(ScreenUtil.getPosElement(new Rect(1000,400,200,40)), "Cambiar de jugador")) {
			Application.LoadLevel("ok");
			KinectWrapper.NuiShutdown();
		}
		
		scrollViewVector = GUI.BeginScrollView ( ScreenUtil.getPosElement(new Rect(100, 150, 200, 400)), scrollViewVector,ScreenUtil.getPosElement(new  Rect (0, 0, 600, 600)));
		
		toggleBool = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 0, 100, 30)), toggleBool, "Ejercicio 1");
		
		toggleBool1 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0,100, 100, 30)), toggleBool1, "Ejercicio 2");
		
		toggleBool2 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 200, 100, 30)), toggleBool2, "Ejercicio 3");
		
		toggleBool3 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 300, 100, 30)), toggleBool3, "Ejercicio 4");
		
		toggleBool4 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 400, 100, 30)), toggleBool4, "Ejercicio 5");
		
		toggleBool5 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 500, 100, 30)), toggleBool5, "Ejercicio 6");
		
		
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,0,100,100)), icon);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,100,100,100)), icon2);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,200,100,100)), icon3);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,300,100,100)), icon4);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,400,100,100)), icon5);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,500,100,100)), icon6);
		
		
		/*	if(!textura1){
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		GUI.DrawTexture( new Rect(10,10,60,60), textura1, ScaleMode.ScaleToFit, true, 10.0ff);
*/	
		GUI.EndScrollView();
		
		
		
		
		
		
		scrollViewVector1 = GUI.BeginScrollView ( ScreenUtil.getPosElement(new Rect(400, 150, 200, 400)), scrollViewVector1,ScreenUtil.getPosElement(new Rect (0, 0, 600, 600)));
		
		toggleBool6 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 0, 100, 30)), toggleBool6, "Ejercicio 7");
		
		toggleBool7 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0,100, 100, 30)), toggleBool7, "Ejercicio 8");
		
		toggleBool8 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 200, 100, 30)), toggleBool8, "Ejercicio 9");
		
		toggleBool9 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 300, 100, 30)), toggleBool9, "Ejercicio 10");
		
		toggleBool10 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 400, 100, 30)), toggleBool10, "Ejercicio 11");
		
		toggleBool11 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 500, 100, 30)), toggleBool11, "Ejercicio 12");
		
		
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,0,100,100)), icon7);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,100,100,100)), icon8);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,200,100,100)), icon9);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,300,100,100)), icon10);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,400,100,100)), icon11);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,500,100,100)), icon12);
		
		
		
		GUI.EndScrollView();
		
		
		
		scrollViewVector2 = GUI.BeginScrollView ( ScreenUtil.getPosElement(new Rect(700, 150, 200, 400)), scrollViewVector2, ScreenUtil.getPosElement(new Rect (0, 0, 600, 600)));
		
		toggleBool12 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 0, 100, 30)), toggleBool12, "Ejercicio 13");
		
		toggleBool13 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0,100, 100, 30)), toggleBool13, "Ejercicio 14");
		
		toggleBool14 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 200, 100, 30)), toggleBool14, "Ejercicio 15");
		
		toggleBool15 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 300, 100, 30)), toggleBool15, "Ejercicio 16");
		
		toggleBool16 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 400, 100, 30)), toggleBool16, "Ejercicio 17");
		
		toggleBool17 = GUI.Toggle ( ScreenUtil.getPosElement(new Rect(0, 500, 100, 30)), toggleBool17, "Ejercicio 18");
		
		
		
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,0,100,100)), icon13);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,100,100,100)), icon14);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,200,100,100)), icon15);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,300,100,100)), icon16);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,400,100,100)), icon17);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,500,100,100)), icon18);
		GUI.Label ( ScreenUtil.getPosElement(new Rect(100,600,100,100)), icon19);
		
		
		
		GUI.EndScrollView();
		
		
		
	}



	IEnumerator WaitForRequest(WWW hs_get){
		//Debug.Log ("entra IEnumerator");
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
			toggleBool = toggleBool1 = toggleBool2 = false;
			
		}
		
	}
	
	bool tooglePause (){
		
		if (Time.timeScale == 0){
			
			//Screen.lockCursor = true;
			Time.timeScale = 1;
			return (false);
			
		}
		else{
			
			Screen.lockCursor = false;
			Time.timeScale = 0;
			return (true);
			
			
		}
		
		
		
	}


	void llenarArreglo(){
		arregloMovimientos.Add (toggleBool);
		arregloMovimientos.Add (toggleBool1);
		arregloMovimientos.Add (toggleBool2);
		arregloMovimientos.Add (toggleBool3);
		arregloMovimientos.Add (toggleBool4);
		arregloMovimientos.Add (toggleBool5);
		arregloMovimientos.Add (toggleBool6);
		arregloMovimientos.Add (toggleBool7);
		arregloMovimientos.Add (toggleBool8);
		arregloMovimientos.Add (toggleBool9);
		arregloMovimientos.Add (toggleBool10);
		arregloMovimientos.Add (toggleBool11);
		arregloMovimientos.Add (toggleBool12);
		arregloMovimientos.Add (toggleBool13);
		arregloMovimientos.Add (toggleBool14);
		arregloMovimientos.Add (toggleBool15);
		arregloMovimientos.Add (toggleBool16);
		arregloMovimientos.Add (toggleBool17);
		animaciones[0]=animacion;
		animaciones[1]=(animacion1);
		animaciones[2]=(animacion2);
		animaciones[3]=(animacion3);
		animaciones[4]=(animacion4);
		animaciones[5]=(animacion5);
		animaciones[6]=(animacion6);
		animaciones[7]=(animacion7);
		animaciones[8]=(animacion8);
		animaciones[9]=(animacion9);
		animaciones[10]=(animacion10);
		animaciones[11]=(animacion11);
		animaciones[12]=(animacion12);
		animaciones[13]=(animacion13);
		animaciones[14]=(animacion14);
		animaciones[15]=(animacion15);
		animaciones[16]=(animacion16);
		animaciones[17]=(animacion17);
		print ("arreglo listo");
	}

	public static void vaciarArr(){
		//asigna el valor false a todos los elementos del arreglo que representan la rutina
		//es decir, borra la rutina
		//arregloMovimientos.Clear();
		arregloMovimientos.Clear();
		movimientosSeleccionados.Clear();
		//todos los toggles tienen valor false
		toggleBool = false; 
		toggleBool1 = false;
		toggleBool2 = false;
		toggleBool3 = false;
		toggleBool4 = false;
		toggleBool5 = false;
		toggleBool6 = false;
		toggleBool7 = false;
		toggleBool8 = false;
		toggleBool9 = false;
		toggleBool10 = false;
		toggleBool11 = false;
		toggleBool12 = false;
		toggleBool13 = false;
		toggleBool14 = false;
		toggleBool15 = false;
		toggleBool16 = false;
		toggleBool17 = false;
	}


	
	void  seleccionarRutina(){
		int cont = 0;
		for (; cont<arregloMovimientos.Count; cont++) {
					if ((bool)arregloMovimientos [cont] == true){
						movimientosSeleccionados.Add (arregloMovimientos[cont]);
				        canti=canti+1;
						print ("elemento agregado");
					}
		}	
	}

	/*void arreglo_guardar_rutina(){
		int cont = 0;
		for (; cont<arregloMovimientos.Count; cont++) {
			if ((bool)arregloMovimientos [cont] == true){
				codigo_movimientos.Add(cont);
			}
		}  
	}*/

  /* void guardarRutina(){
		llenarArreglo();
		arreglo_guardar_rutina();
		//int mov = movimientosSeleccionados[0];

		WWW www = new WWW(url2);
		StartCoroutine(WaitForRequest2(www));

		//var form= new WWWForm(); //here you create a new form connection
		//form.AddField( "Nom_rutina",nom_rutina);
		//form.AddField ("Cod_movimiento", mov);
		//WWW www = new WWW(url2,form);
		}*/

	IEnumerator WaitForRequest2(WWW hs_get){
		yield return hs_get;
		Debug.Log ("entra al envio");
		if(hs_get.error == null){
	      string mensaje = hs_get.text;
		  Debug.Log ("codigo ultima rutina"+mensaje);
		} else {
			print("ERROR: "+hs_get.error);
		}
}
}