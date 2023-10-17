using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.Base
{
    public class UnmanagedAppContext  : DbContext
    {
        public UnmanagedAppContext()
            : base("name=UnmanagedEntities")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
        
    }
}
