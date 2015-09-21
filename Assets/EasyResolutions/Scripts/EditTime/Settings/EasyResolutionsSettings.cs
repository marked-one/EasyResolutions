// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;

	/// Easy Resolutions settings.
	public class EasyResolutionsSettings : ScriptableObject, IScriptableObject<EasyResolutionsSettings>, ISettings
	{
		#region Data fields

		/// Enables/disables log messages from Easy Resolutions.
		[SerializeField, Tooltip("Enables/disables log messages from Easy Resolutions.")]
		bool _logging = false;

		#endregion
		#region IScriptableObject<Settings>

		/// Gets the real object.
		public EasyResolutionsSettings Real { get { return this; } }

		#endregion
		#region ISettings

		/// Enables/disables logging. It is your 
		/// responsibility to save Settings or 
		/// mark Settings dirty after the value is set. 
		public bool Logging { get { return _logging; } set { _logging = value; } }

		#endregion
	}
}
#endif