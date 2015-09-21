// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;
	
	/// UnityEngine.Screen static adapter.
	public class ScreenStaticAdapter : IScreen
	{
		/// Returns the screen width.
		public int Width { get { return Screen.width; } }
		
		/// Returns the screen height.
		public int Height { get { return Screen.height; } }
	}
}
