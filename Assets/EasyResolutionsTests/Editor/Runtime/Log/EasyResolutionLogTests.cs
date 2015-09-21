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
	internal class LogTests
	{
		#region Constructor

		[Test]
		public void ConstructorThrowsArgumentNullExceptionIfNullDebugAdapterSpecified()
		{
			Assert.That(() => new Log(null), Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ConstructorDoesntThrowIfNonNullDebugAdapterSpecified()
		{
			var debug = Substitute.For<IDebug>();
			Assert.That(() => new Log(debug), Throws.Nothing);
		}

		#endregion
		#region Enabled

		[Test]
		public void DisabledByDefault()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			Assert.That(log.Enabled, Is.False);
		}

		[Test]
		public void EnabledSetsAndGetsValue()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = true;
			Assert.That(log.Enabled, Is.True);
			log.Enabled = false;
			Assert.That(log.Enabled, Is.False);
		}

		#endregion
		#region Info

		[Test]
		public void InfoLogsMessageIfEnabled()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = true;
			log.Info("Some message");
			debug.Received(1).Log(Arg.Any<string>());
		}

		[Test]
		public void InfoLogsMessageIfEnabledAndMessageIsNull()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = true;
			log.Info(null);
			debug.Received(1).Log(Arg.Any<string>());
		}

		[Test]
		public void InfoDoesntLogMessageIfDisabled()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = false;
			log.Info("Some message");
			debug.DidNotReceive().Log(Arg.Any<string>());
		}

		#endregion
		#region Warning

		[Test]
		public void WarningLogsMessageIfEnabled()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = true;
			log.Warning("Some message");
			debug.Received(1).LogWarning(Arg.Any<string>());
		}
		
		[Test]
		public void WarningLogsMessageIfEnabledAndMessageIsNull()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = true;
			log.Warning(null);
			debug.Received(1).LogWarning(Arg.Any<string>());
		}
		
		[Test]
		public void WarningDoesntLogMessageIfDisabled()
		{
			var debug = Substitute.For<IDebug>();
			var log = new Log(debug);
			log.Enabled = false;
			log.Warning("Some message");
			debug.DidNotReceive().LogWarning(Arg.Any<string>());
		}

		#endregion
	}
}