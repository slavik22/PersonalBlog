using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

[TestFixture]
public class CommentRepositoryTests
{
    [TestCase(1)]
    [TestCase(2)]
    public async Task CommentRepository_GetByIdAsync_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment>(context);

        var comment = await commentRepository.GetByIdAsync(id);

        var expected = ExpectedComments.FirstOrDefault(x => x.Id == id);

        Assert.That(comment, Is.EqualTo(expected).Using(new CommentEqualityComparer()), message: "GetByIdAsync method works incorrect");
    }
    
    [Test]
    public async Task CommentRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var commentRepository = new GenericRepository<Comment>(context);

        var comments = await commentRepository.GetAllAsync();

        Assert.That(comments, Is.EqualTo(ExpectedComments).Using(new CommentEqualityComparer()), message: "GetAllAsync method works incorrect");
    }
    [Test]
    public async Task CommentRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment?>(context);

        var comment = new Comment() { Id = 3, Title = "New", Content = "New", PostId = 1};

        await commentRepository.AddAsync(comment);
        await context.SaveChangesAsync();

        Assert.That(context.Tags.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
    }

    
    [Test]
    public async Task CommentRepository_DeleteByIdAsync_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment>(context);


        await commentRepository.Delete(1);
        await context.SaveChangesAsync();

        Assert.That(context.Comments.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
    }

    [Test]
    public async Task CommentRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var commentRepository = new GenericRepository<Comment>(context);


        var comment = new Comment
        {
            Id = 1, Title = "New", Content = "New", PostId = 1
        };

        commentRepository.Update(comment);
        await context.SaveChangesAsync();

        Assert.That(comment, Is.EqualTo(new Comment
        {
            Id = 1, Title = "New", Content = "New", PostId = 1
        }).Using(new CommentEqualityComparer()), message: "Update method works incorrect");
    }

    
    private static IEnumerable<Comment> ExpectedComments =>
        new[]
        {
            new Comment {Id = 1, Title = "Hello from Slava", Content = "I completely agree with author's opinion", PostId = 1},
            new Comment {Id = 2, Title = "Hello from Tom", Content = "I completely agree with author's opinion", PostId = 2}
        };

}