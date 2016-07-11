using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDAL : IDisposable
    {
        Document GetDodumentById(long id);
        Category GetCategoriesById(long id);
        Document GetDodumentByName(string name);
        Category GetCategoryByName(string name);
        IQueryable<Document> GetAllDocuments();
        IQueryable<Category> GetAllCategory();        
        IQueryable<Document> GetAllDodumentsByCategoryId(long id);
        IQueryable<Document> GetDocument(Expression<Func<Document, bool>> predicate);        
        IQueryable<Category> GetCategory(Expression<Func<Category, bool>> predicate);
        bool DeleteDocument(Document entity);
        bool DeleteDocument(Expression<Func<Document, bool>> predicate);
        bool DeleteDocumentById(long id);
        bool DeleteCategory(Category entity);
        bool DeleteCategory(Expression<Func<Category, bool>> predicate);
        bool DeleteCategoryById(long id);
        bool InsertDocument(Document entity);
        bool InsertCategory(Category entity);        
        void InsertAllCategory(IEnumerable<Category> entities);
        void InsertAllDocument(IEnumerable<Document> entities);        
        bool UpdateDocument(Document entity);
        bool UpdateCategory(Category entity);        
        void UpsertDocument(Document entity);
        void UpsertCategory(Category entity);
        bool IsUniqueCategory(Category entity);
        bool IsUniqueDocument(Document entity);
        bool IsUniqueCategory(string categoryName);
    }
}
