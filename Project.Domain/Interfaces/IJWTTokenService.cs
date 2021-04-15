using DataAccess.Entitty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IJWTTokenService
    {
        string CreateToken(User user);
    }
}
