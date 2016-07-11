using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp2.Models
{
    public class CategoryView
    {
        public int id { get; set; }
        public string text { get; set; }
        public int? parent_id { get; set; }
        public string image { get; set; }
        
        public int? leftNode { get; set; }
        public int? rightNode { get; set; }

        public List<CategoryView> items { get; set; }

        public CategoryView() { }

        public CategoryView(Category c)
        {
            id = c.Id;
            text = c.Name;
            parent_id = c.Parent_id;
            image = c.Image;
            items = null;            
            leftNode = c.LeftNode;
            rightNode = c.RightNode;
        }
    }
}