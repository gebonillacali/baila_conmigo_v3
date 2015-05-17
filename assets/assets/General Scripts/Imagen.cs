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
///Clase encargada de las imagenes de los movimientos.
///</summary>
///<remarks>
///Gestiona las imagenes que se mostraran en el transcurso del videojuego
///</remarks>
public class Imagen : MonoBehaviour {

	// Variables Globales.
	private string nombre;
    private GameObject cambas;
    private Texture2D textura;
	
	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="Imagen"/> por defecto.
	/// </summary>
	public Imagen(){
		nombre = "";
		textura = null;
	    cambas = null;
	
	}

	/// <summary>
	/// Inicializa y genera una nueva instancia de la  clase <see cref="Imagen"/>.
	/// </summary>
	/// <param name='pNombreCambas'>
	/// pNombre es el nombre del area de dibujo de la imagen.
	/// </param>
	public Imagen (string pNombreCambas){
	    cambas = GameObject.Find(pNombreCambas);
	}
	
	/// <summary>
	/// Obtiene el nombre de la imagen.
	/// </summary>
	/// <returns>
	/// Devuelve el nombre de la imagen de tipo cadena de caracteres.
	/// </returns>
	public string GetNombre(){
		return nombre;
	}
	
	/// <summary>
	/// Asigna el nombre a la imagen.
	/// </summary>
	/// <param name='pNombre'>
	/// pNombre es el nombre de la imagen de tipo cadena de caracteres.
	/// </param>
	public void SetNombre(string pNombre){
		nombre = pNombre;
	}
			
	/// <summary>
	/// Obtiene la textura de la imagen.
	/// </summary>
	/// <returns>
	/// Devuelve la textura de la imagen.
	/// </returns>
	public Texture2D GetTextura(){
		return textura;
	}
	
	/// <summary>
	/// Asigna la textura a la imagen.
	/// </summary>
	/// <param name='pTextura'>
	/// pTextura es la textura en 2D.
	/// </param>
	public void SetTextura(Texture2D pTextura){
		textura = pTextura;
		cambas.guiTexture.texture = textura;
	}
}


