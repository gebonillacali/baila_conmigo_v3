using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Kinect;
using System;

/// <summary>
/// Skeleton wrapper player.
/// Clase de Modelo que realiza el match entre los datos del kinect de una rutina y los previamente guardados.
/// </summary>
public class SkeletonWrapperPlayer : MonoBehaviour {


	private Kinect.KinectInterface kinect;	
	public KinectManager kinectManager;
	private bool updatedSkeleton = false;
	private bool newSkeleton = false;
	private string pathFile = "";
	private bool isPlaying = false;
	private int puntaje=0;

	public void setPathFile(string pathFile) {
		this.puntaje = 0;
		this.pathFile = pathFile;
		setPuntaje = false;
		((ReproductorArchivoLector)kinect).reproductorNotifier = this.reproductorNotifier;
		((ReproductorArchivoLector)kinect).setInputFile (pathFile);
		((ReproductorArchivoLector)kinect).kinectManager = kinectManager;
		songPlayer.clip = audioModulo0;
		//songPlayer.Play ();
		isPlaying = true;
	}
	
	[HideInInspector]
	public Kinect.NuiSkeletonTrackingState[] players;
	[HideInInspector]
	public int[] trackedPlayers;
	[HideInInspector]
	public Vector3[,] bonePos;
	[HideInInspector]
	public Vector3[,] rawBonePos;
	[HideInInspector]
	public Vector3[,] boneVel;
	[HideInInspector]
	public Quaternion[,] boneLocalOrientation;
	[HideInInspector]
	public Quaternion[,] boneAbsoluteOrientation;
	
	public Kinect.NuiSkeletonPositionTrackingState[,] boneState;	
	private System.Int64 ticks;
	private float deltaTime;
	
	private Matrix4x4 kinectToWorld;
	public Matrix4x4 flipMatrix;

	private static float PERCENT_PASSED = 95.0f;

	//canciones
	public AudioClip audioModulo0;
	public AudioClip audioSuccessful;
	public AudioClip audiocongratulations;
	public AudioClip audioPuedesHacerloMejor;

	public AudioSource songPlayer;
	public AudioSource incidentsPlayer;
	public ReproductorArchivoLector.ReproductorNotifier reproductorNotifier;
	
	// Use this for initialization
	void Start () {
		kinect = new ReproductorArchivoLector();
		((ReproductorArchivoLector)kinect).reproductorNotifier = this.reproductorNotifier;
		players = new Kinect.NuiSkeletonTrackingState[Kinect.Constants.NuiSkeletonCount];
		trackedPlayers = new int[Kinect.Constants.NuiSkeletonMaxTracked];
		trackedPlayers[0] = -1;
		trackedPlayers[1] = -1;
		bonePos = new Vector3[2,(int)Kinect.NuiSkeletonPositionIndex.Count];
		rawBonePos = new Vector3[2,(int)Kinect.NuiSkeletonPositionIndex.Count];
		boneVel = new Vector3[2,(int)Kinect.NuiSkeletonPositionIndex.Count];
		
		boneState = new Kinect.NuiSkeletonPositionTrackingState[2,(int)Kinect.NuiSkeletonPositionIndex.Count];
		boneLocalOrientation = new Quaternion[2, (int)Kinect.NuiSkeletonPositionIndex.Count];
		boneAbsoluteOrientation = new Quaternion[2, (int)Kinect.NuiSkeletonPositionIndex.Count];
		
		//create the transform matrix that converts from kinect-space to world-space
		Matrix4x4 trans = new Matrix4x4();
		Debug.Log ("sensorhighet:" + kinect.getSensorHeight ());
		trans.SetTRS( new Vector3(-kinect.getKinectCenter().x,
		                          kinect.getSensorHeight()-kinect.getKinectCenter().y,
		                          -kinect.getKinectCenter().z),
		             Quaternion.identity, Vector3.one );
		Matrix4x4 rot = new Matrix4x4();
		Quaternion quat = Quaternion.identity;
		Debug.Log (quat);
		double theta = Mathf.Atan((kinect.getLookAt().y+kinect.getKinectCenter().y-kinect.getSensorHeight()) / (kinect.getLookAt().z + kinect.getKinectCenter().z));
		float kinectAngle = (float)(theta * (180 / Mathf.PI));
		quat.eulerAngles = new Vector3(-kinectAngle, 0, 0);
		rot.SetTRS( Vector3.zero, quat, Vector3.one);

		//final transform matrix offsets the rotation of the kinect, then translates to a new center
		kinectToWorld = flipMatrix*trans*rot;
	}

	private int countFrame = 0;
	private int numFrames = 0;

	private float handledFrameRate = 0f;
	private float velocidadMovimiento = 0f;
	private int currFrame=0;
	private float deltaTimeFPS=0f;
	private FrameRateManager frameRateManager = new FrameRateManager();
	private float factorFrameRate = 1F;
	private float numFrameRequired = 2.5F;
	private int countFrameToVoice = 0;
	private int puntajeToVoice=0;
	private int lastPuntajeToVoice=0;
	public bool allowToPlay = false;
	private float timerShowPuntaje = -1f;

	private void showFrameRate(bool show) {
		int w = Screen.width, h = Screen.height;
		
		GUIStyle style = new GUIStyle();
		
		Rect rect = new Rect(w - 300, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTimeFPS * 1000.0f;
		float fps = 1.0f / deltaTimeFPS;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		if (show) {
			GUI.Label(rect, text, style);
			GUI.Label (new Rect (800, 0, 350, 100), "Puntaje :" + puntaje);
			GUI.Label (new Rect (800, 50, 350, 100), "Puntaje Voice :" + puntajeToVoice);
		}

		factorFrameRate = frameRateManager.convertFrameRate (fps);
	}
	
	void OnGUI() {
		
		showFrameRate (false);
		
		GUI.Box(ScreenUtil.getPosElement(new Rect(0,0,350,100)), "Velocidad de Reproduccion");
		GUI.Label (new Rect (140, 20, 200, 50), "" + Math.Round((Decimal)(10f - velocidadMovimiento)));
		velocidadMovimiento = GUI.HorizontalSlider (new Rect (10, 50, 250, 20), velocidadMovimiento, 10, 0);

		if (timerShowPuntaje > 0) {

			GUIStyle labelsStyleTitle = new GUIStyle(GUI.skin.label);
			labelsStyleTitle.fontSize = ScreenUtil.getFontFixedSize(35);
			labelsStyleTitle.fontStyle = FontStyle.Bold;
			Color previousColor = GUI.contentColor;

			Rect shapeMessage = ScreenUtil.getPosElement(new Rect(600,150,300,50));

			if (30 <= lastPuntajeToVoice && lastPuntajeToVoice <= 100) {
				//Positive Action
				GUI.contentColor = new Color(0f,1f,0f,timerShowPuntaje/10f);
				GUI.Label(shapeMessage, ":) " + "+" + lastPuntajeToVoice + "pts", labelsStyleTitle);
			} else {
				if (lastPuntajeToVoice >= 10) {
					//Medium action
					GUI.contentColor = new Color(1f,0.92f,0.016f,timerShowPuntaje/10f);
					GUI.Label(shapeMessage, ":| " + "+" + lastPuntajeToVoice + "pts", labelsStyleTitle);
				} else {
					//Negative action
					GUI.contentColor = new Color(1f,0f,0f,timerShowPuntaje/10f);
					GUI.Label(shapeMessage, ":( " + "+" + lastPuntajeToVoice + "pts", labelsStyleTitle);
				}
			}
			GUI.contentColor = previousColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (allowToPlay && isPlaying) {
			if(handledFrameRate > numFrameRequired) {
				if (countFrame >= 20) {
					float percent = 0f;
					bool passed = true;

					try {

						ReproductorArchivoLector movPlayer = (ReproductorArchivoLector)kinect;
						NuiSkeletonData skeletonDataFactored = movPlayer.convertedSkeletonFactor ();
						if (numFrames == 0) {
							numFrames = movPlayer.getNumFrames();
						}

						int indexTracked = -1;
						
						for (int ii = 0; ii < kinectManager.skeletonFrame.SkeletonData.Length; ii++) {
							if (kinectManager.skeletonFrame.SkeletonData[ii].eTrackingState == KinectWrapper.NuiSkeletonTrackingState.SkeletonTracked) {
								indexTracked = ii;
								break;
							}
						}

						int length = (int)NuiSkeletonPositionIndex.Count;
						float umbral = 1.555555f;
						int isInsideRegionCheck = 0;
						for (int i = 0; i < length; i++) {
							Vector4 positionKinect = indexTracked >=0 && i < kinectManager.skeletonFrame.SkeletonData[indexTracked].SkeletonPositions.Length ? kinectManager.skeletonFrame.SkeletonData[indexTracked].SkeletonPositions [i] : new Vector4(); 
							Vector4 positionReproductor = skeletonDataFactored.SkeletonPositions[i];
							isInsideRegionCheck += movPlayer.isInsideRegion((Vector3)positionKinect, (Vector3)positionReproductor, umbral) ? 1 : 0;
							percent = (((float)isInsideRegionCheck / 20f ) * 100f);
						}
					} catch (Exception exp) {
						string exception = exp.Message;
					}

					//Debug.Log("Percent:"+percent);

					passed = percent > PERCENT_PASSED;

					if (passed) {
						//Positive Action
						puntaje += 10;
						puntajeToVoice += 10;
						if (SessionJuego.isRutinaGenerada()) {
							int indexMovimiento = MovimientoFrameRange.getIndexByRange(SessionJuego.getMovimientoFrameRange(),((ReproductorArchivoLector)kinect).curFrame);
							Debug.Log("indexMovimiento:" +indexMovimiento);

							SessionJuego.getMovimientoFrameRange()[indexMovimiento].Cod_movimiento = SessionJuego.getCodigoMovimientos()[indexMovimiento];
							SessionJuego.getMovimientoFrameRange()[indexMovimiento].PuntajeMovimiento += 10;

							Debug.Log(SessionJuego.getMovimientoFrameRange()[indexMovimiento].Cod_movimiento + " " + SessionJuego.getMovimientoFrameRange()[indexMovimiento].PuntajeMovimiento + " Pts");
						}
					}

					if(countFrameToVoice >= 5) {
						timerShowPuntaje = 10f;
						lastPuntajeToVoice = puntajeToVoice;
						puntajeToVoice = 0;
						countFrameToVoice = 0;
					} else {
						countFrameToVoice++;
					}

					countFrame = 0;
				} else {
					countFrame++;
				}
			}
		} else {
			//songPlayer.Stop();
		}
		deltaTimeFPS += (Time.deltaTime - deltaTimeFPS) * 0.1f;
		if (timerShowPuntaje > 0) {
			timerShowPuntaje -= (Time.deltaTime*2);
		}
	}
	
	void LateUpdate () {
		updatedSkeleton = false;
		newSkeleton = false;	
	}
		
	/// <summary>
	/// First call per frame checks if there is a new skeleton frame and updates,
	/// returns true if there is new data
	/// Subsequent calls do nothing have the same return as the first call.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool pollSkeleton () {

		if (allowToPlay && handledFrameRate > numFrameRequired) {
			handledFrameRate = 0F;
			if (!updatedSkeleton && currFrame > (int)velocidadMovimiento)
			{
				currFrame = 0;
				updatedSkeleton = true;
				if (kinect.pollSkeleton())
				{
					newSkeleton = true;
					System.Int64 cur = kinect.getSkeleton().liTimeStamp;
					System.Int64 diff = cur - ticks;
					ticks = cur;
					deltaTime = diff / (float)1000;
					processSkeleton();
				}
			} else {
				currFrame+=2;
			}
		}  else {
			handledFrameRate += factorFrameRate;
		}
		return newSkeleton;
	}

	bool setPuntaje = false;

	public bool finalizoReproduccion() {
		try {
			isPlaying = !((ReproductorArchivoLector)kinect).finalizoReproduccion (); 
		} catch (Exception exp) {
			isPlaying = false;
		}
		if (!isPlaying) {
			this.allowToPlay = false;
			if (!setPuntaje) {
				setPuntaje = true;
				SessionJuego.setPuntajeJugador(puntaje);
				Debug.Log("puntaje:" + puntaje);
				Debug.Log("numFrames:" + numFrames);
				SessionJuego.setAcumuladoJugador(((float)(puntaje*5))/(float)(((float)numFrames/20f)*10f));
			}
		}
		return !isPlaying;
	}
	
	private void processSkeleton () {
		int[] tracked = new int[Kinect.Constants.NuiSkeletonMaxTracked];
		tracked[0] = -1;
		tracked[1] = -1;
		int trackedCount = 0;
		//update players
		for (int ii = 0; ii < Kinect.Constants.NuiSkeletonCount; ii++)
		{
			players[ii] = kinect.getSkeleton().SkeletonData[ii].eTrackingState;
			if (players[ii] == Kinect.NuiSkeletonTrackingState.SkeletonTracked)
			{
				tracked[trackedCount] = ii;
				trackedCount++;
			}
		}
		//this should really use trackingID instead of index, but for now this is fine
		switch (trackedCount)
		{
		case 0:
			trackedPlayers[0] = -1;
			trackedPlayers[1] = -1;
			break;
		case 1:
			//last frame there were no players: assign new player to p1
			if (trackedPlayers[0] < 0 && trackedPlayers[1] < 0)
				trackedPlayers[0] = tracked[0];
			//last frame there was one player, keep that player in the same spot
			else if (trackedPlayers[0] < 0) 
				trackedPlayers[1] = tracked[0];
			else if (trackedPlayers[1] < 0)
				trackedPlayers[0] = tracked[0];
			//there were two players, keep the one with the same index (if possible)
			else
			{
				if (tracked[0] == trackedPlayers[0])
					trackedPlayers[1] = -1;
				else if (tracked[0] == trackedPlayers[1])
					trackedPlayers[0] = -1;
				else
				{
					trackedPlayers[0] = tracked[0];
					trackedPlayers[1] = -1;
				}
			}
			break;
		case 2:
			//last frame there were no players: assign new players to p1 and p2
			if (trackedPlayers[0] < 0 && trackedPlayers[1] < 0)
			{
				trackedPlayers[0] = tracked[0];
				trackedPlayers[1] = tracked[1];
			}
			//last frame there was one player, keep that player in the same spot
			else if (trackedPlayers[0] < 0)
			{
				if (trackedPlayers[1] == tracked[0])
					trackedPlayers[0] = tracked[1];
				else{
					trackedPlayers[0] = tracked[0];
					trackedPlayers[1] = tracked[1];
				}
			}
			else if (trackedPlayers[1] < 0)
			{
				if (trackedPlayers[0] == tracked[1])
					trackedPlayers[1] = tracked[0];
				else{
					trackedPlayers[0] = tracked[0];
					trackedPlayers[1] = tracked[1];
				}
			}
			//there were two players, keep the one with the same index (if possible)
			else
			{
				if (trackedPlayers[0] == tracked[1] || trackedPlayers[1] == tracked[0])
				{
					trackedPlayers[0] = tracked[1];
					trackedPlayers[1] = tracked[0];
				}
				else
				{
					trackedPlayers[0] = tracked[0];
					trackedPlayers[1] = tracked[1];
				}
			}
			break;
		}
		
		//update the bone positions, velocities, and tracking states)
		for (int player = 0; player < 2; player++)
		{
			//print(player + ", " +trackedPlayers[player]);
			if (trackedPlayers[player] >= 0)
			{
				for (int bone = 0; bone < (int)Kinect.NuiSkeletonPositionIndex.Count; bone++)
				{
					Vector3 oldpos = bonePos[player,bone];
					
					bonePos[player,bone] = kinectToWorld.MultiplyPoint3x4(kinect.getSkeleton().SkeletonData[trackedPlayers[player]].SkeletonPositions[bone]);
					//bonePos[player,bone] = kinectToWorld.MultiplyPoint3x4(bonePos[player, bone]);
					rawBonePos[player, bone] = kinect.getSkeleton().SkeletonData[trackedPlayers[player]].SkeletonPositions[bone];
					
					
					Kinect.NuiSkeletonBoneOrientation[] or = kinect.getBoneOrientations(kinect.getSkeleton().SkeletonData[trackedPlayers[player]]);
					boneLocalOrientation[player,bone] = or[bone].hierarchicalRotation.rotationQuaternion.GetQuaternion();
					boneAbsoluteOrientation[player,bone] = or[bone].absoluteRotation.rotationQuaternion.GetQuaternion();
					
					//print("index " + bone + ", start" + (int)or[bone].startJoint + ", end" + (int)or[bone].endJoint);
					
					boneVel[player,bone] = (bonePos[player,bone] - oldpos) / deltaTime;
					boneState[player,bone] = kinect.getSkeleton().SkeletonData[trackedPlayers[player]].eSkeletonPositionTrackingState[bone];
					//print(kinect.getSkeleton().SkeletonData[player].Position.z);
				}
			}
		}
	}
	
}
