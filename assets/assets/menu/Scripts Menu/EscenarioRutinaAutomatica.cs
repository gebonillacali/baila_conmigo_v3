using UnityEngine;
using System.Collections;

public class EscenarioRutinaAutomatica : MonoBehaviour {
	
	private ArrayList limpiar;
	public static bool bt1 = false;
	public static bool bt2 = false;
	public static bool bt3 = false;
	public static bool bt4 = false;
	public static bool rutinaAutomatica = false;
	public bool allOptions = true;


	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
	bool paused = false;


	public bool setMeOnly (){
		
		bt1 = bt2 = bt3 = bt4 = false;
		return true;
	}
		
	public void OnGUI () {

		limpiar = EscenarioRutinaManual.movimientosSeleccionados;

		if (paused ){
			
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



	//Top Left Button
		if (GUI.Toggle (ScreenUtil.getPosElement(new Rect(250,250,100,100)), bt1, "Rutina 1")) bt1 = setMeOnly();
		
		//Middle Button
		if (GUI.Toggle ( ScreenUtil.getPosElement(new Rect(450,250,100,100)), bt2, "Rutina 2")) bt2 = setMeOnly();
		
		//Bottom Right Button
		if (GUI.Toggle ( ScreenUtil.getPosElement(new Rect(650,250,100,100)), bt3, "Rutina 3")) bt3 = setMeOnly();
				
		if (GUI.Toggle ( ScreenUtil.getPosElement(new Rect(850,250,100,100)), bt4, "Rutina4")) bt4 = setMeOnly();
		
		if(GUI.Button (ScreenUtil.getPosElement(new Rect (520,350,100,35)), "Aceptar")){
			rutinaAutomatica = true;
			Application.LoadLevel("1.EscenarioPrincipal1");
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
	}

	void start(){
		//Screen.lockCursor = true;
		Time.timeScale = 1;	
	}

	
	void Update () {
		
		
		
		if (Input.GetKeyUp (KeyCode.Escape)) {
			
			paused = tooglePause();
			
			
		}
		
	}


	bool tooglePause (){
		
		if (Time.timeScale == 0){
			
			Screen.lockCursor = true;
			Time.timeScale = 1;
			return (false);
			
		}
		else{
			
			Screen.lockCursor = false;
			Time.timeScale = 0;
			return (true);
			
			
		}
		
		
		
	}







}
