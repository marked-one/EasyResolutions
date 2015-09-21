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

	/// Scriptable object loader.
	public class ScriptableObjectLoader : IScriptableObjectLoader
	{
		IAssetDatabase _assetDatabase;
		IScriptableObjectStatic _scriptableObjectStatic;
		ISettingsPath _settingsPath;

		/// Constructor. Throws ArgumentNullException 
		/// if at least one of the arguments is null.
		public ScriptableObjectLoader(IAssetDatabase assetDatabase, IScriptableObjectStatic scriptableObjectStatic, ISettingsPath settingsPath)
		{
			if(assetDatabase == null)
				throw new ArgumentNullException("assetDatabase");

			if(scriptableObjectStatic == null)
				throw new ArgumentNullException("scriptableObjectStatic");

			if(settingsPath == null)
				throw new ArgumentNullException("settingsPath");

			_assetDatabase = assetDatabase;
			_scriptableObjectStatic = scriptableObjectStatic;
			_settingsPath = settingsPath;
		}

		/// Load the asset using the specified name and type.
		/// Throws ArgumentNullException if assetName is null and
		/// ArgumentException if assetName contains invalid chars.
		/// Returns null when fails to load the ScriptableObject asset.
		public IScriptableObject<T> Load<T>(string assetName) where T : ScriptableObject, IScriptableObject<T> 
		{
			var path = Path.Combine(_settingsPath.Get(), assetName);
			var scriptableObject = _assetDatabase.LoadScriptableObjectAtPath<T>(path);
			if(scriptableObject !=  null)
				return scriptableObject;

			scriptableObject = _scriptableObjectStatic.CreateInstance<T>();
			if(scriptableObject ==  null)
				return null;

			_assetDatabase.CreateAsset (scriptableObject, path);
			_assetDatabase.SaveAssets();
			_assetDatabase.Refresh();
			return scriptableObject;
		}
	}
}
#endif