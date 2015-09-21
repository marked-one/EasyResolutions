// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;
	using UnityEditor;

	/// Editor part of Settings class.
	[CustomEditor(typeof(EasyResolutionsSettings))]
	public class SettingsEditor : Editor 
	{
		/// Updates Inspector GUI.
		public override void OnInspectorGUI() 
		{
			var settings = (EasyResolutionsSettings)target;

			// Logging field.
			settings.Logging = EditorGUILayout.Toggle("Logging", settings.Logging);

			// Changes?
			if (GUI.changed)
				EditorUtility.SetDirty(settings);
		}
	}
}
#endif