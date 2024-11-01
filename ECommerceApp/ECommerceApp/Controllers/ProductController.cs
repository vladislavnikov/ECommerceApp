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

        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var topPlatforms = await _productRepository.GetTopPlatformsAsync();
            return Ok(topPlatforms);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGames(string term, int limit, int offset)
        {
            var games = await _productRepository.SearchGamesAsync(term, limit, offset);

            var dtoList = _mapper.Map<List<ProductDto>>(games);

            return Ok(dtoList);
        }

    }
}
