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
	using System.IO;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class ScriptableObjectLoaderTests 
	{
		#region Constructor

		[Test]
		public void Constructor_IfAssetDatabaseIsNull_ThrowsArgumentNullException()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			Assert.That(() => new ScriptableObjectLoader(null, scriptableObjectStatic, settingsPath), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfScriptableObjectStaticIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var settingsPath = Substitute.For<ISettingsPath>();
			Assert.That(() => new ScriptableObjectLoader(assetDatabase, null, settingsPath), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfSettingsPathIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			Assert.That(() => new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfAllArgumentsAreNotNull_ThrowsNothing()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			Assert.That(() => new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath), Throws.Nothing);
		}
		
		#endregion
		#region Load

		[Test]
		public void Load_IfAssetNameIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			Assert.That(() => scriptableObjectLoader.Load<ScriptableObjectStub>((string)null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Load_IfAssetNameContainsInvalidChars_ThrowsArgumentException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			settingsPath.Get().Returns("Settings/Path");
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var chars = Path.GetInvalidPathChars();
			foreach(var character in chars)
				Assert.That(() => scriptableObjectLoader.Load<ScriptableObjectStub>("Some" + character + ".asset"), Throws.ArgumentException);
		}

		[Test]
		public void Load_IfResultOfSettingsPathGetIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			settingsPath.Get().Returns((string)null);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			Assert.That(() => scriptableObjectLoader.Load<ScriptableObjectStub>("Some.asset"), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Load_IfResultOfSettingsPathGetContainsInvalidChars_ThrowsArgumentException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var chars = Path.GetInvalidPathChars();
			foreach(var @char in chars)
			{
				settingsPath.Get().Returns("Settings/" + @char + "Path");
				Assert.That(() => scriptableObjectLoader.Load<ScriptableObjectStub>("Some.asset"), Throws.ArgumentException);
			}
		}

		[Test] 
		public void Load_PassesSpecifiedPathAppendedToResultOfSettingsPathGetToAssetDatabaseLoadScriptableObjectAtPath()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			scriptableObjectLoader.Load<ScriptableObjectStub>(assetName);
			assetDatabase.Received (1).LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Is<string>(str => str.StartsWith(settingsPathValue) && str.EndsWith(assetName)));
		}
		
		[Test] 
		public void Load_IfResultOfAssetDatabaseLoadScriptableObjectAtPathIsNotNull_ReturnsResultOfAssetDatabaseLoadScriptableObjectAtPath()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns(scriptableObject);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			Assert.That(scriptableObjectLoader.Load<ScriptableObjectStub>(assetName), Is.SameAs(scriptableObject));
		}

		[Test] 
		public void Load_IfResultOfAssetDatabaseLoadScriptableObjectAtPathAndResultOfScriptableObjectStaticCreateInstanceAreNull_ReturnsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			scriptableObjectStatic.CreateInstance<ScriptableObjectStub>().Returns((IScriptableObject<ScriptableObjectStub>)null);
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			Assert.That(scriptableObjectLoader.Load<ScriptableObjectStub>(assetName), Is.Null);
		}

		[Test] 
		public void Load_ReturnsResultOfScriptableObjectStaticCreateInstanceIfItIsNotNullAndIfResultOfAssetDatabaseLoadScriptableObjectAtPathIsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectStatic.CreateInstance<ScriptableObjectStub>().Returns(scriptableObject);
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			Assert.That(scriptableObjectLoader.Load<ScriptableObjectStub>(assetName), Is.SameAs(scriptableObject));
		}

		[Test]
		public void LoadPassesSpecifiedPathAppendedToResultOfSettingsPathGetToAssetDatabaseCreateAssetIfResultOfAssetDatabaseLoadScriptableObjectAtPathIsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectStatic.CreateInstance<ScriptableObjectStub>().Returns(scriptableObject);
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			scriptableObjectLoader.Load<ScriptableObjectStub>(assetName);
			assetDatabase.CreateAsset<ScriptableObjectStub>(Arg.Any<IObject<ScriptableObjectStub>>(), Arg.Is<string>(str => str.StartsWith(settingsPathValue) && str.EndsWith(assetName)));
		}

		[Test]
		public void LoadPassesResultOfScriptableObjectStaticCreateInstanceToAssetDatabaseCreateAssetIfResultOfAssetDatabaseLoadScriptableObjectAtPathIsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectStatic.CreateInstance<ScriptableObjectStub>().Returns(scriptableObject);
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			scriptableObjectLoader.Load<ScriptableObjectStub>(assetName);
			assetDatabase.CreateAsset<ScriptableObjectStub>(scriptableObject, Arg.Any<string>());
		}

		[Test]
		public void LoadCallsMethodsInAppropriateOrderIfResultOfAssetDatabaseLoadScriptableObjectAtPathIsNullAndResultOfScriptableObjectStaticCreateInstanceIsNotNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns((IScriptableObject<ScriptableObjectStub>)null);
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectStatic.CreateInstance<ScriptableObjectStub>().Returns(scriptableObject);
			var settingsPath = Substitute.For<ISettingsPath>();
			var settingsPathValue = "Settings/Path";
			settingsPath.Get().Returns(settingsPathValue);
			var scriptableObjectLoader = new ScriptableObjectLoader(assetDatabase, scriptableObjectStatic, settingsPath);
			var assetName = "Some.asset";
			scriptableObjectLoader.Load<ScriptableObjectStub>(assetName);
			Received.InOrder(() => {
				settingsPath.Get();
				assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>());
				scriptableObjectStatic.CreateInstance<ScriptableObjectStub>();
				assetDatabase.CreateAsset (scriptableObject, Arg.Any<string>());
				assetDatabase.SaveAssets();
				assetDatabase.Refresh();
			});
		}
		
		#endregion
	}
}