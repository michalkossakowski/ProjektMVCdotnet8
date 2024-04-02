using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactEntity>> GetAll();
        Task<ContactEntity> GetById(int id);
        bool Add(ContactEntity contact);
        bool Delete(ContactEntity contact);
        bool DeleteById(int id);
        bool Save();
    }
}
