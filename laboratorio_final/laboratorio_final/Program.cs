using System;
using System.Collections.Generic;

namespace VeterinariaCRUD
{
    class Program
    {
        // CONEXIÓN AUTOMÁTICA A LOCALDB (Por defecto en Visual Studio)
        static string connectionString = "Server=DESKTOP-VNF03T2\\SQLEXPRESS;Database=VeterinariaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            MascotaRepository repo = new MascotaRepository(connectionString);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== VETERINARIA - CRUD MASCOTAS ===");
                Console.WriteLine("1. Insertar   2. Listar   3. Actualizar   4. Eliminar   0. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Insertar(repo); break;
                    case "2": Listar(repo); break;
                    case "3": Actualizar(repo); break;
                    case "4": Eliminar(repo); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void Insertar(MascotaRepository repo)
        {
            var m = new Mascota
            {
                Nombre = Leer("Nombre: "),
                Especie = Leer("Especie: "),
                Raza = Leer("Raza: "),
                EdadMeses = int.Parse(Leer("Edad (meses): ")),
                NombreDuenio = Leer("Dueño: "),
                TelefonoDuenio = Leer("Teléfono: ")
            };
            repo.Insertar(m);
        }

        static void Listar(MascotaRepository repo)
        {
            var lista = repo.Listar();
            if (lista.Count == 0) { Console.WriteLine("(Sin registros)"); return; }
            Console.WriteLine($"{"ID",-4} {"Mascota",-15} {"Especie",-10} {"Raza",-12} {"Meses",-6} {"Dueño",-15} {"Tel",-12}");
            Console.WriteLine(new string('-', 80));
            foreach (var m in lista)
                Console.WriteLine($"{m.IdMascota,-4} {m.Nombre,-15} {m.Especie,-10} {m.Raza,-12} {m.EdadMeses,-6} {m.NombreDuenio,-15} {m.TelefonoDuenio,-12}");
        }

        static void Actualizar(MascotaRepository repo)
        {
            var m = new Mascota
            {
                IdMascota = int.Parse(Leer("ID a actualizar: ")),
                Nombre = Leer("Nuevo nombre: "),
                Especie = Leer("Nueva especie: "),
                Raza = Leer("Nueva raza: "),
                EdadMeses = int.Parse(Leer("Nueva edad (meses): ")),
                NombreDuenio = Leer("Nuevo dueño: "),
                TelefonoDuenio = Leer("Nuevo teléfono: ")
            };
            repo.Actualizar(m);
        }

        static void Eliminar(MascotaRepository repo)
        {
            int id = int.Parse(Leer("ID a eliminar: "));
            repo.Eliminar(id);
        }

        static string Leer(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }
    }
}