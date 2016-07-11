using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

//namespace TelerikMvcApp1.Models
//{    
//    public class Category
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public int? Parent_id { get; set; }
//        public string Image { get; set; }        
//    }

//    public class Document
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public string Content { get; set; }
//        public DateTime Upload_date { get; set; }
//        public Status Status { get; set; }
//        public int CategoryId { get; set; }
//    }

//    public enum Status
//    {
//        Unapproved = 1, 
//        Approved, 
//        Archived
//    }
    
//    public class DocContext : DbContext
//    {
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Document> Documents { get; set; }        
//    }

//    public interface IDAL<T> : IDisposable where T : class
//    {
//        T GetById(long id);
//        IQueryable<T> GetAll();
//        IQueryable<T> SelectAll();
//        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

//        bool Insert(T entity);
//        void InsertAll(IEnumerable<T> entities);

//        bool Update(T entity);
//        void Upsert(T entity);

//        bool Delete(T entity);
//        bool Delete(Expression<Func<T, bool>> predicate);
//        bool DeleteAll();
//        bool DeleteById(long id);
//        void Refresh();
//    }

//    public class DAL
//    {

//        protected DocContext Context
//        {
//            get { return new DocContext(); }
//        }

//        public virtual Document GetDodumentById(long id)
//        {
//            return Context.Documents.Find(id);
//        }

//        public virtual Category GetCategoriesById(long id)
//        {
//            return Context.Categories.Find(id);
//        }

//        public virtual IQueryable<Document> GetAllDocuments()
//        {
//            return Context.Documents.AsQueryable();
//        }

//        public virtual IQueryable<Category> GetAllCategory()
//        {
//            return Context.Categories.AsQueryable();
//        }

//        public IQueryable<Document> GetDocument(Expression<Func<Document, bool>> predicate)
//        {
//            return Context.Documents.Where(predicate);
//        }

//        public IQueryable<Category> GetCategory(Expression<Func<Category, bool>> predicate)
//        {
//            return Context.Categories.Where(predicate);
//        }

//        public virtual bool DeleteDocument(Document entity)
//        {
//            Context.Documents.Remove(entity);
//            return ApplyChanges();
//        }

//        public bool DeleteDocument(Expression<Func<Document, bool>> predicate)
//        {
//            var entities = GetDocument(predicate).ToList();
//            entities.ForEach(e => Context.Documents.Remove(e));
//            return ApplyChanges();
//        }

//        public virtual bool DeleteDocumentById(long id)
//        {
//            var entity = GetDodumentById(id);
//            if (entity != null)
//            {
//                Context.Documents.Remove(entity);
//                return ApplyChanges();
//            }
//            return false;
//        }

//        public virtual bool DeleteCategory(Category entity)
//        {
//            Context.Categories.Remove(entity);
//            return ApplyChanges();
//        }

//        public bool DeleteCategory(Expression<Func<Category, bool>> predicate)
//        {
//            var entities = GetCategory(predicate).ToList();
//            entities.ForEach(e => Context.Categories.Remove(e));
//            return ApplyChanges();
//        }

//        public virtual bool DeleteCategoryById(long id)
//        {
//            var entity = GetCategoriesById(id);
//            if (entity != null)
//            {
//                Context.Categories.Remove(entity);
//                return ApplyChanges();
//            }
//            return false;
//        }

//        public virtual bool InsertDocument(Document entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }

//            Context.Documents.Add(entity);
//            try
//            {
//                var cnt = Context.SaveChanges();
//                return cnt == 1;
//            }
//            catch (SqlException)
//            {
//                return false;
//            }
//        }

//        public virtual bool InsertCategory(Category entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("entity");
//            }

//            Context.Categories.Add(entity);
//            try
//            {
//                var cnt = Context.SaveChanges();
//                return cnt == 1;
//            }
//            catch (SqlException)
//            {
//                return false;
//            }
//        }

//        public virtual void InsertAllCategory(IEnumerable<Category> entities)
//        {
//            if (entities == null)
//            {
//                throw new ArgumentNullException("entities");
//            }
//            foreach (var entity in entities)
//            {
//                Context.Categories.Add(entity);
//            }
//            Context.SaveChanges();
//        }

//        public virtual void InsertAllDocument(IEnumerable<Document> entities)
//        {
//            if (entities == null)
//            {
//                throw new ArgumentNullException("entities");
//            }
//            foreach (var entity in entities)
//            {
//                Context.Documents.Add(entity);
//            }
//            Context.SaveChanges();
//        }

//        public virtual bool UpdateDocument(Document entity)
//        {
//            var cnt = 0;
//            if (entity != null)
//            {
//                try
//                {
//                    if (!Context.Documents.Contains(entity))
//                    {
//                        Context.Documents.Attach(entity);
//                    }
//                    //Dal.SetModified(entity);                    
//                    cnt = Context.SaveChanges();
//                }
//                catch (DbEntityValidationException ex)
//                {
//                    throw ex;
//                }
//                catch (DbUpdateException exception)
//                {
//                    Console.WriteLine(exception.Message);
//                }
//            }
//            return cnt == 1;
//        }

//        public virtual bool UpdateCategory(Category entity)
//        {
//            var cnt = 0;
//            if (entity != null)
//            {
//                try
//                {
//                    if (!Context.Categories.Contains(entity))
//                    {
//                        Context.Categories.Attach(entity);
//                    }
//                    //Dal.SetModified(entity);                    
//                    cnt = Context.SaveChanges();
//                }
//                catch (DbEntityValidationException ex)
//                {
//                    throw ex;
//                }
//                catch (DbUpdateException exception)
//                {
//                    Console.WriteLine(exception.Message);
//                }
//            }
//            return cnt == 1;
//        }

//        public virtual void UpsertDocument(Document entity)
//        {
//            var item = Context.Documents.Find(entity);
//            if (item == null)
//            {
//                Context.Documents.Add(entity);
//            }
//            Context.SaveChanges();
//        }

//        public virtual void UpsertCategory(Category entity)
//        {
//            var item = Context.Categories.Find(entity);
//            if (item == null)
//            {
//                Context.Categories.Add(entity);
//            }
//            Context.SaveChanges();
//        }

//        protected bool ApplyChanges()
//        {
//            try
//            {
//                Context.SaveChanges();
//                return true;
//            }
//            catch (DbUpdateException)
//            {
//                return false;
//            }
//        }

//        public void Dispose()
//        {
//            if (Context != null)
//            {
//                Context.Dispose();
//            }
//        }
//    }
//}