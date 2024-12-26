using GestionLaverie.Domaine.Entities;
using LiverieAPI.Model.Business;
using Microsoft.AspNetCore.Mvc;

namespace LiverieAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationBusiness _configBusiness;

        public ConfigurationController(ConfigurationBusiness configBusiness)
        {
            _configBusiness = configBusiness;
        }

        [HttpGet("{key}")]
        public IActionResult GetConfiguration(string key)
        {
            var configValue = _configBusiness.GetConfig(key);
            if (configValue != null)
            {
                return Ok(configValue);
            }
            return NotFound($"Configuration for key '{key}' not found.");
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class ProprietaireController : ControllerBase
    {
        private readonly IProprietaireService _proprietaireService;

        public ProprietaireController(IProprietaireService proprietaireService)
        {
            _proprietaireService = proprietaireService;
        }

        public object GetProprietaireById { get; private set; }

        [HttpPost]
        public IActionResult CreateProprietaire([FromBody] Proprietaire proprietaire)
        {
            if (proprietaire == null)
            {
                return BadRequest();
            }

            _proprietaireService.Create(proprietaire);
            return CreatedAtAction(nameof(GetProprietaireById), new { id = proprietaire.Id }, proprietaire);
        }



    }
}
