// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using System;
	using NUnit.Framework;
	using NSubstitute;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class ProgressSegmentTests 
	{
		#region Constructor
		
		[Test]
		public void Constructor_IfProgressIsNull_ThrowsArgumentException()
		{
			Assert.That(() => new ProgressSegment(null), Throws.Exception.TypeOf<ArgumentNullException>());
		}
		
		[Test]
		public void Constructor_IfProgressIsNotNull_ThrowsNothing()
		{
			var progress = Substitute.For<IProgress>();
			Assert.That(() => new ProgressSegment(progress), Throws.Nothing);
		}

		#endregion
		#region Finished
		
		[Test]
		public void Finished_IfNotYetStarted_IsTrue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			Assert.That(segment.Finished, Is.True);
		}
		
		[Test]
		public void Finished_IfStarted_IsFalse()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That(segment.Finished, Is.False);
		}
		
		[Test]
		public void Finished_IfFinished_IsTrue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			Assert.That(segment.Finished, Is.True);
		}
		
		#endregion
		#region Description
		
		[Test]
		public void Description_IfNullDescriptionSet_ReturnsEmptyString()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = null;
			Assert.That (segment.Description, Is.Empty);
		}
		
		[Test]
		public void Description_IfEmptyStringDescriptionSet_ReturnsEmptyString()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = string.Empty;
			Assert.That (segment.Description, Is.Empty);
		}
		
		[Test]
		public void Description_IfValidStringDescriptionSet_ReturnThatString()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			Assert.That (segment.Description, Is.EqualTo("Some description"));
		}
		
		#endregion
		#region Start
		
		[Test]
		public void Start_IfNegativeValueSpecified_ThrowsArgumentException()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			Assert.That(() => segment.Start(-1), Throws.ArgumentException);
		}
		
		[Test]
		public void Start_IfZeroValueSpecified_ThrowsArgumentException()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			Assert.That(() => segment.Start(0), Throws.ArgumentException);
		}
		
		[Test]
		public void Start_IfAlreadyStarted_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Start(1), Is.False);
		}
		
		[Test]
		public void Start_IfAlreadyStarted_DoesntCallProgressStart()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Start(1);
			progress.Received(0).Start(Arg.Any<int>());
		}

		[Test]
		public void Start_IfAlreadyStarted_DoesntCallProgressFinished()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Start(1);
#pragma warning disable 219
#pragma warning disable 168
			var temp = progress.Received(0).Finished;
#pragma warning restore 168
#pragma warning restore 219
		}

		[Test]
		public void Start_IfAlreadyStarted_DoesntCallProgressDescription()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Start(1);
			progress.Received(0).Description = Arg.Any<string>();
		}

		[Test]
		public void Start_IfAlreadyStarted_DoesntCallProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Start(1);
			progress.Received(0).Add(Arg.Any<float>());
		}

		[Test]
		public void Start_IfNotStarted_CallsProgressDescriptionWithDescriptionValueTwice()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			segment.Start(1);
			progress.Received(2).Description = segment.Description;
		}

		[Test]
		public void Start_IfProgressFinishedReturnsFalse_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			progress.Finished.Returns(false);
			var segment = new ProgressSegment(progress);
			Assert.That (segment.Start(1), Is.False);
		}

		[Test]
		public void Start_IfProgressFinishedReturnsTrue_CallsProgressStartOnceWithValueOf1()
		{
			var progress = Substitute.For<IProgress>();
			progress.Finished.Returns(true);
			var segment = new ProgressSegment(progress);
			segment.Start(5);
			progress.Received(1).Start (1);
		}

		[Test]
		public void Start_IfNotStarted_CallsProgressAddWithZeroValue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			segment.Start(1);
			progress.Received(1).Add (0);
		}

		[Test]
		public void Start_IfProgressFinishedReturnsTrueAndProgressStartReturnsFalse_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			progress.Finished.Returns(true);
			progress.Start(Arg.Any<int>()).Returns(false);
			var segment = new ProgressSegment(progress);
			Assert.That (segment.Start(1), Is.False);
		}

		[Test]
		public void Start_IfProgressFinishedReturnsTrueAndProgressStartReturnstrue_ReturnsTrue()
		{
			var progress = Substitute.For<IProgress>();
			progress.Finished.Returns(true);
			progress.Start(Arg.Any<int>()).Returns(true);
			var segment = new ProgressSegment(progress);
			Assert.That (segment.Start(1), Is.True);
		}

		[Test]
		public void Start_IfNotStarted_CallsProgressMethodsInAppropriateOrder()
		{
			var progress = Substitute.For<IProgress>();
			progress.Finished.Returns(true);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Received.InOrder (() => 
			{
				progress.Description = Arg.Any<string>();
#pragma warning disable 219
#pragma warning disable 168
				var temp = progress.Finished;
#pragma warning restore 168
#pragma warning restore 219
				progress.Start(1);
				progress.Description = Arg.Any<string>();
				progress.Add(0);
			});
		}
		
		#endregion
		#region Next

		[Test]
		public void Next_IfAlreadyFinished_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			Assert.That (segment.Next(), Is.False);
		}
		
		[Test]
		public void Next_IfAlreadyFinished_DoesntCallProgressDescription()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Next();
			progress.Received(0).Description = Arg.Any<string>();
		}
		
		[Test]
		public void Next_IfNotFinished_CallsProgressDescriptionWithDescriptionValue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Next();
			progress.Received(1).Description = segment.Description;
		}

		[Test]
		public void Next_IfAlreadyFinished_DoesntCallProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Next();
			progress.Received(0).Add(Arg.Any<float>());
		}

		[Test]
		public void Next_IfNotFinished_DoesCallProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Next();
			progress.Received(1).Add(Arg.Any<float>());
		}
		
		[Test]
		public void Next_IfNotFinishedAndProgressAddReturnsFalse_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			progress.Add (Arg.Any<float>()).Returns(false);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Next(), Is.False);
		}
		
		[Test]
		public void Next_IfNotFinishedAndProgressAddReturnsTrue_ReturnsTrue()
		{
			var progress = Substitute.For<IProgress>();
			progress.Add (Arg.Any<float>()).Returns(true);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Next(), Is.True);
		}
		
		[Test]
		public void Next_IfNotFinished_PassesValidValueToProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			var stepsCount = 10;
			segment.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				progress.Received(1).Add((float)i / (float)stepsCount);
				progress.ClearReceivedCalls();
				segment.Next ();
			}
		}

		#endregion
		#region Add
		
		[Test]
		public void Add_IfNegativeValueSpecified_ThrowsArgumentException()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That(() => segment.Add(-1), Throws.ArgumentException);
		}
		
		[Test]
		public void Add_IfValueGreaterThan1Specified_ThrowsArgumentException()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That(() => segment.Add(1.1f), Throws.ArgumentException);
		}

		[Test]
		public void Add_IfAlreadyFinished_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			Assert.That (segment.Add(0.5f), Is.False);
		}
		
		[Test]
		public void Add_IfAlreadyFinished_DoesntCallProgressDescription()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Add(0.5f);
			progress.Received(0).Description = Arg.Any<string>();
		}
		
		[Test]
		public void Add_IfNotFinished_CallsProgressDescriptionWithDescriptionValue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Add(0.5f);
			progress.Received(1).Description = segment.Description;
		}
		
		[Test]
		public void Add_IfAlreadyFinished_DoesntCallProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Add(0.5f);
			progress.Received(0).Add(Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotFinished_DoesCallProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Add(0.5f);
			progress.Received(1).Add(Arg.Any<float>());
		}
		
		[Test]
		public void Add_IfNotFinishedAndProgressAddReturnsFalse_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			progress.Add (Arg.Any<float>()).Returns(false);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Add(0.5f), Is.False);
		}
		
		[Test]
		public void Add_IfNotFinishedAndProgressAddReturnsTrue_ReturnsTrue()
		{
			var progress = Substitute.For<IProgress>();
			progress.Add (Arg.Any<float>()).Returns(true);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Add(0.5f), Is.True);
		}
		
		[Test]
		public void Add_IfNotFinished_PassesValidValueToProgressAdd()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			var stepsCount = 10;
			segment.Start(stepsCount);
			for(var i = 0; i < stepsCount; i++)
			{
				progress.ClearReceivedCalls();
				segment.Add (0.5f);
				progress.Received(1).Add(((float)i + 0.5f)/ (float)stepsCount);
				segment.Next();
			}
		}

		#endregion
		#region Finish

		[Test]
		public void Finish_IfAlreadyFinished_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			Assert.That (segment.Finish(), Is.False);
		}

		[Test]
		public void Finish_IfAlreadyFinished_DoesntCallProgressDescription()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Finish();
			progress.Received(0).Description = Arg.Any<string>();
		}
		
		[Test]
		public void Finish_IfNotFinished_CallsProgressDescriptionWithDescriptionValue()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Description = "Some description";
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Finish();
			progress.Received(1).Description = segment.Description;
		}
		
		[Test]
		public void Finish_IfAlreadyFinished_DoesntCallProgressNext()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			segment.Finish();
			progress.ClearReceivedCalls();
			segment.Finish();
			progress.Received(0).Next();
		}
		
		[Test]
		public void Finish_IfNotFinished_DoesCallProgressNext()
		{
			var progress = Substitute.For<IProgress>();
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			progress.ClearReceivedCalls();
			segment.Finish();
			progress.Received(1).Next();
		}

		[Test]
		public void Finish_IfNotFinishedAndProgressNextReturnsFalse_ReturnsFalse()
		{
			var progress = Substitute.For<IProgress>();
			progress.Next().Returns(false);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Finish(), Is.False);
		}
		
		[Test]
		public void Finish_IfNotFinishedAndProgressNextReturnsTrue_ReturnsTrue()
		{
			var progress = Substitute.For<IProgress>();
			progress.Next ().Returns(true);
			var segment = new ProgressSegment(progress);
			segment.Start(1);
			Assert.That (segment.Finish(), Is.True);
		}

		#endregion
	}
}
