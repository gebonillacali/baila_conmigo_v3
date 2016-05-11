using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Kinect;
using System;

/// <summary>
/// Reproductor archivo lector.
/// Clase de Modelo que permite Leer un archivo de rutinas o movimientos
/// Genera Rutinas a partir de movimientos
/// Evalua movimientos guardados contra los reproducidos en tiempo real
/// </summary>
public class ReproductorArchivoLector : MonoBehaviour, Kinect.KinectInterface {
	
	private string inputFile = "";
	private float timer = 0;
	private bool isDefault = true;
	private int frameCount = 0;

	/// <summary>
	/// how high (in meters) off the ground is the sensor
	/// </summary>
	public float sensorHeight = 1;
	/// <summary>
	/// where (relative to the ground directly under the sensor) should the kinect register as 0,0,0
	/// </summary>
	public Vector3 kinectCenter = new Vector3(0, 0, 2);
	/// <summary>
	/// what point (relative to kinectCenter) should the sensor look at
	/// </summary>
	public Vector4 lookAt = new Vector4(0, 1, 0, 0);

	public KinectManager kinectManager;

	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	private bool newSkeleton = false;
	public int curFrame = 0;
	private NuiSkeletonFrame[] skeletonFrame;
	public ReproductorNotifier reproductorNotifier;

	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	//private bool updatedColor = false;
	//private bool newColor = false;
	//private Color32[] colorImage;
	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	//private bool updatedDepth = false;
	//private bool newDepth = false;
	//private short[] depthPlayerData;
	
	
	// Use this for initialization
	void Start () {

	}
	
	void Update () {

	}

	public interface ReproductorNotifier {
		void rutinaCargada (int numFrames, List<MovimientoFrameRange> rangoMovimientos);
		List<MovimientoFrameRange> getRangoMovimientos ();
		bool isRutinaGenerada();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		newSkeleton = false;
	}


	public void setInputFile(string inputFile) {
		this.inputFile = inputFile;
		LoadPlaybackFile ();
	}

	public void LoadPlaybackFile()  {

		if (inputFile.Contains (",")) {

			LoadPlaybackMultipleFiles(inputFile.Split(','), 0, new List<NuiSkeletonFrame>(), new List<MovimientoFrameRange>());
			return;
		}

		FileStream input = new FileStream(@inputFile, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter();
		SerialSkeletonFrame[] serialSkeleton = (SerialSkeletonFrame[])bf.Deserialize(input);
		skeletonFrame = new NuiSkeletonFrame[serialSkeleton.Length * SessionJuego.getRepeticionesMovimientos()];
		for (int repeticiones = 0 ; repeticiones < SessionJuego.getRepeticionesMovimientos(); repeticiones++) {
			for(int ii = 0; ii < serialSkeleton.Length; ii++){
				float percent = ((float)ii / (float)skeletonFrame.Length) * 100f;
				skeletonFrame[(repeticiones*serialSkeleton.Length)+ii] = serialSkeleton[ii].deserialize();
			}
		}
		input.Close();
		timer = 0;
		curFrame = 0;
		frameCount = 0;
		List<MovimientoFrameRange> movimientoFrameRanges = new List<MovimientoFrameRange> ();
		MovimientoFrameRange movimientoFrameRange = new MovimientoFrameRange ();
		movimientoFrameRange.InitFrame = 0;
		movimientoFrameRange.EndFrame = skeletonFrame.Length * SessionJuego.getRepeticionesMovimientos();
		movimientoFrameRange.Cod_movimiento = "19";
		movimientoFrameRanges.Add (movimientoFrameRange);
		if (reproductorNotifier != null) {

			reproductorNotifier.rutinaCargada(skeletonFrame.Length, movimientoFrameRanges);
		}

		Debug.Log("Simulating " + @inputFile + " frames loaded " + skeletonFrame.Length);
	}

	public void LoadPlaybackMultipleFiles(string []inputFiles, int i, List<NuiSkeletonFrame> finalSkeletonFrame, List<MovimientoFrameRange> movimientoFrameRanges)  {

		if (i < inputFiles.Length) {
			string inputFileLoad = inputFiles[i];
			FileStream input = new FileStream(@inputFileLoad, FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter();
			SerialSkeletonFrame[] serialSkeleton = (SerialSkeletonFrame[])bf.Deserialize(input);
			MovimientoFrameRange movimientoFrameRange = new MovimientoFrameRange();
			movimientoFrameRange.InitFrame = finalSkeletonFrame.Count;

			for(int ii = 0; ii < serialSkeleton.Length; ii++){
				finalSkeletonFrame.Add(serialSkeleton[ii].deserialize());
			}

			input.Close();

			movimientoFrameRange.EndFrame = movimientoFrameRange.InitFrame + serialSkeleton.Length;
			movimientoFrameRange.Cod_movimiento = "";
			movimientoFrameRanges.Add(movimientoFrameRange);

			Debug.Log("Archivo " + inputFileLoad + " frames loaded " + serialSkeleton.Length);
			LoadPlaybackMultipleFiles(inputFiles, i+1, finalSkeletonFrame, movimientoFrameRanges);

		} else {

			timer = 0;
			curFrame = 0;
			frameCount = 0;
			if (SessionJuego.getRepeticionesMovimientos() > 1) {

				for (int repeticiones = 1 ; repeticiones < SessionJuego.getRepeticionesMovimientos(); repeticiones++) {
					for (int numMovimientos = 0; numMovimientos < inputFiles.Length; numMovimientos++) {
						MovimientoFrameRange movimientoFrameRange = new MovimientoFrameRange();
						movimientoFrameRange.InitFrame = finalSkeletonFrame.Count;
						int initFrame = movimientoFrameRanges[numMovimientos].InitFrame;
						int endFrame = movimientoFrameRanges[numMovimientos].EndFrame;
						for (int iterator=initFrame; iterator < endFrame; iterator++) {
							finalSkeletonFrame.Add(finalSkeletonFrame[iterator]);
						}
						movimientoFrameRange.EndFrame = movimientoFrameRange.InitFrame + movimientoFrameRanges[numMovimientos].EndFrame;
						movimientoFrameRange.Cod_movimiento = "";
						movimientoFrameRanges.Add(movimientoFrameRange);
					}
				}
			}
			skeletonFrame = finalSkeletonFrame.ToArray();
			Debug.Log("Archivos cargados frames loaded " + skeletonFrame.Length);

			if (reproductorNotifier != null) {
				reproductorNotifier.rutinaCargada(skeletonFrame.Length, movimientoFrameRanges);
			}
		}
	}

	public int getNumFrames() {
		return skeletonFrame.Length;
	}
	
	float KinectInterface.getSensorHeight() {
		return sensorHeight;
	}

	Vector3 KinectInterface.getKinectCenter() {
		return kinectCenter;
	}

	Vector4 KinectInterface.getLookAt() {
		return lookAt;
	}

	public bool finalizoReproduccion() {
		//Debug.Log ("finalizoReproduccion:" + (skeletonFrame.Length > 0 && curFrame >= skeletonFrame.Length));
		return skeletonFrame.Length > 0 && curFrame >= skeletonFrame.Length;
	}
	
	/*bool KinectInterface.pollSkeleton() {
		int frame = Mathf.FloorToInt(Time.realtimeSinceStartup / playbackSpeed);
		if(frame > curFrame){
			curFrame = frame;
			Debug.Log("curFrame:" + curFrame);
			newSkeleton = true;
			return newSkeleton;
		}
		return newSkeleton;
	}*/

	bool KinectInterface.pollSkeleton() {
		frameCount++;
		//Debug.Log ("curFrame:" + curFrame + " - frameCount:" + frameCount);

		if(frameCount >= 1){
			frameCount = 0;
			curFrame++;
			newSkeleton = true && curFrame < (skeletonFrame.Length - 1);
			return newSkeleton;
		}
		return newSkeleton;
	}
	
	/*NuiSkeletonFrame KinectInterface.getSkeleton() {
		Debug.Log ("Returning data");
		return skeletonFrame[curFrame % skeletonFrame.Length];
	}*/

	NuiSkeletonFrame KinectInterface.getSkeleton() {
		//Debug.Log ("getSkeleton currentFrame:" + curFrame);
		return skeletonFrame[curFrame];
	}

	public float getDistanceSkeletonPosIndex(NuiSkeletonFrame frame, Kinect.NuiSkeletonPositionIndex joint1,Kinect.NuiSkeletonPositionIndex joint2, int indexTracked) {
		Vector4 joint1Pos = (int)joint1 < frame.SkeletonData[indexTracked].SkeletonPositions.Length ? frame.SkeletonData[indexTracked].SkeletonPositions [(int)joint1] : new Vector4();
		Vector4 joint2Pos = (int)joint2 < frame.SkeletonData[indexTracked].SkeletonPositions.Length ? frame.SkeletonData[indexTracked].SkeletonPositions [(int)joint2] : new Vector4();
		return Vector3.Distance (joint1Pos, joint2Pos);
	}

	public float getFactorConversionPosIndex(Kinect.NuiSkeletonPositionIndex joint1,Kinect.NuiSkeletonPositionIndex joint2, int indexTracked) {
		float distanceReproducion = this.getDistanceSkeletonPosIndex (skeletonFrame[curFrame], joint1, joint2, indexTracked); 
		float distancePlayer = this.getDistanceSkeletonPosIndex (convertSkeletonFrame(kinectManager.skeletonFrame), joint1, joint2, indexTracked); 

		return distancePlayer/distanceReproducion;
	}

	public NuiSkeletonData convertedSkeletonFactor() {
		NuiSkeletonData [] skeletonDataFactored = new NuiSkeletonData[skeletonFrame [curFrame].SkeletonData.Length];
		NuiSkeletonData [] skeletonDataFile = skeletonFrame [curFrame].SkeletonData;
		skeletonDataFile.CopyTo(skeletonDataFactored, 0);
		NuiSkeletonData skeletonDataTracked;
		int indexTracked = -1;

		for (int i = 0; i < skeletonDataFile.Length; i++) {
			if (skeletonDataFile[i].eTrackingState == NuiSkeletonTrackingState.SkeletonTracked) {
				indexTracked = i;
				break;
			}
		}
		/*
		//Moving hand left
		changePositionOfJoint (NuiSkeletonPositionIndex.HandLeft, NuiSkeletonPositionIndex.WristLeft, ref skeletonDataFactored, indexTracked);
		//Moving wristleft
		changePositionOfJoint (NuiSkeletonPositionIndex.WristLeft, NuiSkeletonPositionIndex.ElbowLeft, ref skeletonDataFactored, indexTracked);
		//Moving elboeleft
		changePositionOfJoint (NuiSkeletonPositionIndex.ElbowLeft, NuiSkeletonPositionIndex.ShoulderLeft, ref skeletonDataFactored, indexTracked);
		//Moving shoulderleft
		changePositionOfJoint (NuiSkeletonPositionIndex.ShoulderLeft, NuiSkeletonPositionIndex.ShoulderCenter, ref skeletonDataFactored, indexTracked);
		
		//Moving hand right
		changePositionOfJoint (NuiSkeletonPositionIndex.HandRight, NuiSkeletonPositionIndex.WristRight, ref skeletonDataFactored, indexTracked);
		//Moving wristright
		changePositionOfJoint (NuiSkeletonPositionIndex.WristRight, NuiSkeletonPositionIndex.ElbowRight, ref skeletonDataFactored, indexTracked);
		//Moving elbowright
		changePositionOfJoint (NuiSkeletonPositionIndex.WristRight, NuiSkeletonPositionIndex.ShoulderRight, ref skeletonDataFactored, indexTracked);
		//Moving shoulderright
		changePositionOfJoint (NuiSkeletonPositionIndex.ShoulderRight, NuiSkeletonPositionIndex.ShoulderCenter, ref skeletonDataFactored, indexTracked);
		
		//Moving footleft
		changePositionOfJoint (NuiSkeletonPositionIndex.FootLeft, NuiSkeletonPositionIndex.AnkleLeft, ref skeletonDataFactored, indexTracked);
		//Moving ankleleft
		changePositionOfJoint (NuiSkeletonPositionIndex.AnkleLeft, NuiSkeletonPositionIndex.KneeLeft, ref skeletonDataFactored, indexTracked);
		//Moving kneeleft
		changePositionOfJoint (NuiSkeletonPositionIndex.KneeLeft, NuiSkeletonPositionIndex.HipLeft, ref skeletonDataFactored, indexTracked);
		//Moving hipleft
		changePositionOfJoint (NuiSkeletonPositionIndex.HipLeft, NuiSkeletonPositionIndex.HipCenter, ref skeletonDataFactored, indexTracked);
		
		//Moving footright
		changePositionOfJoint (NuiSkeletonPositionIndex.FootRight, NuiSkeletonPositionIndex.AnkleRight, ref skeletonDataFactored, indexTracked);
		//Moving ankleright
		changePositionOfJoint (NuiSkeletonPositionIndex.AnkleRight, NuiSkeletonPositionIndex.KneeRight, ref skeletonDataFactored, indexTracked);
		//Moving kneeright
		changePositionOfJoint (NuiSkeletonPositionIndex.KneeRight, NuiSkeletonPositionIndex.HipRight, ref skeletonDataFactored, indexTracked);
		//Moving hipright
		changePositionOfJoint (NuiSkeletonPositionIndex.HipRight, NuiSkeletonPositionIndex.HipCenter, ref skeletonDataFactored, indexTracked);
		
		//Moving head
		changePositionOfJoint (NuiSkeletonPositionIndex.Head, NuiSkeletonPositionIndex.ShoulderCenter, ref skeletonDataFactored, indexTracked);
		
		//Moving shouldercenter
		changePositionOfJoint (NuiSkeletonPositionIndex.ShoulderCenter, NuiSkeletonPositionIndex.Spine, ref skeletonDataFactored, indexTracked);
		//Moving hipcenter
		changePositionOfJoint (NuiSkeletonPositionIndex.HipCenter, NuiSkeletonPositionIndex.Spine, ref skeletonDataFactored, indexTracked);
		*/
		return skeletonDataFactored[indexTracked];
	}

	private void changePositionOfJoint(Kinect.NuiSkeletonPositionIndex target, Kinect.NuiSkeletonPositionIndex relation, ref NuiSkeletonData[] skeletonDataFactored, int indexTracked) {
		//Moving hand left
		float factor = getFactorConversionPosIndex (relation, target, indexTracked);
		try {
			if (curFrame < skeletonFrame.Length && ((int) target) < skeletonDataFactored[indexTracked].SkeletonPositions.Length && ((int) relation) < skeletonFrame[curFrame].SkeletonData[indexTracked].SkeletonPositions.Length && skeletonDataFactored[indexTracked].SkeletonPositions [(int)target] != null && skeletonFrame[curFrame].SkeletonData[indexTracked].SkeletonPositions [(int)relation] != null) {
				skeletonDataFactored[indexTracked].SkeletonPositions [(int)target] = factorPosition (skeletonFrame[curFrame].SkeletonData[indexTracked].SkeletonPositions [(int)relation]
				                                                                                     , skeletonFrame[curFrame].SkeletonData[indexTracked].SkeletonPositions [(int)target], factor);
			} 
		} catch (Exception exp) {
			Debug.Log("Error:" + exp);
		}
	}

	private Vector4 factorPosition(Vector4 position1, Vector4 position2, float factor) {
		Vector3 vectorDireccion = position2 - position1;
		vectorDireccion = Vector3.ClampMagnitude (vectorDireccion, vectorDireccion.magnitude * factor);
		return vectorDireccion;
	} 
		                                                                               
	public bool isInsideRegion(Vector3 position1, Vector3 position2, float umbral) {
		return (position1.x - umbral) <= position2.x && position2.x <= (position1.x + umbral) &&
			   (position1.y - umbral) <= position2.y && position2.y <= (position1.y + umbral) &&
				(position1.z - umbral) <= position2.z && position2.z <= (position1.z + umbral);
	}


	//NuiSkeletonPositionIndex.Head, NuiSkeletonPositionIndex.Spine


	/// <summary>
	/// Convierte el SkeletonFrame de <see cref="KinectWrapper.NuiSkeletonFrame"/> a <see cref="Kinect.NuiSkeletonFrame"/>
	/// </summary>
	/// <returns>The skeleton frame.</returns>
	/// <param name="frame">Frame.</param>
	private Kinect.NuiSkeletonFrame convertSkeletonFrame(KinectWrapper.NuiSkeletonFrame frame) {
		Kinect.NuiSkeletonFrame frameConverted;
		frameConverted.dwFlags = frame.dwFlags;
		frameConverted.dwFrameNumber = frame.dwFrameNumber;
		frameConverted.liTimeStamp = frame.liTimeStamp;
		int length = frame.SkeletonData.Length;
		frameConverted.SkeletonData = new Kinect.NuiSkeletonData[length];
		for (int i = 0; i< length; i++) {
			Kinect.NuiSkeletonData data = convertSkeletonData(frame.SkeletonData[i]); 
			frameConverted.SkeletonData[i] = data;
		}
		frameConverted.vFloorClipPlane = frame.vFloorClipPlane;
		frameConverted.vNormalToGravity = frame.vNormalToGravity;
		return frameConverted;
	}
	
	/// <summary>
	/// Convierte el SkeletonData de <see cref="KinectWrapper.NuiSkeletonData"/> a <see cref="Kinect.NuiSkeletonData"/>
	/// </summary>
	/// <returns>The skeleton data.</returns>
	/// <param name="data">Data.</param>
	private Kinect.NuiSkeletonData convertSkeletonData(KinectWrapper.NuiSkeletonData data) {
		Kinect.NuiSkeletonData dataConverted;
		dataConverted.dwEnrollmentIndex_NotUsed = data.dwEnrollmentIndex_NotUsed;
		dataConverted.dwQualityFlags = data.dwQualityFlags;
		dataConverted.dwTrackingID = data.dwTrackingID;
		dataConverted.dwUserIndex = data.dwUserIndex;
		dataConverted.eSkeletonPositionTrackingState = new NuiSkeletonPositionTrackingState[data.eSkeletonPositionTrackingState.Length];
		for (int i = 0; i < data.eSkeletonPositionTrackingState.Length; i++) {
			dataConverted.eSkeletonPositionTrackingState[i] = (NuiSkeletonPositionTrackingState)data.eSkeletonPositionTrackingState[i];	
		}
		dataConverted.eTrackingState = (NuiSkeletonTrackingState)data.eTrackingState;
		dataConverted.Position = data.Position;
		dataConverted.SkeletonPositions = data.SkeletonPositions;
		return dataConverted;
	}

	/*
	NuiSkeletonBoneOrientation[] KinectInterface.getBoneOrientations(NuiSkeletonFrame skeleton){
		return null;
	}
	*/
	NuiSkeletonBoneOrientation[] KinectInterface.getBoneOrientations(NuiSkeletonData skeletonData){
		NuiSkeletonBoneOrientation[] boneOrientations = new NuiSkeletonBoneOrientation[(int)(NuiSkeletonPositionIndex.Count)];
		NativeMethods.NuiSkeletonCalculateBoneOrientations(ref skeletonData, boneOrientations);
		return boneOrientations;
	}
	
	bool KinectInterface.pollColor() {
		return false;
	}
	
	Color32[] KinectInterface.getColor() {
		return null;
	}
	
	bool KinectInterface.pollDepth() {
		return false;
	}
	
	short[] KinectInterface.getDepth() {
		return null;
	}

}
