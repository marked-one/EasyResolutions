// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;
	using System;
	using System.Text;
	using System.Collections;
	using System.Collections.Generic;

	/// Scenes index. Could be read in runtime but written only in editor. 
	public class EasyResolutionsScenesIndex : ScriptableObject, ISerializationCallbackReceiver, IScriptableObject<EasyResolutionsScenesIndex>, IScenesIndex
	{
		#region Data fields

		/// This class is necessary for serialization.
		[Serializable] class StringList { public List<string> List = new List<string>(); }

		/// Stores names.
		[SerializeField, HideInInspector]
		List<string> _names = new List<string>();

		/// Stores resolutions.
		[SerializeField, HideInInspector]
		List<StringList> _resolutions = new List<StringList>();

		/// Stores full names.
		[SerializeField, HideInInspector]
		List<StringList> _fullNames = new List<StringList>();

		/// Dictionary that is serialized via lists.
		Dictionary<string, Dictionary<string, string>> _scenes = new Dictionary<string, Dictionary<string, string>>();

		#endregion
		#region IScriptableObject<ScenesIndex>

		///
		public EasyResolutionsScenesIndex Real { get { return  this; } }

		#endregion
		#region ISerializationCallbackReceiver

		/// Called before the serialization of the object happens.
		/// Serializes dictionary to lists.
		public void OnBeforeSerialize()
		{
			ClearLists();
			foreach(var scene in _scenes)
			{
				_names.Add(scene.Key);
				var resolutions = new StringList();
				_resolutions.Add(resolutions);
				var fullNames = new StringList();
				_fullNames.Add(fullNames);
				foreach(var resolution in scene.Value)
				{
					resolutions.List.Add(resolution.Key);
					fullNames.List.Add(resolution.Value);
				}
			}
		}
		
		/// Called after the deserialization of the object happens.
		/// Deserializes dictionary from lists.
		public void OnAfterDeserialize()
		{
			for(var i = 0; i < _names.Count; i++)
			{
				var resolutions = new Dictionary<string, string>();
				for(var j = 0; j < _resolutions[i].List.Count; j++)
					resolutions.Add(_resolutions[i].List[j], _fullNames[i].List[j]);
				
				_scenes.Add(_names[i], resolutions);
			}
			
			ClearLists();

#if UNITY_EDITOR
			NeedUpdateInfo = true;
#endif
		}

		/// Clears the lists.
		void ClearLists()
		{
			_names.Clear();
			_resolutions.Clear();
			_fullNames.Clear();
		}
		
		#endregion
		#region Scenes index

		public Dictionary<string, string> this[string name]
		{
			get
			{
				if(string.IsNullOrEmpty(name))
					throw new ArgumentException("Name must be a non-empty string", "name");

				if(!_scenes.ContainsKey(name))
					return new Dictionary<string, string>();

				return _scenes[name];
			}
		}
		
		/// Gets or sets the full scene name by scene name and resolution.
		/// Throws ArgumentException if the scene name or specified value
		/// are null or empty and ArgumentNullException if resolution is null.
		public string this[string name, string resolution]
		{
#if UNITY_EDITOR
			get
			{
				if(string.IsNullOrEmpty(name))
					throw new ArgumentException("Name must be a non-empty string", "name");
				
				if(resolution == null)
					throw new ArgumentNullException("resolution");
				
				if(!_scenes.ContainsKey(name))
					return string.Empty;
				
				var scene = _scenes[name];
				if(!scene.ContainsKey(resolution))
					return string.Empty;
				
				return scene[resolution];
			}

			set
			{
				if(string.IsNullOrEmpty(name))
					throw new ArgumentException("Name must be a non-empty string", "name");
				
				if(resolution == null)
					throw new ArgumentNullException("resolution");
				
				if(string.IsNullOrEmpty(value))
					throw new ArgumentException("Value must be a non-empty string", "value");

				if(!_scenes.ContainsKey(name))
				{
					var resolutions = new Dictionary<string, string>();
					resolutions.Add(resolution, value);
					_scenes.Add(name, resolutions);
				}
				else
				{
					var resolutions = _scenes[name];
					if(resolutions.ContainsKey(resolution))
						resolutions[resolution] = value;
					else
						resolutions.Add(resolution, value);
				}

				NeedUpdateInfo = true;
			}
#endif
		}

#if UNITY_EDITOR
		/// Clears all the contents.
		public void Clear()
		{
			_scenes.Clear();
			NeedUpdateInfo = true;
		}
#endif
		
		/// Gets the scenes enumerator.
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		
		/// Gets the generic scenes enumerator.
		public IEnumerator<KeyValuePair<string, Dictionary<string, string>>> GetEnumerator()
		{
			return _scenes.GetEnumerator();
		}

#if UNITY_EDITOR
		/// Gets or sets a value indicating whether the info should be updated. 
		/// Get new info using ToString() method.
		public bool NeedUpdateInfo { get; set; }
		
		/// Returns a string that represents the current ScenesIndex.
		public override string ToString ()
		{
			var builder = new StringBuilder();
			foreach(var scene in _scenes)
			{
				builder.AppendFormat("{0}\n", scene.Key);
				foreach(var resolution in scene.Value)
					builder.AppendFormat("        {0} -> {1}\n", resolution.Key, resolution.Value);

				builder.AppendLine();
			}

			if(builder.Length > 0)
				builder.Length -= 1;

			return builder.ToString();
		}
#endif

		#endregion
	}
}
