using Sales.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId => "User1"; //Environment.UserName or //System.Security.Principal.WindowsIdentity.GetCurrent().Name
    }
}
