using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BackgroundRepository: DatabaseConnector
    {
        public BackgroundRepository(string connectionString) : base(connectionString) { }
    }
}