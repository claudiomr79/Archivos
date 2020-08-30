using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            //PRIMERA FORMA

            //FileStream lector = new FileStream("agenda.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            //while (lector.Length > lector.Position)
            //{
            //    Console.Write((char)lector.ReadByte());
            //}
            //lector.Close();
            //Console.ReadKey();

            //SEGUNDA FORMA

            Leer();
            Escribir();
            Leer();
            Console.ReadKey();
        }

        private static void Leer()
        {
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;
            Console.WriteLine("Nombre\tApellido\te-mail\tTelefono");
            while ((linea = lector.ReadLine()) != null)
            {

                try
                {
                    if (linea != null)
                    {
                        string[] valores = linea.Split(';');
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", valores[0], valores[1], valores[2], valores[3]);
                    }
                }
                catch (System.IndexOutOfRangeException)
                {

                    Console.WriteLine("No se por que salta este error");
                }
            }
            lector.Close();
        }
        private static void Escribir()
        {
            StreamWriter escritor = File.AppendText("agenda.txt");
            Console.WriteLine("Ingrese nuevos contactos");
            string rta = "S";
            while (rta == "S")
            {
                Console.Write("Ingrese nombre: ");
                string nombre = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Ingrese Apellido: ");
                string apellido = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Ingrese email: ");
                string email = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Ingrese telefono: ");
                string telefono = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                escritor.WriteLine(nombre + ";" + apellido + ";" + email + ";" + telefono);
                Console.WriteLine("Desea ingresar otro contacto S/N?");
                rta = Console.ReadLine();
            }
            escritor.Close();
        }
    }
}
