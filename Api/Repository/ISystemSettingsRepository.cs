using Core;
using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface ICityRepository
    {
        void CreateCity(City settings);
        List<City> GetAllCity();
        City GetCityByName(string name);
        void UpdateCity(City settings);
        void DeleteCity(string id);
        List<City> GetAllActiveCity();
    }
}