using OnlineStore.Models.WebModels.Quest.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest.Interfaces
{
    public interface IQuestHomeService
    {
        IndexViewModel PrepareIndexModel();

        Task<IEnumerable<ProductConciseViewModel>> GetProductsBySubcategoryAsync(string subcategoryId, ClaimsPrincipal user);

        IEnumerable<ProductConciseViewModel> GetProductsByKeywords(string words, ClaimsPrincipal user);
    }
}
