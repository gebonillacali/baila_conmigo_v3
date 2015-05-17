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
///Clase encargada de crear el ambiente sonoro del videojuego
///</summary>
///<remarks>
///Gestiona las el ambiente sonoro del videojuego.
///</remarks>
public class AmbienteSonoro : MonoBehaviour
{
	// Variables Globales.
	private GameObject ambienteSonoro;
	private AudioClip clipCancion;
	private bool reproduccion;
	private Cancion cancion;
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="AmbienteSonoro"/>.
	/// </summary>
	/// <param name='pZona'>
	/// P zona.
	/// </param>
	public AmbienteSonoro (string pZona){
		ambienteSonoro = GameObject.Find(pZona);
		reproduccion = false;
		cancion = null;
	}
	
	/// <summary>
	/// Obtiene el ambiente sonoro.
	/// </summary>
	/// <returns>
	/// Devuelve un ambiente sonoro.
	/// </returns>
	public GameObject GetAmbienteSonoro(){
		return ambienteSonoro;
	}
	
	/// <summary>
	/// SAsigna un ambiente sonoro.
	/// </summary>
	/// <param name='pZona'>
	/// P zona.
	/// </param>
	public void SetAmbienteSonoro(GameObject pZona){
		ambienteSonoro = pZona;
	}
	
	/// <summary>
	/// Obtiene la canción del ambiente sonoro.
	/// </summary>
	/// <returns>
	/// Devuelve una canción.
	/// </returns>
	public Cancion GetCancion(){
		return cancion;
	}
	
	/// <summary>
	/// Asigna una canción al ambiente sonoro.
	/// </summary>
	/// <param name='pCancion'>
	/// pCancion.
	/// </param>
	public void  SetCancion(AudioClip pCancion){
		ambienteSonoro.audio.clip = pCancion;
	}
	
	/// <summary>
	/// Obtiene si el ambiente sonoro se enucentra reproduciendo una canción.
	/// </summary>
	/// <returns>
	/// Devuelve si se enucentra reproducciendo una canción.
	/// </returns>
	public bool GetReproduccion(){
		return reproduccion;
	}
	
	
	/// <summary>
	/// Reproduce la canción del ambiente sonoro.
	/// </summary>
	/// <param name='pReproduccion'>
	/// pReproduccion.
	/// </param>
	public void SetReproducion(bool pReproduccion){
		reproduccion = pReproduccion;
		if(reproduccion){
			ambienteSonoro.audio.Play();
		}else{
			ambienteSonoro.audio.Stop();
		}
	}
	
	public void PlayReproducion(){
		ambienteSonoro.audio.Play();
	}
	
	public void StopReproducion(){
		ambienteSonoro.audio.Stop();
	}
		
	/// <summary>
	/// Pausa  la reproducción de la canción.
	/// </summary>
	public void PausarReproduccion(){
		ambienteSonoro.audio.Pause();
	}
}

