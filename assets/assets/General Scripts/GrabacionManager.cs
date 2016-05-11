using UnityEngine;
using System.Collections;
using SimpleJSON;

using System.IO;

/// <summary>
/// Grabacion manager.
/// Clase que permite gestionar la grabacion de los movimientos que se realizan en el Kinect
/// 
/// </summary>
public class GrabacionManager : MonoBehaviour, GrabadorMovimiento.GrabacionStatus, KinectManager.GestureListenerExecute, AudioMp3Manager.AudioMp3Listener {

	private Rect windowRect;
	
	private int bW = 200;
	private int bH = 50;
	private int gW = 200;
	private int GH = 260;

	private GrabadorMovimiento grabadorMovimiento;
	private string nombreRutina = "";
	private string path = "Recordings/playback/";
	private bool isMovimiento = false;
	private float timer = 0f;
	private string pathFile = "";
	
	public GUIText CalibrationText;
	public Texture imagenSalta;
	public KinectManager kinectManager;
	public string urlSaveRutina = "openclassmedia.org/bailaconmigo/crearRutinaNueva.php";
	public string urlSaveMovimiento = "openclassmedia.org/bailaconmigo/crearMovimientoNuevo.php";
	public AudioSource grabacionIniciada;
	public AudioSource grabacionFinalizada;

	private string cancionRutina = "";
	private bool isRequestingCancion = false;
	public AudioMp3Manager mp3Manager;
	
	// Use this for initialization
	void Start () {
		grabadorMovimiento = new GrabadorMovimiento (kinectManager, this);
		kinectManager.gestureListenerExecutor = this;
		mp3Manager.audioListener = this;

	}
	
	void OnGUI () {
		bool showCounter = false;

		GUIStyle buttonsStyle = new GUIStyle(GUI.skin.button);
		buttonsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUIStyle labelsStyle = new GUIStyle(GUI.skin.label);
		labelsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUIStyle textFileldsStyle = new GUIStyle(GUI.skin.textField);
		textFileldsStyle.fontSize = ScreenUtil.getFontFixedSize(20);

		GUIStyle labelsStyleTitle = new GUIStyle(GUI.skin.label);
		labelsStyleTitle.fontSize = ScreenUtil.getFontFixedSize(35);
		labelsStyleTitle.fontStyle = FontStyle.Bold;

		GUIStyle labelsStyleTitleCounter = new GUIStyle(GUI.skin.label);
		labelsStyleTitleCounter.fontSize = ScreenUtil.getFontFixedSize(200);
		labelsStyleTitleCounter.fontStyle = FontStyle.Bold;

		GUI.Label(ScreenUtil.getPosElement(new Rect(440,50,600,bH)),"Grabacion de Rutinas FNP", labelsStyleTitle);

		GUI.BeginGroup(new Rect (((Screen.width/2)- (gW/2)),((Screen.height/2)-(gW/2)), gW, GH));
		if (!grabadorMovimiento.isRecording ()) {

			if (GUI.Button (new Rect (0, 120, bW, bH), "Iniciar Grabacion", buttonsStyle)) {
				cancionRutina = "";
				grabadorMovimiento.StartRecord ();
			}
			if (GUI.Button (new Rect (0, 200, bW, bH), "Volver", buttonsStyle)) {
				Application.LoadLevel("ok");
				KinectWrapper.NuiShutdown();
			}

		} else {
			if (grabadorMovimiento.isWaitingForRoutineName()) {
				GUI.Label(new Rect (0, 0, bW, bH), "Ingrese el nombre de la rutina o movimiento", labelsStyle);
				nombreRutina = GUI.TextField(new Rect (0, 60, bW, bH), nombreRutina, textFileldsStyle);
				isMovimiento = GUI.Toggle(new Rect (0, 120, bW, bH), isMovimiento, "Grabaras un movimiento?");
				if (GUI.Button(new Rect (0, 180, bW, bH), "Ingresar", buttonsStyle)) {
					grabadorMovimiento.setRoutineName(nombreRutina.Replace(" ", ""));
					timer = 0;
				}
			} else {
				if (cancionRutina.Length <= 0) {
					if (!isRequestingCancion) {
						Debug.Log ("Loading cancion");
						
					}
				} else {
					if (grabadorMovimiento.getCountDown() > 0) {
						showCounter = true;
					} else {

						if (GUI.Button (new Rect (0, 120, bW, bH), "Detener Grabacion", buttonsStyle)) {
							if (!isMovimiento) {
								mp3Manager.stop();
							}
							grabacionFinalizada.Play();
							grabadorMovimiento.StopRecord ();
						}
					}
				}
			}
		}
		GUI.EndGroup();
		if (showCounter) {
			int seconds = Mathf.RoundToInt(timer % 60); 
			if (seconds > 0 && seconds%4 == 0) {
				timer = 0;
				grabadorMovimiento.setCountDown(grabadorMovimiento.getCountDown()-1);
				if (grabadorMovimiento.getCountDown() == 0) {
					//Grabacion Iniciada
					Debug.Log("grabacion iniciada play");
					grabacionIniciada.Play();
					if (!isMovimiento) {
						mp3Manager.play();
					}
				}
			}
			if(grabadorMovimiento.getCountDown() == 4) {
				GUI.Box(new Rect(250,50,600,500), "Instrucciones");
				GUI.DrawTexture(new Rect(430,250,200,200), imagenSalta);
				GUI.Label(ScreenUtil.getPosElement(new Rect(400,100,600,200)), "Para terminar la grabacion solo Salta muy alto", labelsStyleTitle);
			} else {
				GUI.Label(ScreenUtil.getPosElement(new Rect(583,200,600,200)), grabadorMovimiento.getCountDown().ToString() + "", labelsStyleTitleCounter);
			}
		}
	}

	public void audioLoaded (string fileLoaded) {
		cancionRutina = fileLoaded;
	}
	
	// Update is called once per frame
	void Update () {

		if (grabadorMovimiento.isRecording() && !grabadorMovimiento.isWaitingForRoutineName() && grabadorMovimiento.getCountDown() <= 0) {
			string statusRecord = grabadorMovimiento.record();
			if (statusRecord.Length > 0) {
				Debug.Log(statusRecord);
			}
		} else {
			if (grabadorMovimiento.isRecording() && !grabadorMovimiento.isWaitingForRoutineName() && cancionRutina.Length <= 0) {
				if (!isRequestingCancion) {
					if (!isMovimiento) {
						isRequestingCancion = true;
						mp3Manager.showFileBrowser();
					} else {
						cancionRutina = "movimiento";
					}
				}

			} else {			
				if (grabadorMovimiento.isRecording() && !grabadorMovimiento.isWaitingForRoutineName() && grabadorMovimiento.getCountDown() > 0) {
					timer += Time.deltaTime;
				}
			}
		}

	}

	/// <summary>
	/// Metodo que indica cuando se ha completado una grabacion.
	/// </summary>
	/// <param name="pathFile">Ruta del Archivo</param>
	public void grabacionCompletada(string pathFile) {
		this.pathFile = pathFile;
		//Subiendo la informacion de la rutina grabada.
		uploadInfo();
	}

	private void grabarRutinaInfo(string codigoRutina) {
		string rutinasJsonStr = File.ReadAllText (@path + "rutinas.json");
		Debug.Log(rutinasJsonStr);
		JSONArray rutinasData = JSONArray.Parse(rutinasJsonStr).AsArray;

		if (codigoRutina.Length <= 0) {
			codigoRutina = "0";
		}

		JSONClass rutina = new JSONClass();
		rutina["nombreRutina"] = this.nombreRutina;
		rutina["pathRutina"] = this.pathFile;
		rutina["codigoRutina"] = codigoRutina;
		rutina["isMovimiento"] = isMovimiento ? "si" : "no";
		rutina["cancionRutina"] = cancionRutina.Replace("\\","/");
		
		rutinasData.Add(rutina);
		
		rutinasJsonStr = JSON.JsonEncode(rutinasData.ToString()).Replace("\"[","[").Replace("]\"","]").Replace("\\","");
		Debug.Log(rutinasJsonStr);
		var sr = File.CreateText(@path + "rutinas.json");
		sr.WriteLine (rutinasJsonStr);
		sr.Close();
	}

	private void uploadInfo() {
		if (!isMovimiento) {
			uploadInfoRutina();
		} else {
			uploadInfoMovimiento();
		}
	}

	/// <summary>
	/// Sube la informacion de la rutina grabada.
	/// </summary>
	private void uploadInfoRutina() {
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "nombreRutina",nombreRutina);
		WWW www = new WWW(urlSaveRutina,form);
		StartCoroutine(WaitForRequest(www));
	}

	private void uploadInfoMovimiento() {
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "nombreRutina",nombreRutina);
		WWW www = new WWW(urlSaveMovimiento,form);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW hs_get){
		yield return hs_get;
		string codigoRutina = hs_get.text;
		if(hs_get.error!= null){
			print("ERROR: "+hs_get.error);
		}
		grabarRutinaInfo (codigoRutina);
	}

	public void gestureComplete(KinectGestures.Gestures gesture) {
		Debug.Log ("Deteniendo grabacion");
		if (grabadorMovimiento.isRecording()) {
			if (!isMovimiento) {
				mp3Manager.stop();
			}
			grabacionFinalizada.Play();
			grabadorMovimiento.StopRecord ();
		}
	}
}

