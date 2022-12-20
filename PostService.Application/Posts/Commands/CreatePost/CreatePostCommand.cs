using MediatR;
using PostService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PostService.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Post>
{
    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Title { get; set; }

    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Content { get; set; }

    [Required]
    public int? UserId { get; set; }
}