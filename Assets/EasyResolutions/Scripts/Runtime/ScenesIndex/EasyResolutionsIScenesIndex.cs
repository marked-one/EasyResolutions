// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using System.Collections;
	using System.Collections.Generic;
	
	/// Scenes index interface.
	public interface IScenesIndex : IEnumerable, IEnumerable<KeyValuePair<string, Dictionary<string, string>>>
	{
		/// Gets or sets the full scene name by scene name and resolution.
		string this[string name, string resolution] 
		{ 
			//get; 
#if UNITY_EDITOR
			set; 
#endif
		}
			
#if UNITY_EDITOR
		/// Clears all the contents.
		void Clear();
#endif
	}
}
