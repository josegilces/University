using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricesBinarias
{
    class Program
    {
        static byte rows = 3;
        static byte columns = 4;
        static string path = @"c:\intel\h.txt";


        //en UN ARCHIVO, SE ALMACENE MATRICES. ESTABLECER QUE SE PUEDA AGREGAR MUCHAS MATRICES. PARA PODER IDENTIFICAR CADA UNA DE LAS MATRICES, NECESITO DETERMIANR UNA SECUENCIA, LA CANTIDAD DE FILAS Y COLUMNAS, LUEGO PROCEDO A GRABAR LOS ELEMENTOS DE ESA MATRIZ.  Cada matriz debe tener 12 elementos de 2 cifras. Usar sistemas de guardado binario.
        static void Main(string[] args)
        {
            //welcome message and aesthetics

            string username = Environment.UserName;
            Console.Title = "Binary Files With Arrays v1.0";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(100, 30); // width x height (in characters)
            Console.SetBufferSize(100, 30); // buffer size (equal to or greater than the window)

            byte option = 0;
            
            bool setDimensions = true;

            do
            {
                Console.Clear();

                Console.Write(" Welcome ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(username);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("!");

                Console.WriteLine("\n\tMenu");
                Console.WriteLine(" 1.- Generate a new matrix in binary file");
                Console.WriteLine(" 2.- Matrix operations");
                Console.WriteLine(" 3.- Read file");
                Console.WriteLine(" 4.- Delete matrix");
                Console.WriteLine(" 5.- Delete file");
                Console.WriteLine(" 6.- Leave");
                Console.Write("\n Enter option: ");
                option = byte.Parse(Console.ReadLine());

                Console.Clear();

                switch (option)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Generate a new matrix in binary file");

                        //if (!setDimensions)
                        //{
                        //    Console.WriteLine("\n Write the array's dimensions");
                        //    Console.Write(" Rows: ");
                        //    rows = Convert.ToByte(Console.ReadLine());
                        //    Console.Write(" Columns: ");
                        //    columns = Convert.ToByte(Console.ReadLine());

                        //    setDimensions = true; //to know if the user has already set dimensions and we dont have issues with width x height
                        //}

                        Console.Write(" First, you need to write the path of the file: ");
                        path = Console.ReadLine();
                        Console.Write(" Then, you need to write a matrix identifier: ");
                        string matrixIdentifier = Console.ReadLine();

                        function_WriteOnFile(matrixIdentifier);


                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Matrix operations");
                        Console.WriteLine(" Addition");
                        Console.WriteLine(" Subtraction");
                        Console.WriteLine(" Multiplication");
                        Console.WriteLine("\n Choose the operation: ");
                        byte option2 = Convert.ToByte(Console.ReadLine());

                        switch (option2)
                        {
                            case 1:
                                Console.Write(" You need to write the first matrix identifier: ");
                                string matrixIdentifier1 = Console.ReadLine();
                                Console.Write(" You need to write the second matrix identifier: ");
                                string matrixIdentifier2 = Console.ReadLine();
                                function_Addition(matrixIdentifier1, matrixIdentifier2);
                                break;
                        }
                        break;
                    case 3:
                        function_FileReading();
                        Console.ReadKey();

                        break;
                    //case 3:
                    //    fn_ActualizaArchivo(ruta);
                    //    break;
                    //case 4:
                    //    fn_EliminarArchivo(ruta);
                    //    break;
                    case 5:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" -> ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Delete file");
                            File.Delete(path);
                            Console.Write("\n The file has been deleted successfully!");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                }

            }
            while (option != 6);
        }
        //function to read the dimensions to be used from the keyboard and write them to a file
        static void function_WriteOnFile(string matrixIdentifier)
        {
            try
            {
                int value;

                Random random = new Random();
                FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file);


                //generate rows and columns
                writer.Write(matrixIdentifier);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        value = random.Next(10, 100);
                        writer.Write(value);
                    }
                }

                //close objects
                writer.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in writing the file: {0}", ex.Message);
            }

        }
        static void function_FileReading()
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(file);
                int counter = 0; //this is a counter for aesthetic purposes

                //read rows
                while (reader.PeekChar() != -1)
                {
                    ++counter;
                    Console.WriteLine($"Matrix {counter}: {reader.ReadString()}");

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            Console.Write(reader.ReadInt32() + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }

                reader.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: {0}", ex.Message);
            }
            Console.ReadKey();

        }
        static void function_Addition(string matrixIdentifier1, string matrixIdentifier2)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(file);
            int counter = 0; //this is a counter for aesthetic purposes

            int[,] matrix1 = new int[3, 4];
            int[,] matrix2 = new int[3, 4];

            //print and save the values in a two-dimensional array
            while (reader.PeekChar() != -1)
            {
                string matrixIdentifier = reader.ReadString();

                if (matrixIdentifier == matrixIdentifier1)
                {
                    ++counter;
                    Console.WriteLine($"Matrix {counter}: {matrixIdentifier}");

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int value = reader.ReadInt32();
                            matrix1[i, j] = value;
                            Console.Write(matrix1[i, j] + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            reader.ReadInt32();
                        }
                    }
                }
            }

           
            reader.Close();
            file.Close();
        }

    }
}
