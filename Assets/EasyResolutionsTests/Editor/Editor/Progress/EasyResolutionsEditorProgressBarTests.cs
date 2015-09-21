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
	internal class EditorProgressBarTests 
	{
		#region Constructor

		[Test]
		public void Constructor_IfEditorUtilityIsNull_ThrowsArgumentException()
		{
			Assert.That(() => new EditorProgressBar(null, true, "Title"), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_IfEditorUtilityIsNotNull_ThrowsNothing()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			Assert.That(() => new EditorProgressBar(editorUtility, true, "Title"), Throws.Nothing);
		}

		[Test]
		public void Constructor_IfTitleIsNull_SetsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, null);
			Assert.That (progressBar.Title, Is.Empty);
		}

		[Test]
		public void Constructor_IfTitleIsEmptyString_SetsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, string.Empty);
			Assert.That (progressBar.Title, Is.Empty);
		}

		[Test]
		public void Constructor_IfTitleIsValidString_SetsThatString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That (progressBar.Title, Is.EqualTo("Title"));
		}

		[Test]
		public void Constructor_IfFalseSpecifed_SetsFalseToCancelable()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			Assert.That (progressBar.Cancelable, Is.False);
		}

		[Test]
		public void Constructor_IfTrueSpecifed_SetsTrueToCancelable()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That (progressBar.Cancelable, Is.True);
		}

		#endregion
		#region Title

		[Test]
		public void Title_IfNullTitleSet_ReturnsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Title = null;
			Assert.That (progressBar.Title, Is.Empty);
		}

		[Test]
		public void Title_IfEmptyStringTitleSet_ReturnsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Title = string.Empty;
			Assert.That (progressBar.Title, Is.Empty);
		}

		[Test]
		public void Title_IfValidStringTitleSet_ReturnThatString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Title = "Some title";
			Assert.That (progressBar.Title, Is.EqualTo("Some title"));
		}

		#endregion
		#region Cancelable

		[Test]
		public void Cancelable_IfFalseSet_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Cancelable = false;
			Assert.That (progressBar.Cancelable, Is.False);
		}

		[Test]
		public void Cancelable_IfTrueSet_ReturnsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Cancelable = true;
			Assert.That (progressBar.Cancelable, Is.True);
		}

		#endregion
		#region Finished

		[Test]
		public void Finished_IfNotYetStarted_IsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That(progressBar.Finished, Is.True);
		}

		[Test]
		public void Finished_IfStarted_IsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That(progressBar.Finished, Is.False);
		}

		[Test]
		public void Finished_IfFinished_IsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			Assert.That(progressBar.Finished, Is.True);
		}

		#endregion
		#region Description

		[Test]
		public void Description_IfNullDescriptionSet_ReturnsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Description = null;
			Assert.That (progressBar.Description, Is.Empty);
		}
		
		[Test]
		public void Description_IfEmptyStringDescriptionSet_ReturnsEmptyString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Description = string.Empty;
			Assert.That (progressBar.Description, Is.Empty);
		}
		
		[Test]
		public void Description_IfValidStringDescriptionSet_ReturnThatString()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Description = "Some description";
			Assert.That (progressBar.Description, Is.EqualTo("Some description"));
		}

		#endregion
		#region Start

		[Test]
		public void Start_IfNegativeValueSpecified_ThrowsArgumentException()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That(() => progressBar.Start(-1), Throws.ArgumentException);
		}

		[Test]
		public void Start_IfZeroValueSpecified_ThrowsArgumentException()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That(() => progressBar.Start(0), Throws.ArgumentException);
		}

		[Test]
		public void Start_IfAlreadyStarted_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Start(1), Is.False);
		}

		[Test]
		public void Start_IfAlreadyStarted_DoesntCallDisplayCancelableProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Start(1);
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Start_IfAlreadyStarted_DoesntCallDisplayProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Start(1);
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Start_IfNotCancelable_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			Assert.That (progressBar.Start(1), Is.False);
		}

		[Test]
		public void Start_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsFalse_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(false);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That (progressBar.Start(1), Is.False);
		}

		[Test]
		public void Start_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsTrue_ReturnsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(true);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			Assert.That (progressBar.Start(1), Is.True);
		}

		[Test]
		public void Start_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, true, title);
			progressBar.Start(1);
			editorUtility.Received (1).DisplayCancelableProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Start_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}

		[Test]
		public void Start_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsZeroProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), 0f);
		}

		[Test]
		public void Start_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, false, title);
			progressBar.Start(1);
			editorUtility.Received (1).DisplayProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Start_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}
		
		[Test]
		public void Start_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsZeroProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), 0f);
		}

		[Test]
		public void Start_IfCancelable_EditorUtilityDisplayProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Start_IfNotCancelable_EditorUtilityDisplayCancelableProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		#endregion
		#region Next

		[Test]
		public void Next_IfAlreadyFinished_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			Assert.That (progressBar.Next(), Is.False);
		}

		[Test]
		public void Next_IfAlreadyFinished_DoesntCallDisplayCancelableProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfAlreadyFinished_DoesntCallDisplayProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Next_IfNotCancelable_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Next(), Is.False);
		}
		
		[Test]
		public void Next_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsFalse_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(false);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Next(), Is.False);
		}
		
		[Test]
		public void Next_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsTrue_ReturnsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(true);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Next(), Is.True);
		}

		[Test]
		public void Next_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, true, title);
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received (1).DisplayCancelableProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsValidProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			var stepsCount = 10;
			progressBar.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), (float)i / (float)stepsCount);
				editorUtility.ClearReceivedCalls();
				progressBar.Next();
			}
		}
		
		[Test]
		public void Next_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, false, title);
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received (1).DisplayProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsValidProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			var stepsCount = 10;
			progressBar.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), (float)i / (float)stepsCount);
				editorUtility.ClearReceivedCalls();
				progressBar.Next();
			}
		}
		
		[Test]
		public void Next_IfCancelable_EditorUtilityDisplayProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfNotCancelable_EditorUtilityDisplayCancelableProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Next();
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		#endregion
		#region Add

		[Test]
		public void Add_IfNegativeValueSpecified_ThrowsArgumentException()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That(() => progressBar.Add (-1f), Throws.ArgumentException);
		}

		[Test]
		public void Add_IfValueGreaterThan1Specified_ThrowsArgumentException()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That(() => progressBar.Add (1.1f), Throws.ArgumentException);
		}

		[Test]
		public void Add_IfAlreadyFinished_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			Assert.That (progressBar.Add(0.5f), Is.False);
		}

		[Test]
		public void Add_IfAlreadyFinished_DoesntCallDisplayCancelableProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		[Test]
		public void Add_IfAlreadyFinished_DoesntCallDisplayProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotCancelable_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Add(0.5f), Is.False);
		}
		
		[Test]
		public void Add_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsFalse_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(false);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Add(0.5f), Is.False);
		}
		
		[Test]
		public void Add_IfCancelableAndEditorUtilityDisplayCancelableProgressBarReturnsTrue_ReturnsTrue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			editorUtility.DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>()).Returns(true);
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			Assert.That (progressBar.Add(0.5f), Is.True);
		}
		
		[Test]
		public void Add_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, true, title);
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received (1).DisplayCancelableProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfCancelable_EditorUtilityDisplayCancelableProgressBarObtainsValidProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			var stepsCount = 10;
			progressBar.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				editorUtility.ClearReceivedCalls();
				progressBar.Add(0.5f);
				editorUtility.Received (1).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), ((float)i + 0.5f) / (float)stepsCount);
				progressBar.Next();
			}
		}
		
		[Test]
		public void Add_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedTitle()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var title = "Some title";
			var progressBar = new EditorProgressBar(editorUtility, false, title);
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received (1).DisplayProgressBar(title, Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsSpecifiedDescription()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			var description = "Some description";
			progressBar.Description = description;
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), description, Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotCancelable_EditorUtilityDisplayProgressBarObtainsValidProgressValue()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			var stepsCount = 10;
			progressBar.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				editorUtility.ClearReceivedCalls();
				progressBar.Add(0.5f);
				editorUtility.Received (1).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), ((float)i + 0.5f) / (float)stepsCount);
				progressBar.Next();
			}
		}
		
		[Test]
		public void Add_IfCancelable_EditorUtilityDisplayProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, true, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received(0).DisplayProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotCancelable_EditorUtilityDisplayCancelableProgressBarIsNotCalled()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Add(0.5f);
			editorUtility.Received(0).DisplayCancelableProgressBar(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<float>());
		}

		#endregion
		#region Finish

		[Test]
		public void Finish_IfAlreadyFinished_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			Assert.That(progressBar.Finish(), Is.False);
			progressBar.Start(1);
			progressBar.Finish();
			Assert.That(progressBar.Finish(), Is.False);
		}

		[Test]
		public void Finish_IfAlreadyFinished_DoesntCallEditorUtilityClearProgressBar()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			progressBar.Finish();
			editorUtility.ClearReceivedCalls();
			progressBar.Finish();
			editorUtility.Received(0).ClearProgressBar();
		}

		[Test]
		public void Finish_IfNotYetFinished_ReturnsFalse()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			Assert.That(progressBar.Finish(), Is.False);
		}

		[Test]
		public void Finish_IfNotYetFinished_CallsEditorUtilityClearProgressBarOnce()
		{
			var editorUtility = Substitute.For<IEditorUtility>();
			var progressBar = new EditorProgressBar(editorUtility, false, "Title");
			progressBar.Start(1);
			editorUtility.ClearReceivedCalls();
			progressBar.Finish();
			editorUtility.Received(1).ClearProgressBar();
		}

		#endregion
	}
}
