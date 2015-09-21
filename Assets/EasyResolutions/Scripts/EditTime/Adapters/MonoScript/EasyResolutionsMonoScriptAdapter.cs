// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEditor;

	/// UnityEditor.MonoScript adapter.
	public class MonoScriptAdapter : IMonoScript
	{
		/// Constructor. Null is allowed.
		public MonoScriptAdapter(MonoScript monoScript)
		{
			Real = monoScript;
		}

		/// Gets the real MonoScript object.
		public MonoScript Real { get; private set; }
	}
}
#endif