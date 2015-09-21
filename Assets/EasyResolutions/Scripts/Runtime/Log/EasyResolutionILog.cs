// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	/// Log interface.
	public interface ILog
	{
		/// Turns logging on/off. 
		bool Enabled { get; set; }

		/// Logs the specified string as info.
		void Info (string message);
		
		/// Logs the specified string as warning.
		void Warning (string message);
	}
}
