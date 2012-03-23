using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Library
{
    public struct BookInfo
    {
        public double ISBN;
        public string Publishers;
        public string Title;
        public string Author;
        public string Genre;
        public int NumberOfPages;
    }

    class Library
    {
        ArrayList List = new ArrayList();
        string buffer;
        BookInfo[] Book = new BookInfo[50];
        BookInfo bufferBook = new BookInfo();
        
        public Library()
        {
            
            /*
            FileStream Stream = new FileStream(@"E:\C#\Library.bin",FileMode.OpenOrCreate,FileAccess.Write);
            StreamWriter Writer = new StreamWriter(Stream);

            for ( int i = 0 ; i < 3 ; i++ )
            {
                buffer = "123456789543 Moscow TheLostSymbol DanBrown - 572";
                Writer.WriteLine(buffer);   
            }

            Writer.Close();
            Stream.Close();
            */
            
            FileStream Stream = new FileStream(@"E:\C#\Library.bin",FileMode.Open,FileAccess.Read);
            StreamReader Reader = new StreamReader(Stream);
            int i = 0;
            while (!Reader.EndOfStream)
            {
                buffer = Reader.ReadLine();
                string[] split = buffer.Split(' ');
                Book[i].ISBN = Convert.ToDouble(split[0]);
                Book[i].Publishers = split[1];
                Book[i].Title = split[2];
                Book[i].Author = split[3];
                Book[i].Genre = split[4];
                Book[i].NumberOfPages = Convert.ToInt32(split[5]);
                List.Add(Book[i]);
                i++;
            }

            Reader.Close();
            Stream.Close();
        }
        
        public bool AddBook()
        {
            Console.Write("Please input ISBN: ");
            bufferBook.ISBN = Convert.ToDouble(Console.ReadLine());

            Console.Write("Please input Publishers: ");
            bufferBook.Publishers = Console.ReadLine();

            Console.Write("Please input Title: ");
            bufferBook.Title = Console.ReadLine();

            Console.Write("Please input Author: ");
            bufferBook.Author = Console.ReadLine();

            Console.Write("Please input Genre: ");
            bufferBook.Genre = Console.ReadLine();

            Console.Write("Please input NumberOfPages: ");
            bufferBook.NumberOfPages = Convert.ToInt32(Console.ReadLine());

            List.Add(bufferBook);
            UpdateLibrary();

            bool Succesfully = true;
            return Succesfully;
        }

        public bool DeleteBook()
        {
            Console.WriteLine("Input ISBN to delete: ");
            int a = IndexForBook(Console.ReadLine());
            if (a >= 0)
            {
                List.RemoveAt(a);
                UpdateLibrary();
            }
            else
                Console.WriteLine("Sorry, cant find this book");
            bool Succesfully = true;
            return Succesfully;
        }

        public bool UpdateBook()
        {
            Console.WriteLine("Input ISBN to update: ");
            string a = Console.ReadLine();
            foreach (BookInfo obj in List)
            {
                if (Convert.ToDouble(a)==obj.ISBN)
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", obj.ISBN, obj.Publishers, obj.Title, obj.Author, obj.Genre, obj.NumberOfPages);
            }

            Console.WriteLine("Input new description for book: ");
            if (AddBook())
            {
                List.RemoveAt((IndexForBook(a)));
                UpdateLibrary();
            }

            bool Succesfully = true;
            return Succesfully;
        }

        public int IndexForBook(string Variable)
        {
            foreach (BookInfo obj in List)
            {
                if (obj.ISBN==Convert.ToDouble(Variable))
                    return List.IndexOf(obj);
            }
            return 0;
        }

        void UpdateLibrary()
        {
            FileStream Stream = new FileStream(@"E:\C#\Library.bin", FileMode.Create, FileAccess.Write);
            StreamWriter Writer = new StreamWriter(Stream);

            foreach (BookInfo obj in List)
            {
                Writer.WriteLine("{0} {1} {2} {3} {4} {5}", obj.ISBN, obj.Publishers, obj.Title, obj.Author, obj.Genre, obj.NumberOfPages);
            }

            Writer.Close();
            Stream.Close();
            
        }

        public void ShowBooks()
        {
            foreach (BookInfo obj in List)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", obj.ISBN, obj.Publishers, obj.Title, obj.Author, obj.Genre, obj.NumberOfPages);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();
            //lib.AddBook();
            lib.DeleteBook();
        }
    }
}
