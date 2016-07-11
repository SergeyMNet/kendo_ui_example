using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelerikMvcApp2.Models
{
    public class DocumentView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public string Extension { get; set; }
        public bool IsHasFile { get; set; }
        public DateTime Upload_date { get; set; }        
        [UIHint("StatusEditor")]
        public Status Status { get; set; }
        [UIHint("CategoryEditor")]
        public CategoryViewModel Category { get; set; }

        public DocumentView() { }

        public DocumentView(Document d)
        {
            Id = d.Id;
            Name = d.Name;
            Description = d.Description;
            Extension = d.Extension;
            Upload_date = d.Upload_date;
            Status = d.Status;
            IsHasFile = !String.IsNullOrEmpty(d.Extension) ? true : false;
            Category = new CategoryViewModel { CategoryID = d.CategoryId, CategoryName = d.Category.Name };
        }
    }        
}