using UnityEngine;
using System.Collections;

public class Creditos : MonoBehaviour {
	
	
	
	private InstruccionVisual creditos;
	private Cronometro cronometro;
	private int vlSelector;

	// Use this for initialization
	public Creditos(){
		creditos = new  InstruccionVisual("gtLogos");
		cronometro = new Cronometro();
		vlSelector = 1;
	}
	
	// Update is called once per frame
	public void secuenceCredits(Texture2D vgUBosque,Texture2D vgCorpSindrome, Texture2D vgLogCorpo ){
		float tiempoActual;
		int timeSpecific = -5;
		
		
		switch(vlSelector){
		case 1:
			cronometro.IniciarCronometro();
		    tiempoActual = cronometro.CalcularTiempoTranscurrido();
				if(tiempoActual < timeSpecific){
					creditos.setImagen(vgUBosque);
					cronometro.DetenerCronometro();
					vlSelector = 2;
				}
		break;
		case 2:
			cronometro.IniciarCronometro();
		    tiempoActual = cronometro.CalcularTiempoTranscurrido();
				if(tiempoActual < timeSpecific){
					creditos.setImagen(vgCorpSindrome);	
					cronometro.DetenerCronometro();
					vlSelector = 3;
				}
			break;
		case 3:
			cronometro.IniciarCronometro();
		    tiempoActual = cronometro.CalcularTiempoTranscurrido();
				if(tiempoActual < timeSpecific){
					creditos.setImagen(vgLogCorpo);	
					cronometro.DetenerCronometro();
					vlSelector = 4;
				}
		break;
	 	}
	}
	
	public bool GetFinishSecuence(){
		if(vlSelector ==4){
			return true;
		}else{
			return false;
		}
	}
}
