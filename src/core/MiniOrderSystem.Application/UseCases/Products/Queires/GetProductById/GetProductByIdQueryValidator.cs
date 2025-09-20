namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ProductMessages.InvalidId.Message);
        }
    }
}
