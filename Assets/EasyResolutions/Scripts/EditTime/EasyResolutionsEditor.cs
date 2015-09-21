// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	/// Main Easy Resolutions editor class.
	public class EasyResolutionsEditor
	{
		#region Easy Resolutions

		// Adapters.
		readonly static DebugAdapter _debugAdapter = new DebugAdapter();
		readonly static ScriptableObjectStaticAdapter _scriptableObjectStaticAdapter = new ScriptableObjectStaticAdapter();
		readonly static MonoScriptStaticAdapter _monoScriptStaticAdapter = new MonoScriptStaticAdapter();
		readonly static AssetDatabaseStaticAdapter _assetDatabaseStaticAdapter = new AssetDatabaseStaticAdapter();
		readonly static SelectionStaticAdapter _selectionStaticAdapter = new SelectionStaticAdapter();
		readonly static ObjectEditorStaticAdapter _objectEditorStaticAdapter = new ObjectEditorStaticAdapter();
		readonly static EditorBuildSettingsStaticAdapter _editorBuildSettingsStaticAdapter = new EditorBuildSettingsStaticAdapter();
		readonly static EditorUtilityStaticAdapter _editorUtilityStaticAdapter = new EditorUtilityStaticAdapter();

		// Utility.
		readonly static Log _log = new Log(_debugAdapter);
		readonly static RootedAssetDatabase _rootedAssetDatabase = new RootedAssetDatabase(_assetDatabaseStaticAdapter, "Assets");
		readonly static SettingsPath _settingsPath = new SettingsPath(_scriptableObjectStaticAdapter, _monoScriptStaticAdapter, _rootedAssetDatabase, _objectEditorStaticAdapter);
		readonly static ScriptableObjectLoader _scriptableObjectLoader = new ScriptableObjectLoader(_rootedAssetDatabase, _scriptableObjectStaticAdapter, _settingsPath);
		readonly static ScriptableObjectSelection _scriptableObjectSelection = new ScriptableObjectSelection(_scriptableObjectLoader, _selectionStaticAdapter);
		readonly static ScriptableObjectSaver _scriptableObjectSaver = new ScriptableObjectSaver(_rootedAssetDatabase, _editorUtilityStaticAdapter);
		readonly static EditorProgressBar _progressBar = new EditorProgressBar(_editorUtilityStaticAdapter, false, "Easy Resolutions");

		// Main.
		readonly static ScenesIndexBuilder _scenesIndexBuilder = new ScenesIndexBuilder(_log, _progressBar, _scriptableObjectLoader, _editorBuildSettingsStaticAdapter, _scriptableObjectSaver, _scriptableObjectSelection); 

		/// Rebuilds the scenes index.
		public static void RebuildScenesIndex()
		{
			var settings = _scriptableObjectLoader.Load<EasyResolutionsSettings>("Settings.asset");
			_log.Enabled = settings.Real.Logging;
			_progressBar.Title = "(Re)building scenes index";
			_progressBar.Cancelable = false;
			_scenesIndexBuilder.Build("Scenes.asset");
		}

		/// Replaces the textures in current scene only.
		public static void ReplaceInCurrentScene()
		{
			var settings = _scriptableObjectLoader.Load<EasyResolutionsSettings>("Settings.asset");
			_log.Enabled = settings.Real.Logging;
			_progressBar.Title = "Replacing in current scene";
			_progressBar.Cancelable = true;
		}

		/// Replaces the textures in enabled scenes.
		public static void ReplaceInEnabledScenes()
		{
			var settings = _scriptableObjectLoader.Load<EasyResolutionsSettings>("Settings.asset");
			_log.Enabled = settings.Real.Logging;
			_progressBar.Title = "Replacing in all enabled scenes";
			_progressBar.Cancelable = true;
		}

		/// Shows Easy Resolutions settings.
		public static void ShowSettings()
		{
			_scriptableObjectSelection.Select<EasyResolutionsSettings>("Settings.asset");
		}

		#endregion
		#region Menu

		/// "(Re)build scenes index" menu item.
		[MenuItem ("Tools/Easy Resolutions/(Re)build scenes index", false, 1)]
		static void RebuildScenesIndexMenuItem () 
		{
			RebuildScenesIndex();
		}

		/// "Replace textures in current scene" menu item.
		[MenuItem ("Tools/Easy Resolutions/Replace textures in current scene", false, 51)]
		static void ReplaceTexturesInCurrentSceneMenuItem () 
		{
			ReplaceInCurrentScene();
		}

		/// "Replace textures in enabled scenes" menu item.
		[MenuItem ("Tools/Easy Resolutions/Replace textures in enabled scenes", false, 52)]
		static void ReplaceTexturesInEnabledScenesMenuItem () 
		{
			ReplaceInEnabledScenes();
		}

		/// "Settings" menu item.
		[MenuItem ("Tools/Easy Resolutions/Settings", false, 101)]
		static void SettingsMenuItem () 
		{
			ShowSettings();
		}
		
		#endregion
	}
}
#endif
