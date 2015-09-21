// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using System;
	using UnityEngine;

	/// Scriptable object saver.
	public class ScriptableObjectSaver : IScriptableObjectSaver
	{
		IAssetDatabase _assetDatabase;
		IEditorUtility _editorUtility;

		/// Constructor. Throws ArgumentNullException 
		/// if at least one of its arguments is null.
		public ScriptableObjectSaver(IAssetDatabase assetDatabase, IEditorUtility editorUtility)
		{
			if(assetDatabase == null)
				throw new ArgumentNullException("assetDatabase");

			if(editorUtility == null)
				throw new ArgumentNullException("editorUtility");

			_assetDatabase = assetDatabase;
			_editorUtility = editorUtility;
		}

		/// Saves the specified scriptable object. Throws 
		/// ArgumentNullException if the specified value is null.
		public void Save<T>(IScriptableObject<T> scriptableObject) where T : ScriptableObject, IScriptableObject<T> 
		{
			if(scriptableObject == null)
				throw new ArgumentNullException("scriptableObject");

			_editorUtility.SetDirty(scriptableObject);
			_assetDatabase.SaveAssets();
			_assetDatabase.Refresh();
		}
	}
}
#endif