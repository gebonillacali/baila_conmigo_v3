using UnityEngine;
using System.Collections;

public class EscenarioSeleccionarTipoRutina : MonoBehaviour {


	private ArrayList limpiar;
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
	bool paused = false;



	void OnGUI (){

		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = ScreenUtil.getFontFixedSize(20);
		
		GUIStyle labelsStyle = new GUIStyle(GUI.skin.label);
		labelsStyle.fontSize = ScreenUtil.getFontFixedSize(20);
		
		GUIStyle textFileldsStyle = new GUIStyle(GUI.skin.textField);
		textFileldsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.fontSize = ScreenUtil.getFontFixedSize(25);
		boxStyle.fontStyle = FontStyle.Bold;

		if (paused ){

			limpiar = EscenarioRutinaManual.movimientosSeleccionados;
			GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
			if (GUI.Button(new Rect(0,0,bW,bH),"Menu Principal"))
			{
				limpiar.Clear();
				Application.LoadLevel("ok");
				KinectWrapper.NuiShutdown();
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}

		GUI.Box(ScreenUtil.getPosElement(new Rect(440,150,500,400)), "Opciones de Reproduccion", boxStyle);

		if(GUI.Button(ScreenUtil.getPosElement(new Rect(515,200,350,50)), "Manual", buttonsStyle)) {
			Application.LoadLevel("ok2");
			KinectWrapper.NuiShutdown();
		}
		
		if (GUI.Button (ScreenUtil.getPosElement (new Rect (515, 270, 350, 50)), "Automatico", buttonsStyle)) {
						Application.LoadLevel ("ok4");
						KinectWrapper.NuiShutdown ();
		} else {
			if (ScreenUtil.onMouseOver(ScreenUtil.getPosElement (new Rect (515, 270, 350, 50)))) {
				MouseControl.MouseMoveToPoint(ScreenUtil.getPosElement (new Rect (515, 270, 350, 50)).center);
			}
		}

		if(GUI.Button(ScreenUtil.getPosElement(new Rect(515,340,350,50)), "Cambiar de jugador", buttonsStyle)) {
			Application.LoadLevel("ok");
			KinectWrapper.NuiShutdown();
		}

		if(GUI.Button (ScreenUtil.getPosElement(new Rect (515,410,350,50)), "Reproduccion de rutinas Creadas", buttonsStyle)){
			Application.LoadLevel("4.EscenarioInterfazdeReproduccion");
			KinectWrapper.NuiShutdown();
		}


		if(GUI.Button(ScreenUtil.getPosElement(new Rect(515,480,350,50)),"Volver", buttonsStyle)){			
			Application.LoadLevel("ok");
			KinectWrapper.NuiShutdown();
		}
		
		
		//if(GUI.Button (new Rect (910,510,130,50), "Tutorial")){ }
		

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
