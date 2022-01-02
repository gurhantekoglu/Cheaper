using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using Cheaper.Model;
using System.Linq;
using Firebase.Storage;
using System.IO;
using Xamarin.Essentials;

namespace Cheaper.Database
{
    public class FirebaseConnection
    {
        // Database Connection
        
        FirebaseClient firebase = new FirebaseClient("https://cheaper-1939d-default-rtdb.firebaseio.com/");

        FirebaseStorage firebaseStorage = new FirebaseStorage("cheaper-1939d.appspot.com");

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

        public async Task<User> GetUser(string username)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == username).FirstOrDefault();
        }

        public async Task AddUser(string username, string password, string profilePhotoUrl)
        {
            await firebase
              .Child("Users")
              .PostAsync(new User() { Username = username, Password = password, ProfilePhotoUrl = profilePhotoUrl });
        }

        public async Task UpdateUser(User user, string newPassword)
        {
            var toUpdateUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == user.Username).FirstOrDefault();

            await firebase
              .Child("Users")
              .Child(toUpdateUser.Key)
              .PutAsync(new User() { Username = user.Username, Password = newPassword, ProfilePhotoUrl = user.ProfilePhotoUrl });
        }

        public async Task DeleteUser(string username)
        {
            var toDeleteUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == username).FirstOrDefault();
            await firebase.Child("Users").Child(toDeleteUser.Key).DeleteAsync();
        }

        public async Task<User> CheckUsername(string username)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == username).FirstOrDefault();
        }

        public async Task<User> CheckUser(string username, string password)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
        }

        // Product Controls

        public async Task<List<Product>> GetAllProducts()
        {

            return (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Select(item => new Product
              {
                  ProductBarcode = item.Object.ProductBarcode,
                  Name = item.Object.Name,
                  Price = item.Object.Price,
                  ShopName = item.Object.ShopName,
                  ProductPhotoUrl = item.Object.ProductPhotoUrl,
                  PriceDate = item.Object.PriceDate
              }).ToList();
        }

        public async Task<Product> GetProduct(string productBarcode)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Product>();
            return allProducts.Where(a => a.ProductBarcode == productBarcode).FirstOrDefault();
        }

        public async Task AddProduct(string productBarcode, string name, string shopName, string productPhotoUrl, double price, DateTime priceDate)
        {

            await firebase
              .Child("Products")
              .PostAsync(new Product() { ProductBarcode = productBarcode, Name = name, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate });
        }

        public async Task UpdateProduct(string productBarcode, string name, string shopName, string productPhotoUrl, double price, DateTime priceDate)
        {
            var toUpdateProduct = (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Where(a => a.Object.ProductBarcode == productBarcode).FirstOrDefault();

            await firebase
              .Child("Products")
              .Child(toUpdateProduct.Key)
              .PutAsync(new Product() { ProductBarcode = productBarcode, Name = name, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate });
        }

        // Image Controls

        // ProfilePhotos

        public async Task<string> UploadProfilePhoto(Stream fileStream, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child("ProfilePhotos")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task<string> GetUrl(string fileName, FileResult result)
        {
            return await firebaseStorage
                .Child("ProfilePhotos")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
    }
}
