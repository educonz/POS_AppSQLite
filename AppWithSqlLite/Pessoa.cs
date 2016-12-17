using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithSqlLite
{
    public class Pessoa
    {
        [SQLite.Net.Attributes.PrimaryKey,
            SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string DataCriacao { get; set; }

        public Pessoa()
        {
            //construtor  
        }
        public Pessoa(string nome, string fone)
        {
            Nome = nome;
            Fone = fone;
            DataCriacao = DateTime.Now.ToString();
        }
    }
}
