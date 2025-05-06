using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validators
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя комнаты не может быть пустым.")
                .MaximumLength(50)
                .WithMessage("Имя комнаты не должно превышать 50 символов");
            RuleFor(x => x.Area)
                .GreaterThan(0)
                .WithMessage("Площадь комнаты должна быть больше нуля.");
            RuleFor(x => x.Voltage)
                .InclusiveBetween(120, 220)
                .WithMessage("Напряжение должно быть от 120 до 220 вольт");
            RuleFor(x => x.GasConnected)
                .NotNull()
                .WithMessage("Поле подключения газа обязательно.");
        }
    }
}
