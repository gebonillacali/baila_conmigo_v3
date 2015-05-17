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
///Clase encargada de reconocer la articulacion del esqueleto
///</summary>
///<remarks>
///Gestiona las articulaciones del esqueleto del personaje del videojuego.
///</remarks>
public class Articulacion : MonoBehaviour
{
	//Variables Globales.
	private GameObject joint;
	private string nombre;
	private Vector3 posicion;
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="Articulacion"/>.
	/// </summary>
	/// <param name='pNombreJoint'>
	/// Nombre de la articulación a buscar.
	/// </param>
	/// <param name='pNombreArt'>
	/// Nombre de la articulación nueva.
	/// </param>
	public Articulacion(string pNombreJoint, string pNombreArt){
		joint = GameObject.Find(pNombreJoint);
		nombre = pNombreArt;
		posicion = Vector3.zero;
	}
	
	/// <summary>
	/// Asigna un nombre a la articulación.
	/// </summary>
	/// <param name='pNombreArt'>
	/// pNombre es el nombre de la articulación.
	/// </param>
	public void SetName(string pNombreArt){
		nombre = pNombreArt;
	}
	
	/// <summary>
	/// Obtiene el nombre de la articulación.
	/// </summary>
	/// <returns>
	/// El nombre de la articulación.
	/// </returns>
	public string GetName(){
		return nombre;
	}
	
	/// <summary>
	/// Obtiene la posición actual de la articulación.
	/// </summary>
	/// <returns>
	/// Devuelve la posición actual de tipo Vector3.
	/// </returns>
	public Vector3 GetPosicion(){
		posicion = joint.transform.position;
		return posicion;
	}
	
	/// <summary>
	/// Asigna una posición  a la articulación.
	/// </summary>
	/// <param name='pPosicion'>
	/// pPosicion es la nueva posición de la articulación de tipo Vector3.
	/// </param>
	public void SetPosicion(Vector3 pPosicion){
		joint.transform.position = pPosicion;
	}
	
}

