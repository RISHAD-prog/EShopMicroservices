
namespace Catalog.API.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid ID): IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler (IDocumentSession session, ILogger<GetProductByIdHandler> logger): IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.ID, cancellationToken);

            if (product == null) {
                throw new ProductNotFoundException(request.ID);
            }

            return new GetProductByIdResult(product);
        }
    }
}
