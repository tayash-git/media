using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using media.media;
using System.Runtime.Remoting.Contexts;

namespace media
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("data.txt"))
            {
                List<string>  list_string = File.ReadAllLines("data.txt").ToList();

                foreach (string line in list_string)
                {
                    library.add_media(convert_string_to_media(line));
                }
            }

            mediatype t = new movie("yes", 12, "test", "me");
            library.add_media(t);

            while (true)
            {
                start1:
                Console.Clear();
                Console.WriteLine("enter command: [1] List all medias [2] Save to file [3] Read from file [4] List all media types");

                byte choice = Convert.ToByte(Console.ReadLine());

                Console.Clear();

                switch (choice)
                {
                    case 1:
                    case1start:
                        Console.Clear();
                        foreach (mediatype media in library.get_medias())
                        {
                            Console.WriteLine($"Media Type: {media.get_media_type().ToString()}");
                            Console.WriteLine($"Name: {media.get_name()}");
                            Console.WriteLine($"Price: ${media.get_price()}");
                            Console.WriteLine($"Author: {media.get_author()}");

                            if (media.borrowed)
                                Console.WriteLine("Currently borrowed");

                            Console.WriteLine("-------------------------------");
                        }

                        Console.WriteLine("[1] Add media");
                        Console.WriteLine("[2] Remove media");
                        Console.WriteLine("[3] View media");
                        Console.WriteLine("[4] Borrow media");
                        Console.WriteLine("[5] Return borrowed media");
                        Console.WriteLine("[6] Exit");
                        Console.WriteLine("");

                        byte choice2 = Convert.ToByte(Console.ReadLine());

                        switch (choice2)
                        {
                            case 1:
                                mediatype media1 = add_media();
                                library.add_media(media1);
                                break;

                            case 2:
                                Console.Write("Input media name to remove: ");
                                string name3 = Console.ReadLine();

                                string result =
                                    (library.remove_media(name3)) ? "Successful!" : "Media not found!";

                                Console.WriteLine(result);

                                break;

                            case 3:
                                bool found = false;

                                Console.WriteLine("Input media name to view:");
                                string name = Console.ReadLine();

                                foreach (mediatype media2 in library.get_medias())
                                {
                                    if (media2.get_name().Equals(name))
                                    {
                                        found = true;
                                        Console.WriteLine("");

                                        Console.WriteLine($"Media Type: {media2.get_media_type().ToString()}");
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine($"Name: {media2.get_name()}");
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine($"Description: {media2.get_description()}");
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine($"Price: ${media2.get_price()}");
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine($"Author: {media2.get_author()}");
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine("");

                                        break;
                                    }
                                }

                                if (!found)
                                    Console.WriteLine("No media found!");

                                Console.ReadKey();

                                break;

                            case 4:
                                bool found1 = false;
                                Console.WriteLine("Input media name to borrow:");
                                string name2 = Console.ReadLine();

                                foreach (mediatype media2 in library.get_medias())
                                {
                                    if (media2.get_name().Equals(name2))
                                    {
                                        found1 = true;

                                        if (media2.borrowed)
                                        {
                                            Console.WriteLine("Already borrowed!");
                                            break;
                                        }

                                        media2.borrow();
                                        Console.WriteLine("Successfully borrowed!");
                                        Console.ReadKey();
                                    }
                                }

                                if (!found1)
                                    Console.WriteLine("No media found!");

                                break;

                            case 5:
                                bool found2 = false;
                                Console.WriteLine("Input media name to return:");
                                string name4 = Console.ReadLine();

                                foreach (mediatype media2 in library.get_medias())
                                {
                                    if (media2.get_name().Equals(name4))
                                    {
                                        found2 = true;
                                        if (!media2.borrowed)
                                        {
                                            Console.WriteLine("Not borrowing!");
                                            break;
                                        }

                                        media2.unborrow();
                                        Console.WriteLine("Successfully returned!");
                                        Console.ReadKey();
                                    }
                                }

                                if (!found2)
                                    Console.WriteLine("No media found!");

                                break;

                            case 6:
                                goto start1;
                        }

                        Console.ReadKey();

                        goto case1start;

                    case 2:
                        library.save_to_file();
                        Console.WriteLine("Saved to file!");
                        Console.ReadKey();
                        break;

                    case 3:
                        string[] file_read = library.read_from_file();

                        foreach (string file in file_read)
                        {
                            mediatype media_read = convert_string_to_media(file);
                            library.add_media(media_read);
                        }

                        Console.WriteLine("File read!");

                        Console.ReadKey();

                        break;

                    case 4:
                        Console.WriteLine("Movie");
                        Console.WriteLine("Book");
                        Console.WriteLine("Audiobook");
                        Console.WriteLine("Newspaper");
                        Console.WriteLine("Game");

                        Console.ReadKey();

                        break;
                }
            }
        }

        static mediatype add_media()
        {
            mediatype media;

            Console.Write("Media Type: ");
            string mediatypeinput = Console.ReadLine();
            string mediatyperesult = mediatypeinput.Length > 0
                ? char.ToUpper(mediatypeinput[0]) + mediatypeinput.Substring(1).ToLower()
                : mediatypeinput;

            types enum_type = (types)Enum.Parse(typeof(types), mediatyperesult);
            Console.WriteLine("");

            Console.Write("Name: ");
            string name = Console.ReadLine();
            // make sure no duplicates exist in the library
            foreach (mediatype med in library.get_medias())
            {
                if (med.get_name().Equals(name))
                    return null;
            }
            Console.WriteLine("");

            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("Price: ");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");

            switch (enum_type)
            {
                case types.Movie:
                    media = new movie(name, price, description, author);
                    break;

                case types.Book:
                    media = new book(name, price, description, author);
                    break;

                case types.AudioBook:
                    media = new audiobook(name, price, description, author);
                    break;

                case types.Game:
                    media = new game(name, price, description, author);
                    break;

                case types.Newspaper:
                    media = new newspaper(name, price, description, author);
                    break;

                default:
                    throw new Exception("invalid enum type!");
            }

            Console.WriteLine("Successfully added media!");

            return media;
        }

        static mediatype convert_string_to_media(string txt)
        {
            string[] split = txt.Split(';');

            types enum_type = (types)Enum.Parse(typeof(types), split[0]);
            string name = split[1];
            int price = Convert.ToInt32(split[2]);
            string description = split[3];
            string author = split[4];
            bool borrowed = Convert.ToBoolean(split[5]);

            mediatype media;

            switch (enum_type)
            {
                case types.Movie:
                    media = new movie(name, price, description, author);
                    media.borrowed = borrowed;
                    break;

                case types.Book:
                    media = new book(name, price, description, author);
                    media.borrowed = borrowed;
                    break;

                case types.AudioBook:
                    media = new audiobook(name, price, description, author);
                    media.borrowed = borrowed;
                    break;

                case types.Game:
                    media = new game(name, price, description, author);
                    media.borrowed = borrowed;
                    break;

                case types.Newspaper:
                    media = new newspaper(name, price, description, author);
                    media.borrowed = borrowed;
                    break;

                default:
                    throw new Exception("invalid enum type!");
            }

            return media;
        }
    }
}
