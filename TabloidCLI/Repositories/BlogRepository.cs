using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               Url
                                          FROM Blog";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                        };
                        blogs.Add(blog);
                    }

                    reader.Close();

                    return blogs;
                }
            }
        }

        //public Author Get(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"SELECT a.Id AS AuthorId,
        //                                       a.FirstName,
        //                                       a.LastName,
        //                                       a.Bio,
        //                                       t.Id AS TagId,
        //                                       t.Name
        //                                  FROM Author a 
        //                                       LEFT JOIN AuthorTag at on a.Id = at.AuthorId
        //                                       LEFT JOIN Tag t on t.Id = at.TagId
        //                                 WHERE a.id = @id";

        //            cmd.Parameters.AddWithValue("@id", id);

        //            Author author = null;

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                if (author == null)
        //                {
        //                    author = new Author()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
        //                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                        Bio = reader.GetString(reader.GetOrdinal("Bio")),
        //                    };
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
        //                {
        //                    author.Tags.Add(new Tag()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("TagId")),
        //                        Name = reader.GetString(reader.GetOrdinal("Name")),
        //                    });
        //                }
        //            }

        //            reader.Close();

        //            return author;
        //        }
        //    }
        //}

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                                     VALUES (@title, @Url)";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@Url", blog.Url);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog 
                                           SET Title = @title,
                                               Url = @url
                                         WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@Url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //public void InsertTag(Author author, Tag tag)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"INSERT INTO AuthorTag (AuthorId, TagId)
        //                                               VALUES (@authorId, @tagId)";
        //            cmd.Parameters.AddWithValue("@authorId", author.Id);
        //            cmd.Parameters.AddWithValue("@tagId", tag.Id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void DeleteTag(int blogId, int tagId)
            //blogId and tagId int are passed in as parameters
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag 
                                         WHERE BlogId = @blogid AND 
                                               TagId = @tagId";
                    cmd.Parameters.AddWithValue("@blogId", blogId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);
                    //delete objects within BlogTag table where BlogId and TagId match the parameters

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

//**** will be moved to blog detail manager once available
//private void RemoveBlogTag()
//{
//    Blog blog = _blogRepository.Get(_blogId);

//    Console.WriteLine($"Which tag would you like to remove from {blog.Name}?");
//    List<Tag> tags = blog.Tags;
//    //create list of tags associated with that blogId from the database

//    for (int i = 0; i < tags.Count; i++)
//        //as long as i is less than the total # of tags, add the next tag to the list
//    {
//        Tag tag = tags[i];
//        Console.WriteLine($" {i + 1}) {tag.Name}");
//    }
//        //displays tag menue # and tag name
    
//    Console.Write("> ");

//    string input = Console.ReadLine();
//    try
//    {
//        int choice = int.Parse(input);
//        Tag tag = tags[choice - 1];
//        _blogRepository.DeleteTag(blog.Id, tag.Id);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Invalid Selection. Won't remove any tags.");
//    }
//}