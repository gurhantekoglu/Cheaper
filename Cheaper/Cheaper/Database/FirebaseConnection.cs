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
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  ProfilePhotoUrl = item.Object.ProfilePhotoUrl
              }).ToList();
        }

        public async Task AddUser(int userId, string username, string password, string profilePhotoUrl)
        {

            await firebase
              .Child("Users")
              .PostAsync(new User() { Username = username, Password = password, ProfilePhotoUrl = profilePhotoUrl });
        }

        public async Task<User> GetUser(string username)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == username).FirstOrDefault();
        }

        public async Task UpdateUser(string username, string password, string profilePhotoUrl)
        {
            var toUpdateUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == username).FirstOrDefault();

            await firebase
              .Child("Users")
              .Child(toUpdateUser.Key)
              .PutAsync(new User() { Username = username, Password = password, ProfilePhotoUrl = profilePhotoUrl });
        }

        public async Task DeleteUser(string username)
        {
            var toDeleteUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == username).FirstOrDefault();
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
