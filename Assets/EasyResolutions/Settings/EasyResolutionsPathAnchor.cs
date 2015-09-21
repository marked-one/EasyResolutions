// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa

// Since the file is not in the Editor folder, we 
// use the other way to exclude its code from runtime.
#if UNITY_EDITOR
namespace EasyResolutions 
{
	using UnityEngine;

	/// Path anchor. This script is placed in the Settings 
	/// folder and since it is a ScriptableObject, we then can get 
	/// the path to it and thus to the Settings folder itself.
	public class EasyResolutionsPathAnchor : ScriptableObject, IScriptableObject<EasyResolutionsPathAnchor>
	{
		/// Gets the real asset.
		public EasyResolutionsPathAnchor Real { get { return this; } }
	}
}
#endif
