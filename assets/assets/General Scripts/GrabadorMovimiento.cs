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
public class GrabadorMovimiento : MonoBehaviour {

	private string path = "Assets/Kinect/Recordings/playback";

	private bool isRecording = false;
	private ArrayList currentData = new ArrayList();
	private string nameGrabador = "movement";
	private int fileCount;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(!isRecording){
			if(Input.GetKeyDown(KeyCode.F10)){
				StartRecord();
			}
		} else {
			if(Input.GetKeyDown(KeyCode.F10)){
				StopRecord();
			}
			if (KinectWrapper.PollSkeleton(ref KinectManager.instance.smoothParameters, ref KinectManager.instance.skeletonFrame)){
				currentData.Add(KinectManager.instance.skeletonFrame);
			}
		}
	}

	void StartRecord() {
		isRecording = true;
		Debug.Log("start recording");
	}
	
	void StopRecord() {
		isRecording = false;
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
