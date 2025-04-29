﻿using Catalog.Services;

namespace Catalog.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        group.MapGet("/", async (ProductService service) =>
        {
            var products = await service.GetProductsAsync();
            return Results.Ok(products);
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);
            if (product is null) return Results.NotFound();

            return Results.Ok(product);
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (Product product, ProductService service) =>
        {
            await service.CreateProductAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        group.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
        {
            var updatedProduct = await service.GetProductByIdAsync(id);
            if (updatedProduct is null) return Results.NotFound();

            await service.UpdateProductAsync(updatedProduct, inputProduct);
            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapDelete("/{id}", async (int id, ProductService service) =>
        {
            var deletedProduct = await service.GetProductByIdAsync(id);
            if (deletedProduct is null) return Results.NotFound();

            await service.DeleteProductAsync(deletedProduct);
            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        // Traditional Search
        group.MapGet("search/{query}", async (string query, ProductService service) =>
        {
            var products = await service.SearchProductsAsync(query);

            return Results.Ok(products);
        })
        .WithName("SearchProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        // Support AI
        group.MapGet("/support/{query}", async (string query, ProductAIService service) =>
            {
                var response = await service.SupportAsync(query);

                return Results.Ok(response);
            })
            .WithName("Support")
            .Produces(StatusCodes.Status200OK);

    }
}