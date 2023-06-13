using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1) List journal entries");
            Console.WriteLine("2) Journal entry details");
            Console.WriteLine("3) Add journal entry");
            Console.WriteLine("4) Edit journal entry");
            Console.WriteLine("5) Remove journal entry");
            Console.WriteLine(" 0) Go Back");

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
                //
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }
        private void List()
        {
            List<Journal> journalEntries = _journalRepository.GetAll();
            foreach (Journal journalEntry in journalEntries)
            {
                Console.WriteLine($"{journalEntry.Title} written on {journalEntry.CreateDateTime}: {journalEntry.Content}");
            }
        }


    }
}
