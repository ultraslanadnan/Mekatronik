using Mekatronik.Concrete;
using Mekatronik.CustomModels;
using Mekatronik.Models;
using Mekatronik.Paging;
using Mekatronik.MyExtensions;
using Mekatronik.ModelValidation;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Mekatronik.Controllers
{
    public class ProductsController : Controller
    {
        ProductManager pm = new ProductManager();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int pg = 1)
        {

            const int pageSize = 6;
            if (pg < 1) { pg = 1; }

            var values = pm.GetListAllProduct().OrderByDescending(x => x.ProductCreateDate).ToList();
            
            if (values != null && values.Count > 0)
            {
                
                int recsCount = values.Count;
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var newvalues  = values.Skip(recSkip).Take(pager.PageSize).ToList();
                if (newvalues.Count == 0 && pg > 1)
                {
                    pg = pg - 1;
                    pager = new Pager(recsCount, pg, pageSize);
                    recSkip = (pg - 1) * pageSize;
                    newvalues = values.Skip(recSkip).Take(pager.PageSize).ToList();
                }
                values = newvalues;
                this.ViewBag.Pager = pager;

            }
            return View(values);
        }
        [HttpGet]
        public IActionResult Edit(int id, int pg = 1)
        {
            Product product = pm.GetProductById(id);
            ProductWithFile productWithFile = new ProductWithFile();
            productWithFile.ProductStatus = product.ProductStatus;
            productWithFile.ProductName = product.ProductName;
            productWithFile.ProductID = product.ProductID;
            productWithFile.ProductImageStr = "/" + product.ProductImage;
            productWithFile.ProductCode = product.ProductCode;
            productWithFile.ProductPrice = product.ProductPrice;

            return View(productWithFile);
        }
        [HttpGet]
        public string Remove(int id, int pg = 1)
        {
            var d = HttpContext.Request.Host.Value;
            var d2 = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(Request);
            string result = "";
            Product product = pm.GetProductById(id);
            if (product != null)
            {
                pm.DeleteProduct(product);
                string endPoint = "/products?pg=" + pg;
                result = GetApiHtmlData(HttpContext.GetRequestApiStr(endPoint));
                result = result.HtmlDe();
            }

            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductWithFile product, int id, int pg = 1)
        {
            Product product1 = pm.GetProductById(id);
            string result = EntityValidation.IsValidModel(product);
            if (result != "")
            {
                ViewBag.Result = result;
                product.ProductImageStr = "/" + product1.ProductImage;
                return View(product);
            }

            if (product.ProductImage != null)
            {
                string folderPath = "images/product/resimyok.jpg";
                folderPath = "images/product/" + Guid.NewGuid().ToString() + ".jpg";
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                await product.ProductImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                product1.ProductImage = folderPath;
            }





            product1.ProductName = product.ProductName;
            product1.ProductCode = product.ProductCode;
            product1.ProductStatus = product.ProductStatus;
            product1.ProductPrice = product.ProductPrice;
            product1.ProductUpdateDate = DateTime.Now;
            pm.UpdateProduct(product1);


            return RedirectToAction("Index", "Products", new { pg = pg });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductWithFile());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductWithFile product)
        {
            string result = EntityValidation.IsValidModel(product);
            if (result != "")
            {
                ViewBag.Result = result;
                return View(product);
            }
            Product product1 = new Product();
            string folderPath = "images/product/resimyok.jpg";
            if (product.ProductImage != null)
            {
                folderPath = "images/product/" + Guid.NewGuid().ToString() + ".jpg";
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                await product.ProductImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            product1.ProductImage = folderPath;
            product1.ProductName = product.ProductName;
            product1.ProductCode = product.ProductCode;
            product1.ProductStatus = product.ProductStatus;
            product1.ProductPrice = product.ProductPrice;
            product1.ProductCreateDate = DateTime.Now;

            pm.CreateProduct(product1);


            return RedirectToAction("Index", "Products");
        }
        public string GetApiHtmlData(string strURL)
        {
            var client = new RestClient(strURL);
            var request = new RestRequest("",Method.Get);
            RestResponse response =  client.Execute(request);
            string result = "";
            if (response != null) { result = response.Content; }
            return result;
        }
    }
}
