using UnityEngine;
using System.Collections;

public class EjecucionRutina : MonoBehaviour {
	private ArrayList limpiar;
	private ArrayList rutinaActual;
	public static bool pausado = false;
	//private GUISkin myskin;
	private Rect windowRect;
	private Movimiento movimiento;
	private float tiempoActual;
	private float tiempoRutina=12f;
	private float acumulado=0;
	private float cantidad=0;
	private Cronometro cronometro;
	public static float calificacionSonido=0;
	private string cod_nino = EscenarioInicial.cod_nino;
	private string cod_sesion="";
	int cont2=0;
	float tiempoCalifica=15f;
	int canti= EscenarioRutinaManual.movimientosSeleccionados.Count;


	public string url="openclassmedia.org/bailaconmigo/crearSesionJuego.php";
	public string url2="openclassmedia.org/bailaconmigo/puntaje_movimiento.php";
	public string url3="openclassmedia.org/bailaconmigo/general.php";
	private string cancion= "cancion1";
	private int cod_rutina= 0;
	
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 170;
	public static bool paused = false;
	public static bool initiated =false;
	
	// Update is called once per frame

	public void Start () {
		rutinaActual = new ArrayList();
		cronometro = new Cronometro();
		movimiento = new Movimiento();
		acumulado = 0;
		cantidad = 0;
		//Screen.lockCursor = true;
		Time.timeScale = 1;

		int numeroRutina = 0;
		if (EscenarioRutinaAutomatica.bt1 == true) {
			numeroRutina = 1;
			canti=10;
		} else if (EscenarioRutinaAutomatica.bt2 == true) {
			numeroRutina = 2;
			canti=7;
		} else if (EscenarioRutinaAutomatica.bt3 == true) {
			numeroRutina = 3;
			canti=8;
		} else if (EscenarioRutinaAutomatica.bt4 == true) {
			numeroRutina = 4;
			canti=8;
		}
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Codigo",cod_nino);
		form.AddField ("Cancion", cancion);
		form.AddField ("Cod_rutina", numeroRutina);
		WWW www = new WWW(url,form);
		StartCoroutine(WaitForRequest(www));
		if (EscenarioRutinaAutomatica.rutinaAutomatica) 
			ejecutarRutinaAutomatica ();
		else
			rutinaActual = EscenarioRutinaManual.arregloMovimientos;
		
		animation.Play("ReconocimientoKinect");
		initiated = true;
	}


	public void cron (float tiempoActual){
		movimiento = new Movimiento();
		cronometro = new Cronometro();
		cronometro.IniciarCronometro();
		tiempoActual = cronometro.CalcularTiempoTranscurrido();
	}


	public void Update () {
		
				if (Input.GetKeyUp (KeyCode.Escape)) {
			
						paused = tooglePause ();		
				}
					if (cont2 <= rutinaActual.Count && tiempoRutina <= 0f) {
						switch (cont2) {
						case 0:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("AbduccionBrazos");
										if(tiempoCalifica<=0){
											rutina (0);
											cont2++;
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
										}
				}else{cont2++;}
								break;
						case 1:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosAbiertos");
										if(tiempoCalifica<=0){
											rutina (1);
											cont2++;
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
										}
				}else{cont2++;}
								break;
						case 2:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosAbiertosArriba");
										if(tiempoCalifica<=0){
											rutina (2);
											cont2++;
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
										}
				}else{cont2++;}
								break;
						case 3:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosArriba");
										if(tiempoCalifica<=0){
											rutina (3);
											cont2++;
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
										}
				}else{cont2++;}
								break;
						case 4:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("PiernasBrazoDerechaArriba");
										if(tiempoCalifica<=0){
											rutina (4);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 5:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("PiernasAbduccionBrazosCodosArriba");
										if(tiempoCalifica<=0){
											rutina (5);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 6:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("AduccionPiernasBrazoDer");
										if(tiempoCalifica<=0){
											rutina (6);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 7:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("AbduccionBrazosRodillaIzq");
										if(tiempoCalifica<=0){
											rutina (7);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 8:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("AbduccionBrazosRodillaDer");
										if(tiempoCalifica<=0){
											rutina (8);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 9:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazoIzqPiernaDer");
										if(tiempoCalifica<=0){
											rutina (9);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 10:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazoDerPiernaIzq");
										if(tiempoCalifica<=0){
											rutina (10);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 11:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazoDerALaCabeza");
										if(tiempoCalifica<=0){
											rutina (11);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 12:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosALaCabeza");
										if(tiempoCalifica<=0){
											rutina (12);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 13:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosArribaOvalo");
										if(tiempoCalifica<=0){
											rutina (13);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 14:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazoIzqAdelante");
										if(tiempoCalifica<=0){
											rutina (14);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 15:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazoDerAdelante");
										if(tiempoCalifica<=0){
											rutina (15);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 16:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("BrazosArribaInclinarIzq");
										if(tiempoCalifica<=0){
											rutina (16);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						case 17:
								if ((bool)rutinaActual [cont2] == true) {
										animation.Play("MoverManoDerArriba");
										if(tiempoCalifica<=0){
											rutina (17);
											tiempoRutina = 7.2f;
											tiempoCalifica = 10.2f;
											cont2++;
										}
				}else{cont2++;}
								break;
						}

				}
				tiempoRutina -= Time.deltaTime;
				tiempoCalifica -= Time.deltaTime;
		//Debug.Log (tiempoRutina);
		}
		
	

	public bool tooglePause (){
		if (Time.timeScale == 0){
			//Screen.lockCursor = true;
			Time.timeScale = 1;
			return (false);
		}
		else{
			Screen.lockCursor = false;
			Time.timeScale = 0;
			return (true);			
		}		
	}


	public void OnGUI (){
		if (paused){

			GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
			if (GUI.Button(new Rect(0,0,bW,bH),"Menu Principal"))
			{
				EscenarioRutinaManual.vaciarArr();
				Application.LoadLevel("ok");
				
			}
			if(GUI.Button(new Rect(0,120,bW,bH),"Salir Del Juego")){
				
				Application.Quit();
			}
			GUI.EndGroup();
		}
	}

	 public void windowFunc(int id){
		if (GUILayout.Button("Resume")){
			pausado = false;
		}
		if (GUILayout.Button("Options")){
			
		}
		if (GUILayout.Button("Quit")){
			
		}
	}


	void rutina(int numMovimiento){
		switch(numMovimiento){
		case 0:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido(); 
			movimiento.Abduccionbrazos(tiempoActual);
			Debug.Log(movimiento.Abduccionbrazos(tiempoActual));
			float puntaje = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje, 1);


			
			//tiempoActual = cronometro.CalcularTiempoTranscurrido();

			break;
		case 1:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverBrazosAbiertos(tiempoActual);
			//Debug.Log(movimiento.moverBrazosAbiertos(tiempoActual));
			float puntaje0 = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje0, 2);
			break;
		case 2:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverBrazosAbiertosArriba(tiempoActual);
			//Debug.Log(movimiento.moverBrazosAbiertosArriba(tiempoActual));
			float puntaje1 = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje1, 3);
			break;
		case 3:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverBrazosArriba(tiempoActual);
			//Debug.Log(movimiento.moverBrazosArriba(tiempoActual));
			float puntaje2 = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje2, 4);
			break;
		case 4:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverAduccionPiernasBrazoDerechoArriba(tiempoActual);
			//Debug.Log(movimiento.MoverAduccionPiernasBrazoDerechoArriba(tiempoActual));
			float puntaje3 = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje3, 5);
			break;
		case 5:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverBrazosArribaCodosAbiertos(tiempoActual);
			//Debug.Log(movimiento.moverBrazosArribaCodosAbiertos(tiempoActual));
			float puntaje4 = movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje4, 6);
			break;
		case 6:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverAbduccionPiernasBrazoIzquierdo(tiempoActual);
			//Debug.Log(movimiento.moverAbduccionPiernasBrazoIzquierdo(tiempoActual));
			float puntaje5= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje5, 7);
			break;
		case 7:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverAbduccionBrazosRodillaIzquerda(tiempoActual);
			Debug.Log(movimiento.MoverAbduccionBrazosRodillaIzquerda(tiempoActual));
			float puntaje6= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje6, 8);
			break;
		case 8:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverAbduccionBrazosRodillaDerecha(tiempoActual);
			Debug.Log(movimiento.MoverAbduccionBrazosRodillaDerecha(tiempoActual));
			float puntaje7= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje7, 9);
			break;
		case 9:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverBrazoIzquierdaPiernaDerecha(tiempoActual);
			Debug.Log(movimiento.MoverBrazoIzquierdaPiernaDerecha(tiempoActual));
			float puntaje8= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje8, 10);
			break;
		case 10:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverBrazoDerechaPiernaIzquierda(tiempoActual);
			Debug.Log(movimiento.MoverBrazoDerechaPiernaIzquierda(tiempoActual));
			float puntaje9= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje9, 11);
			break;
		case 11:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverBrazoDerechaPiernaIzquierda(tiempoActual);
			Debug.Log(movimiento.MoverBrazoDerechaPiernaIzquierda(tiempoActual));
			float puntaje10= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje10, 12);
			break;
		case 12:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.BrazosAlaCabeza(tiempoActual);
			Debug.Log(movimiento.BrazosAlaCabeza(tiempoActual));
			float puntaje11= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje11, 13);
			break;
		case 13:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.moverBrazosArriba(tiempoActual);
			Debug.Log(movimiento.moverBrazosArriba(tiempoActual));
			float puntaje12= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje12, 14);
			break;
		case 14:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.BrazoIzquierdoSemiflexionado(tiempoActual);
			Debug.Log(movimiento.BrazoIzquierdoSemiflexionado(tiempoActual));
			float puntaje13= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje13, 15);
			break;
		case 15:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.BrazoDerechoSemiflexionado(tiempoActual);
			Debug.Log(movimiento.BrazoDerechoSemiflexionado(tiempoActual));
			float puntaje14= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje14, 16);
			break;
		case 16:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverBrazosArribaInclinarIzquierda(tiempoActual);
			Debug.Log(movimiento.MoverBrazosArribaInclinarIzquierda(tiempoActual));
			float puntaje15= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje15, 17);
			break;
		case 17:
			cronometro.IniciarCronometro(); 
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			movimiento.MoverManoDerechaArribaIncinarIzquierda(tiempoActual);
			Debug.Log(movimiento.MoverManoDerechaArribaIncinarIzquierda(tiempoActual));
			float puntaje16= movimiento.GetCalificacionMov();
			puntaje_movimiento(puntaje16, 18);
			break;
			}
		
	}

	//Metodo rutinas automaticas
	void ejecutarRutinaAutomatica(){
		//rutina numero 1}
		int numeroRutina = 0;
		if (EscenarioRutinaAutomatica.bt1 == true) {
			numeroRutina = 1;
		} else if (EscenarioRutinaAutomatica.bt2 == true) {
			numeroRutina = 2;
		} else if (EscenarioRutinaAutomatica.bt3 == true) {
			numeroRutina = 3;
		} else if (EscenarioRutinaAutomatica.bt4 == true) {
			numeroRutina = 4;
		} else {
			Debug.Log ("Seleccione una rutina");
		}
		switch(numeroRutina) {
		case 1:
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			Debug.Log ("rutina 1 agregada");
			break;
		case 2:
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			Debug.Log ("rutina 2 agregada");
			break;
		case 3: 
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			Debug.Log ("rutina 3 agregada");
			break;
		case 4:
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (false);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			rutinaActual.Add (true);
			Debug.Log ("rutina 4 agregada");
			break;
		}
	}

	IEnumerator WaitForRequest(WWW hs_get){
		yield return hs_get;
		string mensaje=hs_get.text;
		cod_sesion = mensaje;
		if(hs_get.error== null){
			Debug.Log ("codigo de sesion es: "+mensaje);
		} else {
			print("ERROR: "+hs_get.error);
		}
	}

	void puntaje_movimiento(float puntaje, int movimiento){
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Puntaje",puntaje.ToString());
		form.AddField ("Movimiento",movimiento);
		form.AddField ("Cod_sesion",cod_sesion);
		acumulado = acumulado + puntaje;
		Debug.Log ("puntaje acumulado "+acumulado);
		cantidad = cantidad + 1;
		Debug.Log ("cantidad"+cantidad);
		WWW www = new WWW(url2,form);
		StartCoroutine(WaitForRequest2(www));
		}

	IEnumerator WaitForRequest2(WWW hs_get){
		yield return hs_get;
		Debug.Log ("cont2" + cont2);
		Debug.Log ("canti" + canti);
	    if (cantidad == canti) {
		Debug.Log ("entra al puntaje general");
		puntaje_general ();}
		if(hs_get.error== null){
			//mensaje = hs_get.text;
			Debug.Log ("se ingreso la calificacion");
		} else {
			print("ERROR: "+hs_get.error);
		}
	}
	void puntaje_general(){
		float acum = acumulado/cantidad;
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Puntaje",acum.ToString());
		form.AddField ("Cod_nino",cod_nino);
		form.AddField ("Cod_sesion",cod_sesion);
		WWW www = new WWW(url3,form);
		StartCoroutine(WaitForRequest3(www));
	}

	IEnumerator WaitForRequest3(WWW hs_get){
		Debug.Log ("esta entrando al final");
		yield return hs_get;
		string mensaje;
		if(hs_get.error== null){
			//mensaje = hs_get.text;
			Debug.Log (" calificacion final");
		} else {
			print("ERROR: "+hs_get.error);
		}
	}
}