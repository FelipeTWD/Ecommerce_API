using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public class DbConnection : IDisposable
{
    public NpgsqlConnection Connection { get; set;}

    public DbConnection()
    {
        Connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=Clientes;Username=postgres;Password=03042002");
        Connection.Open();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
