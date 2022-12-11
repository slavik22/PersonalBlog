namespace Tests;

using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Email == y.Email
                   && x.Mobile == y.Mobile
                   && x.Name == y.Name
                   && x.Surname == y.Surname
                   && x.BirthDate == y.BirthDate
                   && x.Password == y.Password
                ;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class PostEqualityComparer : IEqualityComparer<Post>
    {
        public bool Equals([AllowNull] Post x, [AllowNull] Post y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Title == y.Title
                   && x.Summary == y.Summary
                   && x.Content == y.Content
                   && x.UpdatedAt == y.UpdatedAt
                   && x.CreatedAt == y.CreatedAt;
        }

        public int GetHashCode([DisallowNull] Post obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CommentEqualityComparer : IEqualityComparer<Comment>
    {
        public bool Equals([AllowNull] Comment x, [AllowNull] Comment y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title
                && x.Content== y.Content
                && x.AuthorName == y.AuthorName;
        }

        public int GetHashCode([DisallowNull] Comment obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CategoryEqualityComparer : IEqualityComparer<Category>
    {
        public bool Equals([AllowNull] Category x, [AllowNull] Category y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Title == y.Title;
        }

        public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class TagEqualityComparer : IEqualityComparer<Tag>
    {
        public bool Equals([AllowNull] Tag x, [AllowNull] Tag y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title;
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }