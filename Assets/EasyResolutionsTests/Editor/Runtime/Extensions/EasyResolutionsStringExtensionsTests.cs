// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using NUnit.Framework;
	using NSubstitute;
	using System.Linq;
	using System.IO;

	[TestFixture]
	[Category("Easy Resolutions")]
	internal class StringExtensionsTests
	{
		#region GetFileNameWithoutExtension

		[Test]
		public void GetFileNameWithoutExtensionReturnsEmptyStringIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).GetFileNameWithoutExtension(), Is.EqualTo(string.Empty));
#pragma warning restore 1720
		}
		
		[Test]
		public void GetFileNameWithoutExtensionReturnsEmptyStringIfStringIsEmpty ()
		{
			Assert.That((string.Empty).GetFileNameWithoutExtension(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetFileNameWithoutExtensionReturnsEmptyStringIfStringContainsInvalidCharacters()
		{
			var pathPart1 = "file/pa";
			var pathPart2 = "th/fileName.extension";
			var invalidCharacters = Path.GetInvalidPathChars();
			foreach(var character in invalidCharacters)
				Assert.That((pathPart1 + character + pathPart2).GetFileNameWithoutExtension(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetFileNameWithoutExtensionReturnsFileNameWithoutExtensionIfStringContainsOnlyFileName()
		{
			var fileName = "fileName";
			Assert.That(fileName.GetFileNameWithoutExtension(), Is.EqualTo(fileName));
		}

		[Test]
		public void GetFileNameWithoutExtensionReturnsFileNameWithoutExtensionIfStringContainsFileNameWithExtension()
	    {
			var fileName = "fileName";
			var extension = ".extension";
			Assert.That((fileName + extension).GetFileNameWithoutExtension(), Is.EqualTo(fileName));
		}

		[Test]
		public void GetFileNameWithoutExtensionReturnsFileNameWithoutExtensionIfStringContainsFileNameWithExtensionWhichIsResolution()
		{
			var fileName = "fileName";
			var extension = ".1024x768";
			Assert.That((fileName + extension).GetFileNameWithoutExtension(), Is.EqualTo(fileName));
		}

		[Test]
		public void GetFileNameWithoutExtensionReturnsFileNameWithoutExtensionIfStringContainsPathWithFileNameWithExtension()
		{
			var path = "file/path/";
			var fileName = "fileName";
			var extension = ".extension";
			Assert.That((path + fileName + extension).GetFileNameWithoutExtension(), Is.EqualTo(fileName));
		}

		#endregion		
		#region GetSceneNameFromPath

		[Test]
		public void GetSceneNameFromPathReturnsEmptyStringIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).GetSceneNameFromPath(), Is.EqualTo(string.Empty));
#pragma warning restore 1720
		}
		
		[Test]
		public void GetSceneNameFromPathReturnsEmptyStringIfStringIsEmpty ()
		{
			Assert.That((string.Empty).GetSceneNameFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetSceneNameFromPathReturnsEmptyStringIfStringContainsInvalidCharacters()
		{
			var pathPart1 = "file/pa";
			var pathPart2 = "th/fileName.extension";
			var invalidCharacters = Path.GetInvalidPathChars();
			foreach(var character in invalidCharacters)
				Assert.That((pathPart1 + character + pathPart2).GetSceneNameFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetSceneNameFromPathReturnsSceneNameIfStringContainsOnlySceneNameWithoutResolutions()
		{
			var fileName = "sceneName";
			Assert.That(fileName.GetSceneNameFromPath(), Is.EqualTo(fileName));
		}

		[Test]
		public void GetSceneNameFromPathReturnsFileNameWithoutExtensionIfStringContainsFileNameWithExtensionWhichIsResolution()
		{
			var sceneName = "sceneName";
			var extension = ".1024x768";
			var fileName = sceneName + extension;
			Assert.That(fileName.GetSceneNameFromPath(), Is.EqualTo(sceneName));
		}

		[Test]
		public void GetSceneNameFromPathReturnsFileNameWithoutExtensionIfStringContainsFileNameWithExtension()
		{
			var sceneName = "sceneName";
			var extension = ".extension";
			var fileName = sceneName + extension;
			Assert.That(fileName.GetFileNameWithoutExtension(), Is.EqualTo(sceneName));
		}

		[Test]
		public void GetSceneNameFromPathReturnsSubstringBeforeResolutionIfStringContainsOnlySceneNameWithResolutionsAndExtension()
		{
			var sceneName = "sceneName";
			var fileName = sceneName + ".1024x768.extension";
			Assert.That(fileName.GetSceneNameFromPath(), Is.EqualTo(sceneName));
		}

		[Test]
		public void GetSceneNameFromPathReturnsSubstringBeforeResolutionIfPathAndSceneNameWithResolutionsWithExtensionSpecified()
		{
			var path = "file/path/";
			var sceneName = "sceneName";
			var resolution = ".1024x768";
			var extension = ".extension";
			Assert.That((path + sceneName + resolution + extension).GetSceneNameFromPath(), Is.EqualTo(sceneName));
		}

		#endregion		
		#region GetResolutionFromPath

		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).GetResolutionFromPath(), Is.EqualTo(string.Empty));
#pragma warning restore 1720
		}
		
		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfStringIsEmpty ()
		{
			Assert.That((string.Empty).GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		
		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfStringContainsInvalidCharacters()
		{
			var pathPart1 = "file/pa";
			var pathPart2 = "th/fileName.extension";
			var invalidCharacters = Path.GetInvalidPathChars();
			foreach(var character in invalidCharacters)
				Assert.That((pathPart1 + character + pathPart2).GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfNameDoesNotContainNumbers ()
		{
			Assert.That("/abc/1024x768/abcxdef".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfNameContainsSingleNumber ()
		{
			Assert.That("/abc/1024x768/1024".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfNameContainsSingleNumberAndX ()
		{
			Assert.That("/abc/1024x768/1024x".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromPatReturnsEmptyStringIfNameContainsMoreThan2NumbersSeparatedByX ()
		{
			Assert.That("/abc/1024x768/1024x768x32".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfNameDoesntContainXBetweenTwoNumbers ()
		{
			Assert.That("/abc/1024x768/1024.768".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromPathReturnsEmptyStringIfNameContainsTwoIntegerNumbersSeparatedByXAndContainsSomeOtherCharacters ()
		{
			Assert.That("/abc/1024x768/abc1024x768".GetResolutionFromPath(), Is.EqualTo(string.Empty));
			Assert.That("/abc/1024x768/1024x768def".GetResolutionFromPath(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromPathReturnsValidResolutionIfNameContainsTwoIntegerNumbersSeparatedByXAndContainsSomeOtherCharactersSeparatedByDotFromNumbers ()
		{
			var resolution = "1024x768";
			var path = "/abc/1024x768/";
			Assert.That((path + "abc." + resolution + ".extension").GetResolutionFromPath(), Is.EqualTo(resolution));
			Assert.That((path + resolution + ".def" + ".extension").GetResolutionFromPath(), Is.EqualTo(resolution));
		}
		
		[Test]
		public void GetResolutionFromPathReturnsValidResolutionIfNameContainsTwoIntegerNumbersSeparatedByX ()
		{
			var resolution = "1024x768";
			var path = "/abc/1024x768/" + resolution;
			Assert.That(path.GetResolutionFromPath(), Is.EqualTo(resolution));
		}
		
		[Test]
		public void GetResolutionFromPathReturnsOnlyFirstResolutionIfThereAreMultiplePresent()
		{
			var resolution1 = "1024x768";
			var resolution2 = "1366x768";
			var path = "/abc/1024x768/" + resolution1 + "." + resolution2;
			Assert.That(path.GetResolutionFromPath(), Is.EqualTo(resolution1));
		}

		#endregion		
		#region GetSceneNameFromFileName

		[Test]
		public void GetSceneNameFromFileNameReturnsEmptyStringIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).GetSceneNameFromFileName(), Is.EqualTo(string.Empty));
#pragma warning restore 1720
		}

		[Test]
		public void GetSceneNameFromFileNameReturnsEmptyStringIfStringIsEmpty ()
		{
			Assert.That((string.Empty).GetSceneNameFromFileName(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetSceneNameFromFileNameReturnsFileNameIfStringDoesNotContainResolutions ()
		{
			var fileName = "scene.name";
			Assert.That(fileName.GetSceneNameFromFileName(), Is.EqualTo(fileName));
		}

		[Test]
		public void GetSceneNameFromFileNameReturnsSubstringBeforeResolutionIfStringDoesContainResolutions ()
		{
			var sceneName = "scene";
			var fileName = sceneName + ".1024x768.name";
			Assert.That(fileName.GetSceneNameFromFileName(), Is.EqualTo(sceneName));
		}

		#endregion		
		#region GetResolutionFromFileName

		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).GetResolutionFromFileName(), Is.EqualTo(string.Empty));
#pragma warning restore 1720
		}

		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringIsEmpty ()
		{
			Assert.That((string.Empty).GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringDoesNotContainNumbers ()
		{
			Assert.That("abcxdef.abcxdef".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringContainsSingleNumber ()
		{
			Assert.That("1024".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringContainsSingleNumberAndX ()
		{
			Assert.That("1024x".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringContainsMoreThan2NumbersSeparatedByX ()
		{
			Assert.That("1024x768x32".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringDoesntContainXBetweenTwoNumbers ()
		{
			Assert.That("1024.768".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void GetResolutionFromFileNameReturnsEmptyStringIfStringContainsTwoIntegerNumbersSeparatedByXAndContainsSomeOtherCharacters ()
		{
			Assert.That("abc1024x768".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
			Assert.That("1024x768def".GetResolutionFromFileName(), Is.EqualTo(string.Empty));
		}

		[Test]
		public void GetResolutionFromFileNameReturnsValidResolutionIfStringContainsTwoIntegerNumbersSeparatedByXAndContainsSomeOtherCharactersSeparatedByDotFromNumbers ()
		{
			var resolution = "1024x768";
			Assert.That(("abc." + resolution).GetResolutionFromFileName(), Is.EqualTo(resolution));
			Assert.That((resolution + ".def").GetResolutionFromFileName(), Is.EqualTo(resolution));
		}
		
		[Test]
		public void GetResolutionFromFileNameReturnsValidResolutionIfStringContainsTwoIntegerNumbersSeparatedByX ()
		{
			var resolution = "1024x768";
			Assert.That(resolution.GetResolutionFromFileName(), Is.EqualTo(resolution));
		}

		[Test]
		public void GetResolutionFromFileNamenReturnsOnlyFirstResolutionIfThereAreMultiplePresentInString()
		{
			var resolution1 = "1024x768";
			var resolution2 = "1366x768";
			var resolutions = resolution1 + "." + resolution2;
			Assert.That(resolutions.GetResolutionFromFileName(), Is.EqualTo(resolution1));
		}

		#endregion
		#region IsResolution
		
		[Test]
		public void IsResolutionReturnsFalseIfStringIsNull ()
		{
#pragma warning disable 1720
			Assert.That(((string)null).IsResolution(), Is.False);
#pragma warning restore 1720
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringIsEmpty ()
		{
			Assert.That(string.Empty.IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringDoesnNotContainNumbers ()
		{
			Assert.That("abcxdef".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringContainsSingleNumber ()
		{
			Assert.That("1024".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringContainsSingleNumberAndX ()
		{
			Assert.That("1024x".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringContainsMoreThan2NumbersSeparatedByX ()
		{
			Assert.That("1024x768x32".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionsReturnsFalseIfStringDoesntContainXBetweenTwoNumbers ()
		{
			Assert.That("1024.768".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringContainsTwoFloatNumbersSeparatedByX ()
		{
			Assert.That("1024.0x768.1".IsResolution(), Is.False);
			Assert.That("1024,0x768,1".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsFalseIfStringContainsTwoIntegerNumbersSeparatedByXAndContainsSomeMoreCharacters ()
		{
			Assert.That("abc1024x768".IsResolution(), Is.False);
			Assert.That("1024x768def".IsResolution(), Is.False);
		}
		
		[Test]
		public void IsResolutionReturnsTrueIfStringContainsTwoIntegerNumbersSeparatedByX ()
		{
			Assert.That("1024x768".IsResolution(), Is.True);
		}
		
		#endregion
		#region AllResolutions
		
		[Test]
		public void AllResolutionsReturnsEmptyArrayIfStringIsNull()
		{
#pragma warning disable 1720
			var resolutions = ((string)null).AllResolutions();
#pragma warning restore 1720
			Assert.That(resolutions.Length, Is.EqualTo(0));
		}
		
		[Test]
		public void AllResolutionsReturnsEmptyArrayIfStringIsEmpty()
		{
			var resolutions = string.Empty.AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(0));
		}
		
		[Test]
		public void AllResolutionsReturnsEmptyArrayIfStringIsNotEmptyAndHasNoResolutions()
		{
			var resolutions = "String.Without.Resolutions".AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(0));
		}
		
		[Test]
		public void AllResolutionsReturnsEmptyArrayIfStringContainsResolutionsButTheyArentDotSeparated()
		{
			var resolutions = "String.With.Resolutions:1024x768:711x400".AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(0));
		}
		
		[Test]
		public void AllResolutionsReturnsArrayWithAllDotSeparatedResolutionsInAString()
		{
			var resolutions = "String.With.Resolutions.1024x768.711x400".AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(2));
			Assert.That(resolutions.Contains("1024x768"));
			Assert.That(resolutions.Contains("711x400"));
		}
		
		[Test]
		public void AllResolutionsReturnsArrayThatContainsResolutionsInTheSameOrderAsInAString()
		{
			var resolutions = "String.With.Resolutions.1024x768.711x400".AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(2));
			Assert.That(resolutions[0], Is.EqualTo("1024x768"));
			Assert.That(resolutions[1], Is.EqualTo("711x400"));
		}
		
		[Test]
		public void AllResolutionsReturnsArrayThatContainsResolutionsFromAStringWithoutRepetitions()
		{
			var resolutions = "String.With.Resolutions.1024x768.711x400.1024x768.711x400".AllResolutions();
			Assert.That(resolutions.Length, Is.EqualTo(2));
			Assert.That(resolutions.Count(item => item.Equals("1024x768")), Is.EqualTo(1));
			Assert.That(resolutions.Count(item => item.Equals("711x400")), Is.EqualTo(1));
		}
		
		#endregion
	}
}
