using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace RaceBay.MVC.Models.ViewModels;
public class ProfileViewModel
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required][EmailAddress] public string Email { get; set; } = string.Empty;
    public string? ExistingProfilePictureUrl { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
