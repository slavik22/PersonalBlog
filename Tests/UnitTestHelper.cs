using BuisnessLogicLayer;

namespace Tests;

using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

    internal static class UnitTestHelper
    {
        public static DbContextOptions<PersonalBlogDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<PersonalBlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PersonalBlogDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(PersonalBlogDbContext context)
        {
            context.Categories.AddRange(
                new Category { Id = 1, Title = "Programing" },
                new Category { Id = 2, Title = "Music" });
            context.Tags.AddRange(
                new Tag { Id = 1, Title = ".net" },
                new Tag { Id = 2, Title = "java" },
                new Tag { Id = 3, Title = "pop" });
            context.Comments.AddRange(
                new Comment {Id = 1, Title = "Hello from Slava", Content = "I completely agree with author's opinion", PostId = 1},
                new Comment {Id = 2, Title = "Hello from Tom", Content = "I completely agree with author's opinion", PostId = 2}
            );
            context.Posts.AddRange(
                new Post { Id = 1, Title = "Hello world", Summary = ".net", Content = ".net", UserId = 1 },
                new Post { Id = 2, Title = "Hello world", Summary = "java", Content = "java", UserId = 2 },
                new Post { Id = 3, Title = "Hello world", Summary = "pop", Content = "pop music", UserId = 3 });
            context.Users.AddRange(
                new User{Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
                new User{Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
                );
            context.SaveChanges();
        }
    }
