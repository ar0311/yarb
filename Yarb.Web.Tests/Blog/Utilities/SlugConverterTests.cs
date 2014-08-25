﻿using Xunit;
using Yarb.Web.Blog.Utilities;

namespace Yarb.Web.Tests.Blog.Utilities
{
    public class SlugConverterTests
    {
        [Fact]
        public void CorrectSlugConversion()
        {
            string title = "This is a new blog post";
            string slug = title.ToSlug();

            Assert.Equal(slug, "this-is-a-new-blog-post");
        }
    }
    
}
