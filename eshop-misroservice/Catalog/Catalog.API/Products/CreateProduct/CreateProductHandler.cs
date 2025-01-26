
using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
       string Name,
       List<string> Category,
       string Description,
       string ImageFile,
       decimal Price
    ): ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is Required");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var createProduct = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(createProduct);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(createProduct.Id);
        }
    }
}
