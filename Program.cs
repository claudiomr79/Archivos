using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

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

            //Leer();
            //Escribir();
            //Leer();
            //Console.ReadKey();

            //XML
            Console.WriteLine("Presione una tecla para generar el archivo de agendaxml.xml con los datos de agenda.txt");
            Console.ReadKey();
            EscribirXML();
            Console.WriteLine("Archivo agendaxml generado correctamente\n\nPresione una tecla para ver su contenido");
            Console.ReadKey();
            Console.WriteLine();
            LeerXML();
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
        private static void EscribirXML ()
        {
            XmlTextWriter escritorXML = new XmlTextWriter("agendaxml.xml", null);
            escritorXML.Formatting = Formatting.Indented;
            escritorXML.WriteStartDocument(true);
            escritorXML.WriteStartElement("DocumentElement");
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;
            do
            {
                linea = lector.ReadLine();
                if (linea != null)
                {
                    string[] valores = linea.Split(';');
                    escritorXML.WriteStartElement("contactos");//abrimos tag de contactos

                    escritorXML.WriteStartElement("nombre");
                    escritorXML.WriteValue(valores[0]);
                    escritorXML.WriteEndElement();//cerramos tag de nombre

                    escritorXML.WriteStartElement("apellido");
                    escritorXML.WriteValue(valores[1]);
                    escritorXML.WriteEndElement();//cerramos tag de apellido

                    escritorXML.WriteStartElement("email");
                    escritorXML.WriteValue(valores[2]);
                    escritorXML.WriteEndElement();//cerramos tag de email

                    escritorXML.WriteStartElement("telefono");
                    escritorXML.WriteValue(valores[3]);
                    escritorXML.WriteEndElement();//cerramos tag de telefono
                    escritorXML.WriteEndElement();//cerramos tag de contactos
                }

            } while (linea != null);
            escritorXML.WriteEndElement();//cerramos tag de DocumentElement
            escritorXML.WriteEndDocument();
            escritorXML.Close();

            lector.Close();
        }
        private static void LeerXML ()
        {
            XmlTextReader lectorXML = new XmlTextReader("agendaxml.xml");
            string tagAnterior = "";
            while(lectorXML.Read())
            {
                if (lectorXML.NodeType == XmlNodeType.Element)
                {
                    tagAnterior = lectorXML.Name;
                }
                else if (lectorXML.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(tagAnterior +": " + lectorXML.Value);
                }
            }
            lectorXML.Close();
        }
    }
}
