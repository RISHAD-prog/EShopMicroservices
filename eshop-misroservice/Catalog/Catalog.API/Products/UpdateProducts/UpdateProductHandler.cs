
namespace Catalog.API.Products.UpdateProducts
{
    public record UpdateProductCommand(
        Guid Id,
       string Name,
       List<string> Category,
       string Description,
       string ImageFile,
       decimal Price
    ) : ICommand<UpdateProductResponse>;
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (result == null)
            {
                throw new ProductNotFoundException();
            }
            
            result.Name = request.Name;
            result.Category = request.Category;
            result.Description = request.Description;
            result.ImageFile = request.ImageFile;
            result.Price = request.Price;

            session.Update(result);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse(true);
        }
    }
}
