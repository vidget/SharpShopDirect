using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpShopDirect.Models
{
    public class Blog
    {
        public virtual int Blogid { get; set; }
        public virtual string MyComment { get; set; }
        public virtual DateTime Date { get; set; }
    }
}