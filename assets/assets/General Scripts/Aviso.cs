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

///<summary>
///Clase encargada crear los avisos del videojuego
///</summary>
///<remarks>
///Crea los avisos que salndrán en el videojuego.
///</remarks>
public class Aviso : MonoBehaviour
{
	//Variables Globales.
	private GameObject Cuadrado;
	private string nombre;
	private string titulo;
	private Vector3 posicion;
	private Animation animacion;
	
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase  <see cref="Aviso"/>.
	/// </summary>
	/// <param name='pNombre'>
	/// pNombre es el nombre del objeto de juego para instanciar el aviso.
	/// </param>
	public Aviso (string pNombre){
		Cuadrado = GameObject.Find(pNombre);
		posicion = Vector3.zero;
		animacion = null;
	}
	
	/// <summary>
	/// Asigna el título del aviso .
	/// </summary>
	/// <param name='pTiulo'>
	/// pTiulo es el título del aviso.
	/// </param>
	public void SetAviso(string pTiulo){
		Cuadrado.guiText.text = pTiulo;
	}
	
	/// <summary>
	/// Obtiene el título del aviso.
	/// </summary>
	/// <returns>
	/// Devuelve el título del aviso.
	/// </returns>
	public string GetAviso(){
		titulo = Cuadrado.guiText.text;
		return titulo;
	}
	
	/// <summary>
	/// Asigna el nombre del aviso.
	/// </summary>
	/// <param name='pNombre'>
	/// pNombre es el título del aviso.
	/// </param>
	public void SetNombre(string pNombre){
		nombre = pNombre;
	}
	
	/// <summary>
	/// Obtiene el título del aviso.
	/// </summary>
	/// <returns>
	/// Devuelve el título del aviso en cadena de caracteres.
	/// </returns>
	public string GetNombre(){
		return nombre;
	}
	
	/// <summary>
	/// Asigna una posición del aviso.
	/// </summary>
	/// <param name='pPosicion'>
	/// pPosicion es la nueva posición de tipo Vector3.
	/// </param>
	public void SetPosicion(Vector3 pPosicion){
		Cuadrado.transform.position = pPosicion;
	}
	
	/// <summary>
	/// Obtiene la posición actual del aviso.
	/// </summary>
	/// <returns>
	/// Devuelve la posición actual del aviso.
	/// </returns>
	public Vector3 GetPosicion(){
		return posicion;
	}
	
	/// <summary>
	/// Obtiene la animacion del aviso.
	/// </summary>
	/// <returns>
	/// Devuelve la animación del aviso.
	/// </returns>
	public Animation GetAnimacion(){
		return animacion;
	}
}

