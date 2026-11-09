using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceBay.API.Data;
using RaceBay.API.Models;
using RaceBay.API.Services;

namespace RaceBay.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly BlobService _blobService;

    public EventsController(AppDbContext context, BlobService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        return Ok(await _context.Events.Include(e => e.Category).ToListAsync());
    }

    [HttpPost]
    [Authorize(Roles = "Organiser")]
    public async Task<IActionResult> CreateEvent([FromForm] EventDto request)
    {
        string? imageUrl = null;
        if (request.BannerImage != null)
        {
            imageUrl = await _blobService.UploadFileAsync(request.BannerImage, "event-banners");
        }

        var newEvent = new Event
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date,
            Location = request.Location,
            CategoryId = request.CategoryId,
            BannerImageUrl = imageUrl
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return Ok(newEvent);
    }
}
