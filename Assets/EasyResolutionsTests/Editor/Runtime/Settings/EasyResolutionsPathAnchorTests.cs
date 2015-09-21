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
	internal class EasyResolutionsPathAnchorTests 
	{
		#region Real
		
		[Test]
		public void RealReturnsThis()
		{
			var pathAnchor = EasyResolutionsPathAnchor.CreateInstance<EasyResolutionsPathAnchor>();
			Assert.That(pathAnchor.Real, Is.SameAs(pathAnchor));
		}
		
		#endregion
	}
}
