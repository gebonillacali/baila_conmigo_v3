using UnityEngine;
using System.Collections;

/// <summary>
/// Frame rate manager Class that handles the FrameRate conversion accross computers.
/// </summary>
public class FrameRateManager {
	private static float DESIRED_FRAM_RATE = 70F;

	public float convertFrameRate(float currentFrameRate) {
		float factor = DESIRED_FRAM_RATE / currentFrameRate;
		return factor;
	}
}
