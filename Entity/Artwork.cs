using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entity
{
    public class Artwork
    {
        private int artworkID;
        private string title;
        private string description;
        private DateTime creationDate;
        private string medium;
        private string imageURL;
        private int artistID;

        public Artwork()
        {
           
        }

        public Artwork(int artworkID, string title, string description, DateTime creationDate, string medium, string imageURL, int artistID)
        {
            this.artworkID = artworkID;
            this.title = title;
            this.description = description;
            this.creationDate = creationDate;
            this.medium = medium;
            this.imageURL = imageURL;
            this.artistID = artistID;
        }

    
        public int ArtworkID
        {
            get { return artworkID; }
            set { artworkID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public string Medium
        {
            get { return medium; }
            set { medium = value; }
        }

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        public int ArtistID
        {
            get { return artistID; }
            set { artistID = value; }
        }
    }
}