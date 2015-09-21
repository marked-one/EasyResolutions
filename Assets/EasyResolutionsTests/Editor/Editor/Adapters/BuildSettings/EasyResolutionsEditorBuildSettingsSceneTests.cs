// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using NUnit.Framework;
	using NSubstitute;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class EditorBuildSettingsSceneTests 
	{
		[Test]
		public void Constructor_IfPathIsNull_SetsPath()
		{
			var editorBuildSettingsScene = new EditorBuildSettingsScene(null, false);
			Assert.That(editorBuildSettingsScene.Path, Is.EqualTo(null));
		}
		
		[Test]
		public void Constructor_IfPathIsEmpty_SetsPath()
		{
			var editorBuildSettingsScene = new EditorBuildSettingsScene(string.Empty, false);
			Assert.That(editorBuildSettingsScene.Path, Is.EqualTo(string.Empty));
		}
		
		[Test]
		public void Constructor_IfPathIsNotEmpty_SetsPath()
		{
			var editorBuildSettingsScene = new EditorBuildSettingsScene("SomePath", false);
			Assert.That(editorBuildSettingsScene.Path, Is.EqualTo("SomePath"));
		}
		
		[Test]
		public void Constructor_IfEnabledIsFalse_SetsEnabled()
		{
			var editorBuildSettingsScene = new EditorBuildSettingsScene(string.Empty, false);
			Assert.That(editorBuildSettingsScene.Enabled, Is.False);
		}
		
		[Test]
		public void Constructor_IfEnabledIsTrue_SetsEnabled()
		{
			var editorBuildSettingsScene = new EditorBuildSettingsScene(string.Empty, true);
			Assert.That(editorBuildSettingsScene.Enabled, Is.True);
		}
	}
}