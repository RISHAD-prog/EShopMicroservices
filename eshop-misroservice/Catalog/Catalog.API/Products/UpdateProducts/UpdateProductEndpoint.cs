
using Catalog.API.Products.GetProductsById;
using Mapster;

namespace Catalog.API.Products.UpdateProducts
{
    public record UpdateProductQuery(
       Guid Id,
       string Name,
       List<string> Category,
       string Description,
       string ImageFile,
       decimal Price
    ) : IQuery<UpdateProductResponse>;
    public record UpdateProductQueryResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductQuery product,ISender sender) =>
            {
                var command = product.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            })
             .WithName("UpdateProduct")
            .Produces<UpdateProductQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
