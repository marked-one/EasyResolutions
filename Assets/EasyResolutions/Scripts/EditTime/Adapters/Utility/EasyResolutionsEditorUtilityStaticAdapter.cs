// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEditor;
	using UnityEngine;

	/// UnityEditor.EditorUtility static adapter.
	public class EditorUtilityStaticAdapter : IEditorUtility
	{
		/// Marks the specified object dirty.
		public void SetDirty<T>(IObject<T> obj) where T : Object
		{
			EditorUtility.SetDirty(obj.Real);
		}

		/// Displays a cancelable progress bar using the 
		/// specified title, info and progress values.
		/// Returns true if canceled, false otherwise.
		public bool DisplayCancelableProgressBar(string title, string info, float progress)
		{
			return EditorUtility.DisplayCancelableProgressBar(title, info, progress);
		}
		
		/// Displays a progress bar using the specified title, info and progress values.
		public void DisplayProgressBar(string title, string info, float progress)
		{
			EditorUtility.DisplayProgressBar(title, info, progress);
		}
		
		/// Clears the progress bar.
		public void ClearProgressBar()
		{
			EditorUtility.ClearProgressBar();
		}
	}
}
#endif