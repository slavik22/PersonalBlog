// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-03-2022
// ***********************************************************************
// <copyright file="PersonalBlogDbContext.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;
/// <summary>
/// Class PersonalBlogDbContext.
/// Implements the <see cref="DbContext" />
/// </summary>
/// <seealso cref="DbContext" />
public class PersonalBlogDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalBlogDbContext"/> class.
    /// </summary>
    /// <remarks>See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information.</remarks>
    public PersonalBlogDbContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalBlogDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public PersonalBlogDbContext(DbContextOptions<PersonalBlogDbContext> options) : base(options){}

    /// <summary>
    /// <para>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </para>
    /// <para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    /// <remarks>See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information.</remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-OC1V65E;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    /// <summary>
    /// Gets or sets the posts.
    /// </summary>
    /// <value>The posts.</value>
    public DbSet<Post> Posts { get; set; }
    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    /// <value>The comments.</value>
    public DbSet<Comment> Comments { get; set; }
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    /// <value>The tags.</value>
    public DbSet<Tag> Tags { get; set; }
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    /// <value>The categories.</value>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Called when [model creating].
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });
        
        modelBuilder.Entity<PostCategory>()
            .HasKey(pt => new { pt.PostId, pt.CategoryId });

        
        modelBuilder.Entity<Post>()
            .HasOne<User>(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
           modelBuilder.Entity<Comment>()
               .HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<PostTag>()
               .HasOne(pt => pt.Post)
               .WithMany(p => p.PostTags)
               .HasForeignKey(pt => pt.PostId)
               .OnDelete(DeleteBehavior.Cascade);
            
           modelBuilder.Entity<PostTag>()
               .HasOne(pt => pt.Tag)
               .WithMany(t => t.PostTags)
               .HasForeignKey(pt => pt.TagId)
               .OnDelete(DeleteBehavior.Cascade);
            
           modelBuilder.Entity<PostCategory>()
               .HasOne(pc => pc.Post)
               .WithMany(p => p.PostCategories)
               .HasForeignKey(pc => pc.PostId)
               .OnDelete(DeleteBehavior.Cascade);
            
           modelBuilder.Entity<PostCategory>()
               .HasOne(pc => pc.Category)
               .WithMany(c => c.PostCategories)
               .HasForeignKey(pc => pc.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

    }
    

}