using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using LaBancaUCB.Infrastructure.Data;
using System.Threading.Tasks;

namespace LaBancaUCB.Infrastructure.Repositories
{
    public class SecurityRepository :
        BaseRepository<Security>, ISecurityRepository
    {

        public SecurityRepository(LaBancaUCBContext context)
            : base(context)
        {
            
        }

        public async Task<Security>
            GetLoginByCredentials(UserLogin userLogin)
        {
            return await _entities.FirstOrDefaultAsync
                (x => x.Login == userLogin.User
                && x.Password == userLogin.Password);
        }
    }
}