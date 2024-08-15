using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
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
