using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactEntity>> GetAll();
        Task<ContactEntity> GetByIdAsync(int id);
        bool Add(ContactEntity contact);
        bool Delete(ContactEntity contact);
        bool Delete(int id);
        bool Update(ContactEntity contact);
        bool Save();
    }
}
