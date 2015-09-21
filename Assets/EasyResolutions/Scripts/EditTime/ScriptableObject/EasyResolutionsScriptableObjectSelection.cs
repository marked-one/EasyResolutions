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

	public class ScriptableObjectSelection : IScriptableObjectSelection
	{
		IScriptableObjectLoader _scriptableObjectLoader;
		ISelection _selection;
		
		public ScriptableObjectSelection(IScriptableObjectLoader scriptableObjectLoader, ISelection selection)
		{
			if(scriptableObjectLoader == null)
				throw new ArgumentNullException("scriptableObjectLoader");

			if(selection == null)
				throw new ArgumentNullException("selection");

			_scriptableObjectLoader = scriptableObjectLoader;
			_selection = selection;
		}
		
		public void Select<T>(string assetName) where T : ScriptableObject, IScriptableObject<T>
		{
			if(string.IsNullOrEmpty(assetName))
				throw new ArgumentException("Asset shouldn't be null or empty.", "assetName");

			var scriptableObject = _scriptableObjectLoader.Load<T>(assetName);
			if(scriptableObject != null)
				_selection.SetActiveObject(scriptableObject);
		}

		public void Select<T>(IScriptableObject<T> scriptableObject) where T : ScriptableObject, IScriptableObject<T>
		{
			if(scriptableObject == null)
				throw new ArgumentNullException("scriptableObject");

			_selection.SetActiveObject(scriptableObject);
		}
	}
}
#endif