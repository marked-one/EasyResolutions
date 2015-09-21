// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;
	
	/// UnityEngine.Object editor static adapter.
	public class ObjectEditorStaticAdapter : IObjectEditorStatic
	{
		/// Destroys the specified object immediately.
		public void DestroyImmediate<T>(IObject<T> obj) where T : Object
		{
			Object.DestroyImmediate(obj.Real);
		}
	}
}
#endif
