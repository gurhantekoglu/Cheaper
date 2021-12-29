using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using Cheaper.Model;
using System.Linq;

namespace Cheaper.Database
{
    public class FirebaseConnection
    {
        // Database Connection
        
        FirebaseClient firebase = new FirebaseClient("https://cheaper-1939d-default-rtdb.firebaseio.com/");

        // User Controls

        public async Task<List<User>> GetAllUsers()
        {

            return (await firebase
              .Child("Users")
              .OnceAsync<User>()).Select(item => new User
              {
                  UserId = item.Object.UserId,
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  ProfilePhoto = item.Object.ProfilePhoto
              }).ToList();
        }

        public async Task AddUser(int userId, string username, string password, string profilePhoto)
        {

            await firebase
              .Child("Users")
              .PostAsync(new User() { UserId = userId, Username = username, Password = password, ProfilePhoto = profilePhoto });
        }

        public async Task<User> GetUser(int userId)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.UserId == userId).FirstOrDefault();
        }

        public async Task UpdateUser(int userId, string username, string password, string profilePhoto)
        {
            var toUpdateUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.UserId == userId).FirstOrDefault();

            await firebase
              .Child("Users")
              .Child(toUpdateUser.Key)
              .PutAsync(new User() { UserId = userId, Username = username, Password = password, ProfilePhoto = profilePhoto });
        }

        public async Task DeleteUser(int userId)
        {
            var toDeleteUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.UserId == userId).FirstOrDefault();
            await firebase.Child("Users").Child(toDeleteUser.Key).DeleteAsync();
        }

        // Product Controls

        public async Task<List<Product>> GetAllProducts()
        {

            return (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Select(item => new Product
              {
                  ProductId = item.Object.ProductId,
                  Name = item.Object.Name,
                  Price = item.Object.Price,
                  ShopName = item.Object.ShopName,
                  ProductPhotoUrl = item.Object.ProductPhotoUrl,
                  PriceDate = item.Object.PriceDate
              }).ToList();
        }

        public async Task AddProduct(int productId, string name, string shopName, string productPhotoUrl, double price, DateTime priceDate)
        {

            await firebase
              .Child("Products")
              .PostAsync(new Product() { ProductId = productId, Name = name, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate });
        }

        public async Task<Product> GetProduct(int productId)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Product>();
            return allProducts.Where(a => a.ProductId == productId).FirstOrDefault();
        }

        public async Task UpdateProduct(int productId, string name, string shopName, string productPhotoUrl, double price, DateTime priceDate)
        {
            var toUpdateProduct = (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Where(a => a.Object.ProductId == productId).FirstOrDefault();

            await firebase
              .Child("Products")
              .Child(toUpdateProduct.Key)
              .PutAsync(new Product() { ProductId = productId, Name = name, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate });
        }
    }
}
