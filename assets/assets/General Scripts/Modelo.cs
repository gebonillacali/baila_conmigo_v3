/*=============================================================================
 BAILA CONMIGO JUEGA CON KINECT
 PROYECTO DE INVESTIGACION I
 Ivan Camilo Latorre Peña, Diana Ramirez, Esteban Rodriguez
==============================================================================*/

/*=============================================================================
LIBRERIAS
=============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///<summary>
///Clase encargada de definir las articulaciones del esqueleto
///</summary>
///<remarks>
///Gestiona las articulaciones del esqueleto del personaje
///</remarks>
public class Modelo : MonoBehaviour {
	
	// Variables Globales.
	private Articulacion piernaIzquierda;
	private Articulacion piernaDerecha;
	
	private Articulacion pieIzquierdo;
	private Articulacion pieDerecho;
	
	private Articulacion pelvis;
	private Articulacion pecho;
	private Articulacion cuello;
	private Articulacion cabeza;
	private Articulacion hombroIzquierdo;
	private Articulacion hombroDerecho;
	
	private Articulacion brazoIzquierdo;
	private Articulacion brazoDerecho;
	
	private Articulacion manoIzquierda;
	private Articulacion manoDerecha;
	
	private Articulacion codoDerecho;
	private Articulacion codoIzquierdo;
	
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la clase <see cref="Modelo"/>.
	/// </summary>
	public Modelo() {
		piernaIzquierda = new Articulacion("RightUpLeg", "piernaIzquierda");
		piernaDerecha = new Articulacion("LeftUpLeg","piernaDerecha");
		pieIzquierdo = new Articulacion("RightFoot", "pieIzquierdo");
		pieDerecho = new Articulacion("LeftFoot", "pieDerecho");
		pelvis = new Articulacion("Spine", "pelvis");
		pecho = new Articulacion("Spine2", "pecho");
		cuello = new Articulacion("Spine1", "cuello");
		cabeza = new Articulacion("Head", "cabeza");
		hombroIzquierdo = new Articulacion("RihtShoulder", "hombroIzquierdo");
		hombroDerecho = new Articulacion("LeftShoulder", "hombroDerecho");
		brazoIzquierdo = new Articulacion("RightArm", "brazoIzquierdo");
		brazoDerecho = new Articulacion("LeftArm", "brazoDerecho");
		manoIzquierda = new Articulacion("RightHand", "manoIzquierda");
		manoDerecha = new Articulacion("LeftHand", "manoDerecha");
		codoDerecho = new Articulacion("LeftForeArm", "codoDerecho");
		codoIzquierdo = new Articulacion("RightForeArm", "codoDizquerdo");
	}
	
	/// <summary>
	/// Obtiene la  articulación de la pierna izquierda del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación de la pierna izquierda.
	/// </returns>
	public Articulacion GetPiernaIzquierda(){
		return piernaIzquierda;
	}
	
	/// <summary>
	/// Obtiene la  articulación de la pierna derecha del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación de la pierna derecha.
	/// </returns>
	public Articulacion GetPiernaDerecha(){
		return piernaDerecha;
	}
	
	/// <summary>
	/// Obtiene la  articulación del pie izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del pie izquierdo.
	/// </returns>
	public  Articulacion GetPieIzquierdo(){
		return pieIzquierdo;
	}
	
	/// <summary>
	/// Obtiene la  articulación de la pie derecho del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del pie derecho.
	/// </returns>
	public  Articulacion GetPieDerecho(){
		return pieDerecho;
	}
	
	/// <summary>
	/// Obtiene la  articulación del pecho del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del pecho.
	/// </returns>
	public  Articulacion GetPecho(){
		return pecho;
	}
	
	/// <summary>
	/// Obtiene la  articulación del cuello del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del cuello.
	/// </returns>
	public  Articulacion GetCuello(){
		return cuello;
	}
	
	/// <summary>
	/// Obtiene la  articulación de la cabeza del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación de la cabeza.
	/// </returns>
	public Articulacion GetCabeza(){
		return cabeza;
	}
	
	/// <summary>
	/// Obtiene la  articulación del hombro izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del  hombro izquierdo.
	/// </returns>
	public Articulacion GetHombroIzquierdo(){
		return hombroIzquierdo;
	}
	
	/// <summary>
	/// Obtiene la  articulación del hombro derecho del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del hombro derecho.
	/// </returns>
	public Articulacion GetHombroDerecho(){
		return hombroDerecho;
	}
	
	/// <summary>
	/// Obtiene la  articulación del brazo izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del brazo izquierdo.
	/// </returns>
	public Articulacion GetBrazoIzquierdo(){
		return brazoIzquierdo;
	}
	
	/// <summary>
	/// Obtiene la  articulación del brazo derecho del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación del brazo derecho.
	/// </returns>
	public Articulacion GetBrazoDerecho(){
		return brazoDerecho;
	}
	
	/// <summary>
	/// Obtiene la  articulación de la mano izquierda del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación de la mano izquierda.
	/// </returns>
	public Articulacion GetManoIzquierda(){
		return manoIzquierda;
	}
	
	/// <summary>
	/// Obtiene la  articulación de la mano derecha del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve la articulación de la mano derecha.
	/// </returns>
	public Articulacion GetManoDerecha(){
		return manoDerecha;
	}
	
	///Obtiene la articulacion del codo derecho del modelo
	public Articulacion GetCodoDerecho(){
		return codoDerecho;
	}
	
	///Obtiene la articulacion del codo izquierdo del modelo
	public Articulacion GetCodoIzquierdo(){
		return codoIzquierdo;
	}
	
	public Articulacion GetPelvis(){
		return pelvis;
	}
	
	
	/// <summary>
	/// Obtiene la posición de la articulación de la pierna izquierda del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la pierna izquierda.
	/// </returns>
	public Vector3 PosicionPiernaIzquierda(){
		return piernaIzquierda.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación de la pierna derecha del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la pierna derecha.
	/// </returns>
	public Vector3 PosicionPiernaDerecha(){
		return piernaDerecha.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del pie izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del pie izquierdo.
	/// </returns>
	public Vector3 PosicionPieIzquierdo(){
		return pieIzquierdo.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del pie derecho del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del pie derecho.
	/// </returns>
	public Vector3 PosicionPieDerecho(){
		return pieDerecho.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del pecho del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del pecho.
	/// </returns>
	public Vector3 PosicionPecho(){
		return pecho.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del cuello del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del cuello izquierda.
	/// </returns>
	public Vector3 PosicionCuello(){
		return cuello.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación de la cabeza del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la cabeza.
	/// </returns>
	public Vector3 PosicionCabeza(){
		return cabeza.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del hombro izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del hombro izquierdo.
	/// </returns>
	public Vector3 PosicionHombroIzquierdo(){
		return hombroIzquierdo.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del hombro derecho del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del hombro derecho.
	/// </returns>
	public Vector3 PosicionHombroDerecho(){
		return hombroDerecho.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del brazo izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del brazo izquierdo.
	/// </returns>
	public Vector3 PosicionBrazoIzquierdo(){
		return brazoIzquierdo.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del brazo derecho del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del brazo derecho.
	/// </returns>
	public Vector3 PosicionBrazoDerecho(){
		return brazoDerecho.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación de la mano izquierda del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la mano izquierda.
	/// </returns>
	public Vector3 PosicionManoIzquierda(){
		return manoIzquierda.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación de la mano derecha del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la mano derecha.
	/// </returns>
	public Vector3 PosicionManoDerecha(){
		return manoDerecha.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del codo derecho del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del codo derecho.
	/// </returns>
	public Vector3 PosicionCodoDerecho(){
		return codoDerecho.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación del codo izquierdo del modelo.
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación del codo izquierdo.
	/// </returns>
	public Vector3 PosicionCodoIzquierdo(){
		return codoIzquierdo.GetPosicion();
	}
	
	/// <summary>
	/// Obtiene la posición de la articulación de la pelvis del modelo
	/// </summary>
	/// <returns>
	/// devuelve la posición de la articulación de la pelvis del modelo
	/// </returns>
	public Vector3 PosicionPelvis(){
		return pelvis.GetPosicion();
	}
	
}