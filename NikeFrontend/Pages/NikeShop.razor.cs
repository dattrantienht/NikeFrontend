using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Linq;


namespace NikeFrontend.Pages
{
    public partial class NikeShop
    {
        [Inject]
        public ProductService _productService { get; set; }
        [Inject]
        public ProductCategoryService _productCategoryService { get; set; }

        public ListProductModelRoot listProductResult { get; set; }
        public SingleProductModelRoot productResult { get; set; }
        public ProductCategoryModelRootobject listProductCategoryResult { get; set; }
        public List<ProductCategoryModel> listProductCategory { get; set; }
        public List<ProductModel> listProduct { get; set; }
        public ProductModel product { get; set; }
        public string keyword { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListProductCategory();
                await getListProduct();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListProductCategory()
        {
            listProductCategoryResult = await _productCategoryService.getListProductCategory();
            listProductCategory = listProductCategoryResult.data;
        }

        public async Task getListProduct()
        {
            listProductResult = await _productService.getListProduct(keyword);
            listProduct = listProductResult.data;
        }

        public async Task getProduct(int id)
        {
            productResult = await _productService.getProduct(id);
            product = productResult.data;
        }

         //hide/show filter
        protected String HideFilterButton { set; get; } = "Hide Filter";
        protected String HideFilterButtonZoomProductCss { set; get; } = "col-lg-10";
        protected string HideFilterCssClass { get; set; } = null;

        protected void HideFilter()
        {
            if (HideFilterButton == "Hide Filter")
            {
                HideFilterButton = "Show Filter";
                HideFilterCssClass = "HideFilter";
                HideFilterButtonZoomProductCss = "col-lg-12";
            }
            else
            {
                HideFilterButtonZoomProductCss = "col-lg-10";
                HideFilterCssClass = null;
                HideFilterButton = "Hide Filter";
            }
        }
        protected int SortId { set; get; }=0 ;
        protected string NameCatergory { set; get; } = "";
        public async Task getProductFilter(string productCategoryName,int sortId)
        {
            //sortId = SortId;
            NameCatergory = productCategoryName;
            listProductResult = await _productService.getListProduct("");
            listProduct = listProductResult.data;
            if(sortId==1)
            { listProduct = (listProduct.Where(x => x.productCategoryName.Contains(productCategoryName))).OrderByDescending(x => x.price).ToList(); }
            else if(sortId==2)
            { listProduct = (listProduct.Where(x => x.productCategoryName.Contains(productCategoryName))).OrderBy(x => x.price).ToList(); }
           
            else
                listProduct = (listProduct.Where(x => x.productCategoryName.Contains(productCategoryName))).ToList();

        }
    }
}
