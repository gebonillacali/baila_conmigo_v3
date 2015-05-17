#pragma strict


var textAreaString = '';


var scrollViewVector : Vector2 = Vector2.zero;

var scrollViewVector1 : Vector3 = Vector3.zero;

var scrollViewVector2 : Vector4 = Vector4.zero;

var textura1 : GUITexture ; 
var icon : Texture2D;
var icon2 : Texture2D;
var icon3 : Texture2D;
var icon4 : Texture2D;
var icon5 : Texture2D;
var icon6 : Texture2D;
var icon7 : Texture2D;
var icon8 : Texture2D;
var icon9 : Texture2D;
var icon10 : Texture2D;
var icon11 : Texture2D;
var icon12 : Texture2D;
var icon13 : Texture2D;
var icon14 : Texture2D;
var icon15 : Texture2D;
var icon16 : Texture2D;
var icon17 : Texture2D;
var icon18 : Texture2D;
var icon19 : Texture2D;
var icon20 : Texture2D;
var icon21 : Texture2D;
var walkClip : AnimationClip;








var toggleBool = false;
var toggleBool1 = false;
var toggleBool2 = false;
var toggleBool3 = false;
var toggleBool4 = false;
var toggleBool5 = false;
var toggleBool6 = false;
var toggleBool7 = false;
var toggleBool8 = false;
var toggleBool9 = false;
var toggleBool10 = false;
var toggleBool11 = false;
var toggleBool12 = false;
var toggleBool13 = false;
var toggleBool14 = false;
var toggleBool15 = false;
var toggleBool16 = false;
var toggleBool17 = false;


function OnGUI () {
	
	// esto es para Seleccionar alguna de las dos opciones

	// Make a background box
	GUI.Box (Rect (1000, 100, 180, 180), "Rutinas Guardadas");



	// Boton Grabar rutina
		if(GUI.Button (Rect (1000,500,100,50), "Grabar Rutina")){
	
	
	}
	
	// Boton jugar
	
	if(GUI.Button (Rect (1000,400,100,50), "Jugar")){
	seleccionarRutina();
	Application.LoadLevel ("1.EscenarioPrincipal");
	
	}
	
	
	scrollViewVector = GUI.BeginScrollView (Rect (100, 150, 200, 400), scrollViewVector, Rect (0, 0, 600, 600));
	
	toggleBool = GUI.Toggle (Rect (0, 0, 100, 30), toggleBool, "Ejercicio 1");

	toggleBool1 = GUI.Toggle (Rect (0,100, 100, 30), toggleBool1, "Ejercicio 2");
	
	toggleBool2 = GUI.Toggle (Rect (0, 200, 100, 30), toggleBool2, "Ejercicio 3");
	
	toggleBool3 = GUI.Toggle (Rect (0, 300, 100, 30), toggleBool3, "Ejercicio 4");
	
	toggleBool4 = GUI.Toggle (Rect (0, 400, 100, 30), toggleBool4, "Ejercicio 5");
	
	toggleBool5 = GUI.Toggle (Rect (0, 500, 100, 30), toggleBool5, "Ejercicio 6");
	
	 
	GUI.Label (Rect (100,0,100,100), icon);
	GUI.Label (Rect (100,100,100,100), icon2);
	GUI.Label (Rect (100,200,100,100), icon3);
	GUI.Label (Rect (100,300,100,100), icon4);
	GUI.Label (Rect (100,400,100,100), icon5);
	GUI.Label (Rect (100,500,100,100), icon6);
	
	
/*	if(!textura1){
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		GUI.DrawTexture(Rect(10,10,60,60), textura1, ScaleMode.ScaleToFit, true, 10.0f);
*/	
	GUI.EndScrollView();

	
	
	


scrollViewVector1 = GUI.BeginScrollView (Rect (400, 150, 200, 400), scrollViewVector1, Rect (0, 0, 600, 600));
	
	toggleBool6 = GUI.Toggle (Rect (0, 0, 100, 30), toggleBool6, "Ejercicio 7");

	toggleBool7 = GUI.Toggle (Rect (0,100, 100, 30), toggleBool7, "Ejercicio 8");
	
	toggleBool8 = GUI.Toggle (Rect (0, 200, 100, 30), toggleBool8, "Ejercicio 9");
	
	toggleBool9 = GUI.Toggle (Rect (0, 300, 100, 30), toggleBool9, "Ejercicio 10");
	
	toggleBool10 = GUI.Toggle (Rect (0, 400, 100, 30), toggleBool10, "Ejercicio 11");
	
	toggleBool11 = GUI.Toggle (Rect (0, 500, 100, 30), toggleBool11, "Ejercicio 12");
	

	GUI.Label (Rect (100,0,100,100), icon7);
	GUI.Label (Rect (100,100,100,100), icon8);
	GUI.Label (Rect (100,200,100,100), icon9);
	GUI.Label (Rect (100,300,100,100), icon10);
	GUI.Label (Rect (100,400,100,100), icon11);
	GUI.Label (Rect (100,500,100,100), icon12);
	


	GUI.EndScrollView();
	
	
	
	scrollViewVector2 = GUI.BeginScrollView (Rect (700, 150, 200, 400), scrollViewVector2, Rect (0, 0, 600, 600));
	
	toggleBool12 = GUI.Toggle (Rect (0, 0, 100, 30), toggleBool12, "Ejercicio 13");

	toggleBool13 = GUI.Toggle (Rect (0,100, 100, 30), toggleBool13, "Ejercicio 14");
	
	toggleBool14 = GUI.Toggle (Rect (0, 200, 100, 30), toggleBool14, "Ejercicio 15");
	
	toggleBool15 = GUI.Toggle (Rect (0, 300, 100, 30), toggleBool15, "Ejercicio 16");
	
	toggleBool16 = GUI.Toggle (Rect (0, 400, 100, 30), toggleBool16, "Ejercicio 17");
	
	toggleBool17 = GUI.Toggle (Rect (0, 500, 100, 30), toggleBool17, "Ejercicio 18");
	


GUI.Label (Rect (100,0,100,100), icon13);
GUI.Label (Rect (100,100,100,100), icon14);
GUI.Label (Rect (100,200,100,100), icon15);
GUI.Label (Rect (100,300,100,100), icon16);
GUI.Label (Rect (100,400,100,100), icon17);
GUI.Label (Rect (100,500,100,100), icon18);
GUI.Label (Rect (100,600,100,100), icon19);

	

	GUI.EndScrollView();
	
	
	
}

function seleccionarRutina (){

	if (toggleBool == true)
	 print ("esta cogiendo");

	//animation.AddClip(walkClip,"CodosArriba");
//	animation.AddClip(animation.clip,"CodosArriba",0, 10);

}
