/*/**
 * BAILA CONMIGO JUEGA CON KINECT
 * PROYECTO DE INVESTIGACION I
 * Ivan Camilo Latorre Peña, Diana Ramirez, Esteban Rodriguez
 **/

/**
 * LIBRERIAS
 * */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

///<summary>
///Clase encargada de gestionar la lectura de los movimientos.
///</summary>
///<remarks>
///Gestiona la solicitud de los movimeintos del videojuego.
///</remarks>
public class CapturarMovimiento : MonoBehaviour {
	
	//Variables Globales;
	private int IdSelectorModule;
	private int IdSelector;
	private bool bFocusInit;
	private bool cargarCancion;
	private bool reproducirInstruccion;
	private ArrayList dataMove;
	private ArrayList afCalificacionesMovimientos;
	private ArrayList atNombresMovimientos;
	private Aviso titulo;
	private Aviso posicionVect;
	private Movimiento movimiento;
	private EvaluacionRutina evaluacion;
	private InstruccionVisual instruccionVisual;
	private Cronometro cronometro;
	private SeleccionMusical audios;
	private SeleccionMusical aciertoSonoro;
	private SeleccionMusical instruccionSonora;
	
	//canciones
	public AudioClip audioModulo0;
	public AudioClip audioSuccessful;
	public AudioClip audiocongratulations;
	
	//Instrucciones Sonoras
	public AudioClip audioInstruccionMoverAduccion;
	public AudioClip audioInstruccionMoverAbduccionBrazos;
	public AudioClip audioInstruccionMoverAbduccionBrazosRodillaDerecha;
	public AudioClip audioInstruccionMoverAbduccionBrazosRodillaIzquerda;
	public AudioClip audioInstruccionMoverAbduccionPiernasBrazoIzquierda;
	public AudioClip audioInstruccionMoverAbduccionPiernasBrazosIzquierda;
	public AudioClip audioInstruccionMoverAduccionPiernasAbducionBrazosCodosArriba;
	public AudioClip audioInstruccionMoverAduccionPiernasBrazoDerechoArriba;
	public AudioClip audioInstruccionMoverAduccionPiernasManoDerecha;
	public AudioClip audioInstruccionMoverAduccionPiernasManosIzquierda;
	public AudioClip audioInstruccionMoverBrazosAbiertos;
	public AudioClip audioInstruccionMoverBrazosAbiertosArriba;
	public AudioClip audioInstruccionMoverBrazosALaCabeza;
	public AudioClip audioInstruccionMoverBrazoDerechoALaCabeza;
	public AudioClip audioInstruccionMoverBrazoDerechoAdelante;
	public AudioClip audioInstruccionMoverBrazosIzquierdoAdelante;
	public AudioClip audioInstruccionMoverBrazosAtras;
	public AudioClip audioInstruccionMoverBrazosAtrasCabezaInclinada;
	public AudioClip audioInstruccionMoverBrazosArriba;
	public AudioClip audioInstruccionMoverBrazosArribaInclinarIzquierda;
	public AudioClip audioInstruccionMoverBrazosCruzadosAdelante;
	public AudioClip audioInstruccionMoverBrazosCruzados;
	public AudioClip audioInstruccionMoverBrazoAtrasCabezaInclinada;
	public AudioClip audioInstruccionMoverBrazoDerechaPiernaIzquierda;
	public AudioClip audioInstruccionMoverBrazoIzquierdoAlaCabeza;
	public AudioClip audioInstruccionMoverBrazoIzquierdaPiernaDerecha;
	public AudioClip audioInstruccionMoverInclinarBrazos;
	public AudioClip audioInstruccionMoverManoDerechaArribaIncinarIzquierda;
	
	//modulo 0
	public bool moverBrazoDerechoALaCabeza = false;
	public bool moverBrazosAbiertos = false;
	public bool moverBrazosAbiertosArriba = false; 
	public bool moverBrazoDerechaPiernaIzquierda = false;
	public bool moverBrazoIzquierdaPiernaDerecha = false;
	public bool moverBrazosArriba = false;
	public bool moverAbduccionBrazos = false;
	public bool moverAbduccionBrazosRodillaIzquerda = false;
	public bool moverAbduccionBrazosRodillaDerecha = false;
	public bool moverAbduccionPiernas = false;
	public bool moverAbduccionPiernasBrazosIzquierda = false;
	public bool moverAbduccionPiernasBrazoIzquierda = false;
	public bool moverAduccionBrazos = false;
	public bool moverAduccionPiernas = false;
	public bool moverAduccionPiernasBrazoDerechoArriba = false;
	public bool moverAduccionPiernasAbducionBrazosCodosArriba = false;
	public bool moverCabezaAbajo = false;	
	public bool moverCabezaDerecha = false;
	public bool moverCabezaIzquierda = false;
	public bool moverEstirarBrazos = false;
	public bool moverPiernaAtras = false;
	
	//modulo 1
	public bool moverBrazoAtrasCabezaInclinada = false;
	public bool moverBrazoDerechoAdelante = false;
	public bool moverBrazoDerechoSemiflexionado = false;
	public bool moverBrazoIzquierdoSemiflexionado = false;
	public bool moverBrazoIzquierdoAdelante = false;
	public bool moverBrazoIzquierdoALaCabeza = false;
	public bool moverBrazosALaCabeza = false;
	public bool moverBrazosAtras = false;
	public bool moverBrazosArribaInclinarIzquierda = false;
	public bool moverBrazosCruzados = false;
	public bool moverBrazosCruzadosAdelante = false;
	public bool moverInclinarBrazos= false;
	public bool moverManoDerechaArribaIncinarIzquierda = false;
	
	//Instrucciones Visuales.
	public Texture2D instruccionMoverAduccion;
	public Texture2D instruccionMoverAbduccionBrazos;
	public Texture2D instruccionMoverAbduccionBrazosRodillaDerecha;
	public Texture2D instruccionMoverAbduccionBrazosRodillaIzquerda;
	public Texture2D instruccionMoverAbduccionPiernasBrazoIzquierda;
	public Texture2D instruccionMoverAbduccionPiernasBrazosIzquierda;
	public Texture2D instruccionMoverAduccionPiernasAbducionBrazosCodosArriba;
	public Texture2D instruccionMoverAduccionPiernasBrazoDerechoArriba;
	public Texture2D instruccionMoverAduccionPiernasManoDerecha;
	public Texture2D instruccionMoverAduccionPiernasManosIzquierda;
	public Texture2D instruccionMoverBrazosAbiertos;
	public Texture2D instruccionMoverBrazosAbiertosArriba;
	public Texture2D instruccionMoverBrazosALaCabeza;
	public Texture2D instruccionMoverBrazoDerechoALaCabeza;
	public Texture2D instruccionMoverBrazoDerechoAdelante;
	public Texture2D instruccionMoverBrazosIzquierdoAdelante;
	public Texture2D instruccionMoverBrazosAtras;
	public Texture2D instruccionMoverBrazosAtrasCabezaInclinada;
	public Texture2D instruccionMoverBrazosArriba;
	public Texture2D instruccionMoverBrazosArribaInclinarIzquierda;
	public Texture2D instruccionMoverBrazosCruzadosAdelante;
	public Texture2D instruccionMoverBrazosCruzados;
	public Texture2D instruccionMoverBrazoAtrasCabezaInclinada;
	public Texture2D instruccionMoverBrazoDerechaPiernaIzquierda;
	public Texture2D instruccionMoverBrazoIzquierdoAlaCabeza;
	public Texture2D instruccionMoverBrazoIzquierdaPiernaDerecha;
	public Texture2D instruccionMoverInclinarBrazos;
	public Texture2D instruccionMoverManoDerechaArribaIncinarIzquierda;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		dataMove  = new ArrayList();
		afCalificacionesMovimientos = new ArrayList ();
		atNombresMovimientos = new ArrayList();
		IdSelector = 1;
		IdSelectorModule = 0;
		bFocusInit = false;
		cargarCancion = true;
		reproducirInstruccion= true;
		titulo = new Aviso("gtTitulo"); 
		posicionVect = new Aviso("posicionVect"); 
		movimiento = new Movimiento();
		instruccionVisual = new  InstruccionVisual("rectInstruccion"); 
		cronometro = new Cronometro();
		audios = new SeleccionMusical("rzAmbienteSonoro", audioModulo0);
		aciertoSonoro = new SeleccionMusical("rzAmbienteAciertoSonoro", audioSuccessful);
		instruccionSonora = new SeleccionMusical("rzAmbienteSonoraInstruccion", audioInstruccionMoverAduccion);
		evaluacion = new EvaluacionRutina();
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="capturarMovimiento"/> class.
	/// </summary>
	/// <param name='pIdSelectorModule'>
	/// P identifier selector module.
	/// </param>
	public CapturarMovimiento(int pIdSelectorModule){
		dataMove  = new ArrayList();
		IdSelectorModule = pIdSelectorModule;
		IdSelector = 1;
		IdSelectorModule = 0;
		bFocusInit = false;
		cargarCancion = true;
		reproducirInstruccion= true;
		afCalificacionesMovimientos = new ArrayList ();
		atNombresMovimientos = new ArrayList();
		titulo = new Aviso("gtTitulo"); 
		posicionVect = new Aviso("posicionVect"); 
		movimiento = new Movimiento();
		instruccionVisual = new  InstruccionVisual("rectInstruccion"); 
		cronometro = new Cronometro();
		audios = new SeleccionMusical("rzAmbienteSonoro", audioModulo0);
		aciertoSonoro = new SeleccionMusical("rzAmbienteAciertoSonoro", audioSuccessful);
		instruccionSonora = new SeleccionMusical("rzAmbienteSonoraInstruccion", audioInstruccionMoverAduccion);
		evaluacion = new EvaluacionRutina();
	}
	
	/// <summary>
	/// Actualiza los objetos de juego con sus cambios respectivos siempre y cuando el jugador  
	/// se encuentre dentro del rango indicado, de esta forma se valida si se capturan los movimientos.
	/// </summary>
	void Update () {
		if(cargarCancion){
			audios.CambiarCancion(cargarCancion, 192.6);
			cargarCancion = false;
		}
		
		/*if(bFocusInit){	
			//string nombreArticulacion = movimiento.GetModelo().GetManoIzquierda().GetName();
			//string posicionArticulacion = movimiento.GetModelo().GetManoIzquierda().GetPosicion().ToString();
			//posicionVect.SetAviso("articulacion: " + nombreArticulacion + " Posicion: " +  posicionArticulacion);
			
			if(IdSelectorModule == 0){
				SeguirSecuenciaModulo0();
			}else if(IdSelectorModule == 1){
				SeguirSecuenciaModulo1();
			}else{
				
			}
		}else{
			validFoco();
		}*/
		
	}
	
	/// <summary>
	/// Valida la posición del jugador a partir del modelo, dependiendo de las coordenadas indicadas y
	/// las actuales del modelo.
	/// </summary>
	/// <returns>
	/// Devuelve si el modelo se encuentra dentro del foco o fuera de él.
	/// </returns>
	public bool validFoco(){
		float tiempoActual;
		cronometro.SetTiempoInicio((float) 0);
		cronometro.SetTiempoFinal((float) 0);
		cronometro.IniciarCronometro();
		tiempoActual = cronometro.CalcularTiempoTranscurrido();
		titulo.SetAviso("Muevete por el Espacio");
		if(tiempoActual < -36){
			bFocusInit = true;
			cronometro.DetenerCronometro();
			return false;
		}else{
			return true;
		}
	}	
	
	/// <summary>
	/// Seguirs the secuencia modulo0.
	/// </summary>
	public void SeguirSecuenciaModulo0(){
		float tiempoActual;
		int timeSpecific = -10;
		
		switch (IdSelector){
		case 1:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);	
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 2;
			}
			
			break;
		case 2:
			moverAbduccionBrazos = true;
			//instruccionVisual.setImagen(instruccionMoverAbduccionBrazos);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverAbduccionBrazos);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific  || !moverAbduccionBrazos){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAbduccionBrazos = false;
				cronometro.DetenerCronometro();
				IdSelector = 3;
			}
			
			break;
		case 3:
			moverBrazosAbiertos = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosAbiertos);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverBrazosAbiertos);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosAbiertos){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAbiertos = false;
				cronometro.DetenerCronometro();
				IdSelector = 4;
			}
			
			break;
		case 4:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific  || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 5;
			}
			
			break;	
		case 5:
			moverBrazosAbiertos = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosAbiertos);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverBrazosAbiertos);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosAbiertos){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAbiertos = false;
				cronometro.DetenerCronometro();
				IdSelector = 6;
			}
			
			break;	
		case 6:
			moverBrazosAbiertosArriba = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosAbiertosArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverBrazosAbiertosArriba);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosAbiertosArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAbiertosArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 7;
			}
			
			break;
		case 7:
			moverBrazosArriba = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			//instruccionSonora.SetClip(audioInstruccionMoverBrazosArriba);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 8;
			}
			
			break;	
		case 8:
			moverBrazosAbiertosArriba = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosAbiertosArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverBrazosAbiertosArriba);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosAbiertosArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAbiertosArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 10;
			}
			
			break;
		case 9:
			moverBrazosAbiertos = true;
			//instruccionVisual.setImagen(instruccionMoverBrazosAbiertos);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverBrazosAbiertos);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazosAbiertos){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAbiertos = false;
				cronometro.DetenerCronometro();
				IdSelector = 10;
			}
			break;
		case 10:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 11;
			}
			break;	
			
		case 11:
			moverAduccionPiernasBrazoDerechoArriba = true;
			//instruccionVisual.setImagen(instruccionMoverAduccionPiernasBrazoDerechoArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccionPiernasBrazoDerechoArriba);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionPiernasBrazoDerechoArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionPiernasBrazoDerechoArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 12;
			}
			
			break;
		case 12:
			moverAduccionPiernasAbducionBrazosCodosArriba = true;
			//instruccionVisual.setImagen(instruccionMoverAduccionPiernasAbducionBrazosCodosArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccionPiernasAbducionBrazosCodosArriba);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionPiernasAbducionBrazosCodosArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionPiernasAbducionBrazosCodosArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 13;
			}
			
			break;			
		case 13:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 14;
			}
			
			break;
		case 14:
			moverAbduccionPiernasBrazoIzquierda = true;
			//instruccionVisual.setImagen(instruccionMoverAbduccionPiernasBrazoIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAbduccionPiernasBrazoIzquierda);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAbduccionPiernasBrazoIzquierda){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAbduccionPiernasBrazoIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 15;
			}
			
			break;
		case 15:
			moverAbduccionPiernasBrazosIzquierda = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAbduccionPiernasBrazosIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAbduccionPiernasBrazosIzquierda);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAbduccionPiernasBrazosIzquierda){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAbduccionPiernasBrazosIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 16;
			}
			
			break;	
		case 16:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 17;
			}
			
			break;	
		case 17:
			moverAbduccionBrazosRodillaIzquerda = true;
			//instruccionVisual.setImagen(instruccionMoverAbduccionBrazosRodillaIzquerda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAbduccionBrazosRodillaIzquerda);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAbduccionBrazosRodillaIzquerda){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAbduccionBrazosRodillaIzquerda = false;
				cronometro.DetenerCronometro();
				IdSelector = 18;
			}
			
			break;	
		case 18:
			moverAbduccionBrazosRodillaDerecha = true;
			//instruccionVisual.setImagen(instruccionMoverAbduccionBrazosRodillaDerecha);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAbduccionBrazosRodillaDerecha);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAbduccionBrazosRodillaDerecha){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAbduccionBrazosRodillaDerecha = false;
				cronometro.DetenerCronometro();
				IdSelector = 19;
			}
			
			break;
		case 19:
			moverBrazoIzquierdoALaCabeza = true;
			//instruccionVisual.setImagen(instruccionMoverBrazoIzquierdoAlaCabeza);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverBrazoIzquierdoAlaCabeza);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazoIzquierdoALaCabeza){
				aciertoSonoro.CambiarCancion(true, 1.3);
				//instruccionMoverBrazoIzquierdoAlaCabeza = false;
				cronometro.DetenerCronometro();
				IdSelector = 20;
			}
			
			break;
		case 20:
			moverBrazoDerechaPiernaIzquierda = true;
			//instruccionVisual.setImagen(instruccionMoverBrazoDerechaPiernaIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverBrazoDerechaPiernaIzquierda);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazoDerechaPiernaIzquierda){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazoDerechaPiernaIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 21;
			}
			
			break;
			
		case 21:
			moverBrazoIzquierdaPiernaDerecha = true;
			//instruccionVisual.setImagen(instruccionMoverBrazoIzquierdaPiernaDerecha);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverBrazoIzquierdaPiernaDerecha);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverBrazoIzquierdaPiernaDerecha){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazoIzquierdaPiernaDerecha = false;
				cronometro.DetenerCronometro();
				IdSelector = 22;
			}
			break;
			
		case 22:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			//instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			instruccionSonora.SetClip(audioInstruccionMoverAduccion);
			instruccionSonora.CambiarCancion(true, 1.3);
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 23;
			}
			
			break;
		case 23:
			instruccionVisual.setImagen(null);
			aciertoSonoro.SetClip(audiocongratulations);
			aciertoSonoro.CambiarCancion(true, 1.3);
			evaluacion.CalificacionRutina(atNombresMovimientos, afCalificacionesMovimientos);
			IdSelector = 0;
			break;
		}
	}
	
	/// <summary>
	/// Seguirs the secuencia modulo1.
	/// </summary>
	public void SeguirSecuenciaModulo1(){
		float tiempoActual;
		int timeSpecific = -15;
		
		switch(IdSelector){
		case 1:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 2;
			}
			
			break;
		case 2:
			moverBrazoDerechoALaCabeza = true;
			instruccionVisual.setImagen(instruccionMoverBrazoDerechoALaCabeza);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazoDerechoALaCabeza){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazoDerechoALaCabeza = false;
				cronometro.DetenerCronometro();
				IdSelector = 3;
			}
			
			break;
		case 3:
			moverBrazosALaCabeza = true;
			instruccionVisual.setImagen(instruccionMoverBrazosALaCabeza);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosALaCabeza){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosALaCabeza = false;
				cronometro.DetenerCronometro();
				IdSelector = 4;
			}
			
			break;
		case 4:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 5;
			}
			
			break;
		case 5:
			moverBrazosAtras = true;
			instruccionVisual.setImagen(instruccionMoverBrazosAtras);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosAtras){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosAtras = false;
				cronometro.DetenerCronometro();
				IdSelector = 6;
			}
			
			break;
			
		case 6:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverAduccion );
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 7;
			}
			
			break;
		case 7:
			moverBrazosCruzadosAdelante = true;
			instruccionVisual.setImagen(instruccionMoverBrazosCruzadosAdelante);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosCruzadosAdelante){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosCruzadosAdelante = false;
				cronometro.DetenerCronometro();
				IdSelector = 8;
			}
			
			break;
		case 8:
			moverBrazoIzquierdoAdelante = true;
			instruccionVisual.setImagen(instruccionMoverBrazosIzquierdoAdelante);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazoIzquierdoAdelante){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazoIzquierdoAdelante = false;
				cronometro.DetenerCronometro();
				IdSelector = 9;
			}
			
			break;
		case 9:
			moverBrazoDerechoAdelante= true;
			instruccionVisual.setImagen(instruccionMoverBrazoDerechoAdelante);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazoDerechoAdelante){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazoDerechoAdelante = false;
				cronometro.DetenerCronometro();
				IdSelector = 10;
			}
			
			break;
		case 10:
			moverBrazosCruzados = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverBrazosCruzados);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosCruzados){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosCruzados = false;
				cronometro.DetenerCronometro();
				IdSelector = 11;
			}
			
			break;
		case 11:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 12;
			}
			
			break;	
			
		case 12:
			moverAduccionBrazos = true;
			moverAduccionPiernas = true;
			instruccionVisual.setImagen(instruccionMoverAduccion);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverAduccionBrazos || !moverAduccionPiernas){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverAduccionBrazos = false;
				moverAduccionPiernas = false;
				cronometro.DetenerCronometro();
				IdSelector = 13;
			}
			
			break;
		case 13:
			moverBrazosArriba = true;
			instruccionVisual.setImagen(instruccionMoverBrazosArriba);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosArriba){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosArriba = false;
				cronometro.DetenerCronometro();
				IdSelector = 14;
			}
			
			break;
		case 14:
			moverBrazosArribaInclinarIzquierda = true;
			instruccionVisual.setImagen(instruccionMoverBrazosArribaInclinarIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverBrazosArribaInclinarIzquierda){
				aciertoSonoro.CambiarCancion(true, 1.3);
				moverBrazosArribaInclinarIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 15;
			}
			
			break;
		case 15:
			moverManoDerechaArribaIncinarIzquierda = true;
			instruccionVisual.setImagen(instruccionMoverManoDerechaArribaIncinarIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverManoDerechaArribaIncinarIzquierda){
				moverManoDerechaArribaIncinarIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 16;
			}
			
			break;
			
		case 16:
			moverManoDerechaArribaIncinarIzquierda = true;
			instruccionVisual.setImagen(instruccionMoverManoDerechaArribaIncinarIzquierda);
			cronometro.IniciarCronometro();
			tiempoActual = cronometro.CalcularTiempoTranscurrido();
			cambioMovimiento(tiempoActual);
			
			if(tiempoActual < timeSpecific || !moverManoDerechaArribaIncinarIzquierda){
				moverManoDerechaArribaIncinarIzquierda = false;
				cronometro.DetenerCronometro();
				IdSelector = 17;
			}
			
			break;
			
		case 17:
			instruccionVisual.setImagen(null);
			aciertoSonoro.SetClip(audiocongratulations);
			aciertoSonoro.CambiarCancion(true, 1.3);
			evaluacion.CalificacionRutina(atNombresMovimientos, afCalificacionesMovimientos);
			IdSelector = 0;
			break;
		}
	}
	
	/// <summary>
	/// Se realiza una seleción del movimiento que se va a evaluar de forma secuencial, se asignan los avisos
	/// correspondientes para cada movimiento tales como la instrucción inicial y  la valoración final para el movimiento.
	/// </summary>
	public void cambioMovimiento(float pTiempoTranscurrido){
		
		if(moverCabezaIzquierda){
			dataMove.Clear();
			dataMove  = movimiento.MoverCabezaIzq(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Cabeza a la Izquierda");
				atNombresMovimientos.Add(1);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverCabezaIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
			
		}else{ 
			titulo.SetAviso("");
			//return false;
		}
		
		if(moverCabezaDerecha){
			dataMove.Clear();
			dataMove  = movimiento.MoverCabezaDer(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Cabeza a la Derecha");
				atNombresMovimientos.Add(2);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverCabezaDerecha = false;
			}else{ 
				titulo.SetAviso("");
			}		
		}
		
		/*if(moverEstirarBrazos){	
			dataMove.Clear();
			dataMove  = movimiento.Estirarbrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Estirar Brazos");
				moverEstirarBrazos = false;
                                atNombresMovimientos.Add(3);
                                afCalificacionesMovimientos.Add((float)dataMove[1]);
                                moverEstirarBrazos = false;
			 	//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}*/
		
		if(moverAbduccionBrazos){
			dataMove.Clear();
			dataMove  = movimiento.Abduccionbrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Abduccion de Brazos");
				atNombresMovimientos.Add(4);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionBrazos = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAduccionBrazos){
			dataMove.Clear();
			dataMove  = movimiento.Aduccionbrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Aduccion de Brazos");
				atNombresMovimientos.Add(5);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAduccionBrazos = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAduccionPiernas){
			dataMove = movimiento.AduccionPiernas(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Aduccion de Piernas");
				atNombresMovimientos.Add(6);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAduccionPiernas = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverBrazosAbiertos){
			dataMove.Clear();
			dataMove  = movimiento.moverBrazosAbiertos(pTiempoTranscurrido);
			if((bool)dataMove[0]){
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Abiertos");
				atNombresMovimientos.Add(7); 
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosAbiertos = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverBrazosAbiertosArriba) {
			dataMove.Clear();
			dataMove  = movimiento.moverBrazosAbiertosArriba(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Abiertos Arriba");
				atNombresMovimientos.Add(8);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosAbiertosArriba = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverBrazosArriba){
			dataMove.Clear();
			dataMove  = movimiento.moverBrazosArriba(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Arriba");
				atNombresMovimientos.Add(9);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosArriba = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAbduccionPiernas){
			dataMove.Clear();
			dataMove  = movimiento.AbduccionPiernas(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Abduccion de Piernas");
				atNombresMovimientos.Add(10);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionPiernas = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		
		if(moverCabezaAbajo){
			dataMove.Clear();
			dataMove  = movimiento.Aduccionbrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Cabeza Abajo");
				atNombresMovimientos.Add(11);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverCabezaAbajo = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverPiernaAtras){
			dataMove.Clear();
			dataMove  = movimiento.MoverPiernaAtras(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Pierna Atras");
				atNombresMovimientos.Add(12);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverPiernaAtras = false;	
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAduccionPiernasBrazoDerechoArriba){
			dataMove.Clear();
			dataMove  = movimiento.MoverAduccionPiernasBrazoDerechoArriba(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Aduccion Mano Derecha");
				atNombresMovimientos.Add(13);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAduccionPiernasBrazoDerechoArriba = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAbduccionPiernasBrazosIzquierda) {
			dataMove.Clear();
			dataMove  = movimiento.MoverAduccionPiernasManosIzquierda(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Aduccion Manos a la Izquierda");
				atNombresMovimientos.Add(14);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionPiernasBrazosIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAbduccionPiernasBrazoIzquierda) {
			dataMove.Clear();
			dataMove  = movimiento.moverAbduccionPiernasBrazoIzquierdo(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Abduccion Piernas Brazo Izquierda");
				atNombresMovimientos.Add(15);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionPiernasBrazoIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverBrazoIzquierdaPiernaDerecha){
			dataMove.Clear();
			dataMove  = movimiento.MoverBrazoIzquierdaPiernaDerecha(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Pierna arriba y brazo derecho");
				atNombresMovimientos.Add(16);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoIzquierdaPiernaDerecha = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverBrazoDerechaPiernaIzquierda){
			dataMove.Clear();
			dataMove  = movimiento.MoverBrazoDerechaPiernaIzquierda(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Pierna arriba y brazo Izquerdo");
				atNombresMovimientos.Add(17);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoDerechaPiernaIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAbduccionBrazosRodillaDerecha){
			dataMove.Clear();
			dataMove  = movimiento.MoverAbduccionBrazosRodillaDerecha(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Abiertos Rodilla Derecha");
				atNombresMovimientos.Add(18);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionBrazosRodillaDerecha = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		if(moverAbduccionBrazosRodillaIzquerda){
			dataMove.Clear();
			dataMove  = movimiento.MoverAbduccionBrazosRodillaIzquerda(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Abiertos Rodilla Izquierda");
				atNombresMovimientos.Add(19);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverAbduccionBrazosRodillaIzquerda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}	
		}
		
		
		if(moverBrazoAtrasCabezaInclinada) {
			dataMove.Clear();
			dataMove  = movimiento.MoverBrazoAtrasCabezaInclinada(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Atras Cabeza Inclinada");
				atNombresMovimientos.Add(20);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoAtrasCabezaInclinada = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazoIzquierdoALaCabeza) {
			dataMove.Clear();
			dataMove  = movimiento.BrazoIzquierdoAlaCabeza(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazo Izquierdo A la Cabeza");
				atNombresMovimientos.Add(21);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoIzquierdoALaCabeza = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazosALaCabeza) {
			dataMove.Clear();
			dataMove  = movimiento.BrazosAlaCabeza(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos A la Cabeza");
				atNombresMovimientos.Add(21);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosALaCabeza = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazosAtras) {
			dataMove.Clear();
			dataMove  = movimiento.BrazosAlaCabeza(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Atras");
				atNombresMovimientos.Add(22);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosAtras = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		
		if(moverBrazosCruzadosAdelante) {
			dataMove.Clear();
			dataMove  = movimiento.MoverBrazosCruzadosAdelante(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Cruzados Adelante");
				atNombresMovimientos.Add(23);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosCruzadosAdelante = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverInclinarBrazos) {
			dataMove.Clear();
			dataMove  = movimiento.MoverInclinarBrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Inclinar  Cabeza");
				atNombresMovimientos.Add(24);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverInclinarBrazos = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazosArribaInclinarIzquierda) {
			dataMove.Clear();
			dataMove  = movimiento.MoverBrazosArribaInclinarIzquierda(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Inclinarse Hacia la Izquierda");
				atNombresMovimientos.Add(25);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosArribaInclinarIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverManoDerechaArribaIncinarIzquierda) {
			dataMove.Clear();
			dataMove  = movimiento.MoverManoDerechaArribaIncinarIzquierda(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Inclinarse Hacia la Izquierda mano derecha");
				atNombresMovimientos.Add(26);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverManoDerechaArribaIncinarIzquierda = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazoDerechoAdelante) {
			dataMove.Clear();
			dataMove  = movimiento.BrazoDerechoSemiflexionado(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazo Derecho Semiflexionado");
				atNombresMovimientos.Add(27);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoDerechoAdelante = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazoIzquierdoSemiflexionado) {
			dataMove.Clear();
			dataMove  = movimiento.BrazoIzquierdoSemiflexionado(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazo Izquierdo Semiflexionado");
				atNombresMovimientos.Add(28);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazoIzquierdoSemiflexionado = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
		}
		
		if(moverBrazosCruzados) {
			dataMove.Clear();
			dataMove  = movimiento.Aduccionbrazos(pTiempoTranscurrido);
			if((bool)dataMove[0]) {
				titulo.SetAviso("Muy Bien!");
				//titulo.SetAviso("Brazos Cruzados");
				atNombresMovimientos.Add(29);
				afCalificacionesMovimientos.Add((float)dataMove[1]);
				moverBrazosCruzados = false;
				//return true;
			}else{ 
				titulo.SetAviso("");
				//return false;
			}
			
		}
	}
}