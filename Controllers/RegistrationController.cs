using Microsoft.AspNetCore.Mvc;
using ZooFerma.Models.Dto;
using ZooFerma.Services.Dto;

namespace ZooFerma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationService _regService;
        private readonly IJsonConvertService _jsonConvertService;

        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService, IJsonConvertService jsonConvertService)
        {
            _logger = logger;
            _regService = registrationService;
            _jsonConvertService = jsonConvertService;
        } 

        [HttpPost]
        public RegistrationResponseDto Registration()
        {
            _logger.LogInformation("Начат процесс регистрации!");
            RegistrationDto? dto;
            RegistrationResponseDto responseDto = new RegistrationResponseDto();
            
            using (var reader = new StreamReader(Request.Body))
            {
                dto = _jsonConvertService.convertToRegistrationDto(reader);
            }

            if (dto != null && _regService.CheckDtoFields(dto))
            {
                if(_regService.Registration(dto))
                {
                    responseDto.Result = true;
                    responseDto.Message = "Регистрация прошла успешно!";
                    _logger.LogInformation("Регистрация прошла успешно!");
                }
                else
                {
                    responseDto.Result = false;
                    responseDto.Message = "Такой email уже существует!";
                    _logger.LogError("Регистрация не была завершена!");
                }
            }
            else
            {
                responseDto.Result = false;
                responseDto.Message = "Ошибка регистрации! Не все данные были переданы!";
                _logger.LogError("Ошибка регистрации. Не полные данные!");
            }

            return responseDto;
        }
    }
}
