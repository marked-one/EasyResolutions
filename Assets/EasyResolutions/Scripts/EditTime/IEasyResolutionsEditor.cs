// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	public interface IEasyResolutionsEditor
	{
		/// Rebuilds the scenes index.
		void RebuildScenesIndex();
	
		/// Replaces the textures in current scene only.
		void ReplaceInCurrentScene();
	
		/// Replaces the textures in all enabled scenes.
		void ReplaceInEnabledScenes();
	
		/// Shows Easy Resolutions settings.
		void ShowSettings();
	}
}
#endif