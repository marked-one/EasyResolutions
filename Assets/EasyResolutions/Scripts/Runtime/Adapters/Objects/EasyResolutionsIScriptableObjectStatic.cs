// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;

	/// Scriptable object static methods interface.
	public interface IScriptableObjectStatic
	{
		/// Creates an instance of ScriptableObject of specified type.
		IScriptableObject<T> CreateInstance<T>() where T : ScriptableObject, IScriptableObject<T>;
	}
}
