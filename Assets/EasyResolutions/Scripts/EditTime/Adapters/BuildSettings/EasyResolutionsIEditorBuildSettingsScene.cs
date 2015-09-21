// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	/// Editor build settings scene interface.
	public interface IEditorBuildSettingsScene
	{
		/// Gets a value indicating whether the scene is enabled.
		bool Enabled { get; }
		
		/// Gets the path to the scene.
		string Path { get; }
	}
}
#endif