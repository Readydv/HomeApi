using FluentValidation;
using HomeApi.Contracts.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validators
{
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        string[] _validLocations;
        public AddDeviceRequestValidator()
        {
            _validLocations = new[]
            {
                "Kitchen",
                "Bathroom",
                "Livingroom",
                "Toilet"
            };

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts)
                .NotEmpty()
                .InclusiveBetween(120, 220)
                .WithMessage("Поддерживаются только устройства от 120 до 220 вольт");
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.Location)
                .NotEmpty()
                .Must(beValidLocation)
                .WithMessage($"Пожалуйста введите настоящую локацию: {string.Join(", ", _validLocations)}");
        }

        private bool beValidLocation(string location)
        {
            return _validLocations.Any(e => e == location);
        }
    }
}
