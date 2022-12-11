// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="AutomapperProfile.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BuisnessLogicLayer;

/// <summary>
/// Class AutomapperProfile.
/// Implements the <see cref="Profile" />
/// </summary>
/// <seealso cref="Profile" />
public class AutomapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutomapperProfile" /> class.
    /// </summary>
    public AutomapperProfile()
    {
        CreateMap<Tag, TagModel>()
            .ReverseMap();

        CreateMap<Post, PostModel>()
            .ForMember(pm => pm.AuthorName, p => p.MapFrom(x => $"{x.User.Name} {x.User.Surname}"));

        CreateMap<PostModel, Post>();

            
        CreateMap<Comment, CommentModel>()
            .ReverseMap();

        CreateMap<User, UserModel>()
            .ReverseMap();
        
        CreateMap<Category, CategoryModel>()
            .ReverseMap();
    }
}
