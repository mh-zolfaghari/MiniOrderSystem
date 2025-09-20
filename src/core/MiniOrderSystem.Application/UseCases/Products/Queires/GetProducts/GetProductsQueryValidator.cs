namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProducts
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(300).WithMessage(ProductMessages.InvalidName.Message);
        }
    }
}
