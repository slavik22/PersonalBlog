using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

[TestFixture]
public class PostRepositoryTests
{
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task PostRepository_GetByIdAsync_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post>(context);

        var post = await postRepository.GetByIdAsync(id);

        var expected = ExpectedPosts.FirstOrDefault(x => x.Id == id);

        Assert.That(post, Is.EqualTo(expected).Using(new PostEqualityComparer()), message: "GetByIdAsync method works incorrect");
    }
    
    [Test]
    public async Task PostRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var postRepository = new GenericRepository<Post>(context);


        var posts = await postRepository.GetAllAsync();

        Assert.That(posts, Is.EqualTo(ExpectedPosts).Using(new PostEqualityComparer()), message: "GetAllAsync method works incorrect");
    }
    [Test]
    public async Task PostRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post?>(context);


        var post = new Post() { Id = 4, Title = "New", Summary = "New", Content = "New", UserId = 1};

        await postRepository.AddAsync(post);
        await context.SaveChangesAsync();

        Assert.That(context.Posts.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
    }

    
    [Test]
    public async Task PostRepository_DeleteByIdAsync_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post>(context);
        
        await postRepository.Delete(1);
        await context.SaveChangesAsync();

        Assert.That(context.Users.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
    }

    [Test]
    public async Task PostRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var postRepository = new GenericRepository<Post>(context);
        
        var post = new Post
        {
            Id = 1, Title = "New", Summary = "New", Content = "New", UserId = 1
        };

        postRepository.Update(post);
        await context.SaveChangesAsync();

        Assert.That(post, Is.EqualTo(new Post
        {
            Id = 1, Title = "New", Summary = "New", Content = "New", UserId = 1
        }).Using(new PostEqualityComparer()), message: "Update method works incorrect");
    }

    
    private static IEnumerable<Post> ExpectedPosts =>
        new[]
        {
            new Post { Id = 1, Title = "Hello world", Summary = ".net", Content = ".net", UserId = 1 },
            new Post { Id = 2, Title = "Hello world", Summary = "java", Content = "java", UserId = 2 },
            new Post { Id = 3, Title = "Hello world", Summary = "pop", Content = "pop music", UserId = 3 }
        };

}