using CMS.SiteProvider;
using CMS.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Repository
{
    public static class CategoryRepository
    {
        public static CMS.DataEngine.ObjectQuery<CategoryInfo> GenerateCategoryQuery(string IncludeSites, bool IncludeGlobalCategories, bool IncludeDisabledCategories)
        {
            List<string> sites = new List<string>();
            if(!String.IsNullOrEmpty(IncludeSites))
            {
                sites = IncludeSites.Split(',').ToList();
            }
            return GenerateCategoryQuery(sites, IncludeGlobalCategories, IncludeDisabledCategories);
        }

        public static CMS.DataEngine.ObjectQuery<CategoryInfo> GenerateCategoryQuery(List<string> IncludeSites, bool IncludeGlobalCategories, bool IncludeDisabledCategories)
        {
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

                CMS.DataEngine.ObjectQuery<CategoryInfo> categoriesQuery = CategoryInfoProvider
                        .GetCategories();

                if (!IncludeDisabledCategories)
                {
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

                return categoriesQuery;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
