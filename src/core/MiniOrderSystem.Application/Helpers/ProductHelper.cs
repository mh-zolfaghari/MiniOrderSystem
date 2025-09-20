using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct;
using MiniOrderSystem.Domain.Entities;

namespace MiniOrderSystem.Application.Helpers
{
    internal static class ProductHelper
    {
        internal static Product ToModel(this CreateProductCommand command)
        {
            return new()
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description
            };
        }

        internal static ProductDto? ToDto(this Product? model, int? id = default)
        {
            if (model is not null)
                return new()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
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
