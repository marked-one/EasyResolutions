// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using System;
	using System.IO;
	using System.Collections.Generic;

	/// String extension methods.
	public static class StringExtensions
	{
		/// Gets the file name without extension from the specified path.
		public static string GetFileNameWithoutExtension(this string path)
		{
			if(string.IsNullOrEmpty(path))
				return string.Empty;

			try
			{
				return Path.GetFileNameWithoutExtension(path);
			}
			catch(ArgumentException)
			{
				return string.Empty;
			}
		}

		/// Gets the scene name from the specified file 
		/// path. If a resolution is present in the 
		/// file name, returns only a part before the 
		/// resolution, the whole file name otherwise.
		public static string GetSceneNameFromPath(this string path)
		{
			return GetSceneNameFromFileName (GetFileNameWithoutExtension(path));
		}

		/// Gets the resolution from the specified file
		/// path. If there are many resolutions in the 
		/// file name, returns the first one only.
		public static string GetResolutionFromPath(this string path)
		{
			return GetResolutionFromFileName (GetFileNameWithoutExtension(path));
		}

		/// Gets the scene name from the specified file 
		/// name. If a resolution is present in the 
		/// specified string, returns only a part before 
		/// the resolution, the whole string otherwise.
		public static string GetSceneNameFromFileName(this string fileName)
		{
			if(string.IsNullOrEmpty(fileName))
				return string.Empty;

			var tokens = fileName.Split('.');
			for(var i = 0; i < tokens.Length; i++)
				if(tokens[i].IsResolution())
					return string.Join(".", tokens, 0, i);
			
			return fileName;
		}

		/// Gets the resolution from the specified file name.
		/// If there are many resolutions in the specified 
		/// file name, returns the first one only.
		public static string GetResolutionFromFileName(this string fileName)
		{
			if(string.IsNullOrEmpty(fileName))
				return string.Empty;

			var tokens = fileName.Split('.');
			for(var i = 0; i < tokens.Length; i++)
				if(tokens[i].IsResolution())
					return tokens[i];
			
			return string.Empty;
		}
		
		/// Checks whether the specified string is a 
		/// resolution (a string that consists of 
		/// two integer numbers separated by 'x').
		public static bool IsResolution (this string resolutionCandidate) 
		{
			if(string.IsNullOrEmpty(resolutionCandidate))
				return false;
			
			var tokens = resolutionCandidate.Split('x');
			if(tokens.Length != 2)
				return false;
			
			var result = 0;
			if(!int.TryParse(tokens[0], out result))
				return false;
			
			if(!int.TryParse(tokens[1], out result))
				return false;
			
			return true;
		}
		
		/// Finds all resolutions in a string
		/// and returns them as an array.
		/// We expect that a string may contain
		/// multiple resolutions separated by dots.
		/// Example: abc.1024x768.def.711x400.ghi
		public static string[] AllResolutions(this string stringWithResolutions)
		{
			if(string.IsNullOrEmpty(stringWithResolutions))
				return new string[0];
			
			var resolutions = new List<string> ();
			var tokens = stringWithResolutions.Split ('.');
			foreach (var token in tokens) 
				if (token.IsResolution() && !resolutions.Contains(token))
					resolutions.Add (token);
			
			return resolutions.ToArray ();
		}
	}
}