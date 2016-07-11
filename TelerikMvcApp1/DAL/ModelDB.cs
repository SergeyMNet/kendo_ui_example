namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class ModelDB : DbContext, IContext
    {
        // Контекст настроен для использования строки подключения "ModelDB" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DAL.ModelDB" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "ModelDB" 
        // в файле конфигурации приложения.
        public ModelDB()
            : base("name=ModelDB")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ModelDB>());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Document> Documents { get; set; }
        
        public void SetModified(Category entity)
        {
            throw new NotImplementedException();
        }

        public void SetModified(Document entity)
        {
            throw new NotImplementedException();
        }        
    }

    public interface IContext : IDisposable
    {        
        IDbSet<Category> Categories { get; set; }
        IDbSet<Document> Documents { get; set; }        

        int SaveChanges();

        void SetModified(Category entity);
        void SetModified(Document entity);     
    }

    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public DateTime Upload_date { get; set; }
        public Status Status { get; set; }

        public byte[] File { get; set; }
        public string Extension { get; set; }

        public int CategoryId { get; set; }
        public int Category_Id { get; set; }
        public virtual Category Category { get; set; }
    }

    public class Category
    {   
        public int Id { get; set; }
        public string Name { get; set; }        
        public int? Parent_id { get; set; }
        public string Image { get; set; }
        
        public int? LeftNode { get; set; }
        public int? RightNode { get; set; }

        public virtual List<Document> Documents { get; set; }
    }
        
    public enum Status
    {
        Unapproved = 1,
        Approved,
        Archived
    }
}