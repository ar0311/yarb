using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yarb.Web.Blog
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        public bool AllowComments { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
        public string Content { get; set; }

    }

    public class Comment
    {
        public string Id { get; set; }
        public DateTimeOffset Created { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }
    }
    
}