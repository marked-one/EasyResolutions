// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using NUnit.Framework;
	using System;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class EasyResolutionsScenesIndexTests 
	{
		#region Real
		
		[Test]
		public void RealReturnsThis()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(scenesIndex.Real, Is.SameAs(scenesIndex));
		}
		
		#endregion
		#region OnBeforeSerialize

		[Test]
		public void OnBeforeSerializeDoesntFail()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.OnBeforeSerialize();
		}

		#endregion
		#region OnAfterDeserialize

		[Test]
		public void OnAfterDeserializeDoesntFail()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.OnAfterDeserialize();
		}

		#endregion
		#region Indexer

		[Test]
		public void IndexerGetThrowsArgumentExceptionIfNameIsNull()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
#pragma warning disable 219
#pragma warning disable 168
			Assert.That(() => {var temp = scenesIndex[null, "resolution"];}, Throws.ArgumentException);
#pragma warning restore 168
#pragma warning restore 219
		}

		[Test]
		public void IndexerSetThrowsArgumentExceptionIfNameIsNull()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex[null, "resolution"] = "full name", Throws.ArgumentException);
		}

		[Test]
		public void IndexerGetThrowsArgumentExceptionIfNameIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
#pragma warning disable 219
#pragma warning disable 168
			Assert.That(() => {var temp = scenesIndex[string.Empty, "resolution"];}, Throws.ArgumentException);
#pragma warning restore 168
#pragma warning restore 219
		}

		[Test]
		public void IndexerSetThrowsArgumentExceptionIfNameIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex[string.Empty, "resolution"] = "full name", Throws.ArgumentException);
		}

		[Test]
		public void IndexerGetThrowsArgumentNullExceptionIfResolutionIsNull()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
#pragma warning disable 219
#pragma warning disable 168
			Assert.That(() => {var temp = scenesIndex["name", null];}, Throws.Exception.TypeOf<ArgumentNullException>());
#pragma warning restore 168
#pragma warning restore 219
		}

		[Test]
		public void IndexerSetThrowsArgumentNullExceptionIfResolutionIsNull()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex["name", null] = "full name", Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void IndexerGetDoesntThrowIfResolutionIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
#pragma warning disable 219
#pragma warning disable 168
			Assert.That(() => {var temp = scenesIndex["name", string.Empty];}, Throws.Nothing);
#pragma warning restore 168
#pragma warning restore 219
		}

		[Test]
		public void IndexerSetDoesntThrowIfResolutionIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex["name", string.Empty] = "full name", Throws.Nothing);
		}

		[Test]
		public void IndexerSetThrowsArgumentExceptionIfValueIsNull()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex["name", "resolution"] = null, Throws.ArgumentException);
		}

		[Test]
		public void IndexerSetThrowsArgumentExceptionIfValueIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(() => scenesIndex["name", "resolution"] = string.Empty, Throws.ArgumentException);
		}

		[Test]
		public void IndexerSetSetsFullNameIfNameIsNotFoundInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			var resolution = "resolution";
			var fullName = "full name";
			scenesIndex[name, resolution] = fullName;
			Assert.That(scenesIndex[name, resolution], Is.EqualTo(fullName));
		}

		[Test]
		public void IndexerSetSetsFullNameIfResolutionIsNotFoundInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			scenesIndex[name, "other resolution"] = "other full name";
			var resolution = "resolution";
			var fullName = "full name";
			scenesIndex[name, resolution] = fullName;
			Assert.That(scenesIndex[name, resolution], Is.EqualTo(fullName));
		}

		[Test]
		public void IndexerSetSetsNewFullNameIfNameAndResolutionAreFoundInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			var resolution = "resolution";
			scenesIndex[name, resolution] = "other full name";
			var fullName = "full name";
			scenesIndex[name, resolution] = fullName;
			Assert.That(scenesIndex[name, resolution], Is.EqualTo(fullName));
		}

		[Test]
		public void IndexerGetReturnsEmptyStringIfNameIsNotFoundInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(scenesIndex["name", "resolution"], Is.Empty);
		}

		[Test]
		public void IndexerGetReturnsEmptyStringIfResolutionIsNotFoundInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			scenesIndex[name, "other resolution"] = "full name";
			Assert.That(scenesIndex[name, "resolution"], Is.Empty);
		}

		[Test]
		public void IndexerGetReturnsSpecifiedFullNameIfNameAndResolutionArePresentInScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var name = "name";
			var resolution = "resolution";
			var fullName = "full name";
			scenesIndex[name, resolution] = fullName;
			Assert.That(scenesIndex[name, resolution], Is.EqualTo(fullName));
		}

		#endregion
		#region Clear

		[Test]
		public void ClearDoesnFailIfScenesIndexIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.Clear();
		}

		[Test]
		public void ClearClearsScenesIndex()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex["name", "resolution"] = "full name";
			scenesIndex.Clear();
			Assert.That(scenesIndex["name", "resolution"], Is.Empty);
		}
		
		#endregion
		#region GetEnumerator

		[Test]
		public void GetEnumeratorEnumeratesNothingIfScenesIndexIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
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
		public void GetEnumeratorEnumeratesScenesIfScenesIndexIsNotEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex["name", "resolution"] = "full name";
			scenesIndex["name 2", "resolution"] = "full name";
			var count = 0;
#pragma warning disable 219
#pragma warning disable 168
			foreach(var scene in scenesIndex)
#pragma warning restore 168
#pragma warning restore 219
				count++;
			
			Assert.That(count, Is.EqualTo(2));
		}

		[Test]
		public void GetEnumeratorEnumeratesScenesAndResolutionsIfScenesIndexIsNotEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex["name", "resolution"] = "full name";
			scenesIndex["name", "resolution 2"] = "full name 2";
			var count = 0;
			foreach(var scene in scenesIndex)
#pragma warning disable 219
#pragma warning disable 168
				foreach(var resolution in scene.Value)
#pragma warning restore 168
#pragma warning restore 219
					count++;
			
			Assert.That(count, Is.EqualTo(2));
		}

		#endregion
		#region NeedUpdateInfo

		[Test]
		public void NeedUpdateInfoIsFalseInitially()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.False);
		}

		[Test]
		public void NeedUpdateInfoIsNotChangedAfterRealIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var old = scenesIndex.NeedUpdateInfo;
#pragma warning disable 219
#pragma warning disable 168
			var temp = scenesIndex.Real;
#pragma warning restore 168
#pragma warning restore 219
			Assert.That(scenesIndex.NeedUpdateInfo, Is.EqualTo(old));
		}

		[Test]
		public void NeedUpdateInfoIsNotChangedAfterOnBeforeSerializeIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var old = scenesIndex.NeedUpdateInfo;
			scenesIndex.OnBeforeSerialize();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.EqualTo(old));
		}

		[Test]
		public void NeedUpdateInfoIsTrueAfterOnAfterDeserializeIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.OnAfterDeserialize();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.True);
		}

		[Test]
		public void NeedUpdateInfoIsNotChangedAfterIndexerGetIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var old = scenesIndex.NeedUpdateInfo;
#pragma warning disable 219
#pragma warning disable 168
			var temp = scenesIndex["name", "resolution"];
#pragma warning restore 168
#pragma warning restore 219
			Assert.That(scenesIndex.NeedUpdateInfo, Is.EqualTo(old));
		}
		
		[Test]
		public void NeedUpdateInfoIsTrueAfterIndexerSetIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex["name", "resolution"] = "full name";
			Assert.That(scenesIndex.NeedUpdateInfo, Is.True);
		}
		
		[Test]
		public void NeedUpdateInfoIsTrueAfterClearISCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.Clear();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.True);
		}

		[Test]
		public void NeedUpdateInfoIsNotChangedAfterGetEnumeratorIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var old = scenesIndex.NeedUpdateInfo;
			scenesIndex.GetEnumerator();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.EqualTo(old));
		}

		[Test]
		public void NeedUpdateInfoIsNotChangedAfterToStringIsCalled()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			var old = scenesIndex.NeedUpdateInfo;
			scenesIndex.ToString();
			Assert.That(scenesIndex.NeedUpdateInfo, Is.EqualTo(old));
		}

		[Test]
		public void NeedUpdateInfoSetsAndGets()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex.NeedUpdateInfo = true;
			Assert.That(scenesIndex.NeedUpdateInfo, Is.True);
			scenesIndex.NeedUpdateInfo = false;
			Assert.That(scenesIndex.NeedUpdateInfo, Is.False);
		}

		#endregion
		#region ToString

		[Test]
		public void ToStringReturnsEmptyStringIfScenesIndexIsEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			Assert.That(scenesIndex.ToString(), Is.Empty);
		}

		[Test]
		public void ToStringReturnsNonEmptyStringIfScenesIndexIsNotEmpty()
		{
			var scenesIndex = EasyResolutionsScenesIndex.CreateInstance<EasyResolutionsScenesIndex>();
			scenesIndex["name", "resolution"] = "full name";
			Assert.That(scenesIndex.ToString(), Is.Not.Empty);
		}

		#endregion
	}
}
