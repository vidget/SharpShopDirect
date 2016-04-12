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
        public virtual string ItemType { get; set; }
        public virtual string Collection { get; set; }
        public virtual string ItemName { get; set; }
        public virtual int ItemNumber { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Description { get; set; }
        public virtual string Color { get; set; }
        public virtual string Image { get; set; }
          
    }




   
}