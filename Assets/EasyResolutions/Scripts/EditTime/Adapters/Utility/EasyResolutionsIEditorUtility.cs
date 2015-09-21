// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;

	/// Editor utility interface.
	public interface IEditorUtility
	{
		/// Marks the specified object dirty.
		void SetDirty<T>(IObject<T> obj) where T : Object;

		/// Displays a cancelable progress bar using the 
		/// specified title, info and progress values.
		/// Returns true if canceled, false otherwise.
		bool DisplayCancelableProgressBar(string title, string info, float progress);
		
		/// Displays a progress bar using the specified title, info and progress values.
		void DisplayProgressBar(string title, string info, float progress);
		
		/// Clears the progress bar.
		void ClearProgressBar();
	}
}
#endif