﻿using System.Collections.Generic;

namespace AxelotTestWork.Application.Models
{
    public class Tracks
    {
        public Tracks()
        {
            InvoiceItems = new HashSet<InvoiceItems>();
            PlaylistTrack = new HashSet<PlaylistTrack>();
        }

        public long TrackId { get; set; }
        public string Name { get; set; }
        public long? AlbumId { get; set; }
        public long MediaTypeId { get; set; }
        public long? GenreId { get; set; }
        public string Composer { get; set; }
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Albums Album { get; set; }
        public virtual Genres Genre { get; set; }
        public virtual MediaTypes MediaType { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
        public virtual ICollection<PlaylistTrack> PlaylistTrack { get; set; }
    }
}
