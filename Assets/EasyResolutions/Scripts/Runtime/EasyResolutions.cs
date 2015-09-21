// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;
#if UNITY_EDITOR
	using System;
#endif

	/// Easy Resolutions main class.
	[AddComponentMenu("Easy Resolutions/Easy Resolutions")]
	public class EasyResolutions : MonoBehaviour, IEasyResolutions, IObject<EasyResolutions>
	{
		#region Serialized data

		[SerializeField]
		EasyResolutionsScenesIndex _scenesIndex;

		[SerializeField]
		EasyResolutionsSettings _settings;

		#endregion
		#region Singleton

		private static EasyResolutions _instance;

		/// Access to the Easy Resolutions singleton.
		/// You may use it in plain C# classes, but 
		/// make sure a GameObject with EasyResolutions 
		/// MonoBehaviour attached exists in your scene.
		public static EasyResolutions Instance
		{
			get
			{
				if(_instance == null)
				{
					// We come here only if Instance is called before Awake().
					// In this case we search the scene for Easy Resolutions MonoBehaviour.
					_instance = GameObject.FindObjectOfType<EasyResolutions>();
					DontDestroyOnLoad(_instance.gameObject);
				}
				
				return _instance;
			}
		}

		/// Awakes Easy Resolutions.
		void Awake() 
		{
			if(_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(this);
			}
			else
			{
				if(this != _instance) // If an Easy Resolutions MonoBehaviour already exists, we don't need another one.
					Destroy(this.gameObject); 
			}
		}

		#endregion
		#region IEasyResolutions

		// Adapters.
		readonly static DebugAdapter _debugAdapter = new DebugAdapter();
		readonly static Log _log = new Log(_debugAdapter);
		readonly static ScreenStaticAdapter _screenAdapter = new ScreenStaticAdapter();

		// Utility.
		// ...

		public string ChooseScene(string name)
		{
			_log.Enabled = _settings.Real.Logging;
			var resolutionsForScene = _scenesIndex[name];
			if(resolutionsForScene.Count <= 0)
				return name;

			var width = _screenAdapter.Width;
			var height = _screenAdapter.Height;

			var resolution = "" + width + "x" + height;
			if(resolutionsForScene.ContainsKey(resolution))
				return resolutionsForScene[name];

			//FindBest();
			return name;
		}

		#endregion
		#region Editor
#if UNITY_EDITOR

		// Adapters.
		readonly static ScriptableObjectStaticAdapter _scriptableObjectStaticAdapter = new ScriptableObjectStaticAdapter();
		readonly static MonoScriptStaticAdapter _monoScriptStaticAdapter = new MonoScriptStaticAdapter();
		readonly static AssetDatabaseStaticAdapter _assetDatabaseStaticAdapter = new AssetDatabaseStaticAdapter();
		readonly static ObjectEditorStaticAdapter _objectEditorStaticAdapter = new ObjectEditorStaticAdapter();
		readonly static EditorUtilityStaticAdapter _editorUtilityStaticAdapter = new EditorUtilityStaticAdapter();

		// Utility.
		readonly static RootedAssetDatabase _rootedAssetDatabase = new RootedAssetDatabase(_assetDatabaseStaticAdapter, "Assets");
		readonly static SettingsPath _settingsPath = new SettingsPath(_scriptableObjectStaticAdapter, _monoScriptStaticAdapter, _rootedAssetDatabase, _objectEditorStaticAdapter);
		readonly static ScriptableObjectLoader _scriptableObjectLoader = new ScriptableObjectLoader(_rootedAssetDatabase, _scriptableObjectStaticAdapter, _settingsPath);

		void Reset()
		{
			var instances = FindObjectsOfType<EasyResolutions>();
			var components = GetComponents<EasyResolutions>();
			if((instances != null && instances.Length > 1) || (components != null && components.Length > 1))
				Invoke("DestroyThis", 0);
			else
				Setup();
		}

		void DestroyThis() 
		{
			DestroyImmediate(this);
		}

		void Setup()
		{
			var settings = _scriptableObjectLoader.Load<EasyResolutionsSettings>("Settings.asset");
			if(settings == null)
				throw new NullReferenceException("Failed to load or create the Easy Resolutions settings. Something went totally wrong.");

			_settings = settings.Real;
			if(_settings == null)
				throw new NullReferenceException("Failed to load or create the Easy Resolutions settings. Something went totally wrong.");

			var scenesIndex = _scriptableObjectLoader.Load<EasyResolutionsScenesIndex>("Scenes.asset");
			if(scenesIndex == null)
				throw new NullReferenceException("Failed to load or create the Easy Resolutions scenes index. Something went totally wrong.");

			_scenesIndex = scenesIndex.Real;
			if(_scenesIndex == null)
				throw new NullReferenceException("Failed to load or create the Easy Resolutions scenes index. Something went totally wrong.");

			_editorUtilityStaticAdapter.SetDirty(this);

			_log.Enabled = _settings.Real.Logging;
			_log.Info("Easy Resolutions singleton was successfully initialized.");
		}

		public EasyResolutions Real { get { return this; }}
#endif
		#endregion
	}
}
