using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class ProductCategory
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public ProductCategoryService _productCategoryService { get; set; }
        [Inject]
        public IToastService _toastService { get; set; }

        public ProductCategoryModelRootobject listProductCategoryResult { get; set; }
        public SingleProductCategoryModelRootobject productCategoryResult { get; set; }

        public List<ProductCategoryModel> listProductCategory { get; set; }
        public ProductCategoryModel productCategory { get; set; }

        private ProductCategoryModel newProductCategory = new ProductCategoryModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListProductCategory();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListProductCategory()
        {
            listProductCategoryResult = await _productCategoryService.getListProductCategory();
            listProductCategory = listProductCategoryResult.data;
        }

        public async Task getProductCategory(int id)
        {
            productCategoryResult = await _productCategoryService.getProductCategory(id);
            productCategory = productCategoryResult.data;
        }

        public async Task addProductCategory()
        {
            HttpResponseMessage response = await _productCategoryService.addProductCategory(newProductCategory);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                await getListProductCategory();
                newProductCategory = new ProductCategoryModel();
                _toastService.ShowSuccess("New product added");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }
    }
}