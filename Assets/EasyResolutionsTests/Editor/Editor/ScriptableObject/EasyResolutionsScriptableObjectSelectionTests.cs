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
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class ScriptableObjectSelectionTests 
	{
		#region Constructor
		
		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfScriptableObjectLoaderIsNull()
		{
			var selection = Substitute.For<ISelection>();
			Assert.That(() => new ScriptableObjectSelection(null, selection), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfSelectionIsNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			Assert.That(() => new ScriptableObjectSelection(scriptableObjectLoader, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsNothingIfAllArgumentsAreNotNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			Assert.That(() => new ScriptableObjectSelection(scriptableObjectLoader, selection), Throws.Nothing);
		}
		
		#endregion
		#region Select
		
		[Test] 
		public void SelectThrowsArgumentExceptionIfSpecifiedNameIsNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			Assert.That(() => scriptableObjectSelection.Select<ScriptableObjectStub>((string)null), Throws.ArgumentException);
		}

		[Test] 
		public void SelectThrowsArgumentExceptionIfSpecifiedNameIsEmpty()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			Assert.That(() => scriptableObjectSelection.Select<ScriptableObjectStub>(string.Empty), Throws.ArgumentException);
		}

		[Test]
		public void SelectPassesSpecifiedPathToScriptableObjectLoaderLoadIfPathIsValid()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			var path = "Some/Path";
			scriptableObjectSelection.Select<ScriptableObjectStub>(path);
			scriptableObjectLoader.Received(1).Load<ScriptableObjectStub>(path);
		}

		[Test]
		public void SelectCallsSelectionSetActiveObjectIfScriptableObjectLoaderLoadReturnsNotNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectLoader.Load<ScriptableObjectStub>(Arg.Any<string>()).Returns(scriptableObject);
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			var path = "Some/Path";
			scriptableObjectSelection.Select<ScriptableObjectStub>(path);
			selection.Received(1).SetActiveObject(scriptableObject);
		}

		[Test]
		public void SelectDoesntCallSelectionSetActiveObjectIfScriptableObjectLoaderLoadReturnsNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			scriptableObjectLoader.Load<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			var path = "Some/Path";
			scriptableObjectSelection.Select<ScriptableObjectStub>(path);
			selection.Received(0).SetActiveObject((IScriptableObject<ScriptableObjectStub>)null);
		}

		[Test] 
		public void SelectThrowsArgumentNullExceptionIfSpecifiedScriptableObjectIsNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			Assert.That(() => scriptableObjectSelection.Select((IScriptableObject<ScriptableObjectStub>)null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void SelectPassesSpecifiedScriptableObjectToSelectionSetActiveObjectIfScriptableObejctIsNotNull()
		{
			var scriptableObjectLoader = Substitute.For<IScriptableObjectLoader>();
			var selection = Substitute.For<ISelection>();
			var scriptableObjectSelection = new ScriptableObjectSelection(scriptableObjectLoader, selection);
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectSelection.Select(scriptableObject);
			selection.Received(1).SetActiveObject(scriptableObject);
		}

		#endregion
	}
}