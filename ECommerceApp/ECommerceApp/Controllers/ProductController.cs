using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Model.Model;
using ECommerceApp.Business.Model.Request;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/games")]
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieves the top platforms by product count.
        /// </summary>
        /// <returns>A list of top used platforms.</returns>
        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var topPlatforms = await _productRepository.GetTopPlatformsAsync();
            return Ok(topPlatforms);
        }

        /// <summary>
        /// Searches for games based on a search term.
        /// </summary>
        /// <param name="term">The search term used to filter games.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="offset">The number of results to skip before starting to collect the result set.</param>
        /// <returns>A list of games that match the search criteria.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchGames(string term, int limit, int offset)
        {
            var games = await _productRepository.SearchGamesAsync(term, limit, offset);

            var dtoList = _mapper.Map<List<ProductDto>>(games);

            return Ok(dtoList);
        }

        /// <summary>
        /// Searches for game.
        /// </summary>
        /// <param name="id">The Id param is for searching a game by its Id.</param>
        /// <returns>Game with an Id</returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);

            var response = _mapper.Map<ProductResponseModel>(product);

            return Ok(product);
        }

        /// <summary>
        /// Creates product.
        /// </summary>
        /// <param name="productRequest">The product create model containing new create details.</param>
        /// <returns>Returns new updated product</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductResponseModel>> CreateProduct([FromForm] ProductRequestModel productRequest)
        {
            await _productRepository.CreateProductAsync(productRequest);
            return CreatedAtAction(nameof(GetProduct), new { id = productRequest.Id }, productRequest);
        }

        /// <summary>
        /// Updates product.
        /// </summary>
        /// <param name="productRequest">The product update model containing new product details.</param>
        /// <returns>No content the new product info.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductRequestModel productRequest)
        {
            await _productRepository.UpdateProductAsync(productRequest);

            return CreatedAtAction(nameof(GetProduct), new { id = productRequest.Id }, productRequest);
        }

        /// <summary>
        /// Delete game.
        /// </summary>
        /// <param name="id">The Id param is for searching a game by its Id.</param>
        /// <returns>No content</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
