using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



// Para o usuário fazer um post no body
app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

//api.app.com/users/{code}

/* A method that gets a product from the repository. */
app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

/* A method that edits a product from the repository. */
app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

/* A method that deletes a product from the repository. */
app.MapDelete("/deleteproduct/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});


app.Run();

public static class ProductRepository { 
    public static List<Product> Products { get; set; }
    public static void Add(Product product) {
        if(Products == null) 
            Products = new List<Product>();

            Products.Add(product);
        }

// Busca um produto pelo código
    public static Product GetBy(string code) {
        return Products.FirstOrDefault(p => p.Code == code);
    }


    /// Remove um produto da lista de productos.
    public static void Remove(Product product) {
        Products.Remove(product);
    }

}

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}
