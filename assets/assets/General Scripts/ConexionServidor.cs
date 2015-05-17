using UnityEngine;
using System.Collections;

public class ConexionServidor{

	private string host;
	private string puerto;
	private string url;
	private string result;
	private WWWForm formulario;
	
	public ConexionServidor(){
		host = "127.0.0.1";
		puerto = "8081"; 
		url = "";
		result = "";
		formulario = new WWWForm();
	}
	
	public ConexionServidor(string pHost, string pPuerto){
		host = pHost;
		puerto = pPuerto;
		url = "";
		result = "";
		formulario = new WWWForm();
	}
	
	public void  EnviarPeticion(string pMetodo){
		url ="http://"+host+":"+puerto+"/"+pMetodo;
		WWW www = new WWW(url, formulario);
		Debug.Log("Servidor"+www.ToString());
		if(www.isDone){
			result = www.text;
		}
	}
	
	public void EmpaquetarDatos(ArrayList pNombreDatos, ArrayList pDatos){
		string campo;
		string dato;
		for(int i=0; i < pNombreDatos.Count;i++){
			campo = pNombreDatos[i].ToString();
			dato = (string)pDatos[i].ToString();
			formulario.AddField(campo,dato);
		}
	}
}

