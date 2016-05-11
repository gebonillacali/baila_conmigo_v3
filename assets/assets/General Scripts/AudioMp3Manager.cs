using UnityEngine;
using System.Collections;
using NAudio;
using NAudio.Wave;
using System.IO;

/// <summary>
/// Audio mp3 manager.
/// Clase que permite la reproduccion de un archivo de audio mp3 en unity 
/// </summary>
public class AudioMp3Manager : MonoBehaviour, testFileBrowser.FileBrowserEvents {

	private static string PROTOCOL_FILE = "file://";

	private IWavePlayer mWaveOutDevice;
	private WaveStream mMainOutputStream;
	private WaveChannel32 mVolumeStream;


	public AudioMp3Listener audioListener;
	public testFileBrowser fileBrowser;
	public bool isPlaying = false;

	void Start () {
		if (fileBrowser != null) {
			fileBrowser.browserEvents = this;
		}
	}

	void Update() {
	}

	void OnDestroy() {
		UnloadAudio ();
	}

	/// <summary>
	/// Audio mp3 listener.
	/// Interface que permite la notificacion de cuando un archivo de audio mp3 ya esta listo para su reproduccion en unity 
	/// </summary>
	public interface AudioMp3Listener {
		void audioLoaded (string fileSelected);
	}

	private bool LoadAudioFromData(byte[] data)
	{
		try
		{
			MemoryStream tmpStr = new MemoryStream(data);
			mMainOutputStream = new Mp3FileReader(tmpStr);
			mVolumeStream = new WaveChannel32(mMainOutputStream);
			
			mWaveOutDevice = new WaveOut();
			mWaveOutDevice.Init(mVolumeStream);
			
			return true;
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning("Error! " + ex.Message);
		}
		
		return false;
	}

	public void loadAudio(string file)
	{


		WWW www = new WWW(PROTOCOL_FILE + file);
		Debug.Log("path = " + PROTOCOL_FILE + file);
		while (!www.isDone) { };
		if (!string.IsNullOrEmpty(www.error))
		{

			return;
		}
		
		byte[] imageData = www.bytes;
		
		if (!LoadAudioFromData(imageData))
		{

			return;
		}
		
		//mWaveOutDevice.Play();
		if (audioListener != null) {
			audioListener.audioLoaded(file);
		}

	}

	public void showFileBrowser() {
		fileBrowser.setShowComponent (true);
	}

	public void fileSelected (string file)
	{
		fileBrowser.setShowComponent (false);
		loadAudio(file);
	}
	
	public void selectionCanceled ()
	{
		//restart fileBrowser
	}

	public void play() {
		isPlaying = true;
		mWaveOutDevice.Play ();

	}

	public void pause() {
		mWaveOutDevice.Pause ();
		isPlaying = false;
	}

	public void stop() {
		mWaveOutDevice.Stop ();
	}



	public void UnloadAudio()
	{
		if (mWaveOutDevice != null)
		{
			mWaveOutDevice.Stop();
		}
		if (mMainOutputStream != null)
		{
			// this one really closes the file and ACM conversion
			mVolumeStream.Close();
			mVolumeStream = null;
			
			// this one does the metering stream
			mMainOutputStream.Close();
			mMainOutputStream = null;
		}
		if (mWaveOutDevice != null)
		{
			mWaveOutDevice.Dispose();
			mWaveOutDevice = null;
		}
	}

}
