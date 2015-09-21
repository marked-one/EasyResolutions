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
	using Object = UnityEngine.Object;

	/// Asset database decorator. Prepends the paths with the
	/// specified root. Partial class, could be extended by the user.
	public partial class RootedAssetDatabase : IAssetDatabase
	{
		string _root;
		IAssetDatabase _assetDatabase;

		/// Constructor. Throws ArgumentNullException if null specified.
		public RootedAssetDatabase(IAssetDatabase assetDatabase, string root)
		{
			if(assetDatabase == null)
				throw new ArgumentNullException("assetDatabase");

			if(root == null)
				throw new ArgumentNullException("root");

			_assetDatabase = assetDatabase;
			_root = PrepareRoot(root);
		}

		// Our root should end with a valid (for Unity: '/') separator.
		string PrepareRoot(string root)
		{
			if(root.Length <= 0)
				return string.Empty;

			root = root.Replace(Path.DirectorySeparatorChar, '/');
			root = root.Replace(Path.AltDirectorySeparatorChar, '/');
			root = root.TrimEnd(' ', '/');
			return root + '/';
		}

		/// Gets the path of the asset of the specified object. 
		/// The returned path is relative to the specified root.
		/// Throws ArgumentNullException if specified obj is null. 
		/// Returns an empty string if path was not found.
		public string GetAssetPath<T>(IObject<T> obj) where T : Object
		{
			if(obj == null)
				throw new ArgumentNullException("obj");

			var path = _assetDatabase.GetAssetPath(obj);
			if(string.IsNullOrEmpty(path))
				return string.Empty;

			if(!path.StartsWith(_root))
				return string.Empty;

			return path.Substring(_root.Length);
		}

		/// Creates the asset of the specified object at the specified path.
		/// The specified path should be relative to the specified root.
		/// Throws ArgumentException, if path contains invalid characters,
	    /// defined in System.IO.Path.GetInvalidPathChars, and 
		/// ArgumentNullException, if obj is null and/or path is null. 
		public void CreateAsset<T>(IObject<T> obj, string path) where T : Object
		{
			if(obj == null)
				throw new ArgumentNullException("obj");

			path = Path.Combine(_root, path);
			_assetDatabase.CreateAsset(obj, path);
		}

		/// Loads the scriptable object at the specified path.
		/// The specified path should be relative to the specified root.
		/// Throws ArgumentException, if path contains invalid characters,
		/// defined in System.IO.Path.GetInvalidPathChars, and 
		/// ArgumentNullException, if path is null. 
		public IScriptableObject<T> LoadScriptableObjectAtPath<T>(string path) where T : ScriptableObject, IScriptableObject<T>
		{
			path = Path.Combine(_root, path);
			return _assetDatabase.LoadScriptableObjectAtPath<T>(path);
		}

		/// Saves unsaved assets.
		public void SaveAssets()
		{
			_assetDatabase.SaveAssets();
		}

		/// Refreshes the asset database.
		public void Refresh()
		{
			_assetDatabase.Refresh();
		}
	}
}
#endif