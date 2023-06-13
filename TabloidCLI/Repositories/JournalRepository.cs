﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection) 
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime FROM Journal";

                    List<Journal> journalEntries = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read()) 
                    {
                        Journal journal = new Journal
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        journalEntries.Add(journal);
                    }

                    reader.Close();
                    return journalEntries;
                }
            }
        }

        public Journal Get(int id) 
        {
            
        }

        public void Insert(Journal journal) 
        {
            
        }

        public void Update(Journal journal) 
        {
            
        }

        public void Delete(int id) 
        {
            
        }
    }
}
