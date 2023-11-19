using PPPK_Zadatak05.Models;
using System;

namespace PPPK_Zadatak05.CosmosServices
{
    public interface ICosmosDBPersonService
    {
        Task<IEnumerable<Person>> GetPeopleAsync(string queryString);
        Task<Person> GetPersonAsync(string id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
