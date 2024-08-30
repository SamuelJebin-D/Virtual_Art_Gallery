using VirtualArtGallery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Dao;
using VirtualArtGallery.excep;
using System.Diagnostics.SymbolStore;

namespace VirtualArtGallery.Main
{
    class MainModule
    {
        private static string connectionString = "Data Source=SAM;Initial Catalog=VirtualArtGallery;Integrated Security=True";

        static void Main(string[] args)
        {
            bool exitRequest = false;
            VirtualArtGalleryService artGalleryService = new VirtualArtGalleryService(connectionString);
            while (!exitRequest)
            {
                Console.WriteLine("\n1. Add Art Work");
                Console.WriteLine("2. Update art work");
                Console.WriteLine("3. Remove art work");
                Console.WriteLine("4. Get artwork by ID");
                Console.WriteLine("5. Search artwork by keyword");
                Console.WriteLine("6. Add artwork to favorite");
                Console.WriteLine("7. Remove artwork from favorite");
                Console.WriteLine("8. Get user favorite artwork");
                Console.WriteLine("9. Exit");
                Console.WriteLine("Enter the your choice");
                int temp = Convert.ToInt32(Console.ReadLine());

                //Add artwork
                
                if (temp == 1)
                {
                    Console.WriteLine("Enter Artwork ID:");
                    int awID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter Artwork Title:");
                    string tit = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Description:");
                    string desc = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Medium:");
                    string med = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Image URL:");
                    string URL = Console.ReadLine();

                    Console.WriteLine("Adding artwork...");
                    Artwork newArtwork = new Artwork
                    {
                        ArtworkID = awID,
                        Title = tit,
                        Description = desc,
                        CreationDate = DateTime.Now,
                        Medium = med,
                        ImageURL = URL
                    };
                    bool isArtworkAdded = artGalleryService.AddArtwork(newArtwork);
                    if (isArtworkAdded)
                    {
                        Console.WriteLine("Artwork added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add artwork.");
                    }
                }


                //update Artwork
                else if (temp == 2)
                {

                    Console.WriteLine("Enter Artwork ID:");
                    int awID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter Artwork Title:");
                    string tit = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Description:");
                    string desc = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Medium:");
                    string med = Console.ReadLine();

                    Console.WriteLine("Enter Artwork Image URL:");
                    string URL = Console.ReadLine();

                    Console.WriteLine("Updating artwork...");
                    Artwork updatedArtwork = new Artwork
                    {
                        ArtworkID = awID, 
                        Title = tit,
                        Description = desc,
                        CreationDate = DateTime.Now,
                        Medium = med,
                        ImageURL = URL
                    };
                    bool isArtworkUpdated = artGalleryService.UpdateArtwork(updatedArtwork);
                    if (isArtworkUpdated)
                    {
                        Console.WriteLine("Artwork updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update artwork.");
                    }
                }


                //Remove artwork
                else if (temp == 3)
                {

                    Console.WriteLine("Enter the ArtworkID that has to be removed");
                    int artworkIdToRemove = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Removing artwork...");
                    bool isArtworkRemoved = artGalleryService.RemoveArtwork(artworkIdToRemove);
                    if (isArtworkRemoved)
                    {
                        Console.WriteLine("Artwork removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to remove artwork.");
                    }
                }


                //get artwork by ID
                else if (temp == 4)
                {
                    Console.WriteLine("Enter the ArtworkID to retrieve details");
                    int artworkIdToRetrieve = Convert.ToInt32(Console.ReadLine()); 
                    Console.WriteLine("Retrieving artwork by ID...");
                    Artwork retrievedArtwork = artGalleryService.GetArtworkById(artworkIdToRetrieve);
                    if (retrievedArtwork != null)
                    {
                        Console.WriteLine($"ArtworkID: {retrievedArtwork.ArtworkID} \nartworkTitle: {retrievedArtwork.Title} \nDescription: {retrievedArtwork.Description} \nCreationDate: {retrievedArtwork.CreationDate} \nMedium: {retrievedArtwork.Medium} \nImageURL {retrievedArtwork.ImageURL}");
                    }
                    else
                    {
                        Console.WriteLine("Artwork not found.");
                    }
                }


                //Search artwork
                else if (temp == 5)
                {
                    Console.WriteLine("Enter the keyword to search artwork");
                    string keyword = Console.ReadLine();
                    Console.WriteLine("Searching artworks...");
                    List<Artwork> searchResults = artGalleryService.SearchArtworks(keyword);
                    if (searchResults.Count > 0)
                    {
                        Console.WriteLine($"Search results for '{keyword}':");
                        foreach (Artwork artwork in searchResults)
                        {
                           
                            Console.WriteLine($"\nArtworkID: {artwork.ArtworkID} \nartworkTitle: {artwork.Title} \nDescription: {artwork.Description} \nCreationDate: {artwork.CreationDate} \nMedium: {artwork.Medium} \nImageURL {artwork.ImageURL}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No artworks found matching the keyword '{keyword}'.");
                    }
                }


                //add artwork to fav table
                else if (temp == 6)
                {
                    Console.WriteLine("Enter UserID");
                    int userId = Convert.ToInt32(Console.ReadLine()); 
                    Console.WriteLine("Enter ArtworkID");
                    int artworkIdToAddToFavorites = Convert.ToInt32(Console.ReadLine()); 
                    Console.WriteLine("Enter title of the artwork");
                    String title = Console.ReadLine();
                    Console.WriteLine("Adding artwork to favorites...");
                    bool isArtworkAddedToFavorites = artGalleryService.AddArtworkToFavorite(userId, artworkIdToAddToFavorites, title);
                    if (isArtworkAddedToFavorites)
                    {
                        Console.WriteLine("Artwork added to favorites successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add artwork to favorites.");
                    }
                }


                //Remove artwork from fav
                else if (temp == 7)
                {
                    Console.WriteLine("Enter UserID");
                    int userIdq = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Removing artwork from favorites...");
                    bool isArtworkRemovedFromFavorites = artGalleryService.RemoveArtworkFromFavorite(userIdq);
                    if (isArtworkRemovedFromFavorites)
                    {
                        Console.WriteLine("Artwork removed from favorites successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to remove artwork from favorites.");
                    }
                }


                //get fav artwork
                else if (temp == 8)
                {
                    Console.WriteLine("Enter UserID");
                    int userIdh = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Retrieving user's favorite artworks...");
                    try
                    {
                        Dictionary<string, string> favoriteArtworkIds = artGalleryService.GetUserFavoriteArtworks(userIdh);
                        if (favoriteArtworkIds.Count > 0)
                        {
                            Console.WriteLine($"User's favorite artworks:");
                            foreach (var artworkId in favoriteArtworkIds)
                            {
                                string artwork = artworkId.Key;
                                string title = artworkId.Value;
                                Console.WriteLine($"Artwork ID: {artwork}, Title: {title}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No favorite artworks found for the user.");
                        }
                    }
                    catch (UserNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }


                else if(temp == 9)
                {
                    exitRequest = true;
                    Console.WriteLine("Exited");
                    break;
                }


                else
                {
                    Console.WriteLine("Invalid option");
                }
     
            }
        }
    }
}