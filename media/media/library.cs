using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace media
{
    static class library
    {
        static List<mediatype> media_list = new List<mediatype>();
        public static bool add_media(mediatype media)
        {
            foreach (mediatype med in media_list)
            {
                if (med.get_name() == media.get_name())
                    return false;
            }

            media_list.Add(media);

            return true;
        }

        public static bool remove_media(string name)
        {
            foreach (mediatype med in media_list)
            {
                if (med.get_name().Equals(name))
                {
                    media_list.Remove(med);

                    return true;
                }
            }

            return false;
        }

        public static List<mediatype> get_medias() { return media_list; }

        public static void save_to_file()
        {
            List<string> media_data = new List<string>();

            foreach (mediatype media in media_list)
            {
                media_data.Add(
                    $"{media.get_media_type().ToString()};{media.get_name()};{media.get_price()};{media.get_description()};{media.get_author()};{media.borrowed.ToString()}"
                );
            }

            File.WriteAllLines("data.txt", media_data);
        }

        public static string[] read_from_file()
        {
            return File.ReadAllLines("data.txt");
        }
    }
}