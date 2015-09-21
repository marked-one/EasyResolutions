// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	/// Progress interface.
	public interface IProgress
	{
		/// Returns true, if the progress is finished, false otherwise.
		bool Finished { get; }

		/// Sets the description. 
		string Description { get; set; }

		/// Starts the progress. You need to specify 
		/// the number of segments to it. Returns 
		/// true if canceled, false otherwise.
		bool Start(int segments);

		/// Switches the progress to the next subsegment.
		/// Returns true if canceled, false otherwise.
		bool Next();

		/// Adds the specified value to the current segment.
		/// Value should be in range 0..1. Returns true if 
		/// canceled, false otherwise.
		bool Add(float value);

		/// Stops the progress. Returns true 
		/// if canceled, false otherwise.
		bool Finish();
	}
}