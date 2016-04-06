namespace SharpShopDirect.Migrations
{
 
    using SharpShopDirect.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SharpShopDirect.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SharpShopDirect.Models.ApplicationDbContext context)
        {
            //Commented out after deploy because when you try to register it looks for this local file to reseed, which isn't
            //necessary. There is probaly an easyier way to just read the file from the local server.


            //string txt = @"C:\Users\vidget\Documents\Visual Studio 2013\Projects\Fashion\Fashion\Content\database.txt";
            //List<string> DatabaseInfoList = new List<string>();


            //using (StreamReader readerOfCSV = new StreamReader(txt))
            //{
            //    string dataLine;

            //    while ((dataLine = readerOfCSV.ReadLine()) != null)
            //    {
            //        DatabaseInfoList.Add(dataLine);
            //    }
            //}


            //foreach (var line in DatabaseInfoList)
            //{

            //    string[] itemArray = line.Split(',');

            //    string itemid = itemArray[0];
            //    string itemType = itemArray[1];
            //    string collection = itemArray[2];
            //    string itemNumber = itemArray[3];
            //    string itemName = itemArray[4];
            //    string itemDecription = itemArray[5];
            //    string itemPrice = itemArray[6];
            //    string itemColor = itemArray[7];
            //    string itemImage = itemArray[8];

            //    context.Items.Add(new Item
            //    {

            //        ItemId = Int32.Parse(itemid),
            //        ItemType = itemType,
            //        Collection = collection,
            //        ItemNumber = Int32.Parse(itemNumber),
            //        ItemName = itemName,
            //        Description = itemDecription,
            //        Price = decimal.Parse(itemPrice),
            //        Color = itemColor,
            //        Image = itemImage

            //    });

            //}




            //context.Blogs.Add(new Blog
            //{
            //    Blogid = 0,
            //    Date = DateTime.Now,
            //    MyComment = "Hello, World"

            //});

            //base.Seed(context);
        }
    }
}
