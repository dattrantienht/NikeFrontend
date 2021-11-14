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
        private ProductCategoryModel editProductCategory = new ProductCategoryModel();
        public int idForDeleteCategory { get; set; }
        public string nameForDeleteCategory { get; set; }

        public void passDataForDeleteModalCategory(int id, string name)
        {
            idForDeleteCategory = id;
            nameForDeleteCategory = name;
        }

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
            editProductCategory = productCategoryResult.data;
        }

        public async Task addProductCategory()
        {
            HttpResponseMessage response = await _productCategoryService.addProductCategory(newProductCategory);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                await getListProductCategory();
                newProductCategory = new ProductCategoryModel();
                _toastService.ShowSuccess("New product category added");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }

        public async Task deleteProductCategory(int id)
        {
            HttpResponseMessage response = await _productCategoryService.deleteProductCategory(id);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                await getListProductCategory();
                _toastService.ShowSuccess("Product category deleted");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }

        public async Task updateProductCategory()
        {
            HttpResponseMessage response = await _productCategoryService.editProductCategory(editProductCategory);
            Console.WriteLine(response);
            editProductCategory = new ProductCategoryModel();
            if (response.IsSuccessStatusCode)
            {
                await getListProductCategory();
                _toastService.ShowSuccess("Product category updated");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }
    }
}