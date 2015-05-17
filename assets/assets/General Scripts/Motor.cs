/*=============================================================================
 BAILA CONMIGO JUEGA CON KINECT
 PROYECTO DE INVESTIGACION I
 Ivan Camilo Latorre Peña, Diana Ramirez, Esteban Rodriguez
==============================================================================*/

/*=============================================================================
LIBRERIAS
=============================================================================*/
using UnityEngine;
using System.Collections;

///<summary>
///Clase encargada de generar la secuencia de movmientos de acuerdo el modulo
///</summary>
///<remarks>
///Genera la secuencia de movmientos de acuerdo el modulo
///</remarks>
public class Motor : MonoBehaviour
{
	int lnSelectedModule;
	CapturarMovimiento movimientosModulo0;
	CapturarMovimiento movimientosModulo1;
	// Use this for initialization
	void Start ()
	{
		lnSelectedModule = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(lnSelectedModule){
		case 0:
			movimientosModulo0 = new CapturarMovimiento(0);
			break;
		case 1:
			movimientosModulo1 = new CapturarMovimiento(1);
			break;
		}
		
	}
}

