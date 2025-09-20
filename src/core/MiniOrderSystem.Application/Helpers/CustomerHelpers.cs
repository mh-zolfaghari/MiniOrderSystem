using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Customers.Commands.CreateCustomer;
using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Shared.Extensions;

namespace MiniOrderSystem.Application.Helpers
{
    internal static class CustomerHelpers
    {
        internal static Customer ToModel(this CreateCustomerCommand command)
        {
            return new()
            {
                Token = Guid.NewGuid(),
                Name = command.Name,
                PhoneNumber = command.PhoneNumber.ToEnglishDigit()!,
                IsActive = true,
                Address = new(command.Street, command.City, command.Country, command.PostalCode.ToEnglishDigit()!)
            };
        }

        internal static CustomerDto? ToDto(this Customer? model, int? id = default)
        {
            if (model is not null)
                return new()
                {
                    Id = model.Id,
                    Token = model.Token,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = model.IsActive,
                    Country = model.Address?.Country,
                    City = model.Address?.City,
                    Street = model.Address?.Street,
                    PostalCode = model.Address?.PostalCode,
                    CreatedAt = model.CreatedAt
                };
            if (id is not null)
                return new()
                {
                    Id = id.Value
                };
            return null;
        }
    }
}
