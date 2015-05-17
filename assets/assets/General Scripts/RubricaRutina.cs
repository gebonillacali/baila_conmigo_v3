using UnityEngine;
using System.Collections;

public class RubricaRutina{
	private  string nombre;
	private  float  categoria;
	
	// Use this for initialization
	public RubricaRutina (string pNombre, float pCategoria){
		nombre = pNombre;
		categoria = pCategoria;
	}
	
	public string GetNombre(){
		return nombre;
	}
	
	public void SetNombre(string pNombre){
		nombre = pNombre;
	}
	
	public float GetCategoria(){
		return categoria;
	}
	
	public void SetCategoria(float pCategoria){
		categoria = pCategoria;
	}
}

