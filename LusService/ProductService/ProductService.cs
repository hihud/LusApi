using AutoMapper;
using LusCore.Product;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace LusService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly GraphClient _client;

        public ProductService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _client = new GraphClient(new Uri(_config["DatabaseServer"]), _config["DatabaseUser"], _config["DatabasePassword"]);
            _client.Connect();
        }

        public List<ProductModel> GetAllProduct()
        {
            var results = _client.Cypher
            .Match("(n: Product)")
            .Return(n => n.As<ProductDto>())
            .Results;
            return _mapper.Map<List<ProductModel>>(results);
        }
        public ProductModel GetProduct(Guid lusId)
        {
            var results = _client.Cypher
          .Match("(p: Product)")
          .Where((ProductDto p) => p.LusId == lusId)
          .Return(p => p.As<ProductDto>())
          .Results;
            return _mapper.Map<ProductModel>(results.FirstOrDefault());
        }

        public void AddProduct(ProductDto product)
        {
            product.LusId = Guid.NewGuid();
            if (product != null)
            {
                _client.Cypher
            .Create("(n:Product {product})")
            .WithParams(new { product })
            .ExecuteWithoutResults();
            }
        }

        public ProductModel UpdateProduct(ProductDto product)
        {
            var dto = _client.Cypher
          .Match("(p: Product)")
          .Where((ProductDto p) => p.LusId == product.LusId)
          .Set("p = {product}")
          .WithParams(new { product })
          .Return(p => p.As<ProductDto>())
          .Results;
            return _mapper.Map<ProductModel>(dto.FirstOrDefault());
        }
    }
}
