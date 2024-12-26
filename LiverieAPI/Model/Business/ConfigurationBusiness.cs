using LiverieAPI.Infrastructuer;
using Microsoft.Extensions.Configuration;
using GestionLaverie.Domaine.Entities;
using LiverieAPI.Model.Domaine.IDAO;

namespace LiverieAPI.Model.Business
{
    public class ConfigurationBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IDAOPropretaire.IDAOProp _daoProprietaire;

        public ConfigurationBusiness(IConfiguration configuration, IDAOPropretaire.IDAOProp daoProprietaire)
        {
            _configuration = configuration;
            _daoProprietaire = daoProprietaire;
        }

        public string GetConfig(string key)
        {
            return _configuration[key];
        }
    }

}
