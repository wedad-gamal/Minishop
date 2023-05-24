using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;

namespace Minishop.Application.Services
{
    public class SizeTypeServices : ISizeTypeServices
    {
        private readonly MinishopDBContext _context;

        public SizeTypeServices(MinishopDBContext context)
        {
            _context = context;
        }
        public void AddSizeType(string name)
        {
            _context.Add(new SizeType() { Name = name });
            _context.SaveChanges();
        }
    }
}
