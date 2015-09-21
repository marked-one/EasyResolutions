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
	internal class SettingsPathTests 
	{
		#region Constructor
		
		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfScriptableObjectStaticIsNull()
		{
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			Assert.That(() => new SettingsPath(null, monoScriptStatic, assetDatabase, objectEditorStatic), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfMonoScriptStaticIsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			Assert.That(() => new SettingsPath(scriptableObjectStatic, null, assetDatabase, objectEditorStatic), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfAssetDatabaseIsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			Assert.That(() => new SettingsPath(scriptableObjectStatic, monoScriptStatic, null, objectEditorStatic), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfObjectEditorStaticIsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var assetDatabase = Substitute.For<IAssetDatabase>();
			Assert.That(() => new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorThrowsNothingIfAllArgumentsAreNotNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			Assert.That(() => new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic), Throws.Nothing);
		}
		
		#endregion
		#region Get
		
		[Test] 
		public void GetReturnsEmptyStringIfScriptableObjectStaticCreateInstanceReturnsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns((IScriptableObject<EasyResolutionsPathAnchor>)null);

			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);

			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns("Some/Path");

			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			Assert.That(settingsPath.Get(), Is.Empty);
		}

		[Test] 
		public void GetReturnsEmptyStringIfMonoScriptStaticFromScriptableObjecteReturnsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);

			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns((IMonoScript)null);

			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns("Some/Path");

			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			Assert.That(settingsPath.Get(), Is.Empty);
		}

		[Test] 
		public void GetReturnsEmptyStringIfAssetDatabaseGetAssetPathReturnsNull()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns((string)null);
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			Assert.That(settingsPath.Get(), Is.Empty);
		}
		
		[Test] 
		public void GetReturnsEmptyStringIfAssetDatabaseGetAssetPathReturnsEmptyString()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns(string.Empty);
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			Assert.That(settingsPath.Get(), Is.Empty);
		}

		[Test] 
		public void GetThrowsArgumentExceptionIfAssetDatabaseGetAssetPathReturnsStringConsistingOfWhitespaces()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns("    ");
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			Assert.That(() => settingsPath.Get(), Throws.ArgumentException);
		}

		[Test] 
		public void GetThrowsArgumentExceptionIfAssetDatabaseGetAssetPathReturnsStringWithInvalidCharacters()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);

			var assetDatabase = Substitute.For<IAssetDatabase>();

			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);

			var invalidCharacters = Path.GetInvalidPathChars();
			foreach(var character in invalidCharacters)
			{
				assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns("Some" + character + "/Path");
				Assert.That(() => settingsPath.Get(), Throws.ArgumentException);
			}
		}

		[Test]
		public void GetReturnsPathReturnedByAssetDatabaseGetAssetPathExcludingLastPartIfPathIsValid()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var first = "Some";
			var last = "Path";
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns(first + '/' + last);
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);
			
			Assert.That(settingsPath.Get(), Is.EqualTo(first));
		}

		[Test]
		public void GetCallsObjectEditorStaticDestroyImmediateWithTheParameterReturnedFromScriptableObjectStaticCreateInstance()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var first = "Some";
			var last = "Path";
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns(first + '/' + last);
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);
			
			settingsPath.Get();
			objectEditorStatic.Received(1).DestroyImmediate(scriptableObject);
		}

		[Test]
		public void GetCallsMethodsInProperOrder()
		{
			var scriptableObjectStatic = Substitute.For<IScriptableObjectStatic>();
			var scriptableObject = Substitute.For<IScriptableObject<EasyResolutionsPathAnchor>>();
			scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>().Returns(scriptableObject);
			
			var monoScriptStatic = Substitute.For<IMonoScriptStatic>();
			var monoScript = Substitute.For<IMonoScript>();
			monoScriptStatic.FromScriptableObject(Arg.Any<IScriptableObject<EasyResolutionsPathAnchor>>()).Returns(monoScript);
			
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var first = "Some";
			var last = "Path";
			assetDatabase.GetAssetPath(Arg.Any<IMonoScript>()).Returns(first + '/' + last);
			
			var objectEditorStatic = Substitute.For<IObjectEditorStatic>();
			var settingsPath = new SettingsPath(scriptableObjectStatic, monoScriptStatic, assetDatabase, objectEditorStatic);
			
			settingsPath.Get();
			Received.InOrder(() => {
				scriptableObjectStatic.CreateInstance<EasyResolutionsPathAnchor>();
				monoScriptStatic.FromScriptableObject(scriptableObject);
				assetDatabase.GetAssetPath(monoScript);
				objectEditorStatic.DestroyImmediate(scriptableObject);
			});
		}

		#endregion
	}
}