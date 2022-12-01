﻿namespace BuisnessLogicLayer.Models;

public class CommentModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public DateTime Published { get; set; }

    public int PostId { get; set; }
    
    public string AuthorName { get; set; } = "";
}