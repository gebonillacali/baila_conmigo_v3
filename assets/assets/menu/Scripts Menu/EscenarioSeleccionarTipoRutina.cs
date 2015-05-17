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

		if (paused ){

			limpiar = EscenarioRutinaManual.movimientosSeleccionados;
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


		if(GUI.Button(new Rect(420,220,150,40), "Manual")) {
			Application.LoadLevel("ok2");
		}
		
		if(GUI.Button(new Rect(700,220,150,40), "Automatico")) {
			Application.LoadLevel("ok4");
		}

		if(GUI.Button(new Rect(900,450,200,40), "Cambiar de jugador")) {
			Application.LoadLevel("ok");
		}

		if(GUI.Button(new Rect(1050,510,50,40),"Salir")){
			
			Application.Quit();
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
