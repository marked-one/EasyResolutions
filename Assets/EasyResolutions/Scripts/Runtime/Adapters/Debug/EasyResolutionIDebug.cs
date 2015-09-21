// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	/// Debug interface.
	public interface IDebug
	{
		/// Logs the specified string as info.
		void Log (string message);
		
		/// Logs the specified string as warning.
		void LogWarning (string message);
	}
}
