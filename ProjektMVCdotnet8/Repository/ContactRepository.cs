﻿using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ContactEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ContactEntity contact)
        {
            throw new NotImplementedException();
        }
    }
}