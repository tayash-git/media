using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace media.media
{
    class audiobook : mediatype
    {
        public audiobook(string name_, int price_, string description_, string author)
            : base(name_, price_, description_, author) { }

        public override types get_media_type()
        {
            return types.AudioBook;
        }
    }
}
