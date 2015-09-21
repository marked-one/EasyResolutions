// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEditor;
	using System.Collections;
	using System.Collections.Generic;

	/// UnityEditor.EditorBuildSettings static adapter.
	public class EditorBuildSettingsStaticAdapter : IEditorBuildSettings
	{
		/// Get the scenes count.
		public int Count { get { return EditorBuildSettings.scenes.Length; } }

		/// Gets the scenes enumerator.
		IEnumerator IEnumerable.GetEnumerator() 
		{ 
			return this.GetEnumerator(); 
		}
		
		/// Gets the generic scenes enumerator.
		public IEnumerator<IEditorBuildSettingsScene> GetEnumerator()
		{
			foreach(var scene in EditorBuildSettings.scenes)
				yield return new EditorBuildSettingsScene(scene.path, scene.enabled);
		}
	}
}
#endif
