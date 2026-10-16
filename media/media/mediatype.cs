using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace media
{
    enum types
    {
        Movie,
        Book,
        AudioBook,
        Newspaper,
        Game,
        Invalid
    }

    abstract class mediatype
    {
        protected readonly string name;
        protected readonly string description;
        protected readonly string author;
        protected readonly int price;

        public bool borrowed;

        protected mediatype(string name_, int price_, string description_, string author)
        {
            name = name_;
            price = price_;
            description = description_;
            this.author = author;
            borrowed = false;
        }

        public string get_name() { return name;  }
        public string get_description() { return description; }
        public string get_author() { return author; }
        public int get_price() { return price; }
        public void borrow() { borrowed = true; }
        public void unborrow() { borrowed = false; }
        public virtual types get_media_type() { return types.Invalid;  }
    }
}
