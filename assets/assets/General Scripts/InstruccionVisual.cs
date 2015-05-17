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
///Clase encargada de mostrar el movmiento a seguir
///</summary>
///<remarks>
///Despliega las imagenes de los movimientos que deben seguirse
///</remarks>
public class InstruccionVisual : MonoBehaviour {
	
	private Imagen imagen;
	
	// Inicializacion de la clase
	public InstruccionVisual(string pNameGObj){
		imagen = new Imagen(pNameGObj);
	}
	
	public void setImagen(Texture2D pInstruccion){
		imagen.SetTextura(pInstruccion);	
	}
	
	
}