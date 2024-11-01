using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Product;
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

    }
}
