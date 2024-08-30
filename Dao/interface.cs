using VirtualArtGallery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Dao
{
    public interface IVirtualArtGallery
    {
        // Artwork Management
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);

        // User Favorites
        bool AddArtworkToFavorite(int userId, int artworkId,String title);
        bool RemoveArtworkFromFavorite(int userId);
        Dictionary<String,String> GetUserFavoriteArtworks(int userId);
    }
}
