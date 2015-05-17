#pragma strict


var textAreaString = '';



var scrollViewVector : Vector2 = Vector2.zero;
var textFieldString = "Pepito Perez";
var textFieldString1 = "Pepito Perez1";
var textFieldString2 = "Pepito Perez2";


var scrollViewVector1 : Vector3 = Vector3.zero;





var innerText : String = "Juanito Perez";

var vScrollbarValue : float;
var windowRect : Rect = Rect (100, 100, 400, 200);

var vSbarValue : float;



function OnGUI () {
	
	// esto es para Seleccionar alguna de las dos opciones

	// Make a background box
	

	// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
	if (GUI.Button (Rect (800,220,100,40), "Manual")) {
		Application.LoadLevel ("ok2");
	}

	// Make the second button.
	if (GUI.Button (Rect (950,220,100,40), "Automatico")) {
		Application.LoadLevel (2);
	}
	
	// esto es para escoger el nombre del jugador
	GUI.Box (Rect (200, 200, 200, 240), "");
	
	if(GUI.Button (Rect (260,350,100,35), "Aceptar")){
	
	
	}
			if(GUI.Button (Rect (910,510,130,50), "Tutorial")){
	}
	textAreaString = GUI.TextArea (Rect (230, 250, 130, 20), textAreaString);






	
	

}


// al darle click 
function CreaMiVentana (windowID : int) {
  if (GUI.Button (Rect (10,20,100,20), "Hola mundo"))
  print ("Recibí un click");
}

