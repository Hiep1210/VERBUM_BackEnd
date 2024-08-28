using System;
using System.Collections.Generic;

namespace verbum_service_domain.Models
{
    public partial class Image
    {
        public Image()
        {
            Users = new HashSet<User>();
        }

        public int ImageId { get; set; }
        /// <summary>
        /// save link, image will be save on thirdparty (cloudinary)
        /// </summary>
        public string ImageLink { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
