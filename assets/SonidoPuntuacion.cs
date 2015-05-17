using UnityEngine;
using System.Collections;

public class SonidoPuntuacion : MonoBehaviour {
	public static AudioSource fuente;
	public static AudioClip felici;
	public static AudioClip bien;
	public static AudioClip vamos;
	public bool ejecutarSonido ;
	public float tiempo;
	public  double ram;

	void Start () {
		fuente = gameObject.AddComponent<AudioSource>();
		fuente.playOnAwake = false;
		vamos = (AudioClip)Resources.Load ("vamos_puedes_hacerlo_mejor");
		felici = (AudioClip)Resources.Load ("felicitaciones");
		bien = (AudioClip)Resources.Load ("muy_bien");
		audio.minDistance =30f;
		ejecutarSonido = false;
		tiempo = 18f;



	}
	
	// Update is called once per frame
	void Update () {
		if (tiempo <= 0) {
			ejecutarSonido=true;
				}
		if (ejecutarSonido) {
			playSonido (EjecucionRutina.calificacionSonido, ejecutarSonido);
			ejecutarSonido=false;
			tiempo=7.5f;
		}
		tiempo -= Time.deltaTime;
	}

	public static void playSonido(double cali, bool playSound){
		//ram = Random.Range(1,5);
		if (playSound) {

			if (cali >= 4) {
						fuente.PlayOneShot (felici);
				}
			else if (cali >= 3 && cali < 4) {
						fuente.PlayOneShot (bien);
				} 
				else {
						fuente.PlayOneShot (vamos);
				}
		}

	}

}
