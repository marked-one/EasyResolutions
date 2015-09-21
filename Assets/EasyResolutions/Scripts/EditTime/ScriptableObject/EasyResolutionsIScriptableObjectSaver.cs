// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;

	public interface IScriptableObjectSaver
	{
		void Save<T>(IScriptableObject<T> scriptableObject) where T : ScriptableObject, IScriptableObject<T>;
	}
}
#endif