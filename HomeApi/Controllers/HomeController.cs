using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace HomeApi.Controllers
{
    public class HomeController : ControllerBase
    {
        public IOptions<HomeOptions> _options;
        private IMapper _mapper;

        public HomeController (IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet]
        [Route ("info")]
        public IActionResult Info()
        {
            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);

            return StatusCode(200, infoResponse);
        }
    }
}
