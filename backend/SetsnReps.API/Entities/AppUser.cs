using Microsoft.AspNetCore.Identity;

namespace SetsnReps.API.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    
    public string ThumbnailImageUri { get; set; } = string.Empty;
}