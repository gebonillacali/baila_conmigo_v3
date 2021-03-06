
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Reproductor movimiento.
/// Modelo que permite la reproduccion de Rutinas
/// </summary>
public class ReproductorMovimiento : MonoBehaviour {

	/// <summary>
	/// Atributos privados logica clase.
	/// </summary>
	private string pathFile = "";
	private bool playing = false;
	private int stateGame = 0;

	/// <summary>
	/// Atributos publicos de configuracion para persistencia.
	/// </summary>
	public string urlIniciarJuego="openclassmedia.org/bailaconmigo/crearSesionJuego.php";
	public string urlGrabarPuntaje="openclassmedia.org/bailaconmigo/puntaje_movimiento.php";
	public string urlGrabarHistorico="openclassmedia.org/bailaconmigo/general.php";

	//Assignments for a bitmask to control which bones to look at and which to ignore
	public enum BoneMask
	{
		None = 0x0,
		//EMPTY = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hips = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		//EMPTY = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xEFFFE,
		Torso = 0x1000000 | Spine | Shoulder_Center | Head, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000000 | Shoulder_Left | Elbow_Left | Wrist_Left | Hand_Left,
		Right_Arm = 0x1000000 |  Shoulder_Right | Elbow_Right | Wrist_Right | Hand_Right,
		Left_Leg = 0x1000000 | Hips | Knee_Left | Ankle_Left | Foot_Left,
		Right_Leg = 0x1000000 | Hips | Knee_Right | Ankle_Right | Foot_Right,
		R_Arm_Chest = Right_Arm | Spine,
		No_Feet = All & ~(Foot_Left | Foot_Right),
		Upper_Body = Head |Elbow_Left | Wrist_Left | Hand_Left | Elbow_Right | Wrist_Right | Hand_Right
	}
	
	public SkeletonWrapperPlayer sw;
	
	public Transform Hip_Center;
	public Transform Spine;
	public Transform Shoulder_Center;
	public Transform Head;
	public Transform Collar_Left;
	public Transform Shoulder_Left;
	public Transform Elbow_Left;
	public Transform Wrist_Left;
	public Transform Hand_Left;
	public Transform Fingers_Left; //unused
	public Transform Collar_Right;
	public Transform Shoulder_Right;
	public Transform Elbow_Right;
	public Transform Wrist_Right;
	public Transform Hand_Right;
	public Transform Fingers_Right; //unused
	public Transform Hip_Override;
	public Transform Hip_Left;
	public Transform Knee_Left;
	public Transform Ankle_Left;
	public Transform Foot_Left;
	public Transform Hip_Right;
	public Transform Knee_Right;
	public Transform Ankle_Right;
	public Transform Foot_Right;
	
	public int player;
	public BoneMask Mask = BoneMask.All;
	public bool animated;
	public float blendWeight = 1;
	
	private Transform[] _bones; //internal handle for the bones of the model
	private uint _nullMask = 0x0;
	
	private Quaternion[] _baseRotation; //starting orientation of the joints
	private Vector3[] _boneDir; //in the bone's local space, the direction of the bones
	private Vector3[] _boneUp; //in the bone's local space, the up vector of the bone
	private Vector3 _hipRight; //right vector of the hips
	private Vector3 _chestRight; //right vectory of the chest

	public ReproductorArchivoLector.ReproductorNotifier reproductorNotifier;
	public ReproduccionActions actions;

	public interface ReproduccionActions {
		void juegoTerminado ();
	}

	public void setPathFile(string pathFile) {
		if (!playing) {
			playing = true;
			sw.reproductorNotifier = this.reproductorNotifier;

			iniciarSesionJuego ();
			this.pathFile = pathFile;
			sw.setPathFile (pathFile);
			stateGame = 1;
		}
	}

	public bool isAllowedToPlay() {
		return sw.allowToPlay;
	}

	public void setAllowToPlay(bool allowToPlay) {
		sw.allowToPlay = allowToPlay;
	}

	public bool isPlaying() {
		return playing;
	}
	
	// Use this for initialization
	void Start () {
		setVisibility(false, gameObject);
		sw.reproductorNotifier = this.reproductorNotifier;

		stateGame = 0;

		//store bones in a list for easier access, everything except Hip_Center will be one
		//higher than the corresponding Kinect.NuiSkeletonPositionIndex (because of the hip_override)
		_bones = new Transform[(int)Kinect.NuiSkeletonPositionIndex.Count + 5] {
			null, Hip_Center, Spine, Shoulder_Center,
			Collar_Left, Shoulder_Left, Elbow_Left, Wrist_Left,
			Collar_Right, Shoulder_Right, Elbow_Right, Wrist_Right,
			Hip_Override, Hip_Left, Knee_Left, Ankle_Left,
			null, Hip_Right, Knee_Right, Ankle_Right,
			//extra joints to determine the direction of some bones
			Head, Hand_Left, Hand_Right, Foot_Left, Foot_Right};
		
		//determine which bones are not available
		for(int ii = 0; ii < _bones.Length; ii++)
		{
			if(_bones[ii] == null)
			{
				_nullMask |= (uint)(1 << ii);
			}
		}
		
		//store the base rotations and bone directions (in bone-local space)
		_baseRotation = new Quaternion[(int)Kinect.NuiSkeletonPositionIndex.Count];
		_boneDir = new Vector3[(int)Kinect.NuiSkeletonPositionIndex.Count];
		
		//first save the special rotations for the hip and spine
		_hipRight = Hip_Right.transform.position - Hip_Left.transform.position;
		_hipRight = Hip_Override.transform.InverseTransformDirection(_hipRight);
		
		_chestRight = Shoulder_Right.transform.position - Shoulder_Left.transform.position;
		_chestRight = Spine.transform.InverseTransformDirection(_chestRight);
		
		//get direction of all other bones
		for( int ii = 0; ii < (int)Kinect.NuiSkeletonPositionIndex.Count; ii++)
		{
			if((_nullMask & (uint)(1 << ii)) <= 0)
			{
				//save initial rotation
				_baseRotation[ii] = _bones[ii].transform.localRotation;
				
				//if the bone is the end of a limb, get direction from this bone to one of the extras (hand or foot).
				if(ii % 4 == 3 && ((_nullMask & (uint)(1 << (ii/4) + (int)Kinect.NuiSkeletonPositionIndex.Count)) <= 0))
				{
					_boneDir[ii] = _bones[(ii/4) + (int)Kinect.NuiSkeletonPositionIndex.Count].transform.position - _bones[ii].transform.position;
				}
				//if the bone is the hip_override (at boneindex Hip_Left, get direction from average of left and right hips
				else if(ii == (int)Kinect.NuiSkeletonPositionIndex.HipLeft && Hip_Left != null && Hip_Right != null)
				{
					_boneDir[ii] = ((Hip_Right.transform.position + Hip_Left.transform.position) / 2F) - Hip_Override.transform.position;
				}
				//otherwise, get the vector from this bone to the next.
				else if((_nullMask & (uint)(1 << ii+1)) <= 0)
				{
					_boneDir[ii] = _bones[ii+1].transform.position - _bones[ii].transform.position;
				}
				else
				{
					continue;
				}
				//Since the spine of the kinect data is ~40 degrees back from the hip,
				//check what angle the spine is at and rotate the saved direction back to match the data
				if(ii == (int)Kinect.NuiSkeletonPositionIndex.Spine)
				{
					float angle = Vector3.Angle(transform.up,_boneDir[ii]);
					_boneDir[ii] = Quaternion.AngleAxis(-40 + angle,transform.right) * _boneDir[ii];
				}
				//transform the direction into local space.
				_boneDir[ii] = _bones[ii].transform.InverseTransformDirection(_boneDir[ii]);
			}
		}
		//make _chestRight orthogonal to the direction of the spine.
		_chestRight -= Vector3.Project(_chestRight, _boneDir[(int)Kinect.NuiSkeletonPositionIndex.Spine]);
		//make _hipRight orthogonal to the direction of the hip override
		Vector3.OrthoNormalize(ref _boneDir[(int)Kinect.NuiSkeletonPositionIndex.HipLeft],ref _hipRight);
	}

	private bool isVisibleAvatar = false;
	
	void Update () {
		//update the data from the kinect if necessary
		if (!sw.finalizoReproduccion()) {
			if (sw.allowToPlay && !isVisibleAvatar) {
				setVisibility(true, gameObject);
				isVisibleAvatar = true;
			}
			//setVisibility(true, sw.kinectManager.Player1Avatars[0]);

			if(sw.pollSkeleton()){
				for( int ii = 0; ii < (int)Kinect.NuiSkeletonPositionIndex.Count; ii++)
				{

					if( ((uint)Mask & (uint)(1 << ii) ) > 0 && (_nullMask & (uint)(1 << ii)) <= 0 )
					{
						RotateJoint(ii);
					}
				}
			}
		} else {
			playing = false;
			if (stateGame == 1) {
				stateGame = 0;
				if (!SessionJuego.isRutinaGenerada()) {
					uploadPuntajeJugador();
				} else {
					uploadPuntajesMovimientosJugador();
				}
			}
			if (isVisibleAvatar) {
				setVisibility(false, gameObject);
				isVisibleAvatar = false;
			}
			//setVisibility(false, sw.kinectManager.Player1Avatars[0]);

		}

	}

	private void iniciarSesionJuego() {
		Debug.Log ("SessionJuego.getCodigoJugador():" + SessionJuego.getCodigoJugador ());
		Debug.Log ("SessionJuego.getCodigoJugador():" + SessionJuego.getCodigoRutina());
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Codigo",SessionJuego.getCodigoJugador());
		form.AddField ("Cancion", "cancion 2");
		form.AddField ("Cod_rutina", SessionJuego.getCodigoRutina().ToString());
		WWW www = new WWW(urlIniciarJuego,form);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW hs_get){
		yield return hs_get;
		string codigoSesion = hs_get.text != null ? hs_get.text : "0";
		SessionJuego.setCodigoSesion (codigoSesion);
		if(hs_get.error != null){
			Debug.Log("ERROR: "+hs_get.error);
		}
		SessionJuego.setPuntajeJugador (0);
		SessionJuego.setAcumuladoJugador(0f);
	}

	private void uploadPuntajeJugador() {

		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Puntaje",SessionJuego.getPuntajeJugador().ToString());
		form.AddField ("Movimiento","19");
		form.AddField ("Cod_sesion",SessionJuego.getCodigoSesion());
		WWW www = new WWW(urlGrabarPuntaje,form);
		StartCoroutine(WaitForRequestCommon(www));
	}

	int indexUploadPuntajesMov = 0;
	List<MovimientoFrameRange> movimientoFrameRanges = new List<MovimientoFrameRange>();

	private void prepareMovimientosForUpload() {
		for (int i=0; i < SessionJuego.getMovimientoFrameRange().Count; i++) {
			MovimientoFrameRange movIncoming = SessionJuego.getMovimientoFrameRange()[i];
			if (movIncoming.Cod_movimiento.Length > 0) {
				if (movimientoFrameRanges.Count > 0) {
					int index = MovimientoFrameRange.getIndexByCodMovimiento(movimientoFrameRanges, movIncoming.Cod_movimiento);
					if (index >= 0) {
						movimientoFrameRanges[index].PuntajeMovimiento += movIncoming.PuntajeMovimiento;
					} else {
						movimientoFrameRanges.Add(movIncoming);
					}
				} else {
					movimientoFrameRanges.Add(movIncoming);
				}
			}
		}
	}

	private void uploadPuntajesMovimientosJugador() {

		prepareMovimientosForUpload ();

		if (indexUploadPuntajesMov < movimientoFrameRanges.Count) {
			MovimientoFrameRange movimientoFrameRange = movimientoFrameRanges[indexUploadPuntajesMov];
			var form= new WWWForm(); //here you create a new form connection
			Debug.Log("Subiendo info de index " + indexUploadPuntajesMov + " cod:" + movimientoFrameRange.Cod_movimiento + " " + movimientoFrameRange.PuntajeMovimiento.ToString() + " Pts Session:" + SessionJuego.getCodigoSesion());
			if (movimientoFrameRange.Cod_movimiento.Length <= 0) {
				indexUploadPuntajesMov++;
				uploadPuntajesMovimientosJugador();
				return;
			}
			form.AddField( "Puntaje",movimientoFrameRange.PuntajeMovimiento.ToString());
			form.AddField ("Movimiento",movimientoFrameRange.Cod_movimiento);
			form.AddField ("Cod_sesion",SessionJuego.getCodigoSesion());
			WWW www = new WWW(urlGrabarPuntaje,form);
			StartCoroutine(WaitForRequestCommonMultiple(www));
		} else {
			indexUploadPuntajesMov = 0;
			uploadHistorico();
		}
	}

	private void uploadHistorico() {
		var form= new WWWForm(); //here you create a new form connection
		form.AddField( "Puntaje",SessionJuego.getAcumuladoJugador().ToString());
		form.AddField ("Cod_nino",SessionJuego.getCodigoJugador());
		form.AddField ("Cod_sesion",SessionJuego.getCodigoSesion());
		WWW www = new WWW(urlGrabarHistorico,form);
		StartCoroutine(WaitForRequestCommon(www));
	}

	IEnumerator WaitForRequestCommon(WWW hs_get){
		yield return hs_get;
		if (hs_get.url ==  urlGrabarPuntaje) {
			uploadHistorico();
		}

		if (hs_get.url == urlGrabarHistorico) {
			actions.juegoTerminado();
		}

	}

	IEnumerator WaitForRequestCommonMultiple(WWW hs_get){
		yield return hs_get;
		Debug.Log ("info subida a " + hs_get.url + " insertado " + hs_get.text + " filas");
		indexUploadPuntajesMov++;
		uploadPuntajesMovimientosJugador ();
	}

	public void setVisibility(bool visible, GameObject gameObject) {
		// toggles the visibility of this gameobject and all it's children
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			r.enabled = visible;
		}
	}
	
	void RotateJoint(int bone) {
		//if blendWeight is 0 there is no need to compute the rotations
		if( blendWeight <= 0 ){ return; }
		Vector3 upDir = new Vector3();
		Vector3 rightDir = new Vector3();
		
		if(bone == (int)Kinect.NuiSkeletonPositionIndex.Spine )
		{
			upDir = ((Hip_Left.transform.position + Hip_Right.transform.position) / 2F) - Hip_Override.transform.position;
			rightDir = Hip_Right.transform.position - Hip_Left.transform.position;
		}
		
		
		//if the model is not animated, reset rotations to fix twisted joints
		if(!animated){_bones[bone].transform.localRotation = _baseRotation[bone];}
		//if the required bone data from the kinect isn't available, return
		if( sw.boneState[player,bone] == Kinect.NuiSkeletonPositionTrackingState.NotTracked)
		{
			return;
		}
		
		//get the target direction of the bone in world space
		//for the majority of bone it's bone - 1 to bone, but Hip_Override and the outside
		//shoulders are determined differently.
		
		Vector3 dir = _boneDir[bone];
		Vector3 target;
		
		//if bone % 4 == 0 then it is either an outside shoulder or the hip override
		if(bone % 4 == 0)
		{
			//hip override is at Hip_Left
			if(bone == (int)Kinect.NuiSkeletonPositionIndex.HipLeft)
			{
				//target = vector from hip_center to average of hips left and right
				target = ((sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.HipLeft] + sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.HipRight]) / 2F) - sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.HipCenter];
			}
			//otherwise it is one of the shoulders
			else
			{
				//target = vector from shoulder_center to bone
				target = sw.bonePos[player,bone] - sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.ShoulderCenter];
			}
		}
		else
		{
			//target = vector from previous bone to bone
			target = sw.bonePos[player,bone] - sw.bonePos[player,bone-1];
		}
		//transform it into bone-local space (independant of the transform of the controller)
		target = transform.TransformDirection(target);
		target = _bones[bone].transform.InverseTransformDirection(target);
		//create a rotation that rotates dir into target
		Quaternion quat = Quaternion.FromToRotation(dir,target);
		//if bone is the spine, add in the rotation along the spine
		
		if(bone == (int)Kinect.NuiSkeletonPositionIndex.Spine)
		{
			//rotate the chest so that it faces forward (determined by the shoulders)
			dir = _chestRight;
			target = sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.ShoulderRight] - sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.ShoulderLeft];
			
			target = transform.TransformDirection(target);
			target = _bones[bone].transform.InverseTransformDirection(target);
			target -= Vector3.Project(target,_boneDir[bone]);
			
			quat *= Quaternion.Lerp(quat, Quaternion.FromToRotation(dir,target), 200);
			
		}
		
		//if bone is the hip override, add in the rotation along the hips
		else if(bone == (int)Kinect.NuiSkeletonPositionIndex.HipLeft)
		{
			//rotate the hips so they face forward (determined by the hips)
			dir = _hipRight;
			target = sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.HipRight] - sw.bonePos[player,(int)Kinect.NuiSkeletonPositionIndex.HipLeft];
			
			target = transform.TransformDirection(target);
			target = _bones[bone].transform.InverseTransformDirection(target);
			target -= Vector3.Project(target,_boneDir[bone]);
			
			//quat *= Quaternion.FromToRotation(dir,target);
			quat *= Quaternion.Lerp(quat, Quaternion.FromToRotation(dir,target), 200);
		}
		
		//reduce the effect of the rotation using the blend parameter
		quat = Quaternion.Lerp(Quaternion.identity, quat, blendWeight);
		//apply the rotation to the local rotation of the bone
		_bones[bone].localRotation = _bones[bone].localRotation  * quat;
		
		if(bone == (int)Kinect.NuiSkeletonPositionIndex.Spine)
		{
			restoreBone(_bones[(int)Kinect.NuiSkeletonPositionIndex.HipLeft],_boneDir[(int)Kinect.NuiSkeletonPositionIndex.HipLeft],upDir);
			restoreBone(_bones[(int)Kinect.NuiSkeletonPositionIndex.HipLeft],_hipRight,rightDir);
		}
		
		return;
	}
	
	void restoreBone(Transform bone,Vector3 dir, Vector3 target)
	{
		//transform target into bone-local space (independant of the transform of the controller)
		//target = transform.TransformDirection(target);
		target = bone.transform.InverseTransformDirection(target);
		//create a rotation that rotates dir into target
		Quaternion quat = Quaternion.FromToRotation(dir,target);
		bone.transform.localRotation *= quat;
	}
}


