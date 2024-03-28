using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Repository
{
    public class ContactRepository : IContactRepository
    {
        public bool Add(ContactEntity post)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ContactEntity post)
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
            throw new NotImplementedException();
        }

        public bool Update(ContactEntity post)
        {
            throw new NotImplementedException();
        }
    }
}
