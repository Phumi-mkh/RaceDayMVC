namespace RaceBay.MVC.Services;

public class ApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public Endpoints Endpoints { get; set; } = new();
}

public class Endpoints
{
    public string Login { get; set; } = string.Empty;
    public string Register { get; set; } = string.Empty;
    public string Events { get; set; } = string.Empty;
    public string Categories { get; set; } = string.Empty;
    public string Enrollments { get; set; } = string.Empty;
    public string Results { get; set; } = string.Empty;
}