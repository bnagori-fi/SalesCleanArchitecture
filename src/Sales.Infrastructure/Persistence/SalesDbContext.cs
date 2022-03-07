using Microsoft.EntityFrameworkCore;
using Sales.Application.Contracts.Infrastructure;
using Sales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Persistence
{

    public class SalesDbContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IDateTimeService dateTimeService;

        public DbSet<Domain.Entities.Sales> Sales { get; set; }

        public SalesDbContext(DbContextOptions<SalesDbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService) : base(options)
        {
            this.currentUserService = currentUserService;
            this.dateTimeService = dateTimeService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase<object>>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = dateTimeService.Now;
                        entry.Entity.LastModifiedBy = currentUserService.UserId;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = dateTimeService.Now;
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }
}
