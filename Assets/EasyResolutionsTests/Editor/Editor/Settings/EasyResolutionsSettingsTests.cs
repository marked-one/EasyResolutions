// Copyright © 2014-2015 Vladimir Klubkov. All rights reserved.
// E-mail: marked.one.more@gmail.com
// This file is part of Easy Resolutions.
// Project page: https://github.com/marked-one/EasyResolutions
// Unity Asset Store page: http://u3d.as/9aa
namespace EasyResolutions
{
	using NUnit.Framework;
	
	[TestFixture]
	[Category("Easy Resolutions")]
	internal class EasyResolutionsSettingsTests 
	{
		#region Real

		[Test]
		public void RealReturnsThis()
		{
			var settings = EasyResolutionsSettings.CreateInstance<EasyResolutionsSettings>();
			Assert.That(settings.Real, Is.SameAs(settings));
		}
		
		#endregion
		#region Logging
		
		[Test] 
		public void LoggingReturnsFalseinitially()
		{
			var settings = EasyResolutionsSettings.CreateInstance<EasyResolutionsSettings>();
			Assert.That(settings.Logging, Is.False);
		}

		[Test]
		public void LoggingSetsAndGets()
		{
			var settings = EasyResolutionsSettings.CreateInstance<EasyResolutionsSettings>();
			settings.Logging = true;
			Assert.That(settings.Logging, Is.True);
			settings.Logging = false;
			Assert.That(settings.Logging, Is.False);
		}
		
		#endregion
	}
}
