// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;

	/// UnityEngine.Debug adapter.
	public class DebugAdapter : IDebug
	{
		/// Logs the specified string as info.
		public void Log (string message)
		{ 
			Debug.Log(message); 
		}

		/// Logs the specified string as warning.
		public void LogWarning (string message)
		{
			Debug.LogWarning(message); 
		} 
	}
}
