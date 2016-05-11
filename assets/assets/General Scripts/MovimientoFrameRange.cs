using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Movimiento frame range.
/// Define los Rangos en una rutina generada por cada movimiento.
/// </summary>
public class MovimientoFrameRange {

	private int initFrame;
	private int endFrame;
	private string cod_movimiento;
	private int puntajeMovimiento;

	public MovimientoFrameRange () {
		this.puntajeMovimiento = 0;
	}
	

	public int InitFrame {
		get {
			return this.initFrame;
		}
		set {
			initFrame = value;
		}
	}

	public int EndFrame {
		get {
			return this.endFrame;
		}
		set {
			endFrame = value;
		}
	}

	public string Cod_movimiento {
		get {
			return this.cod_movimiento;
		}
		set {
			cod_movimiento = value;
		}
	}

	public int PuntajeMovimiento {
		get {
			return this.puntajeMovimiento;
		}
		set {
			puntajeMovimiento = value;
		}
	}

	public static int getIndexByRange(List<MovimientoFrameRange> movimientos, int frame) {
		for (int i = 0; i < movimientos.Count; i++) {
			MovimientoFrameRange movimiento = movimientos[i];
			if (movimiento.InitFrame <= frame && frame <= movimiento.EndFrame) {
				return i;
			}
		}
		return -1;
	}

	public static int getIndexByCodMovimiento(List<MovimientoFrameRange> movimientos, string cod_movimiento) {
		for (int i = 0; i < movimientos.Count; i++) {
			MovimientoFrameRange movimiento = movimientos[i];
			if (movimiento.Cod_movimiento == cod_movimiento) {
				return i;
			}
		}
		return -1;
	}
}
