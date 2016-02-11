using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Kinect;

/**
 * Clase que permite grabar los movimientos que se realizan en el Kinect
 * 
 * @author gbonilla@unbosque.edu.co - efrancor@unbosque.edu.co
 */
public class GrabadorMovimiento {

	private string path = "Assets/Kinect/Recordings/playback";

	private bool recording = false;
	private ArrayList currentData = new ArrayList();
	private string nameGrabador = "movement";
	private int fileCount;

	// Use this for initialization
	void Start () {
	
	}

	public void StartRecord() {
		recording = true;
		Debug.Log("start recording");
		currentData = new ArrayList();
	}

	public bool isRecording() {
		return recording;
	}

	
	public string record() {
		string recordMessage = "No Data";
		if (KinectWrapper.PollSkeleton (ref KinectManager.instance.smoothParameters, ref KinectManager.instance.skeletonFrame)) {                
			currentData.Add (KinectManager.instance.skeletonFrame.SkeletonData);
			recordMessage = "" + KinectManager.instance.skeletonFrame.SkeletonData.Length;
		}
		return recordMessage;
	}

	
	public void StopRecord() {
		recording = false;

		//edit by lxjk
		string filePath = path + nameGrabador;
		FileStream output = new FileStream(@filePath,FileMode.Create);
		//end lxjk
		BinaryFormatter bf = new BinaryFormatter();
		
		SerialSkeletonFrame[] data = new SerialSkeletonFrame[currentData.Count];
		for(int ii = 0; ii < currentData.Count; ii++){
			data[ii] = new SerialSkeletonFrame((NuiSkeletonFrame)currentData[ii]);
		}
		bf.Serialize(output, data);
		output.Close();
		fileCount++;
		Debug.Log("stop recording");
	}
}
