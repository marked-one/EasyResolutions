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

	/// Asset database interface. Partial 
	/// interface, could be extended by the user.
	public partial interface IAssetDatabase
	{
		/// Gets the path of the asset of the specified object. 
		string GetAssetPath<T>(IObject<T> obj) where T : Object;

		/// Creates the asset of the specified object at the specified path.
		void CreateAsset<T>(IObject<T> obj, string path) where T : Object;

		/// Loads the scriptable object at the specified path.
		IScriptableObject<T> LoadScriptableObjectAtPath<T>(string path) where T : ScriptableObject, IScriptableObject<T>;

		/// Saves unsaved assets.
		void SaveAssets();

		/// Refreshes the asset database.
		void Refresh();
	}
}
#endif