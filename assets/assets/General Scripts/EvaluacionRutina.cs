using UnityEngine;
using System.Collections;

public class EvaluacionRutina{

	private double puntaje;
	private float calificacion;
	private Rubrica categoria1;
	private Rubrica categoria2;
	private Rubrica categoria3;
	private ConexionServidor enviarEvalaucion;
	
	
	public EvaluacionRutina(){
		puntaje = 0 ;
		calificacion = 0;
		categoria1 = new Rubrica("categoria 1", 1);
	  	categoria2 = new Rubrica("categoria 2", 2);
	  	categoria3 = new Rubrica("categoria 3", 3);
		enviarEvalaucion = new ConexionServidor();
	}
	
	public EvaluacionRutina(double pPuntaje){
		puntaje = pPuntaje;
		calificacion = 0;
		categoria1 = new Rubrica("categoria 1", 1);
	  	categoria2 = new Rubrica("categoria 2", 2);
	  	categoria3 = new Rubrica("categoria 3", 3);
		enviarEvalaucion = new ConexionServidor();
	}
	
	public void CalificacionRutina(ArrayList atNombresMovimientos, ArrayList afCalificacionesMovimientos){
		float sumCalificacion = 0;
		
		foreach(float i in afCalificacionesMovimientos){
			sumCalificacion += i;
		}
		
		puntaje = sumCalificacion/afCalificacionesMovimientos.Count;
		
		if ((puntaje >= 0) && (puntaje < 1.66)){
			calificacion = categoria1.GetCategoria();
		}else if  ((puntaje >= 1.66) && (puntaje < 3.33)){
			calificacion = categoria2.GetCategoria();
		}else if  ((puntaje > 3.33) && (puntaje <= 5)){
			calificacion  = categoria3.GetCategoria();
		}
		
		atNombresMovimientos.Add("evaluacionTotal");
		afCalificacionesMovimientos.Add(calificacion);
		
		if(calificacion!=0){
			enviarEvalaucion.EmpaquetarDatos(atNombresMovimientos,afCalificacionesMovimientos);
			enviarEvalaucion.EnviarPeticion("public/enviarResultadosMovimientos");
		}
		
	}
	
	public double GetPuntaje(){
		return puntaje; 
	}
	
	public void SetPuntaje(double pPuntaje){
		puntaje = pPuntaje;
	}
}
