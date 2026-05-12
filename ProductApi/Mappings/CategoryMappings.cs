using ProductApi.Models;
using ProductApi.Models.Dtos;

namespace ProductApi.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryResponse ToResponse(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public static Category ToEntity(this CreateCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name               
            };
        }
        public static void UpdateEntity(this UpdateCategoryRequest request, Category category)
        {
            category.Name = request.Name;
        }
    }
}
