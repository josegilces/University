using System;
using System.IO;

namespace MatricesBinarias
{
    /*En un archivo matrices.bin, se guardarán la configuración y elementos de matrices. La configuración es secuencia(identificador de la matriz), filas y columnas. Los elementos de la matriz serán generados de manera aletoria con elementos de 2 cifras.
En matrices.bin se guardan todas la matrices que se soliciten generar y la secuencia identificará la matriz.
Se podrán realizar operaciones entre matrices (+, -, *), para lo cual se seleccionará dos matrices y se mostrará el resultado de la operación en la consola.
La aplicación contendrá una opción que permita mostrar por consola una determinada matriz, para lo cual se solicitará por teclado el ingreso de la secuencia de la matriz que se desee revisar.
    */
    class Program
    {
        static byte rows = 3;
        static byte columns = 4;
        static string path = "";
        static byte counter = 0; //help assign a name to the result matrices

        static void Main(string[] args)
        {
            //welcome message and aesthetics

            string username = Environment.UserName;
            Console.Title = "Binary Files With Arrays v1.0";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(100, 30); // width x height (in characters)
            Console.SetBufferSize(100, 30); // buffer size (equal to or greater than the window)

            byte option = 0; //option from Menu

            do
            {
                Console.Clear();

                Console.Write(" Welcome ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(username);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("!");

                Console.WriteLine("\n\tMenu");
                Console.WriteLine(" 1.- Set the path of the file");
                Console.WriteLine(" 2.- Generate a new matrix in binary file");
                Console.WriteLine(" 3.- Matrix operations");
                Console.WriteLine(" 4.- Read file");
                Console.WriteLine(" 5.- Read matrix");
                Console.WriteLine(" 6.- Delete file");
                Console.WriteLine(" 7.- Leave");
                Console.Write("\n Enter option: ");

                //input validation
                try
                {
                    option = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine(" Write a option! (Ex: 1)");
                    Console.ReadKey();
                }

                Console.Clear();

                switch (option)
                {
                    //set the path of the file
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Set the path of the file");

                        Console.Write("\n Write the path of the file: ");
                        path = Console.ReadLine();
                        break;

                    //generate a new matrix in binary file
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Generate a new matrix in binary file");
                        if (!(path.Length > 0))
                        {
                            Console.Write("\n First, you need to set the path of the file!");
                        }
                        else
                        {
                            Console.Write("\n You need to write a matrix identifier: ");
                            string matrixIdentifier = Console.ReadLine();

                            WriteOnFile(matrixIdentifier);
                            Console.WriteLine("\n The file has been deleted successfully!");
                        }

                        Console.ReadKey();
                        break;
                    //matrix operations
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Matrix operations");
                        Console.WriteLine("\n 1.- Addition");
                        Console.WriteLine(" 2.- Subtraction");
                        Console.WriteLine(" 3.- Multiplication");
                        Console.WriteLine(" 4.- Back");

                        Console.Write("\n Choose the operation: ");

                        //input validation
                        byte operation = 0;

                        try
                        {
                            operation = Convert.ToByte(Console.ReadLine());
                            if (operation == 4)
                            {
                                break;
                            }
                            else
                            {
                                Console.Write("\n You need to write the first matrix identifier: ");
                                string matrixIdentifier1 = Console.ReadLine();
                                Console.Write("\n You need to write the second matrix identifier: ");
                                string matrixIdentifier2 = Console.ReadLine();

                                SaveInsideArray(matrixIdentifier1, matrixIdentifier2, operation);
                                Console.ReadKey();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(" Write a option! (Ex: 1)");
                            Console.ReadKey();
                        }

                        break;
                    //read file
                    case 4:

                        FileReading();
                        Console.ReadKey();

                        break;
                    //read matrix
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" -> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" Read matrix");
                        Console.Write("\n Write the matrix identifier: ");
                        string identifier = Console.ReadLine();
                        FileReading(true, identifier);

                        Console.ReadKey();
                        break;

                    //delete file
                    case 6:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" -> ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Delete file");
                            File.Delete(path);
                            path = "";
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
            while (option != 7);
            Console.Write("Thank u for using my program ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(username);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("!");
        }
        //function to read the dimensions to be used from the keyboard and write them to a file
        static void WriteOnFile(string matrixIdentifier, bool mode = false, int[,] result = null)
        {
            try
            {
                int value;

                Random random = new Random();
                FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file);

                //here the program can choose whether to generate values for the rows and columns or to save the result of a matrices operation
                if (!mode)
                {
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
                }
                else //save the result of a matrices operation
                {
                    writer.Write(matrixIdentifier);

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            value = result[i, j];
                            writer.Write(value);
                        }
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
        static void FileReading(bool mode = false, string matrixIdentifier = null)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(file);
                int counter = 0; //this is a counter for aesthetic purposes

                //here the program can choose whether to read all values for the rows and columns or a specified matrix
                if (!mode)
                {
                    //read all rows and columns
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
                }
                else //read specified matrix
                {
                    bool found = false;
                    while (reader.PeekChar() != -1)
                    {
                        string identifier = reader.ReadString();
                        if (identifier == matrixIdentifier)
                        {
                            Console.WriteLine($"Matrix {identifier}");
                            for (int i = 0; i < rows; i++)
                            {
                                for (int j = 0; j < columns; j++)
                                {
                                    Console.Write(reader.ReadInt32() + "\t");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine();

                            found = true;
                        }
                        else //read the binary file's values until find the matrix needed
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
                    //message if dont found the matrix 
                    if (!found) Console.WriteLine("\n The matrix could not be found.");

                }

                //free memory
                reader.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: {0}", ex.Message);
            }

        }
        static void SaveInsideArray(string matrixIdentifier1, string matrixIdentifier2, byte operation)
        {
            //connection to the file
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(file);

            int[,] matrix1 = new int[3, 4]; //array to save the matrices
            int[,] matrix2 = new int[3, 4];

            //print and save the values in a two-dimensional array
            while (reader.PeekChar() != -1)
            { 
                string matrixIdentifier = reader.ReadString();

                if (matrixIdentifier == matrixIdentifier1) //identify the matrixIdentifier 1 and 2 inside the binary file and save them 
                {
                    Console.WriteLine($"Matrix 1: {matrixIdentifier}");

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
                }
                else
                {
                    if (matrixIdentifier == matrixIdentifier2) //identify and save second matrix inside the binary file
                    {
                        Console.WriteLine($"Matrix 2: {matrixIdentifier}");

                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                int value = reader.ReadInt32();
                                matrix2[i, j] = value;
                                Console.Write(matrix2[i, j] + "\t");
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        //read the binary file's values until find the matrix needed 
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                reader.ReadInt32();
                            }
                        }
                    }
                }
            }
            //free memory
            reader.Close();
            file.Close();

            //message if dont found the matrix 
            if (!(matrix1[0, 0] > 0)) Console.WriteLine(" The first matrix could not be found.");
            if (!(matrix2[0, 0] > 0)) Console.WriteLine(" The second matrix could not be found.");
            else
            {
                Operations(matrix1, matrix2, operation);
            }

        }
        static void Operations(int[,] matrix1, int[,] matrix2, byte operation)
        {
            int[,] result = new int[3, 4];

            switch (operation)
            {
                //addition operation
                case 1:
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                int temp = matrix1[i, j] + matrix2[i, j];
                                result[i, j] = temp;
                            }
                        }
                        break;
                    }
                //subtraction
                case 2:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int temp = matrix1[i, j] - matrix2[i, j];
                            result[i, j] = temp;
                        }
                    }
                    break;
                //multiplication
                case 3:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int temp = matrix1[i, j] * matrix2[i, j];
                            result[i, j] = temp;
                        }
                    }
                    break;
            }
            
            //print the results
            Console.WriteLine(" The matrix result between Matrix 1 en Matrix 2 is:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(result[i, j] + "\t");
                }
                Console.WriteLine();
            }
            ++counter;
            WriteOnFile($"MatrixResult {counter}", true, result);

        }
    }
}
