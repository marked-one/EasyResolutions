// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using NUnit.Framework;
	using NSubstitute;
	using System;
	using System.Collections.Generic;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class ScenesIndexBuilderTests 
	{
		#region Constructor

		[Test]
		public void Constructor_IfLogIsNull_ThrowsArgumentNullException()
		{
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(null, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfProgressIsNull_ThrowsArgumentNullException()
		{
			var log = Substitute.For<ILog>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(log, null, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfScriptableObjectLoaderIsNull_ThrowsArgumentNullException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(log, progress, null, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfEditorBuildSettingsIsNull_ThrowsArgumentNullException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(log, progress, scriptableObjectLoader, null, scriptableObjectSaver, scriptableObjectSelection), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfScriptableObjectSaverIsNull_ThrowsArgumentNullException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, null, scriptableObjectSelection), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfScriptableObjectSelectionIsNull_ThrowsArgumentNullException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			Assert.That(() => new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfAllArgumentsAreNotNull_ThrowsNothing()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();
			Assert.That(() => new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection), Throws.Nothing);
		}
		
		#endregion
		#region Build

		[Test] 
		public void Build_IfSpecifiedAssetNameIsNull_ThrowsArgumentException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			Assert.That(() => scenesIndexBuilder.Build(null), Throws.ArgumentException);
		}

		[Test] 
		public void Build_IfSpecifiedAssetNameIsEmpty_ThrowsArgumentException()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			Assert.That(() => scenesIndexBuilder.Build(string.Empty), Throws.ArgumentException);
		}

		[Test]
		public void Build_IfScriptableObjectLoaderLoadReturnsNull_Stops()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns((IScriptableObject<EasyResolutionsScenesIndex>)null);
			scenesIndexBuilder.Build("Some.asset");
			editorBuildSettings.Received(0).GetEnumerator();
			scriptableObjectSaver.Received(0).Save<EasyResolutionsScenesIndex>(Arg.Any<IScriptableObject<EasyResolutionsScenesIndex>>());
			scriptableObjectSelection.Received(0).Select<EasyResolutionsScenesIndex>(Arg.Any<IScriptableObject<EasyResolutionsScenesIndex>>());
			progress.Received (0).Description = Arg.Any<string>();
			progress.Received (0).Start(Arg.Any<int>());
			progress.Received (0).Next();
			progress.Received (0).Finish();
		}

		[Test]
		public void Build_IfScriptableObjectLoaderLoadReturnsNotNull_ClearsScenesIndexReturnedByScriptableObjectLoaderLoad()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			var scenesIndexScriptableObject = Substitute.For<IScriptableObject<EasyResolutionsScenesIndex>>();
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			var resolution = "resolution";
			scenesIndex[name, resolution] = "full name";
			scenesIndexScriptableObject.Real.Returns(scenesIndex);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns(scenesIndexScriptableObject);
			scenesIndexBuilder.Build("Some.asset");
			Assert.That(scenesIndex[name, resolution], Is.Empty);
		}

		[Test]
		public void Build_IfNoScenesInEditorBuildSettings_AddsNothingToScenesIndex()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			editorBuildSettings.Count.Returns(0);
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			var scenesIndexScriptableObject = Substitute.For<IScriptableObject<EasyResolutionsScenesIndex>>();
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndexScriptableObject.Real.Returns(scenesIndex);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns(scenesIndexScriptableObject);
			scenesIndexBuilder.Build("Some.asset");
			var count = 0;
#pragma warning disable 219
#pragma warning disable 168
			foreach(var scene in scenesIndex)
#pragma warning restore 168
#pragma warning restore 219
				count++;

			Assert.That(count, Is.EqualTo(0));
		}

		[Test]
		public void Build_IfNoScenesInEditorBuildSettings_ProgressMethodsAreNotCalled()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			editorBuildSettings.Count.Returns(0);
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			var scenesIndexScriptableObject = Substitute.For<IScriptableObject<EasyResolutionsScenesIndex>>();
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndexScriptableObject.Real.Returns(scenesIndex);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns(scenesIndexScriptableObject);
			scenesIndexBuilder.Build("Some.asset");
			progress.Received(0).Description = Arg.Any<string>();
			progress.Received(0).Start(Arg.Any<int>());
			progress.Received(0).Finish();
			progress.Received(0).Next();
		}

		[Test]
		public void Build_IfSomeScenesArePresent_AddsScenesFromEditorBuildSettingsToValidScenesIndex()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			editorBuildSettings.Count.Returns(2);
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			var scenesIndexScriptableObject = Substitute.For<IScriptableObject<EasyResolutionsScenesIndex>>();
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndexScriptableObject.Real.Returns(scenesIndex);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns(scenesIndexScriptableObject);
			editorBuildSettings.GetEnumerator().Returns(Enumeration());
			scenesIndexBuilder.Build("Some.asset");
			Assert.That(scenesIndex["Scene", "1024x768"], Is.EqualTo("Scene.1024x768"));
		}

		[Test]
		public void Build_IfSomeScenesArePresent_CallsProgressMethodsInProperOrderWithProperArguments()
		{
			var log = Substitute.For<ILog>();
			var progress = Substitute.For<IProgress>();
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var editorBuildSettings = Substitute.For<IEditorBuildSettings>();
			editorBuildSettings.Count.Returns(2);
			var scriptableObjectSaver = Substitute.For<IScriptableObjectSaver>();
			var scriptableObjectSelection = Substitute.For<IScriptableObjectSelection>();	
			var scenesIndexBuilder = new ScenesIndexBuilder(log, progress, scriptableObjectLoader, editorBuildSettings, scriptableObjectSaver, scriptableObjectSelection);
			var scenesIndexScriptableObject = Substitute.For<IScriptableObject<EasyResolutionsScenesIndex>>();
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndexScriptableObject.Real.Returns(scenesIndex);
			scriptableObjectLoader.Load<EasyResolutionsScenesIndex>(Arg.Any<string>()).Returns(scenesIndexScriptableObject);
			editorBuildSettings.GetEnumerator().Returns(Enumeration());
			scenesIndexBuilder.Build("Some.asset");
			Received.InOrder(() =>
			{
				progress.Description = string.Empty;
				progress.Start(2);
				progress.Description = "Some/Path/Scene.1024x768.unity";
				progress.Next();
				progress.Description = "Some/Path/Scene.711x400.unity";
				progress.Next();
				progress.Finish();
			});
		}

		IEnumerator<IEditorBuildSettingsScene> Enumeration()
		{
			yield return new EditorBuildSettingsScene("Some/Path/Scene.1024x768.unity", true);
			yield return new EditorBuildSettingsScene("Some/Path/Scene.711x400.unity", false);
		}
		
		#endregion
	}
}