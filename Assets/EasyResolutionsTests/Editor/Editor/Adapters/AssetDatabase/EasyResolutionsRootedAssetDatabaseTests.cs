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
	using UnityEngine;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class RootedAssetDatabaseTests 
	{
		#region Constructor
		
		[Test]
		public void Constructor_IfAssetDatabaseIsNull_ThrowsArgumentNullException()
		{
			Assert.That(() => new RootedAssetDatabase(null, "Root"), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfRootIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			Assert.That(() => new RootedAssetDatabase(assetDatabase, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfAllArgumentsAreValid_ThrowsNothing()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			Assert.That(() => new RootedAssetDatabase(assetDatabase, string.Empty), Throws.Nothing);
			Assert.That(() => new RootedAssetDatabase(assetDatabase, "Root"), Throws.Nothing);
		}
		
		#endregion
		#region GetAssetPath
		
		[Test]
		public void GetAssetPath_IfObjectIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			Assert.That(() => rootedAssetDatabase.GetAssetPath<ScriptableObjectStub>(null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void GetAssetPath_CallsAssetDatabaseCreateAssetOnce()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			rootedAssetDatabase.GetAssetPath(scriptableObject);
			assetDatabase.Received(1).GetAssetPath(Arg.Any<IObject<ScriptableObjectStub>>());
		}
		
		[Test]
		public void GetAssetPath_PassesSpecifedObjectToAssetDatabaseGetAssetPath()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			rootedAssetDatabase.GetAssetPath(scriptableObject);
			assetDatabase.Received().GetAssetPath(scriptableObject);
		}

		[Test]
		public void GetAssetPath_IfAssetDatabaseGetAssetPathReturnedNull_ReturnsEmptyString()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IObject<ScriptableObjectStub>>()).Returns((string)null);
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			Assert.That(rootedAssetDatabase.GetAssetPath(scriptableObject), Is.Empty);
		}
		
		[Test]
		public void GetAssetPath_IfAssetDatabaseGetAssetPathReturnedEmptyString_ReturnsEmptyString()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IObject<ScriptableObjectStub>>()).Returns(string.Empty);
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			Assert.That(rootedAssetDatabase.GetAssetPath(scriptableObject), Is.Empty);
		}
		
		[Test]
		public void GetAssetPath_IfAssetDatabaseGetAssetPathReturnsPathThatDoesntStartWithRoot_ReturnsEmptyString()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.GetAssetPath(Arg.Any<IObject<ScriptableObjectStub>>()).Returns("Invalid path");
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			Assert.That(rootedAssetDatabase.GetAssetPath(scriptableObject), Is.Empty);
		}

		[Test]
		public void GetAssetPath_IfReturnedPathStartsWithRoot_RemovesRootFromPathReturnedByAssetDatabaseGetAssetPath()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var root = "Root";
			var relativePath = "Some/Path";
			assetDatabase.GetAssetPath(Arg.Any<IObject<ScriptableObjectStub>>()).Returns(root + '/' + relativePath);
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, root);
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			var result = rootedAssetDatabase.GetAssetPath(scriptableObject);
			Assert.That(result.StartsWith(root), Is.False);
			Assert.That(result.EndsWith(relativePath));
		}

		#endregion
		#region CreateAsset
		
		[Test]
		public void CreateAsset_IfObjectIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			Assert.That(() => rootedAssetDatabase.CreateAsset<ScriptableObjectStub>(null, "Some/Path"), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void CreateAsset_IfPathIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			Assert.That(() => rootedAssetDatabase.CreateAsset(scriptableObject, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void CreateAsset_IfPathContainsInvalidChars_ThrowsArgumentException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			var chars = Path.GetInvalidPathChars();
			foreach(var @char in chars)
				Assert.That(() => rootedAssetDatabase.CreateAsset(scriptableObject, "Some/" + @char + "Path"), Throws.ArgumentException);
		}
		
		[Test]
		public void CreateAsset_CallsAssetDatabaseCreateAssetOnce()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			rootedAssetDatabase.CreateAsset(scriptableObject, "Some/Path");
			assetDatabase.Received(1).CreateAsset(Arg.Any<IObject<ScriptableObjectStub>>(), Arg.Any<string>());
		}
		
		[Test]
		public void CreateAsset_PrependsSpecifiedPathWithRootAndPassesItToAssetDatabaseCreateAsset()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var root = "Root";
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, root);
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			var path = "Some/Path";
			rootedAssetDatabase.CreateAsset(scriptableObject, path);
			assetDatabase.Received().CreateAsset(Arg.Any<IObject<ScriptableObjectStub>>(), Arg.Is<string>(str => str.StartsWith(root) && str.EndsWith(path)));
		}
		
		[Test]
		public void CreateAsset_PassesSpecifedObjectToAssetDatabaseCreateAsset()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var scriptableObject = Substitute.For<IObject<ScriptableObjectStub>>();
			rootedAssetDatabase.CreateAsset(scriptableObject, "Some/Path");
			assetDatabase.Received().CreateAsset(scriptableObject, Arg.Any<string>());
		}
		
		#endregion
		#region LoadAssetAtPath

		[Test]
		public void LoadScriptableObjectAtPath_IfPathIsNull_ThrowsArgumentNullException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			Assert.That(() => rootedAssetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void LoadScriptableObjectAtPath_IfPathContainsInvalidChars_ThrowsArgumentException()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			var chars = Path.GetInvalidPathChars();
			foreach(var @char in chars)
				Assert.That(() => rootedAssetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>("Some/" + @char + "Path"), Throws.ArgumentException);
		}
		
		[Test]
		public void LoadScriptableObjectAtPath_CallsAssetDatabaseLoadScriptableObjectAtPathOnce()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			rootedAssetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>("Some/Path");
			assetDatabase.Received(1).LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>());
		}

		[Test]
		public void LoadScriptableObjectAtPath_PrependsSpecifiedPathWithRootAndPassesItToAssetDatabaseLoadScriptableObjectAtPath()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var root = "Root";
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, root);
			var path = "Some/Path";
			rootedAssetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(path);
			assetDatabase.Received().LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Is<string>(str => str.StartsWith(root) && str.EndsWith(path)));
		}

		[Test]
		public void LoadScriptableObjectAtPath_ReturnsScriptableObjectReturnedByAssetDatabaseLoadScriptableObjecAtPath()
		{
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			var assetDatabase = Substitute.For<IAssetDatabase>();
			assetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>(Arg.Any<string>()).Returns(scriptableObject);
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			Assert.That(rootedAssetDatabase.LoadScriptableObjectAtPath<ScriptableObjectStub>("Some/Path"), Is.SameAs(scriptableObject));
		}

		#endregion
		#region SaveAssets
		
		[Test]
		public void SaveAssets_CallsAssetDatabaseSaveAssetsOnce()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			rootedAssetDatabase.SaveAssets();
			assetDatabase.Received(1).SaveAssets();
		}
		
		#endregion
		#region Refresh

		[Test]
		public void Refresh_CallsAssetDatabaseRefreshOnce()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var rootedAssetDatabase = new RootedAssetDatabase(assetDatabase, "Root");
			rootedAssetDatabase.Refresh();
			assetDatabase.Received(1).Refresh();
		}

		#endregion
	}
}
