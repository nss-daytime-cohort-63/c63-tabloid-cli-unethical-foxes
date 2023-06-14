﻿using Microsoft.IdentityModel.Tokens;
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
                    //
                case "3":
                    Add();
                    return this;
                case "4":
                    //
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a journal entry: ";
            }
            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for(int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i+1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
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

        private void Add()
        {
            Console.WriteLine("New journal entry");
            Journal journalEntry = new Journal();

            Console.Write("Title: ");
            journalEntry.Title = Console.ReadLine();

            Console.Write("Content: ");
            journalEntry.Content = Console.ReadLine();

            journalEntry.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journalEntry);
        }

        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal entry would you like to remove?");
            if(journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}
