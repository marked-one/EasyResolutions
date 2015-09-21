// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;
	using System;
	
	/// Log class. 
	public class Log : ILog
	{
		IDebug _debug;

		/// Constructor. Throws ArgumentNullException if null specified.
		public Log(IDebug debug)
		{
			if(debug == null)
				throw new ArgumentNullException("debug");

			_debug = debug;
		}

		/// Turns logging on/off. Disabled by default.
		public bool Enabled { get; set; }
		
		/// Logs the specified string as info.
		public void Info (string message)
		{
			if (Enabled)
				_debug.Log("Easy Resolutions: " + message);
		}
		
		/// Logs the specified string as warning.
		public void Warning (string message)
		{
			if (Enabled)
				_debug.LogWarning("Easy Resolutions: " + message);
		}
	}
}
