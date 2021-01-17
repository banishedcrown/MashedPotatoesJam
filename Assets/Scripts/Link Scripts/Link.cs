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
		if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
			Application.OpenURL("https://www.banishedcrown.com");
		}
	}

	public void Franz()
	{
		#if !UNITY_EDITOR
		openWindow("https://github.com/chavezfk");
		#endif

		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://github.com/chavezfk");
		}
	}

	public void Isaac()
	{
		#if !UNITY_EDITOR
		openWindow("https://mewbusi.blogspot.com");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://mewbusi.blogspot.com");
		}
	}

	public void Michelle()
	{
		#if !UNITY_EDITOR
		openWindow("http://michellelugomusic.com");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("http://michellelugomusic.com");
		}
	}

	public void Dora()
	{
		#if !UNITY_EDITOR
		openWindow("https://dora-vrhoci.medium.com");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://dora-vrhoci.medium.com");
		}
	}

	public void BlackThorn()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.youtube.com/watch?v=qqOqLNqAdDo");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://www.youtube.com/watch?v=qqOqLNqAdDo");
		}
	}

	public void Luka712()
	{
		#if !UNITY_EDITOR
		openWindow("https://luka712.github.io/2018/07/21/CRT-effect-Shadertoy-Unity/");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://luka712.github.io/2018/07/21/CRT-effect-Shadertoy-Unity/");
		}
	}

	public void RFH()
	{
		#if !UNITY_EDITOR
		openWindow("https://banishedcrown.itch.io/rfh");
		#endif
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://banishedcrown.itch.io/rfh");
		}
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}