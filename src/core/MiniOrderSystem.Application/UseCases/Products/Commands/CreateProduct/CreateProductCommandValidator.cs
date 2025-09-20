namespace MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(ProductMessages.NameMustBeSent.Message)
                .MaximumLength(300).WithMessage(ProductMessages.InvalidName.Message);

            RuleFor(x => x.Description)
                .MaximumLength(3000).WithMessage(ProductMessages.InvalidDescription.Message);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ProductMessages.InvalidPrice.Message);
        }
    }
}
