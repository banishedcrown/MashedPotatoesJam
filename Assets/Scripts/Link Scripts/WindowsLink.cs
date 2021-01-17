using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsLink : MonoBehaviour
{
	public void BanishedCrownStudios()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://www.banishedcrown.com");
		}
	}

	public void Franz()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://github.com/chavezfk");
		}
	}

	public void Isaac()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://mewbusi.blogspot.com");
		}
	}

	public void Michelle()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("http://michellelugomusic.com");
		}
	}

	public void Dora()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://dora-vrhoci.medium.com");
		}
	}

	public void BlackThorn()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://www.youtube.com/watch?v=qqOqLNqAdDo");
		}
	}

	public void Luka712()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://luka712.github.io/2018/07/21/CRT-effect-Shadertoy-Unity/");
		}
	}

	public void RFH()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://banishedcrown.itch.io/rfh");
		}
	}
}
