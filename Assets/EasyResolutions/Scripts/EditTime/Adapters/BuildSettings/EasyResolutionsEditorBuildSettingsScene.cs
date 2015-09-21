// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	/// Editor build settings scene.
	public class EditorBuildSettingsScene : IEditorBuildSettingsScene
	{
		/// Constructor. Null or empty path is allowed.
		public EditorBuildSettingsScene(string path, bool enabled)
		{
			Path = path;
			Enabled = enabled;
		}
		
		/// Gets a value indicating whether the scene is enabled.
		public bool Enabled { get; private set; }
		
		/// Gets the path to the scene.
		public string Path { get; private set; }
	}
}
#endif