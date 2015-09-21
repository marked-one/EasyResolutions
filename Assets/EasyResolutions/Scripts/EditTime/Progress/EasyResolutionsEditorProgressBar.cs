// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
#if UNITY_EDITOR
namespace EasyResolutions
{
	using UnityEngine;
	using System;

	public class EditorProgressBar : IProgress 
	{
		#region Data fields

		IEditorUtility _editorUtility;
		bool _cancelable;
		string _title = string.Empty;
		string _description = string.Empty;
		int _segments;
		int _currentSegment;

		#endregion
		#region EditorProgressBar

		/// Constructor. Throws ArgumentNullException if editorUtility is null.
		public EditorProgressBar(IEditorUtility editorUtility, bool cancelable, string title)
		{
			if(editorUtility == null)
				throw new ArgumentNullException("editorUtility");

			_editorUtility = editorUtility;
			Cancelable = cancelable;
			if(title != null)
				_title = title;

			Finished = true;
		}

		/// Controls whether the progress is cancelable.
		public bool Cancelable { set; get; }

		/// Sets or gets the title.
		public string Title
		{ 
			set { _title = value == null ? string.Empty : value; }
			get { return _title; }
		}

		#endregion
		#region IProgress

		/// Returns true, if the progress is finished, false otherwise.
		public bool Finished { get; private set; }

		/// Sets the description. 
		public string Description 
		{ 
			set { _description = value == null ? string.Empty : value; }
		 	get { return _description; }
		}
		
		/// Starts the progress. You need to specify 
		/// the number of segments to it. Does nothing, 
		/// if already started. Throws ArgumentException
		/// if the segments number is negative. Returns 
		/// true if canceled, false otherwise.
		public bool Start(int segments)
		{
			if(segments <= 0)
				throw new ArgumentException("The number of segments must be positive.", "segments");
			
			if(!Finished)
				return false;

			Reset (false, segments);
			return DisplayProgress(0);
		}
		
		/// Switches the progress to the next segment.
		/// May finish the progress. Does nothing if 
		/// already finished. Returns true if canceled, 
		/// false otherwise.
		public bool Next()
		{
			if(Finished)
				return false;

			if(_currentSegment < _segments)
				_currentSegment++;
			
			return DisplayProgress((float)_currentSegment / (float)_segments);
		}
		
		/// Adds the specified value to the current segment.
		/// Value should be in range 0..1. This method is not
		/// really meant to be used manually (but it's possible).
		/// Does nothing if already finished. Returns true 
		/// if canceled, false otherwise.
		public bool Add(float value)
		{
			if(value < 0 || value > 1)
				throw new ArgumentException("Value should be in range 0..1.", "value");
			
			if(Finished)
				return false; 
			
			return DisplayProgress(((float)_currentSegment + value) / (float)_segments);
		}

		bool DisplayProgress(float progress)
		{
			if(Cancelable)
				return _editorUtility.DisplayCancelableProgressBar(_title, _description, progress);

			_editorUtility.DisplayProgressBar(_title, _description, progress);
			return false;
		}
		
		/// Stops the progress. Returns true 
		/// if canceled, false otherwise.
		public bool Finish()
		{
			if(Finished)
				return false;

			Reset();
			_editorUtility.ClearProgressBar();
			return false;
		}

		void Reset()
		{
			Reset(true, 0);
		}
		
		void Reset(bool finished, int segments)
		{
			Finished = finished;
			_segments = segments;
			_currentSegment = 0;
		}

		#endregion
	}
}
#endif
