using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Grabador movimiento. Clase que realiza la grabacion de movimientos.
/// gbonilla@unbosque.edu.co - efrancor@unbosque.edu.co
/// </summary>
using Kinect;


public class GrabadorMovimiento {

	/// <summary>
	/// Interface que establece como esta el status de la grabacion de un archivo.
	/// </summary>
	public interface GrabacionStatus {
		/// <summary>
		/// Metodo que indica cuando se ha completado una grabacion.
		/// </summary>
		/// <param name="pathFile">Ruta del Archivo</param>
		void grabacionCompletada(string pathFile);
	}

	private string path = "Recordings/playback/";
	private static string ext = ".bcm";
	private bool recording = false;
	private bool waitingForRoutineName = false;
	private List<KinectWrapper.NuiSkeletonFrame> currentData = new List<KinectWrapper.NuiSkeletonFrame>();
	private string nameGrabador = "movement";
	private int fileCount;
	private KinectManager kinectManager;
	private GrabacionStatus grabacionStatusObj;
	private bool isConverterRequired = true;

	/// <summary>
	/// Inicializa una nueva instancia de la clase <see cref="GrabadorMovimiento"/>.
	/// </summary>
	/// <param name="kinectManager">Kinect manager.</param>
	/// <param name="grabacionStatus">Grabacion status.</param>
	public GrabadorMovimiento(KinectManager kinectManager, GrabacionStatus grabacionStatus) {
		this.kinectManager = kinectManager;
		this.grabacionStatusObj = grabacionStatus;
	}

	/// <summary>
	/// Inicia la grabacion.
	/// </summary>
	public void StartRecord() {
		waitingForRoutineName = true;
		recording = true;
		Debug.Log("start recording");
		currentData = new List<KinectWrapper.NuiSkeletonFrame>();;
	}

	/// <summary>
	/// Dice si se esta grabando en el momento.
	/// </summary>
	/// <returns><c>true</c>, si se esta grabando, <c>false</c> de lo contrario.</returns>
	public bool isRecording() {
		return recording;
	}

	/// <summary>
	/// Indica si se esta esperando a que se ingrese el nombre de la rutina.
	/// </summary>
	/// <returns><c>true</c>, si se espera por el nombre de la rutina, <c>false</c> de lo contrario.</returns>
	public bool isWaitingForRoutineName() {
		return waitingForRoutineName;
	}

	/// <summary>
	/// Establece el nombre de la rutina a grabarse.
	/// </summary>
	/// <param name="name">Name.</param>
	public void setRoutineName(string name) {
		this.waitingForRoutineName = false;
		this.nameGrabador = name;
	}

	/// <summary>
	/// Graba cada frame de movimiento que se captura con el Kinect
	/// </summary>
	public string record() {
		string recordMessage = "";
		//if (KinectWrapper.PollSkeleton (ref kinectManager.smoothParameters, ref kinectManager.skeletonFrame, false)) {                
		//if (kinectManager.skeletonFrame != null) {                
			currentData.Add (kinectManager.skeletonFrame);
			recordMessage = "Obteniendo... ";
		//}
		return recordMessage;
	}

	/// <summary>
	/// Para la grabacion de la rutina, 
	/// guarda los datos en el archivo 
	/// y notifica la ruta y el nombre del archivo.
	/// </summary>
	public void StopRecord() {
		recording = false;

		string filePath = path + nameGrabador + ext;
		FileStream output = new FileStream(@filePath,FileMode.Create);

		BinaryFormatter bf = new BinaryFormatter();
		if (isConverterRequired) {
			recordWithConversion(output, bf);
		} else {
			recordWithOutConversion(output, bf);
		}

		output.Close();
		grabacionStatusObj.grabacionCompletada (filePath);

		Debug.Log("stop recording");
	}

	private void recordWithOutConversion(FileStream fs, BinaryFormatter bf) {
		KinectWrapper.SerialSkeletonFrame[] data = new KinectWrapper.SerialSkeletonFrame[currentData.Count];
		for(int ii = 0; ii < currentData.Count; ii++){
			data[ii] = new KinectWrapper.SerialSkeletonFrame(currentData[ii]);
		}
		bf.Serialize(fs, data);
	}

	private void recordWithConversion(FileStream fs, BinaryFormatter bf) {
		SerialSkeletonFrame[] data = new SerialSkeletonFrame[currentData.Count];
		for(int ii = 0; ii < currentData.Count; ii++){
			data[ii] = new SerialSkeletonFrame(convertSkeletonFrame(currentData[ii]));
		}
		bf.Serialize(fs, data);
	}


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
}
