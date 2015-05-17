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
///Clase encargada de crear el cronometro del videojuego
///</summary>
///<remarks>
///Cronometro del videojuego.
///</remarks>
public class Cronometro : MonoBehaviour {
	
	//Variables Globales
	private float tiempoInicio;
	private float tiempoFinal;
	
//	public bool iniciarCronometro = false;
//	public bool detenerCronometro = false;
	
//	void Update(){
//		if(iniciarCronometro){
//			IniciarCronometro();
//			iniciarCronometro = false;
//			Debug.Log(tiempoInicio.ToString());
//		}
//		if(detenerCronometro){
//			DetenerCronometro();
//			detenerCronometro = false;
//			Debug.Log(tiempoFinal.ToString());
//			float result = CalcularTiempoTranscurrido();
//			Debug.Log(result.ToString());
//		}
//	}
	
	//Constructor
	public Cronometro(){
		tiempoInicio = 0;
		tiempoFinal = 0;
	}
	
	public void SetTiempoInicio(float pTiempoInicio){
		tiempoInicio = pTiempoInicio;
	}
	
	public float GetTiempoInicio(){
		return tiempoInicio;
	}
	
	public void SetTiempoFinal(float pTiempoFinal){
		tiempoFinal = pTiempoFinal;
	}
	
	public float GetTiempoFinal(){
		return tiempoFinal;
	}
	
	public float CalcularTiempoTranscurrido(){
		return tiempoFinal-tiempoInicio;
	}
	
	public void IniciarCronometro(){
		tiempoInicio = Time.realtimeSinceStartup;
	}
	
	public void DetenerCronometro(){
		tiempoFinal = Time.realtimeSinceStartup;
	}
}
