// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	public class EasyResolutionsEditorAdapter : IEasyResolutionsEditor 
	{
		/// Rebuilds the scenes index.
		public void RebuildScenesIndex()
		{
			EasyResolutionsEditor.RebuildScenesIndex();
		}
	
		/// Replaces the textures in current scene only.
		public void ReplaceInCurrentScene()
		{
			EasyResolutionsEditor.ReplaceInCurrentScene();
		}
	
		/// Replaces the textures in enabled scenes.
		public void ReplaceInEnabledScenes()
		{
			EasyResolutionsEditor.ReplaceInEnabledScenes();
		}
	
		/// Shows Easy Resolutions settings.
		public void ShowSettings()
		{
			EasyResolutionsEditor.ShowSettings();
		}
	}
}
#endif