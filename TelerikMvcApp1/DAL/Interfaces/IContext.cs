using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IContext<T> : IDisposable where T : class
    {
        IDbSet<T> Entities { get; set; }
        int SaveChanges();
        void SetModified(T entity);        
    }
}
