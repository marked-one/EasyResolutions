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

	// (Re)builds the scenes index.
	public class ScenesIndexBuilder
	{
		ILog _log;
		IProgress _progress;
		IScriptableObjectLoader _scriptableObjectLoader;
		IEditorBuildSettings _editorBuildSettings;
		IScriptableObjectSaver _scriptableObjectSaver;
		IScriptableObjectSelection _scriptableObjectSelection;

		/// Constructor. Throws ArgumentNullException 
		/// if at least one of the arguments is null.
		public ScenesIndexBuilder(ILog log, IProgress progress, IScriptableObjectLoader scriptableObjectLoader, IEditorBuildSettings editorBuildSettings, 
		                          IScriptableObjectSaver scriptableObjectSaver, IScriptableObjectSelection scriptableObjectSelection)
		{
			if(log == null)
				throw new ArgumentNullException("log");

			if(progress == null)
				throw new ArgumentNullException("progress");

			if(scriptableObjectLoader == null)
				throw new ArgumentNullException("scriptableObjectLoader");

			if(editorBuildSettings == null)
				throw new ArgumentNullException("editorBuildSettings");

			if(scriptableObjectSaver == null)
				throw new ArgumentNullException("scriptableObjectSaver");

			if(scriptableObjectSelection == null)
				throw new ArgumentNullException("scriptableObjectSelection");

			_log = log;
			_progress = progress;
			_scriptableObjectLoader = scriptableObjectLoader;
			_editorBuildSettings = editorBuildSettings;
			_scriptableObjectSaver = scriptableObjectSaver;
			_scriptableObjectSelection = scriptableObjectSelection;
		}

		/// Rebuilds the scenes index in the specified asset.
		public void Build(string assetName)
		{
			if(string.IsNullOrEmpty(assetName))
				throw new ArgumentException("Asset name may not be null or empty.", "assetName");
		
			var scenesIndex = LoadScenesIndex(assetName);
			if(scenesIndex == null)
				return;

			RebuilSceneIndex(scenesIndex);
			SaveScenesIndex(scenesIndex);
		}

		EasyResolutionsScenesIndex LoadScenesIndex(string assetName)
		{
			_log.Info("Loading scenes index...");
			var scenesIndexAsset = _scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(assetName);
			if(scenesIndexAsset == null)
			{
				_log.Warning("Failed to load the scenes index asset (EasyResolutionsScenesIndex).");
				return null;
			}
			
			_log.Info("...scenes index loaded.");
			return scenesIndexAsset.Real;
		}

		void RebuilSceneIndex(IScenesIndex scenesIndex)
		{
			_log.Info("Rebuilding scenes index...");
			scenesIndex.Clear();
			var count = _editorBuildSettings.Count;
			if(count > 0)
			{
				_progress.Description = string.Empty;
				_progress.Start(count);
				foreach(var scene in _editorBuildSettings)
				{
					_progress.Description = scene.Path;
					if(!scene.Enabled)
					{
						_log.Info("Scene " + scene.Path + " is disabled.");
						_progress.Next();
						continue;
					}
					
					if(string.IsNullOrEmpty(scene.Path))
					{
						_log.Warning("Can't get the path of a scene.");
						_progress.Next();
						continue;
					}
					
					string fileName = null;
					try
					{
						fileName = scene.Path.GetFileNameWithoutExtension();
					}
					catch(ArgumentException)
					{
						_log.Warning("Failed to get the file name from the path to the scene " + scene.Path + ". ArgumentException throwm. Most likely the path contains forbidden chars.");
						_progress.Next();
						continue;
					}
					
					if(string.IsNullOrEmpty(fileName))
					{
						_log.Warning("Failed to get the file name from the path to the scene " + scene.Path + ".");
						_progress.Next();
						continue;
					}
					
					var name = fileName.GetSceneNameFromFileName();
					var resolution = fileName.GetResolutionFromFileName();
					scenesIndex[name, resolution] = fileName;
					_log.Info("Scene " + fileName + " with name " + name + " and " + resolution + " resolution asses to index.");
					_progress.Next();
				}
				
				_progress.Finish();
			}
			_log.Info("...rebuilding scenes index finished.");
		}

		void SaveScenesIndex(EasyResolutionsScenesIndex scenesIndex)
		{
			_log.Info("Saving scenes index...");
			_scriptableObjectSaver.Save(scenesIndex);
			_scriptableObjectSelection.Select<EasyResolutionsScenesIndex>(scenesIndex);
			_log.Info("...saving scenes index finished.");
		}
	}
}
#endif