using UnityEngine;
using System.Collections;

public class SeleccionMusical{
	// Variables Globales
	private AudioClip clip;
	private Cancion cancion;
	private AmbienteSonoro ambienteSonoro;
	private Cronometro cronometro;

	public SeleccionMusical(string pNameAmbienteSonoro, AudioClip pClip){
		clip = pClip;
		cancion = new Cancion();
		ambienteSonoro = new AmbienteSonoro(pNameAmbienteSonoro);
		cronometro = new Cronometro();
		
	}
	
	/// <summary>
	/// Cambia la canción.
	/// </summary>
	public void CambiarCancion(bool pCargarCancion, double pDuracion){
		double tiempoActual;
		
		if(pCargarCancion){
			if(ambienteSonoro.GetReproduccion()){
				cronometro.IniciarCronometro();
		    	tiempoActual = (cronometro.CalcularTiempoTranscurrido())*-1;
				while(true){
					if(tiempoActual >= pDuracion){
						break;
					}
				}
			}
			
			cancion.SetNombre(clip.name);
			cancion.SetDuracion(pDuracion);
			cancion.SetLocacion(clip.name);
			ambienteSonoro.SetCancion(clip);
			ambienteSonoro.SetReproducion(pCargarCancion);
		}else{
			if(ambienteSonoro.GetReproduccion()){
				ambienteSonoro.SetReproducion(pCargarCancion);
			}
		}
	}
	
	public void SetClip(AudioClip pClip){
		clip = pClip;
	}
	
	public AudioClip GetClip(){
		return clip;
	}
}

