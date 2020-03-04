using Learning.Ado.DAL;
using System;

namespace Learning.Ado
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PersonDAL dal = new PersonDAL();

                /*Console.WriteLine("Creando persona ...");

                dal.Create(new Person() { Id = Guid.NewGuid(),Name= "Peter",LastName = "Parker",Gender = true});
                Console.WriteLine("Registro creado");*/
                Console.WriteLine("Datos existentes:");

                var dataList = dal.Find(100);
                foreach (var person in dataList)
                {
                    Console.WriteLine($"Id: {person.Id} Nombre: {person.Name} Apellidos: {person.LastName}");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando registro {ex.Message}");
                throw;
            }
        }
    }
}
