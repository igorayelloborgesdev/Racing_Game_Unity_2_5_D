using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {
	/// <summary>
	/// Limits the position to camera.
	/// </summary>
	/// <returns>The position to camera.</returns>
	public static Vector3 LimitPositionToCamera(Vector3 positionObj)
	{
		Vector3 pos = Camera.main.WorldToViewportPoint (positionObj);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01 (pos.y);
		return Camera.main.ViewportToWorldPoint(pos);
	}
	/// <summary>
	/// Gets the random.
	/// </summary>
	/// <returns>The random.</returns>
	/// <param name="maxNumber">Max number.</param>
	public static int GetRandom(int maxNumber)
	{
		System.Random rnd = new System.Random();
		return rnd.Next(maxNumber);
	}
	public static float GetRandom(float minNumber, float maxNumber)
	{
		System.Random rnd = new System.Random();
		return (float)((rnd.NextDouble() * maxNumber) + minNumber);
	}
	/// <summary>
	/// Pauses the game.
	/// </summary>
	public static void PauseGame()
	{
		Time.timeScale = 0;
	} 
	/// <summary>
	/// Continues the game.
	/// </summary>
	public static void ContinueGame()
	{
		Time.timeScale = 1;
	}
	/// <summary>
	/// Gets the rule3 percent.
	/// </summary>
	/// <returns>The rule3 percent.</returns>
	/// <param name="val">Value.</param>
	/// <param name="oneHmax">One hmax.</param>
	public static float GetRule3Percent(float val, float oneHmax, float onePerc)
	{
		return (val * onePerc)/ oneHmax;
	}
}