using Domain.Models;

namespace Presentation.Components.Pages
{
    public partial class Home
    {
        private List<Category>? Categories;
        private bool IsAdmin;

        // Variabler för att skapa ny kategori
        private bool showCreateCategory = false;
        private string newCategoryTitle = "";
        private string newCategoryDescription = "";

        // Variabler för att redigera kategori
        private int? editCategoryId = null;
        private string editCategoryTitle = "";
        private string editCategoryDescription = "";

        // Variabler för att skapa ny subkategori
        private int? createSubCategoryForCategoryId = null;
        private string newSubCategoryTitle = "";
        private string newSubCategoryDescription = "";

        // Variabler för att redigera subkategori
        private int? editSubCategoryId = null;
        private string editSubCategoryTitle = "";
        private string editSubCategoryDescription = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            IsAdmin = authState.User.Identity?.Name == "mariorossi";
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            Categories = await CategoryService.GetAllAsync();
        }

        private async Task CreateCategory()
        {
            if (string.IsNullOrWhiteSpace(newCategoryTitle)) return;
            var category = new Category { Title = newCategoryTitle, Description = newCategoryDescription };
            await CategoryService.AddAsync(category);
            newCategoryTitle = "";
            newCategoryDescription = "";
            showCreateCategory = false;
            await LoadCategories();
        }

        private void StartEditCategory(Category category)
        {
            // spara vilken kategori som redigeras
            editCategoryId = category.Id;
            editCategoryTitle = category.Title;
            editCategoryDescription = category.Description;
        }

        private async Task SaveEditCategory()
        {
            var category = Categories?.FirstOrDefault(c => c.Id == editCategoryId);
            if (category == null) return;
            category.Title = editCategoryTitle;
            category.Description = editCategoryDescription;
            await CategoryService.UpdateAsync(category);
            CancelEditCategory();
            await LoadCategories();
        }

        private void CancelEditCategory()
        {
            editCategoryId = null;
            editCategoryTitle = "";
            editCategoryDescription = "";
        }

        private async Task DeleteCategory(int id)
        {
            await CategoryService.DeleteAsync(id);
            await LoadCategories();
        }

        private void ToggleCreateSubCategory(int categoryId)
        {
            // visa eller dölj formuläret beroende på om det redan är öppet
            createSubCategoryForCategoryId = createSubCategoryForCategoryId == categoryId ? null : categoryId;
            newSubCategoryTitle = "";
            newSubCategoryDescription = "";
        }

        private async Task CreateSubCategory(int categoryId)
        {
            if (string.IsNullOrWhiteSpace(newSubCategoryTitle))
                return;
            var sub = new SubCategory { Title = newSubCategoryTitle, Description = newSubCategoryDescription, CategoryId = categoryId };
            await SubCategoryService.AddAsync(sub);
            newSubCategoryTitle = "";
            newSubCategoryDescription = "";
            createSubCategoryForCategoryId = null;
            await LoadCategories();
        }

        private void StartEditSubCategory(SubCategory sub)
        {
            editSubCategoryId = sub.Id;
            editSubCategoryTitle = sub.Title;
            editSubCategoryDescription = sub.Description;
        }

        private async Task SaveEditSubCategory()
        {
            // leta upp rätt subkategori från listan
            var sub = Categories?
                .SelectMany(c => c.SubCategories ?? new())
                .FirstOrDefault(s => s.Id == editSubCategoryId);
            if (sub == null) return;
            sub.Title = editSubCategoryTitle;
            sub.Description = editSubCategoryDescription;
            await SubCategoryService.UpdateAsync(sub);
            CancelEditSubCategory();
            await LoadCategories();
        }

        private void CancelEditSubCategory()
        {
            editSubCategoryId = null;
            editSubCategoryTitle = "";
            editSubCategoryDescription = "";
        }

        private async Task DeleteSubCategory(int id)
        {
            await SubCategoryService.DeleteAsync(id);
            await LoadCategories();
        }
    }
}