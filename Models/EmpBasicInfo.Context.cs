using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CRUDAppAJS
{
    public partial class EmployeeDB : DbContext
    {
        public EmployeeDB()
            : base("name=EmployeeDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmpBasicInfo> EmpBasicInfo { get; set; }
    }
}
