using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Learning.Ado.DAL
{
    class PersonDAL
    {
        readonly DataContext _context;
        public PersonDAL()
        {
            _context = new DataContext();
        }

        public void Create(Person person)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", person.Id },
                { "@name", person.Name },
                { "@lastName", person.LastName },
                { "@gender", person.Gender }

            };

            string queryBase = @" insert into People(Id, Name, LastName, Gender) values (@id, @name, @lastName, @gender)";

            _context.ExecuteNonQuery(delegate (int result)
            {
                if (result != 1)
                    throw new Exception("No se pudo crear el dato");
            }, queryBase, CommandType.Text, parameters);
        }

        public List<Person> Find(int rowsPerPage)
        {
            List<Person> result = new List<Person>();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@rowsperPage", rowsPerPage }

            };

            string queryBase = @"select top(@rowsperPage) * from People";

            _context.ExecuteQuery(delegate (SqlDataReader dr)
            {
                Person person = new Person() { 
                    Id = Guid.Parse(dr["Id"].ToString()),
                    Name = dr["Name"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Gender = Convert.ToBoolean(dr["Gender"])
                };
                result.Add(person);
            }, queryBase, CommandType.Text, parameters);
            return result;
        }
    }
}
