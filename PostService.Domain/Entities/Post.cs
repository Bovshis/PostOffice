using System.ComponentModel.DataAnnotations;

namespace PostService.Domain.Entities;

public class Post
{
    public int PostId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Title { get; set; }

    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Content { get; set; }

    [Required]
    public int? UserId { get; set; }
    public User User { get; set; }
}