/**
 * BAILA CONMIGO JUEGA CON KINECT
 * PROYECTO DE INVESTIGACION I
 * Ivan Camilo Latorre Peña, Diana Ramirez, Esteban Rodriguez
 */

/**
 * LIBRERIAS
 */
using UnityEngine;
using System.Collections;

///<summary>
///Clase encargada de evaluar los movimientos.
///</summary>
///<remarks>
///Evalua los movimeintos del videojuego.
///</remarks>
public class Movimiento : MonoBehaviour {
	// Variables Globales.
	private Modelo modelo;
	public static ArrayList datosMovimientos;
	private CalificacionMovimiento calificarMovimiento;
	float calificacionMov = 0;

	
	/// <summary>
	/// Initializes a new instance of the <see cref="Movimiento"/> class.
	/// </summary>
	public Movimiento(){
		modelo = new Modelo();
		datosMovimientos = new ArrayList();
		calificarMovimiento = new CalificacionMovimiento();
		calificarMovimiento.SetTiempo(15);
	}
		
	/// <summary>
	/// Gets the model of the player.
	/// </summary>
	/// <returns>
	/// The modelo.
	/// </returns>
	public Modelo GetModelo(){
		return modelo;
	}


	public float GetCalificacionMov(){
		return calificacionMov;
	}
	
	/**
	 * MOVIMIENTOS MODULO 0
	 **/
	/// <summary>
		/// Obtiene si el jugador a movido su cabeza a la izquierda.
		/// </summary>
		/// <returns>
		/// Devuelve si el jugador movio su cabeza a la izquierda o no.
		/// </returns>
	public ArrayList MoverCabezaIzq(float pTiempoTranscurrido) {
		Debug.Log ("MoverCabezaIzq");
		calificacionMov = 0;
		datosMovimientos.Clear();
		float posCabezaX = modelo.PosicionCabeza().x;
	
		if(posCabezaX<16.9){
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posCabezaX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
		/// Obtiene si el jugador a movido su cabeza a la derecha.
		/// </summary>
		/// <returns>
		/// Devuelve si el jugador movio su cabeza a la derecha o no.
		/// </returns>
	public ArrayList MoverCabezaDer(float pTiempoTranscurrido) {
		Debug.Log ("MoverCabezaDer");
		calificacionMov = 0;
		datosMovimientos.Clear();
		float posCabezaX = modelo.PosicionCabeza().x;
		
		if(posCabezaX>17.9){
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posCabezaX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
		/// Obtiene si el jugador a movido su cabeza hacia abajo.
		/// </summary>
		/// <returns>
		/// Devuelve si el jugador movio su cabeza hacia abajo o no.
		/// </returns>
	public ArrayList MoverCabezaAbajo(float pTiempoTranscurrido) {
		Debug.Log ("MoverCabezaAbajo");
		calificacionMov = 0;
		datosMovimientos.Clear();
		float posCabezaY = modelo.PosicionCabeza().y;
		
		if(posCabezaY<22.550){
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posCabezaY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	/// <summary>
		/// Obtiene si el jugador a realizado aducción de sus brazos.
		/// </summary>
		/// <returns>
		/// Devuelve si el jugador realizó aducción de sus brazos o no.
		/// </returns>
	public ArrayList Aduccionbrazos(float pTiempoTranscurrido) {
		Debug.Log ("Aduccionbrazos");
		calificacionMov = 0;
		datosMovimientos.Clear();
		float posBrazoDerechoY = modelo.PosicionManoDerecha().y;
		float posBrazoIzquierdoY = modelo.PosicionManoIzquierda().y;
			
		if(posBrazoDerechoY<13.3 & posBrazoIzquierdoY<13.3) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posBrazoDerechoY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	/// <summary>
	/// Obtiene si el jugador a realizado aducción de sus piernas.
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador realizó aducción de sus piernas o no.
	/// </returns>
	public ArrayList AduccionPiernas(float pTiempoTranscurrido) {
		Debug.Log ("AduccionPiernas");
		calificacionMov = 0;
		datosMovimientos.Clear();
		
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float distpies = posPieDerechoX-posPieIzquierdoX;
		if(distpies<1) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(0, distpies, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
	/// Obtiene si el jugador a realizado abducción de sus brazos.
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador realizó abducción de sus brazos o no.
	/// </returns>
	public ArrayList Abduccionbrazos(float pTiempoTranscurrido) {
		Debug.Log ("Abduccion brazos");
		calificacionMov = 0;
		datosMovimientos.Clear();
		float posManoDerX = modelo.PosicionManoDerecha().x;
		Debug.Log (modelo.PosicionManoDerecha().x);
		float posManoDerY = modelo.PosicionManoDerecha().y;
		Debug.Log (modelo.PosicionManoDerecha().y);
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		Debug.Log (modelo.PosicionManoIzquierda().x);
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		Debug.Log (modelo.PosicionManoIzquierda().y);
		
		if((posManoDerX > 23 && posManoDerY > 13 && 
			posManoIzqX > 7 && posManoIzqY > 15) )
		{
		  float der = posManoDerX - posManoDerY;
		  float izqu = posManoIzqY - posManoIzqX;
		  float rango=0;
			if (der <= 2) {
				rango = 1;}
		    else if (der >= 2.1 && der <= 4) {
				rango = 2;}
			else if(der >=4.1 && der <= 6){
				rango = 3;}
			else if(der >=6.1 && der <= 8){
				rango = 4;}
			else if(der >=8.1 && der <= 10){
				rango = 5;}
			rango=(rango+izqu)/2;
			//Rubrica calificacion = calificarMovimiento.ClasificarRango (10, posManoDerX, pTiempoTranscurrido);
			Rubrica calificacion =  calificarMovimiento.ClasificarRangos(rango);

			//Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posManoDerX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			Debug.Log ("entro a calificar");
			Debug.Log("puntaje " + calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
			//Debug.Log ("puntaje "+ calificacionMov);
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the brazos abiertos.
	/// </summary>
	/// <returns>
	/// The brazos abiertos.
	/// </returns>
	/// <param name='pTiempoTranscurrido'>
	/// P tiempo transcurrido.
	/// </param>
	public ArrayList moverBrazosAbiertos(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("Brazos abiertos");

		float posManoDerechaX = modelo.PosicionManoDerecha().x;
		Debug.Log (posManoDerechaX);
		float posManoDerechaY = modelo.PosicionManoDerecha().y;
		Debug.Log (posManoDerechaY);
		float posManoIzquierdaX = modelo.PosicionManoIzquierda().x;
		Debug.Log (posManoIzquierdaX);
		float posManoIzquierdaY = modelo.PosicionManoIzquierda().y;
		Debug.Log (posManoIzquierdaY);
		
		if(posManoDerechaX > 20 && posManoDerechaY > 14 && 
			posManoIzquierdaX >7 && posManoIzquierdaY > 14) {
			Debug.Log ("entro a calificar Brazos abiertos");
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(10, posManoIzquierdaX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			Debug.Log("puntaje" + calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
			//Debug.Log ("puntaje " + calificacionMov);
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the brazos abiertos arriba.
	/// </summary>
	/// <returns>
	/// The brazos abiertos arriba.
	/// </returns>
	public ArrayList moverBrazosAbiertosArriba(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("Brazos abiertos arriba");
		float posManoDerechaX = modelo.PosicionManoDerecha().x;
		Debug.Log (posManoDerechaX);
		float posManoDerechaY = modelo.PosicionManoDerecha().y;
		Debug.Log (posManoDerechaY);
		float posManoIzquierdaX = modelo.PosicionManoIzquierda().x;
		Debug.Log (posManoIzquierdaX);
		float posManoIzquierdaY = modelo.PosicionManoIzquierda().y;
		Debug.Log (posManoIzquierdaY);
		
		if(posManoDerechaX>19 && posManoDerechaY>15 && 
			posManoIzquierdaX>10 && posManoIzquierdaY>14) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(21, posManoDerechaX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			//Debug.Log ("entro a calificar");
			//Debug.Log("puntaje" + calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
			//Debug.Log("puntaje" + calificacionMov);
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the brazos arriba.
	/// </summary>
	/// <returns>
	/// The brazos arriba.
	/// </returns>
	public ArrayList moverBrazosArriba(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("Brazos arriba");

		float posBrazoDerechoY = modelo.PosicionManoDerecha().y;
		Debug.Log (posBrazoDerechoY);
		float posBrazoIzquierdoY = modelo.PosicionManoIzquierda().y;
		Debug.Log (posBrazoIzquierdoY);
		
		if((posBrazoDerechoY>15 & posBrazoIzquierdoY>15) &&
			(posBrazoDerechoY<=27 & posBrazoIzquierdoY<=27)){
			
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(27, posBrazoDerechoY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			//Debug.Log ("Puntaje" + calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
			//Debug.Log ("no califico");
		}
		return datosMovimientos;
	}
	
	/// <summary>
		/// Movers the codo izquierdo mano izquierda.
		/// </summary>
		/// <returns>
		/// The codo izquierdo mano izquierda.
		/// </returns>/
	public ArrayList moverCodoIzquierdoManoIzquierda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("moverCodoIzquierdoManoIzquierda");
		float codoIzquierdoX = modelo.PosicionCodoIzquierdo().x;
		Debug.Log (codoIzquierdoX);
		float codoIzquierdoY = modelo.PosicionCodoIzquierdo().y;
		Debug.Log (codoIzquierdoY);
		float manoIzquierdaX = modelo.PosicionManoIzquierda().x;
		Debug.Log (manoIzquierdaX);
		float manoIzquierdaY = modelo.PosicionManoIzquierda().y;
		Debug.Log (manoIzquierdaY);
		
		if ((codoIzquierdoX > 11.8 && codoIzquierdoY > 21.4) && 
			(manoIzquierdaX > 13 &&  manoIzquierdaY > 25)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(21, codoIzquierdoY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			Debug.Log (calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the brazos arriba codos abiertos.
	/// </summary>
	/// <returns>
	/// The brazos arriba codos abiertos.
	/// </returns>
	public ArrayList moverBrazosArribaCodosAbiertos(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("moverBrazosArribaCodosAbiertos");
		float codoIzquierdoX = modelo.PosicionCodoIzquierdo().x;
		Debug.Log (codoIzquierdoX);
		float codoIzquierdoY = modelo.PosicionCodoIzquierdo().y;
		Debug.Log (codoIzquierdoY);
		float manoIzquierdaX = modelo.PosicionManoIzquierda().x;
		Debug.Log (manoIzquierdaX);
		float manoIzquierdaY = modelo.PosicionManoIzquierda().y;
		Debug.Log (manoIzquierdaY);
		float codoDerechoX = modelo.PosicionCodoDerecho().x;
		Debug.Log (codoDerechoX);
		float codoDerechoY = modelo.PosicionCodoDerecho().y;
		Debug.Log (codoDerechoY);
		float manoDerechaX = modelo.PosicionManoDerecha().x;
		Debug.Log (manoDerechaX);
		float manoDerechaY = modelo.PosicionManoDerecha().y;
		Debug.Log (manoDerechaY);
		
		if ((codoIzquierdoX > 11 && codoIzquierdoY > 19) && 
			(codoDerechoX > 23 && codoDerechoY > 19) && 
			(manoDerechaX > 22 && manoDerechaY > 22) &&
			(manoIzquierdaX >10 && manoIzquierdaY > 23)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)20.5, codoIzquierdoX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
			Debug.Log (calificacionMov);
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}	
	
	/// <summary>
	/// Obtiene si el jugador a realizado abducción de sus piernas.
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador realizó abducción de sus piernas o no.
	/// </returns>
	public ArrayList AbduccionPiernas(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("AbduccionPiernas");
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float distpies = posPieDerechoX-posPieIzquierdoX;
		
		if(distpies>3) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(3, distpies, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
	/// Obtiene si el jugador a estirado sus brazos
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador a estirado sus brazos o no.
	/// </returns>
	public ArrayList Estirarbrazos(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("Estirarbrazos");
		float posBrazoDerechoZ = modelo.PosicionManoDerecha().z;
		float posBrazoIzquierdoZ = modelo.PosicionManoIzquierda().z;
			
		if(posBrazoDerechoZ<(-1) & posBrazoIzquierdoZ<(-1)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(-1, posBrazoDerechoZ, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
	/// Obtiene si el jugador a levantado sus manos.
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador levantó sus manos o no.
	/// </returns>
	public ArrayList ManosArriba(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("ManosArriba");
		float posManoDerechaY = modelo.PosicionManoDerecha().y;
		float posManoIzquierdaY = modelo.PosicionManoIzquierda().y;
			
		if(posManoDerechaY>26 & posManoIzquierdaY>26) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(26, posManoDerechaY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
	/// Obtiene si el jugador a movido sus piernas hacia atras.
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador movió sus piernas hacia atras o no.
	/// </returns>
	public ArrayList MoverPiernaAtras(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverPiernaAtras");
		float posPiernaZ = modelo.PosicionPiernaDerecha().z;
		if(posPiernaZ>4.7) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(3, posPiernaZ, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the aduccion piernas brazo derecho arriba.
	/// </summary>
	/// <returns>
	/// The aduccion piernas brazo derecho arriba.
	/// </returns>
	public ArrayList MoverAduccionPiernasBrazoDerechoArriba(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverAduccionPiernasBrazoDerechoArriba");
		float codoIzquierdoX = modelo.PosicionCodoIzquierdo().x;
		float codoIzquierdoY = modelo.PosicionCodoIzquierdo().y;
		float manoIzquierdaX = modelo.PosicionManoIzquierda().x;
		float manoIzquierdaY = modelo.PosicionManoIzquierda().y;
		
		if ((codoIzquierdoX > 11.8 && codoIzquierdoY > 21.4) && 
			(manoIzquierdaX > 13 &&  manoIzquierdaY > 25)) {
		
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)13, manoIzquierdaX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the abduccion piernas brazo izquierdo.
	/// </summary>
	/// <returns>
	/// The abduccion piernas brazo izquierdo.
	/// </returns>
	public ArrayList moverAbduccionPiernasBrazoIzquierdo(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("moverAbduccionPiernasBrazoIzquierdo");
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		
		float distpies = posPieDerechoX-posPieIzquierdoX;
		
		if((distpies>3) && (posManoIzqX > 22.4 && posManoIzqY > 22.1)){
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)22.1, posManoIzqY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		}else{
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Movers the aduccion piernas manos izquierda.
	/// </summary>
	/// <returns>
	/// The aduccion piernas manos izquierda.
	/// </returns>
	public ArrayList MoverAduccionPiernasManosIzquierda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverAduccionPiernasManosIzquierda");
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float distpies = posPieDerechoX-posPieIzquierdoX;
		float posManoDerechaY = modelo.PosicionManoDerecha().y;
		float posManoDerechaX = modelo.PosicionManoDerecha().x;
		float posManoIzquierdaY = modelo.PosicionManoIzquierda().y;
		float posManoIzquierdaX = modelo.PosicionManoIzquierda().x;
			
		if((distpies>3) & ((posManoDerechaY>20 & posManoDerechaY<=23.5) & 
			(posManoDerechaX>7.5 & posManoDerechaX<=12.5)) & 
			((posManoIzquierdaY>20 & posManoIzquierdaY<=23.5) & 
			(posManoIzquierdaX>7.5 & posManoIzquierdaX<=12.5))) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)12.5, posManoIzquierdaY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else{
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}

	/// <summary>
	/// Obtiene si el jugador abduce los brazo izquierdo y estira la pierna derecha
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador abduce los brazo izquierdo y estira la pierna derecha o no
	/// </returns>
	public ArrayList MoverBrazoIzquierdaPiernaDerecha(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazoIzquierdaPiernaDerech");
		float posPieDerX = modelo.PosicionPieDerecho().x;
		float posPieIzqY = modelo.PosicionPieIzquierdo().x;
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float distpies = posPieDerX-posPieIzqY;
		
		if((distpies>3) && (posManoIzqX > 11 && posManoIzqY > 14)) {
				Rubrica calificacion =  calificarMovimiento.ClasificarRango(14, posManoIzqY, pTiempoTranscurrido);
				datosMovimientos.Add(true);
				datosMovimientos.Add(calificacion.GetCategoria());
			    calificacionMov = calificacion.GetCategoria();
			} else {
				datosMovimientos.Add(false);
			    calificacionMov = 0;
			}
		
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador abduce los brazos derecho y estira la pierna izquierda
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador abduce los brazos derecho y estira la pierna izquierda o no
	/// </returns>
	public ArrayList MoverBrazoDerechaPiernaIzquierda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazoDerechaPiernaIzquierda");
		float posPieDerX = modelo.PosicionPieDerecho().x;
		float posPieIzqY = modelo.PosicionPieIzquierdo().x;
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float distpies = posPieDerX-posPieIzqY;
				
		if((distpies>3) && (posManoDerX > 24 && posManoDerY > 14)) {
				Rubrica calificacion =  calificarMovimiento.ClasificarRango(14, posManoDerY, pTiempoTranscurrido);
				datosMovimientos.Add(true);
				datosMovimientos.Add(calificacion.GetCategoria());
			    calificacionMov = calificacion.GetCategoria();
			} else {
				datosMovimientos.Add(false);
			    calificacionMov = 0;
		} 
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador abduce los brazos y flexiona la rodilla derecha
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pabduce los brazos y flexiona la rodilla derecha o no
	/// </returns>
	public ArrayList MoverAbduccionBrazosRodillaDerecha(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverAbduccionBrazosRodillaDerecha");
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float posPiernDerY = modelo.PosicionPieDerecho().y;
			
		if((posManoDerX>22.5 && posManoDerY>14.5) && 
			(posManoDerX<=27.5 && posManoDerY<=18.5) && 
			(posManoIzqX>9 && posManoIzqY>14.5) && 
			(posManoIzqX<=14.5 && posManoIzqY<=18.5)) {
			if(posPiernDerY>4 && posPiernDerY<=12) {
				Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)18.5, posManoDerY, pTiempoTranscurrido);
				datosMovimientos.Add(true);
				datosMovimientos.Add(calificacion.GetCategoria());
				calificacionMov = calificacion.GetCategoria();
			} else {
				datosMovimientos.Add(false);

			}
		}else{
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador abduce los brazos y flexiona la rodilla izquierda
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pabduce los brazos y flexiona la rodilla izquierda o no
	/// </returns>	
	public ArrayList MoverAbduccionBrazosRodillaIzquerda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverAbduccionBrazosRodillaIzquerda");
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float posPiernIzqY = modelo.PosicionPieIzquierdo().y;
			
		if((posManoDerX>22.5 && posManoDerY>14.5) && 
			(posManoDerX<=27.5 && posManoDerY<=18.5) && 
			(posManoIzqX>9 && posManoIzqY>14.5) && 
			(posManoIzqX<=14.5 && posManoIzqY<=18.5)){
			if(posPiernIzqY>4 && posPiernIzqY<=12) {
				Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)18.5, posManoDerY, pTiempoTranscurrido);
				datosMovimientos.Add(true);
				datosMovimientos.Add(calificacion.GetCategoria());
				calificacionMov = calificacion.GetCategoria();
			} else {
				datosMovimientos.Add(false);
			}
		}else{
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/**
	 * MOVIMIENTOS MODULO 1
	 **/
	
	/// <summary>
	/// Movers the brazo atras cabeza inclinada.
	/// </summary>
	/// <returns>
	/// The brazo atras cabeza inclinada.
	/// </returns>
	public ArrayList MoverBrazoAtrasCabezaInclinada(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazoAtrasCabezaInclinada");
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posCabezaX = modelo.PosicionCabeza().x;
			
		if((posManoDerX>25.8 && posManoDerY>16.3) && 
			(posManoDerX<=28 && posManoDerY<= 20.1) && 
			(posCabezaX>17.1 && posCabezaX>18)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)16.3, posManoDerY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador pone el brazo izquierdo en la cabeza
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pone el brazo izquierdo en la cabeza
	/// </returns>	
	public ArrayList BrazoIzquierdoAlaCabeza(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("BrazoIzquierdoAlaCabeza");
		float posCodoIzqX = modelo.PosicionCodoIzquierdo().x;
		float posCodoIzqY = modelo.PosicionCodoIzquierdo().y;
		
		if((posCodoIzqX>12.2 && posCodoIzqY>22.8)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)22.8, posCodoIzqY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador pone los brazos en la cabeza
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pone los brazos en la cabeza o no.
	/// </returns>
	public ArrayList BrazosAlaCabeza(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("BrazosAlaCabeza");
		float posCodoIzqX = modelo.PosicionCodoIzquierdo().x;
		float posCodoIzqY = modelo.PosicionCodoIzquierdo().y;
		float posCodoDerX = modelo.PosicionCodoDerecho().x;
		float posCodoDerY = modelo.PosicionCodoDerecho().y;
		
		
		if((posCodoIzqX>12.2 && posCodoIzqY>22.8)  && 
			(posCodoDerX>23.8 && posCodoDerY>22.8)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)22.8, posCodoDerY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}

	/// <summary>
	/// Obtiene si el jugador pone los brazos en la espalda
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pone los brazos en la espalda o no
	/// </returns>
	public ArrayList MoverBrazosAtras(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazosAtras");
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float posManoIzqZ = modelo.PosicionManoIzquierda().z;
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posManoDerZ = modelo.PosicionManoDerecha().z;
		float disManos = posManoDerX - posManoIzqX;
		
		if((posManoDerY<15 && posManoIzqY<15) && 
			(posManoIzqZ>=8.5 && posManoDerZ>=6.5)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)6.5, posManoDerY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
		
	/// <summary>
	/// Movers the brazos cruzados adelante.
	/// </summary>
	/// <returns>
	/// The brazos cruzados adelante.
	/// </returns>
	public ArrayList MoverBrazosCruzadosAdelante(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazosCruzadosAdelante");
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqZ = modelo.PosicionManoIzquierda().z;
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerZ = modelo.PosicionManoDerecha().z;
		
		
		if((posManoDerX >= 18.8 && posManoIzqZ >= 3.0) && 
			(posManoIzqX >= 17.5 && posManoIzqZ >= 3.0)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)17.5, posManoIzqX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	/// <summary>
	/// Movers the inclinar brazos.
	/// </summary>
	/// <returns>
	/// The inclinar brazos.
	/// </returns>
	/// <param name='pTiempoTranscurrido'>
	/// P tiempo transcurrido.
	/// </param>
	public ArrayList MoverInclinarBrazos(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverInclinarBrazos");
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqZ = modelo.PosicionManoIzquierda().z;
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerZ = modelo.PosicionManoDerecha().z;
		float posCabezaZ = modelo.PosicionCabeza().z;
		float posPechoZ= modelo.PosicionPecho().z;
		float disManosX =  posManoDerX - posManoIzqX;
		float disManosZ =  posManoDerZ - posManoIzqZ;
		float disPechoManos = (posPechoZ-disManosZ) + (posPechoZ-disManosZ);
		
		if((posCabezaZ<5) && (disManosX<=2 && disManosX>-2.5) && 
			(disPechoManos<=7)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango(7, disPechoManos, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador pone los brazos en la espalda estirados e inclina la cabeza
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador pone los brazos en la espalda estirados e inclina la cabeza o no
	/// </returns>
	public ArrayList MoverBrazosArribaInclinarIzquierda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverBrazosArribaInclinarIzquierda");
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posPechoX = modelo.PosicionPecho().x;
		float posPelvis = modelo.PosicionPelvis().x;
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float distpies = posPieDerechoX-posPieIzquierdoX;
		
		if( (distpies>3) && (posManoDerX<15 && posManoIzqX<15) && 
			(posManoDerX>=10 && posManoIzqX>=10) &&
			(posManoDerY>25 && posManoIzqY>25)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)10, posManoDerX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	 
	public ArrayList MoverManoDerechaArribaIncinarIzquierda(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverManoDerechaArribaIncinarIzquierda");
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posPechoX = modelo.PosicionPecho().x;
		float posPelvis = modelo.PosicionPelvis().x;
		float posPieDerechoX = modelo.PosicionPieDerecho().x;
		float posPieIzquierdoX = modelo.PosicionPieIzquierdo().x;
		float distpies = posPieDerechoX-posPieIzquierdoX;
		
		if( (distpies>3) && (posManoDerX<15 && posManoDerX>=10) &&
			posManoDerY > 25) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)15, posManoDerX, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador cruza los brazos
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador scruza los brazos o no.
	/// </returns>
		public ArrayList BrazosCruzados(float pTiempoTranscurrido) {
			datosMovimientos.Clear();
		    calificacionMov = 0;
		    Debug.Log ("MoverManoDerechaArribaIncinarIzquierda");
			float posManoDerX = modelo.PosicionManoDerecha().x;
			float posManoDerY = modelo.PosicionManoDerecha().y;
			float posManoIzqX = modelo.PosicionManoIzquierda().x;
			float posManoIzqY = modelo.PosicionManoIzquierda().y;
		
		if((posManoDerX>18.0 && posManoDerY>18.3) && 
			(posManoIzqX>18.5 && posManoIzqY>18.3)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)18.3, posManoIzqY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador semiflexiona el brazo derecho al frente del pecho
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador semiflexiona el brazo derecho al frente del pecho o no.
	/// </returns>
	public ArrayList BrazoDerechoSemiflexionado(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverManoDerechaArribaIncinarIzquierda");
		float posManoDerX = modelo.PosicionManoDerecha().x;
		float posManoDerY = modelo.PosicionManoDerecha().y;
		float posCodoDerX = modelo.PosicionCodoDerecho().x;
		float posCodoDerY = modelo.PosicionCodoDerecho().y;
		
		if((posManoDerX>18.0 && posManoDerY>18.3)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)18.7, posManoDerY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else {
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}
	
	/// <summary>
	/// Obtiene si el jugador semiflexiona el brazo izquierdo al frente del pecho
	/// </summary>
	/// <returns>
	/// Devuelve si el jugador semiflexiona el brazo izquierdo al frente del pecho o no.
	/// </returns>
	public ArrayList BrazoIzquierdoSemiflexionado(float pTiempoTranscurrido) {
		datosMovimientos.Clear();
		calificacionMov = 0;
		Debug.Log ("MoverManoDerechaArribaIncinarIzquierda");
		float posManoIzqX = modelo.PosicionManoIzquierda().x;
		float posManoIzqY = modelo.PosicionManoIzquierda().y;
		float posCodoIzqX = modelo.PosicionCodoIzquierdo().x;
		float posCodoIzqY = modelo.PosicionCodoIzquierdo().y;
		
		if((posManoIzqX > 17 && posManoIzqY > 18.3)) {
			Rubrica calificacion =  calificarMovimiento.ClasificarRango((float)18.3, posManoIzqY, pTiempoTranscurrido);
			datosMovimientos.Add(true);
			datosMovimientos.Add(calificacion.GetCategoria());
			calificacionMov = calificacion.GetCategoria();
		} else { 
			datosMovimientos.Add(false);
			calificacionMov = 0;
		}
		return datosMovimientos;
	}

}