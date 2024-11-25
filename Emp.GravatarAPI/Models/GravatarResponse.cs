namespace Emp.GravatarAPI.Models;

class GravatarResponse
{
    public Entry[] Entry { get; set; }   
}

class Entry
{
    public Name Name { get; set; }
    public Photo[]? Photos { get; set; }
    public string DisplayName { get; set; }
}

class Name
{
    public string Formatted { get; set; }
}

 class Photo
{
    public string Value { get; set; }
    public string Type { get; set; }
}