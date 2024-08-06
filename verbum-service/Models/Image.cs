using System;
using System.Collections.Generic;

namespace verbum_service.Models
{
    public partial class Image
    {
        public Image()
        {
        }

        public int Id { get; set; }
        public string Url { get; set; } = null!;

    }
}
