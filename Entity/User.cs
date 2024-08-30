using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entity
{
    public class User
    {
        private int userID;
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string profilePicture;
        private List<int> favoriteArtworks;

        public User()
        {
            
            favoriteArtworks = new List<int>();
        }

        public User(int userID, string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture)
        {
            this.userID = userID;
            this.username = username;
            this.password = password;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            favoriteArtworks = new List<int>();
        }

        
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string ProfilePicture
        {
            get { return profilePicture; }
            set { profilePicture = value; }
        }

        public List<int> FavoriteArtworks
        {
            get { return favoriteArtworks; }
            set { favoriteArtworks = value; }
        }
    }
}