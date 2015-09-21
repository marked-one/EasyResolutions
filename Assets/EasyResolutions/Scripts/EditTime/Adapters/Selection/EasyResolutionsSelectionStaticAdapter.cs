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

	/// UnityEditor.Selection static adapter.
	public class SelectionStaticAdapter : ISelection
	{
		/// Activates the selection for the specified object.
		public void SetActiveObject<T> (IObject<T> obj) where T : Object
		{
			Selection.activeObject = obj.Real;
		}
	}
}
#endif