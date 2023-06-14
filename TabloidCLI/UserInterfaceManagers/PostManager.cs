using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1) List Posts");
            Console.WriteLine("2) Add Post");
            Console.WriteLine("3) Edit Post");
            Console.WriteLine("4) Remove Post");
            Console.WriteLine("5) Search by Author");
            Console.WriteLine("0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                //;
                case "3":
                //
                case "4":
                //
                case "5":
                    SearchByAuthor();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Title} URL: {post.Url}");
            }
        }

        private void SearchByAuthor()
        {
            Console.WriteLine("Input the author's ID");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}) {author.FirstName} {author.LastName}");
            }
            Console.Write("> ");
            int inputedId = int.Parse(Console.ReadLine());
            List<Post> posts = _postRepository.GetByAuthor(inputedId);
            foreach (Post post in posts)
            {
                if (inputedId == post.Author.Id)
                {
                    Console.WriteLine($"{post.Title} Written By: {post.Author.FirstName} {post.Author.LastName} URL: {post.Url}");
                };
            }
        }

        private void AddPost()
        {
            // complete later
        }

        private void RemovePost()
        {
            // complete later
        }

        private void EditPost()
        {
            // complete later
        }
    }
}
