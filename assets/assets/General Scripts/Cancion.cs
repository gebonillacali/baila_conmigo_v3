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
///Clase encargada de crear la cancion dentro del videojuego
///</summary>
///<remarks>
///Establece los parametos de la canción/es del videojuego
///</remarks>
public class Cancion : MonoBehaviour
{
	//Variables Globales.
	private string nombre;
	private double duracion;
	private string locacion;
	
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="Cancion"/> por defecto.
	/// </summary>
	public Cancion (){
	 	nombre = "";
		duracion = 0;
		locacion = "";
	}
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="Cancion"/>.
	/// </summary>
	/// <param name='pNombre'>
	/// pNombre es el nombre de la canción.
	/// </param>
	/// <param name='pDuracion'>
	/// pDuracion es la duración de la canción.
	/// </param>
	/// <param name='pLocacion'>
	/// pLocanción es la ruta donde se encuentra alojada la canción.
	/// </param>
	public Cancion (string pNombre, double pDuracion, string pLocacion){
	 	nombre = pNombre;
		duracion = pDuracion;
		locacion = pLocacion;
	}
	
	/// <summary>
	/// Obtiene el nombre de la canción.
	/// </summary>
	/// <returns>
	/// Devuelve el nombre de la canción.
	/// </returns>
	public string GetNombre(){
		return nombre;
	}
	
	/// <summary>
	/// Asigna un título a la canción.
	/// </summary>
	/// <param name='pNombre'>
	/// pNombre es el título de la canción.
	/// </param>
	public void SetNombre(string pNombre){
		nombre = pNombre;
	}
	
	/// <summary>
	/// Obtiene la duración de la canción. 
	/// </summary>
	/// <returns>
	/// Devuelve la duración de la canción de tipo entero largo.
	/// </returns>
	public double GetDuracion(){
		return duracion;
	}
	
	/// <summary>
	/// Asigna la duración de la canción.
	/// </summary>
	/// <param name='pDuracion'>
	/// pDuracion es la duración de la canción de tipo entero largo.
	/// </param>
	public void SetDuracion(double pDuracion){
		duracion = pDuracion;
	}
	
	/// <summary>
	/// Obtiene la locación de la canción.
	/// </summary>
	/// <returns>
	/// Devuelve la locación de la canción.
	/// </returns>
	public string GetLocacion(){
		return locacion;
	}
	
	/// <summary>
	/// Asigna la locación de la canción.
	/// </summary>
	/// <param name='pLocacion'>
	/// pLocacion es la locación de la canción de tipo cadena de caracteres.
	/// </param>
	public void SetLocacion(string pLocacion){
		locacion = pLocacion;
	}
}

