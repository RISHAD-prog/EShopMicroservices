﻿namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductQuery(Guid ID) : IQuery<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : IQueryHandler<DeleteProductQuery, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.ID, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
