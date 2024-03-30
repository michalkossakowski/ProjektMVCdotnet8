using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektMVCdotnet8.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool Add(ContactEntity contact)
        {
            _context.Add(contact);
            return Save();
        }

        public bool Delete(ContactEntity contact)
        {
            _context.Remove(contact);
            return Save();
        }

        public bool DeleteById(int id)
        {
            var contact = _context.ContactForms.Find(id);
            _context.ContactForms.Remove(contact);
            return Save();
        }

        public async Task<IEnumerable<ContactEntity>> GetAll()
        {
            return await _context.ContactForms.ToListAsync();
        }

        public async Task<ContactEntity> GetById(int id)
        {
            return await _context.ContactForms.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
