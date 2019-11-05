using LusCore.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace LusService.ProductService
{
    public interface IProductService
    {
        List<ProductModel> GetAllProduct();
        ProductModel GetProduct(Guid lusId);
        void AddProduct(ProductDto product);
        ProductModel UpdateProduct(ProductDto product);
    }
}
