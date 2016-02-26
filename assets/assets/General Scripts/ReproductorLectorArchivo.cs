using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Kinect;


public class ReproductorArchivoLector : MonoBehaviour, Kinect.KinectInterface {
	
	private string inputFile = "";
	private float playbackSpeed = 0.0333f;
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
	
	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	private bool newSkeleton = false;
	private int curFrame = 0;
	private NuiSkeletonFrame[] skeletonFrame;
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
		//LoadPlaybackFile(inputFile);
	}
	
	void Update () {

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
		FileStream input = new FileStream(@inputFile, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter();
		SerialSkeletonFrame[] serialSkeleton = (SerialSkeletonFrame[])bf.Deserialize(input);
		skeletonFrame = new NuiSkeletonFrame[serialSkeleton.Length];
		for(int ii = 0; ii < serialSkeleton.Length; ii++){
			skeletonFrame[ii] = serialSkeleton[ii].deserialize();
		}
		input.Close();
		timer = 0;
		curFrame = 0;
		frameCount = 0;
		Debug.Log("Simulating "+@inputFile + " frames loaded " + skeletonFrame.Length);
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
		Debug.Log ("finalizoReproduccion:" + (skeletonFrame.Length > 0 && curFrame >= skeletonFrame.Length));
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
		Debug.Log ("curFrame:" + curFrame + " - frameCount:" + frameCount);

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
