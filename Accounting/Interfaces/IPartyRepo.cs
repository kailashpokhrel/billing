using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface IPartyRepo
    {
      
        Task<int> AddPartyAsync(Party partyId);
        Task<Party> GetPartyByIdAsync(int partyId);
        Task<IEnumerable<Party>> GetAllPartiesAsync();
        Task<int> UpdatePartyAsync(Party party);
        Task<int> DeletePartyAsync(int partyId);

        Task<IEnumerable<Party>> GetPartiesAsync();


    }
}
