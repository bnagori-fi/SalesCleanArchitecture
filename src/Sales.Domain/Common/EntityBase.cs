using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Common
{
    public abstract class EntityBase<TKey> :AuditEntityBase
    {
        public TKey Id { get; set; }

    }
}
