using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Kinect;

/**
 * Clase que permite grabar los movimientos que se realizan en el Kinect
 * 
 * @author gbonilla@unbosque.edu.co - efrancor@unbosque.edu.co
 * @link {http://gamedev.stackexchange.com/questions/21544/how-do-you-save-game-state/27546#27546}
 */
public class GrabadorMovimiento {

	private string path = "Recordings/playback/";
	private static string ext = ".bcm";
	private bool recording = false;
	private List<KinectWrapper.NuiSkeletonFrame> currentData = new List<KinectWrapper.NuiSkeletonFrame>();
	private string nameGrabador = "movement";
	private int fileCount;
	private KinectManager kinectManager;

	public GrabadorMovimiento(KinectManager kinectManager) {
		this.kinectManager = kinectManager;
	}

	public void StartRecord() {
		recording = true;
		Debug.Log("start recording");
		currentData = new List<KinectWrapper.NuiSkeletonFrame>();;
	}

	public bool isRecording() {
		return recording;
	}

	
	public string record() {
		string recordMessage = "";
		if (KinectWrapper.PollSkeleton (ref kinectManager.smoothParameters, ref kinectManager.skeletonFrame)) {                
			currentData.Add (kinectManager.skeletonFrame);
			recordMessage = "Obteniendo... ";
		}
		return recordMessage;
	}

	
	public void StopRecord() {
		recording = false;


		//edit by lxjk
		string filePath = path + nameGrabador + ext;
		FileStream output = new FileStream(@filePath,FileMode.Create);
		//end lxjk
		BinaryFormatter bf = new BinaryFormatter();
		
		SerialSkeletonFrame[] data = new SerialSkeletonFrame[currentData.Count];
		for(int ii = 0; ii < currentData.Count; ii++){
			data[ii] = new SerialSkeletonFrame(convertSkeletonFrame(currentData[ii]));
		}
		bf.Serialize(output, data);
		output.Close();
		fileCount++;

		Debug.Log("stop recording");
	}

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
