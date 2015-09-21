// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using System;
	using System.IO;
	using UnityEngine;

	public class SettingsPath : ISettingsPath
	{
		IScriptableObjectStatic _scriptableObjectStatic;
		IMonoScriptStatic _monoScriptStatic;
		IAssetDatabase _assetDatabase;
		IObjectEditorStatic _objectEditorStatic;
		
		public SettingsPath(IScriptableObjectStatic scriptableObjectStatic, IMonoScriptStatic monoScriptStatic,
		                    IAssetDatabase assetDatabase, IObjectEditorStatic objectEditorStatic)
		{
			if(scriptableObjectStatic == null)
				throw new ArgumentNullException("scriptableObjectStatic");

			if(monoScriptStatic == null)
				throw new ArgumentNullException("monoScriptStatic");

			if(assetDatabase == null)
				throw new ArgumentNullException("assetDatabase");

			if(objectEditorStatic == null)
				throw new ArgumentNullException("objectEditorStatic");

			_scriptableObjectStatic = scriptableObjectStatic;
			_monoScriptStatic = monoScriptStatic;
			_assetDatabase = assetDatabase;
			_objectEditorStatic = objectEditorStatic;
		}
		
		public string Get()
		{
			var anchor = _scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>();
			if(anchor == null)
				return string.Empty;

			var monoScript = _monoScriptStatic.FromScriptableObject(anchor);
			if(monoScript == null)
				return string.Empty;

			var path = _assetDatabase.GetAssetPath(monoScript);
			_objectEditorStatic.DestroyImmediate(anchor);
			if(string.IsNullOrEmpty(path))
				return string.Empty;

			return Path.GetDirectoryName(path);
		}
	}
}
#endif