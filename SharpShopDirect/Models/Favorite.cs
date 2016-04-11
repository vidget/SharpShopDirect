using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpShopDirect.Models
{
    public class Favorite
    {

        public virtual int FavoriteId { get; set; }  
        public virtual string UserId { get; set; }
        public virtual int ItemId { get; set; }

          
    }




   
}