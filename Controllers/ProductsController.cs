using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.DTOs;
using ProductManagementSystem.Models;
using ProductManagementSystem.Services;

namespace ProductManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly BlobService _blobService;
    private readonly ApplicationDbContext _context;

    public ProductsController(BlobService blobService, ApplicationDbContext context)
    {
        _blobService = blobService;
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        string imageUrl = await _blobService.UploadFileAsync(dto.ImageFile);
        Product product = new Product()
        {
            Name = dto.Name,
            Category = dto.Category,
            Description = dto.Description,
            ImageUrl = imageUrl,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }


    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        List<ProductDto> productsDto = await _context.Products.Select(p => new ProductDto()
        {
            Category = p.Category,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Name = p.Name,
            Price = p.Price,
            ProductId = p.ProductId,
            Quantity = p.Quantity
        }).ToListAsync();

        return Ok(productsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        ProductDto dto = new ProductDto()
        {
            Category = product.Category,
            Description = product.Description,
            ImageUrl= product.ImageUrl,
            Name= product.Name,
            Price= product.Price,
            ProductId= product.ProductId,
            Quantity = product.Quantity
        };

        return Ok(dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        Product product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return BadRequest("Product not found");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm]UpdateProductDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        Product product = await _context.Products.FindAsync(id);
        if(product == null)
        {
            return NotFound();
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Category = dto.Category;
        product.Description = dto.Description;
        product.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();
        return Ok(dto);
    }
}
