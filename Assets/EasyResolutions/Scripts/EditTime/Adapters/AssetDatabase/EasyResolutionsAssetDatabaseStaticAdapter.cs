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
	
	/// UnityEditor.AssetDatabase static adapter. 
	/// Partial class, could be extended by the user.
	public partial class AssetDatabaseStaticAdapter : IAssetDatabase
	{
		/// Gets the path of the asset of the specified object. 
		/// Returned path starts with the Assets folder.
		public string GetAssetPath<T>(IObject<T> obj) where T : Object
		{
			return AssetDatabase.GetAssetPath(obj.Real);
		}

		/// Creates the asset of the specified object at the specified path.
		/// The specified path should start with the Assets folder.
		public void CreateAsset<T>(IObject<T> obj, string path) where T : Object
		{
			AssetDatabase.CreateAsset(obj.Real, path);
		}

		/// Loads the scriptable object at the specified path.
		/// The specified path should start with the Assets folder.
		public IScriptableObject<T> LoadScriptableObjectAtPath<T>(string path) where T : ScriptableObject, IScriptableObject<T>
		{
			return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as IScriptableObject<T>;
		}

		/// Saves unsaved assets.
		public void SaveAssets()
		{
			AssetDatabase.SaveAssets();
		}

		/// Refreshes the asset database.
		public void Refresh()
		{
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		}
	}
}
#endif