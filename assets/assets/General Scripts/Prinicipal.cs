using UnityEngine;
using System.Collections;

public class Prinicipal : MonoBehaviour {
	// Update is called once per frame
	
	private InstruccionVisual menu;
	private Creditos credits;
	public Texture2D vgUBosque;
	public Texture2D vgCorpSindrome;
	public Texture2D vgLogCorpo;
	
	void Start () {
		Color verde = new Color((float)0.59, (float)0.694, (float)0.129, (float)0.352);
		GameObject.Find("rHabitacion").renderer.materials[2].color = verde;
		credits = new Creditos();
	}
	
	void Update(){
		if(!credits.GetFinishSecuence()){
			credits.secuenceCredits(vgUBosque, vgCorpSindrome, vgLogCorpo);
		}else{
			Application.LoadLevel(1);
		} 
	}
}


		
	
	
	

