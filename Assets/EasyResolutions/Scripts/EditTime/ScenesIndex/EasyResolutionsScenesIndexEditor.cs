// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEditor;

	/// Editor part of the ScenesIndex class.
	[CustomEditor(typeof(EasyResolutionsScenesIndex))]
	public class ScenesIndexEditor : Editor 
	{
		string _scenesIndexInfo = string.Empty;

		/// Updates the Inspector GUI.
		public override void OnInspectorGUI() 
		{
			var scenesIndex = (EasyResolutionsScenesIndex)target;
			if(scenesIndex.NeedUpdateInfo)
			{
				_scenesIndexInfo = scenesIndex.ToString();
				scenesIndex.NeedUpdateInfo = false;
			}

			EditorGUILayout.HelpBox(_scenesIndexInfo, MessageType.None);
		}
	}
}
#endif
