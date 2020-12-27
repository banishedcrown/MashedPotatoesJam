using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void BanishedCrownStudios()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.banishedcrown.com");
		#endif
	}

	public void Franz()
	{
		#if !UNITY_EDITOR
		openWindow("https://github.com/chavezfk");
		#endif
	}

	public void Isaac()
	{
		#if !UNITY_EDITOR
		openWindow("https://mewbusi.blogspot.com");
		#endif
	}

	public void Michelle()
	{
		#if !UNITY_EDITOR
		openWindow("http://michellelugomusic.com");
		#endif
	}

	public void Dora()
	{
		#if !UNITY_EDITOR
		openWindow("https://dora-vrhoci.medium.com");
		#endif
	}

	public void BlackThorn()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.youtube.com/watch?v=qqOqLNqAdDo");
		#endif
	}

	public void Luka712()
	{
		#if !UNITY_EDITOR
		openWindow("https://luka712.github.io/2018/07/21/CRT-effect-Shadertoy-Unity/");
		#endif
	}

	public void RFH()
	{
	#if !UNITY_EDITOR
		openWindow("https://banishedcrown.itch.io/rfh");
	#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}