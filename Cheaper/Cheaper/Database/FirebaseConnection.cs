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
                  ProductName = item.Object.ProductName,
                  Price = item.Object.Price,
                  ShopName = item.Object.ShopName,
                  ProductPhotoUrl = item.Object.ProductPhotoUrl,
                  PriceDate = item.Object.PriceDate,
                  Username = item.Object.Username
              }).ToList();
        }

        public async Task<Product> GetProduct(string productName)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Product>();
            return allProducts.Where(a => a.ProductName == productName).FirstOrDefault();
        }

        public async Task AddProduct(string productBarcode, string productName, string shopName, string productPhotoUrl, double price, DateTime priceDate, string username)
        {

            await firebase
              .Child("Products")
              .PostAsync(new Product() { ProductBarcode = productBarcode, ProductName = productName, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate, Username = username });
        }

        public async Task<Product> CheckProductBarcode(string productBarcode)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Product>();
            return allProducts.Where(a => a.ProductBarcode == productBarcode).FirstOrDefault();
        }

        // FollowList Controls

        public async Task<List<Product>> GetAllFollowList()
        {

            return (await firebase
              .Child("FollowList")
              .OnceAsync<Product>()).Select(item => new Product
              {
                  ProductBarcode = item.Object.ProductBarcode,
                  ProductName = item.Object.ProductName,
                  Price = item.Object.Price,
                  ShopName = item.Object.ShopName,
                  ProductPhotoUrl = item.Object.ProductPhotoUrl,
                  PriceDate = item.Object.PriceDate
              }).ToList();
        }

        public async Task AddFollowList(string productBarcode, string productName, string shopName, string productPhotoUrl, double price, DateTime priceDate)
        {

            await firebase
              .Child("FollowList")
              .PostAsync(new Product() { ProductBarcode = productBarcode, ProductName = productName, ShopName = shopName, ProductPhotoUrl = productPhotoUrl, Price = price, PriceDate = priceDate });
        }

        public async Task<Product> CheckFollowListProductBarcode(string productBarcode)
        {
            var allFollowList = await GetAllFollowList();
            await firebase
              .Child("FollowList")
              .OnceAsync<Product>();
            return allFollowList.Where(a => a.ProductBarcode == productBarcode).FirstOrDefault();
        }

        public async Task DeleteFollowList(string productBarcode)
        {
            var toDeleteFollowList = (await firebase
              .Child("FollowList")
              .OnceAsync<Product>()).Where(a => a.Object.ProductBarcode == productBarcode).FirstOrDefault();
            await firebase.Child("FollowList").Child(toDeleteFollowList.Key).DeleteAsync();
        }

        // Image Controls

        // ProfilePhotos

        public async Task<string> UploadProfilePhoto(string fileName, Stream imageStream)
        {
            var uploadPhoto = await new FirebaseStorage("cheaper-1939d.appspot.com")
                .Child("ProfilePhotos")
                .Child(fileName)
                .PutAsync(imageStream);
            string imgurl = uploadPhoto;
            return imgurl;
        }

        // ProductPhotos

        public async Task<string> UploadProductPhoto(string fileName, Stream imageStream)
        {
            var uploadPhoto = await new FirebaseStorage("cheaper-1939d.appspot.com")
                 .Child("ProductPhotos")
                 .Child(fileName)
                 .PutAsync(imageStream);
            string imgurl = uploadPhoto;
            return imgurl;
        }
    }
}
