using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foundation.Macaw.AMP.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MetaDescription { get; set; }
        public string Keywords { get; set; }
        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public string AuthorId { get; set; }
        public string Author { get; set; }

        public List<string> Tags { get; set; }

        public BlogPost()
        {
            Tags = new List<string>();
        }
    }
}