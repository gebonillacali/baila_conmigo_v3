using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Session juego.
/// Clase que maneja la logica de la sesion de un juego o reproduccion de rutina.
/// </summary>
public static class SessionJuego {

	private static string nombreJugador;
	private static string codigoJugador;
	private static int puntajeJugador;
	private static int codigoRutina;
	private static string codigoSesion;
	private static float acumuladoJugador;
	private static List<MovimientoFrameRange> movimientosFrameRange;
	private static List<string> codigoMovimientos;
	private static int repeticionesMovimientos;
	private static bool rutinaGenerada;

	public static void setNombreJugador(string nombreJugadorValue) {
		nombreJugador = nombreJugadorValue;
	}

	public static string getNombreJugador() {
		return nombreJugador;
	}

	public static void setCodigoJugador(string codigoJugadorValue) {
		codigoJugador = codigoJugadorValue;
	}

	public static string getCodigoJugador() {
		return codigoJugador;
	}

	public static void setCodigoSesion(string codigoSesionValue) {
		codigoSesion = codigoSesionValue;
	}
	
	public static string getCodigoSesion() {
		return codigoSesion;
	}

	public static void setCodigoRutina(int codigoRutinaValue) {
		codigoRutina = codigoRutinaValue;
	}
	
	public static int getCodigoRutina() {
		return codigoRutina;
	}

	public static void setPuntajeJugador(int puntajeJugadorValue) {
		puntajeJugador = puntajeJugadorValue;
	}
	
	public static int getPuntajeJugador() {
		return puntajeJugador;
	}

	public static void setAcumuladoJugador(float acumuladoJugadorValue) {
		acumuladoJugador = acumuladoJugadorValue;
	}
	
	public static float getAcumuladoJugador() {
		return acumuladoJugador;
	}

	public static List<MovimientoFrameRange> getMovimientoFrameRange() {
		return movimientosFrameRange;
	}

	public static List<string> getCodigoMovimientos() {
		return codigoMovimientos;
	}

	public static void setRutinaGenerada(bool rutinaGeneradaValue) {
		rutinaGenerada = rutinaGeneradaValue;
	}

	public static bool isRutinaGenerada() {
		return rutinaGenerada;
	}

	public static int getRepeticionesMovimientos() {
		return repeticionesMovimientos;
	}

	public static void setRepeticionesMovimientos(int numRepeticiones) {
		repeticionesMovimientos = numRepeticiones;
	}

	public static void limpiarDatos() {
		codigoJugador = "";
		nombreJugador = "";
		puntajeJugador = 0;
		codigoSesion = "";
		acumuladoJugador = 0f;
		codigoRutina = 0;
		rutinaGenerada = false;
		movimientosFrameRange = new List<MovimientoFrameRange> ();
		codigoMovimientos = new List<string> ();
		repeticionesMovimientos = 1;
	}

	public static void prepareForNextGame() {
		puntajeJugador = 0;
		codigoSesion = "";
		acumuladoJugador = 0f;
		codigoRutina = 0;
		rutinaGenerada = false;
		movimientosFrameRange = new List<MovimientoFrameRange> ();
		codigoMovimientos = new List<string> ();
		repeticionesMovimientos = 1;
	}
}
