using UnityEngine;
using System.Collections;

public class CalificacionMovimiento{

	private long tiempo;
	private float rango;
	private Rubrica categoria1;
	private Rubrica categoria2;
	private Rubrica categoria3;
	private Rubrica categoria4;
	private Rubrica categoria5;
	
	// Use this for initialization
	public  CalificacionMovimiento (long pTiempo, float pRango){
		tiempo = pTiempo;
		rango = pRango;
		categoria1 = new Rubrica("categoria 1", 1);
	  	categoria2 = new Rubrica("categoria 2", 2);
	  	categoria3 = new Rubrica("categoria 3", 3);
   		categoria4 = new Rubrica("categoria 4", 4);
		categoria5 = new Rubrica("categoria 5", 5);
	}
	
	public CalificacionMovimiento(){
		tiempo = 0;
		rango = 0;
		categoria1 = new Rubrica("categoria 1", 1);
	  	categoria2 = new Rubrica("categoria 2", 2);
	  	categoria3 = new Rubrica("categoria 3", 3);
   		categoria4 = new Rubrica("categoria 4", 4);
		categoria5 = new Rubrica("categoria 5", 5);
	}
	public double ClasificarTiempo(float pTiempoObtenido){
		pTiempoObtenido *= -1;
		if (pTiempoObtenido <= tiempo){
			return 1;
		}else if (pTiempoObtenido > tiempo){
			return 0.5;
		}else{
			return 0;
		}
	}

	public Rubrica ClasificarRangos(float ran){
		if (ran >= 0.0 && ran <= 2.0){
			return categoria1;
		}else if (ran > 2.0 && ran <= 4.0){
			return categoria2;
		}else if  ((ran > 4.0) && (ran <= 6.0)){
			return categoria3;
		}else if  ((ran > 6.0) && (ran <= 8.0)){
			return categoria4;
		}else if  ((ran > 8.0) && (ran <=10.0)){
			return categoria5;	
		}else{
			return categoria1;}
	}



	public Rubrica ClasificarRango(float pRangoInicio, float  pRangoObtenido, float  pTiempoObtenido){

		rango = (1-(pRangoObtenido-pRangoInicio)/(pRangoObtenido+pRangoInicio)) * (float)ClasificarTiempo(pTiempoObtenido);
		if (rango == 0){
			return categoria1;
		}else if ((rango >= 0) && (rango < 0.25)){
			return categoria2;
		}else if  ((rango >= 0.25) && (rango < 0.50)){
			return categoria3;
		}else if  ((rango >= 0.50) && (rango < 0.75)){
			return categoria4;
		}else if  ((rango >= 0.75) && (rango < 1)){
			return categoria5;
		}else if ((rango >1.5) && (rango <= 2)){
			return categoria1;
		}else{
			return categoria3;
		}
	}
	
	public long GetTiempo(){
		return tiempo;
	}
	
	public void SetTiempo(long pTiempo){
		tiempo = pTiempo;
	}
	
	public float GetRangoResultante(){
		return rango;	
	}
	
	public void SetRango(float pRango){
		rango = pRango;
	}
	
	
}

