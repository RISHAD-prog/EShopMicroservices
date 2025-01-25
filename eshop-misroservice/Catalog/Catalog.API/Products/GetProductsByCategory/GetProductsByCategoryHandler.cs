
namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(string category): IQuery<GetProductsByCategoryResponse>;
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryRequest, GetProductsByCategoryResponse>
    {
        public async Task<GetProductsByCategoryResponse> Handle(GetProductsByCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                .Where(item => item.Category.Contains(request.category))
                .ToListAsync(cancellationToken);
            
            return new GetProductsByCategoryResponse(result);
        }
    }
}
