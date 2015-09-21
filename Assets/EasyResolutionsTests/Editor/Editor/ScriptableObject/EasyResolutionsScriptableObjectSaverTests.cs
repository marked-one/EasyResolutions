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
	internal class ScriptableObjectSaverTests 
	{
		#region Constructor
		
		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfAssetDatabaseIsNull()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			Assert.That(() => new ScriptableObjectSaver(null, editorUtility), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfEditorUtilityIsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			Assert.That(() => new ScriptableObjectSaver(assetDatabase, null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void ConstructorThrowsNothingIfAllArgumentsAreNotNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var editorUtility = Substitute.For<IEditorUtility>();
			Assert.That(() => new ScriptableObjectSaver(assetDatabase, editorUtility), Throws.Nothing);
		}
		
		#endregion
		#region Save
		
		[Test] 
		public void SaveThrowsArgumentNullExceptionIfSpecifiedScriptableObjectIsNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var editorUtility = Substitute.For<IEditorUtility>();
			var scriptableObjectSelection = new ScriptableObjectSaver(assetDatabase, editorUtility);
			Assert.That(() => scriptableObjectSelection.Save<ScriptableObjectStub>((IScriptableObject<ScriptableObjectStub>)null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test] 
		public void SavePassesSpecifiedScriptableObjectToEditorUtilitySetDirtyIfItIsNotNull()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var editorUtility = Substitute.For<IEditorUtility>();
			var scriptableObjectSelection = new ScriptableObjectSaver(assetDatabase, editorUtility);
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectSelection.Save<ScriptableObjectStub>(scriptableObject);
			editorUtility.Received(1).SetDirty(scriptableObject);
		}
		
		[Test]
		public void SaveCallsMethodsInProperOrder()
		{
			var assetDatabase = Substitute.For<IAssetDatabase>();
			var editorUtility = Substitute.For<IEditorUtility>();
			var scriptableObjectSelection = new ScriptableObjectSaver(assetDatabase, editorUtility);
			var scriptableObject = Substitute.For<IScriptableObject<ScriptableObjectStub>>();
			scriptableObjectSelection.Save<ScriptableObjectStub>(scriptableObject);
			Received.InOrder(() => {
				editorUtility.SetDirty(scriptableObject);
				assetDatabase.SaveAssets();
				assetDatabase.Refresh();
			});
		}
		
		#endregion
	}
}