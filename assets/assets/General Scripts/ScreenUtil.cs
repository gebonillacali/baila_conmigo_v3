using UnityEngine;
using System.Collections;

/// <summary>
/// Screen util.
/// Class that allows to positioning the items on sceen dynamically.
/// </summary>
public static class ScreenUtil {

	public static Rect getPosElement(Rect rect) {
		float percentWidth = 1366 < Screen.width ? (1366.0f / (float)Screen.width) : ((float)Screen.width / 1366.0f);
		float percentHeight = 768 < Screen.height ? (float)(768.0f / (float)Screen.height) : (float)((float)Screen.height / 768.0f);
		return new Rect (rect.x*percentWidth, rect.y*percentHeight, rect.width*percentWidth, rect.height * percentHeight);
	}
	
	public static int getFontFixedSize(int fontSize) {
		float percentWidth = 1366 < Screen.width ? (1366.0f / (float)Screen.width) : ((float)Screen.width / 1366.0f);
		float percentHeight = 768 < Screen.height ? (float)(768.0f / (float)Screen.height) : (float)((float)Screen.height / 768.0f);
		return (int) (percentWidth < percentHeight ? percentWidth * (float)fontSize : percentHeight * (float)fontSize);
	}

	public static bool onMouseOver(Rect rect) {
		return rect.Contains (new Vector2(MouseControl.mouseXPos, MouseControl.mouseYPos));
	}
}
