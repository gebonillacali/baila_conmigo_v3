using UnityEngine;
using System.Collections;

public class testFileBrowser : MonoBehaviour {
	//skins and textures
	public GUISkin[] skins;
	public Texture2D file,folder,back,drive;
	
	string[] layoutTypes = {"Type 0","Type 1"};
	//initialize file browser
	FileBrowser fb = new FileBrowser();
	string output = "no file";

	private bool showComponent;
	public FileBrowserEvents browserEvents;

	public interface FileBrowserEvents {
		void fileSelected (string file);
		void selectionCanceled ();
	}

	// Use this for initialization
	void Start () {
		showComponent = false;
		//setup file browser style
		//fb.guiSkin = skins[0]; //set the starting skin
		//set the various textures
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;
		//show the search bar
		fb.showSearch = true;
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = true;
		fb.setDirectory ("c:/");
	}
	
	void OnGUI(){
		if (showComponent) {
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			fb.setLayout(0);
			GUILayout.Space(10);

			foreach(GUISkin s in skins){
				if(GUILayout.Button(s.name)){
					fb.guiSkin = s;
				}
			}
			GUILayout.Space(10);
			fb.showSearch = true;
			fb.searchRecursively = true; 
			GUILayout.EndVertical();
			GUILayout.Space(10);
			GUILayout.Label("Selected File: "+output);
			GUILayout.EndHorizontal();
			//draw and display output
			if(fb.draw()){ //true is returned when a file has been selected
				//the output file is a member if the FileInfo class, if cancel was selected the value is null
				output = (fb.outputFile==null)?"cancel hit":fb.outputFile.ToString();
				if (browserEvents != null) {
					if (output == "cancel hit") {
						browserEvents.selectionCanceled();
					} else {
						setShowComponent(false);
						browserEvents.fileSelected(output);
					}
				}
			}
		}
	}

	public void setShowComponent(bool state) {
		this.showComponent = state;
	}
}
