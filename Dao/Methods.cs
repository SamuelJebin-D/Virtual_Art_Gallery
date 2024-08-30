
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;
using System.Data;
using System.Data.SqlClient;
using VirtualArtGallery.excep;
using System.Linq.Expressions;

namespace VirtualArtGallery.Dao
{
    public class VirtualArtGalleryService : IVirtualArtGallery
    {

        string connectionString = "Data Source=SAM;Initial Catalog=VirtualArtGallery;Integrated Security=True";

        public VirtualArtGalleryService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public VirtualArtGalleryService()
        {

        }
        
        public bool AddArtwork(Artwork artwork)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkDuplicateQuery = "SELECT COUNT(*) FROM Artwork WHERE ArtworkID = @ArtworkID";

                SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection);
                checkDuplicateCommand.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                connection.Open();
                string query = "INSERT INTO Artwork (ArtworkID, Title, Description, CreationDate, Medium, ImageURL) " +
                               "VALUES (@ArtworkID, @Title, @Description, @CreationDate, @Medium, @ImageURL)";
                int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                if (duplicateCount > 0)
                {
                    return false;
                }
                else
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                    command.Parameters.AddWithValue("@Title", artwork.Title);
                    command.Parameters.AddWithValue("@Description", artwork.Description);
                    command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    command.Parameters.AddWithValue("@Medium", artwork.Medium);
                    command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;

                }
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Artwork SET Title = @Title, Description = @Description, " +
                                   "CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL " +
                                   "WHERE ArtworkID = @ArtworkID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", artwork.Title);
                    command.Parameters.AddWithValue("@Description", artwork.Description);
                    command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    command.Parameters.AddWithValue("@Medium", artwork.Medium);
                    command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    command.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new ArtWorkNotFoundException("Artwork with the given ID not found.");
                    }
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public bool RemoveArtwork(int artworkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Artwork WHERE ArtworkID = @ArtworkID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ArtworkID", artworkID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new ArtWorkNotFoundException($"Artwork with the given ID not found.");
                    }
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            
        }


        public Artwork GetArtworkById(int artworkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ArtworkID", artworkID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Artwork
                        {
                            ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                            Medium = reader["Medium"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                        };
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException($"Artwork with the given ID not found.");
                    }
                }
            }
            catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
            return null;
        }



        public List<Artwork> SearchArtworks(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Artwork> artworks = new List<Artwork>();

                while (reader.Read())
                {
                    artworks.Add(new Artwork
                    {
                        ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                        Medium = reader["Medium"].ToString(),
                        ImageURL = reader["ImageURL"].ToString(),
                       
                    });
                }

                return artworks;
            }
        }


        public bool AddArtworkToFavorite(int userId, int artworkId,String title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO UserFavorites (UserID, ArtworkID, Title) VALUES (@UserID, @ArtworkID,@Title)";
                string checkDuplicateQuery = "SELECT COUNT(*) FROM UserFavorites WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
                SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection);
                checkDuplicateCommand.Parameters.AddWithValue("@UserID", userId);
                checkDuplicateCommand.Parameters.AddWithValue("@ArtworkID", artworkId);

                connection.Open();

                int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                if (duplicateCount > 0)
                {
                    return false;
                }
                else
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ArtworkID", artworkId);
                    command.Parameters.AddWithValue("@Title", title);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

        }


        public bool RemoveArtworkFromFavorite(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM UserFavorites WHERE UserID = @UserID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new UserNotFoundException($"User with the given ID not found.");
                    }
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;

            }
        }


        public Dictionary<string,string> GetUserFavoriteArtworks(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM UserFavorites WHERE UserID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Dictionary<string,string> favoriteArtworks = new Dictionary<string,string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string artwork = reader["ArtworkID"].ToString();
                        string title = reader["Title"].ToString();
                        favoriteArtworks.Add(artwork, title);
                    }
                    return favoriteArtworks;
                }
                else
                {
                    throw new UserNotFoundException($"User with ID {userId} not found.");
                }
            }
            
        }
    }
}
