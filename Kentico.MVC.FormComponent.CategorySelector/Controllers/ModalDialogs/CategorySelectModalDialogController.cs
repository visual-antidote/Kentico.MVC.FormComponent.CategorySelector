using CMS.EventLog;
using CMS.SiteProvider;
using CMS.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.ModalDialogs;

namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Controllers.ModalDialogs
{
    [Authorize]
    public class CategorySelectModalDialogController : Controller
    {
        public ActionResult Index(List<string> IncludeSites, bool IncludeGlobalCategories = true, bool IncludeDisabledCategories = false)
        {
            CategorySelectModalDialogViewModel model = new CategorySelectModalDialogViewModel(new List<CategorySelectItemViewModel>());

            try
            {
                var categoriesInHeirarchy = _LoadCategories(IncludeSites, IncludeGlobalCategories, IncludeDisabledCategories);

                model = new CategorySelectModalDialogViewModel(categoriesInHeirarchy);
            }
            catch (Exception ex)
            {
                model.IsError = true;
                model.ErrorMessage = "Error loading categories. Please check the Event Log for more details.";
                EventLogProvider.LogException("CategorySelectModalDialogController", "Error", ex);
            }


            // Return PartialView instead of View or else you get this error: The model item passed into the dictionary is of type 'Models.ModalDialogs.ColorModalDialogViewModel', but this dictionary requires a model item of type 'MedioClinic.Models.PageViewModel'.
            // That is because the normal view uses the _Layout which assumes uses of the PageViewModel object
            // We don't need the full view here, just the stand-alone modal dialog
            return PartialView("ModalDialogs/CategorySelectModalDialog/_CategorySelectModalDialog", model);
        }


        private List<CategorySelectItemViewModel> _BuildTreeAndGetRoots(List<CategorySelectItemViewModel> actualObjects)
        {
            Dictionary<int, CategorySelectItemViewModel> lookup = new Dictionary<int, CategorySelectItemViewModel>();
            actualObjects.ForEach(x => lookup.Add(x.ID, new CategorySelectItemViewModel { CategoryLevel = x.CategoryLevel, ID = x.ID, ParentID = x.ParentID, CodeName = x.CodeName, Description = x.Description, DisplayName = x.DisplayName, GUID = x.GUID }));
            foreach (var item in lookup.Values)
            {
                CategorySelectItemViewModel proposedParent;
                if (item.ParentID.HasValue && lookup.TryGetValue(item.ParentID.Value, out proposedParent))
                {
                    item.Parent = proposedParent;
                    proposedParent.Categories.Add(item);
                }
            }
            return lookup.Values.Where(x => x.Parent == null).ToList();
        }

        private List<CategorySelectItemViewModel> _LoadCategories(List<string> IncludeSites, bool IncludeGlobalCategories, bool IncludeDisabledCategories)
        {
            List<CategorySelectItemViewModel> categoriesInHeirarchy = new List<CategorySelectItemViewModel>();

            try
            {
                List<int> siteFilterIDs = new List<int>();
                if (IncludeSites != null && IncludeSites.Count() > 0)
                {
                    foreach (var siteCodeName in IncludeSites)
                    {
                        var site = SiteInfoProvider.GetSiteInfo(siteCodeName);
                        if (site != null)
                        {
                            siteFilterIDs.Add(site.SiteID);
                        }
                    }
                }

                var categoriesQuery = CategoryInfoProvider
                    .GetCategories();

                if (!IncludeDisabledCategories) { 
                    categoriesQuery = categoriesQuery.WhereEquals("CategoryEnabled", true);
                }

                categoriesQuery = categoriesQuery.WhereEquals("CategorySiteID", SiteContext.CurrentSiteID);
                foreach (var siteC in siteFilterIDs)
                {
                    categoriesQuery = categoriesQuery.Or().WhereEquals("CategorySiteID", siteC);
                }

                if (IncludeGlobalCategories)
                {
                    categoriesQuery = categoriesQuery.Or().WhereEquals("CategorySiteID", null); // Also get global categories
                }

                categoriesQuery = categoriesQuery.OrderBy("CategorySiteId", "CategoryLevel", "CategoryOrder");

                var categories = categoriesQuery.ToList()
                    .Select(x => new CategorySelectItemViewModel()
                    {
                        ID = x.CategoryID,
                        CodeName = x.CategoryName,
                        DisplayName = x.CategoryDisplayName,
                        Description = x.CategoryDescription,
                        GUID = x.CategoryGUID,
                        ParentID = x.CategoryParentID,
                        CategoryLevel = x.CategoryLevel
                    }
                );

                categoriesInHeirarchy = _BuildTreeAndGetRoots(categories.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoriesInHeirarchy;
        }
    }
}
