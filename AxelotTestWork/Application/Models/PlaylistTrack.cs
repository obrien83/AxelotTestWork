namespace AxelotTestWork.Application.Models
{
    public class PlaylistTrack
    {
        public long PlaylistId { get; set; }
        public long TrackId { get; set; }

        public virtual Playlists Playlist { get; set; }
        public virtual Tracks Track { get; set; }
    }
}
