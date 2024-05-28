using System.Net;
using System.Net.Mail;
using System.Text;
using ZooFerma.Interfaces;
using ZooFerma.Models.Dto;
using ZooFerma.Services.Dao;

namespace ZooFerma.Services.Dto.Impls
{
    public class RegistrationServiceImpl: IRegistrationService
    {
        private readonly ILogger<IRegistrationService> _logger;
        private readonly IUserService userService;
        private readonly EmailSender emailSender;

        public RegistrationServiceImpl(ILogger<IRegistrationService> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
            emailSender = new RegistrationEmailSender();
        }

        public bool Registration(RegistrationDto dto)
        {
            List<string> emails = userService.GetEmails();
            foreach (string email in emails)
            {
                if (email.Equals(dto.email))
                {
                    _logger.LogError("Такой email уже есть в БД");
                    return false;
                }
            }

            _logger.LogInformation("Начинаем запись в базу данных!");
            userService.SaveUser(dto);
            _logger.LogInformation("Отправка сообщения зарегистрированному пользователю!");
            emailSender.Send(dto.email);
            return true;
        }

        public bool CheckDtoFields(RegistrationDto dto)
        {
            return !string.IsNullOrEmpty(dto.email.Trim()) && !string.IsNullOrEmpty(dto.phone.Trim()) 
                && !string.IsNullOrEmpty(dto.fio.Trim()) && !string.IsNullOrEmpty(dto.password.Trim());
        }
    }
}
