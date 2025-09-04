namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();
        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Phone" });
            _categories.Add(new Category { CategoryId = 2, Name = "Desktop" });
            _categories.Add(new Category { CategoryId = 3, Name = "Tablet" });

            _products.Add(new Product { ProductId = 1, Name = "iPhone 15 Plus", Price = 50000.00m, IsActive = true, Image = "apple-iphone-15-plus.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "iPhone 15 Pro Max", Price = 80000.00m, IsActive = true, Image = "iphone-15-pro.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "iPhone 16", Price = 65000.00m, IsActive = true, Image = "iphone-16.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "iPhone 16 Pro Max", Price = 99000.00m, IsActive = true, Image = "iphone-16-pro-max.jpg", CategoryId = 1 });

            _products.Add(new Product { ProductId = 5, Name = "MacBook Air", Price = 35000.00m, IsActive = true, Image = "macbook-air.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 6, Name = "MacBook Pro", Price = 55000.00m, IsActive = true, Image = "macbook-pro.jpg", CategoryId = 2 });

            _products.Add(new Product { ProductId = 7, Name = "iPad Air", Price = 29000.00m, IsActive = true, Image = "ipad-air.jpg", CategoryId = 3 });
            _products.Add(new Product { ProductId = 8, Name = "iPad Pro", Price = 49000.00m, IsActive = true, Image = "ipad-pro.png", CategoryId = 3 });
        }

        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if(entity != null) 
            {
                if(!string.IsNullOrEmpty(updatedProduct.Name)) 
                {
                    entity.Name = updatedProduct.Name;
                }
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;
            }
        }

        public static void EditIsActive(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if(entity != null) 
            {
                entity.IsActive = updatedProduct.IsActive;
            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);

            if(entity != null) 
            {
                _products.Remove(entity);
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
    }
}