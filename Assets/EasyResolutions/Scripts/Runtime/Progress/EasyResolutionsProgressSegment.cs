// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using UnityEngine;
	using System;

	/// Decorates a progress with a segment.
	public class ProgressSegment : IProgress 
	{
		IProgress _progress;
		int _subsegments;
		int _currentSubsegment;
		string _description = string.Empty;

		/// Constructor. Throws ArgumentNullException if argument is null.
		public ProgressSegment(IProgress progress)
		{
			if(progress == null)
				throw new ArgumentNullException("progress");

			_progress = progress;
			Finished = true;
		}

		/// Sets the description. It is then
		/// passed to the decorated progress.
		public string Description 
		{ 
			set { _description = value == null ? string.Empty : value; }
		    get { return _description; }
		}

		/// Returns true, if the progress is finished, false otherwise.
		public bool Finished { get; private set; }

		/// Starts the segment. You need to specify 
		/// the number of subsegments to it. Does
		/// nothing, if already started. Starts 
		/// the progress with one segment if it isn't 
		/// started already. Throws ArgumentException
		/// if the subsegments number is negative. 
		/// Returns true if canceled, false otherwise.
		public bool Start(int subsegments)
		{
			if(subsegments <= 0)
				throw new ArgumentException("The number of subsegments must be positive.", "subsegments");

			if(!Finished)
				return false;

			Reset(false, subsegments);
			_progress.Description = _description;
			var retval = _progress.Finished ? _progress.Start(1) : false;
			IncreaseSegment(0);
			return retval;
		}

		/// Switches the segment to the next subsegment.
		/// Does nothing if already finished.
		/// Returns true if canceled, false otherwise.
		public bool Next()
		{
			if(Finished)
				return false; 
		
			if(_currentSubsegment < _subsegments)
				_currentSubsegment++;

			return IncreaseSegment((float)_currentSubsegment / (float)_subsegments);
		}

		/// Adds the specified value to the current subsegment.
		/// Value should be in range 0..1. This method is not
		/// really meant to be used manually (but it's possible).
		/// Throws ArgumentException if value usn't in range 0..1.
		/// Does nothing if already finished. Returns true if 
		/// canceled, false otherwise.
		public bool Add(float value)
		{
			if(value < 0 || value > 1)
				throw new ArgumentException("Value should be in range 0..1.", "value");

			if(Finished)
				return false;

			return IncreaseSegment(((float)_currentSubsegment + value) / (float)_subsegments);
		}

		bool IncreaseSegment(float value)
		{
			_progress.Description = Description;
			return _progress.Add(value);
		}

		/// Stops the segment. Returns true 
		/// if canceled, false otherwise.
		public bool Finish()
		{
			if(Finished)
				return false;

			Reset();
			return NextSegment();
		}

		void Reset()
		{
			Reset(true, 0);
		}

		void Reset(bool finished, int subsegments)
		{
			Finished = finished;
			_subsegments = subsegments;
			_currentSubsegment = 0;
		}

		bool NextSegment()
		{
			_progress.Description = Description; 
			return _progress.Next(); 
		}
	}
}