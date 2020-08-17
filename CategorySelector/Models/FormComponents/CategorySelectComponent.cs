using CMS.EventLog;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.Taxonomy;
using Kentico.Forms.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents;
using VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Repository;

[assembly: RegisterFormComponent(CategorySelectComponent.IDENTIFIER, typeof(CategorySelectComponent), "{$VisualAntidote.FormComponent.CategorySelect.Name$}",
    IsAvailableInFormBuilderEditor = false, ViewName = "FormComponents/_CategorySelectComponent")]
namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents
{
    /// <summary>
    /// Represents a Category Selector. All categories from the current site will be listed. Global categories can be included/excluded via the CategorySelectProperties.IncludeGlobalCategories property.  To include categories from other sites, use the CategorySelectProperties.IncludeSites property.
    /// </summary>
    public class CategorySelectComponent : FormComponent<CategorySelectProperties, List<string>>
    {
        public const string IDENTIFIER = "VisualAntidote.Category.Select.Form.Component";

        [BindableProperty]
        public String CategoryCodeNameListAsText { get; set; }

        public String CategoriesSelectedCount
        {
            get
            {
                var count = "0";

                if (!String.IsNullOrEmpty(CategoryCodeNameListAsText))
                {
                    count = CategoryCodeNameListAsText.Split(',').ToList().Count.ToString();
                }

                return $"{count}";
            }
        }

        public String IncludeSites
        {
            get
            {
                if (this.Properties != null && !String.IsNullOrEmpty(this.Properties.IncludeSites))
                {
                    return this.Properties.IncludeSites;
                }

                return "";
            }
        }

        public bool IncludeGlobalCategories
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.IncludeGlobalCategories;
                }

                return true;
            }
        }

        public bool IncludeDisabledCategories
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.IncludeDisabledCategories;
                }

                return false;
            }
        }

        public int? MinimumSelectedCategoryNumber
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.MinimumSelectedCategoryNumber;
                }

                return null;
            }
        }

        public int? MaximumSelectedCategoryNumber
        {
            get
            {
                if (this.Properties != null)
                {
                    return this.Properties.MaximumSelectedCategoryNumber;
                }

                return null;
            }
        }

        public override List<string> GetValue()
        {
            return _ConvertCategoryFieldToList();
        }

        public override void SetValue(List<string> value)
        {
            if (value != null)
            {
                CategoryCodeNameListAsText = String.Join(",", value);
            }
        }

        private List<string> _ConvertCategoryFieldToList()
        {
            var categoryCodeNameList = new List<string>();
            if (!String.IsNullOrEmpty(CategoryCodeNameListAsText))
            {
                categoryCodeNameList = CategoryCodeNameListAsText.Split(',').ToList();
            }

            return categoryCodeNameList;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> baseValidationResults = base.Validate(validationContext).ToList();

            try
            {

                IEnumerable<string> checkedCatgoryList = _ConvertCategoryFieldToList();

                var categoriesQuery = CategoryRepository.GenerateCategoryQuery(this.IncludeSites, this.IncludeGlobalCategories, this.IncludeDisabledCategories);

                categoriesQuery = categoriesQuery
                    .WhereIn("CategoryName", checkedCatgoryList.ToList()); // Run the same query that loaded the liust for the UI, but now also compare to what the user entered.

                var queriedCategoryList = categoriesQuery.ToList().Select(x => x.CategoryName);

                // need to make sure that each checkedCatgoryList exists in the queriedCategoryList
                var allOfListCheckedIsInListQueried = checkedCatgoryList.Intersect(queriedCategoryList).Count() == checkedCatgoryList.Count();

                var prefUltureCode = MembershipContext.AuthenticatedUser.PreferredUICultureCode;
                if (allOfListCheckedIsInListQueried == false)
                {
                    var badQueryMsg = ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.InvalidCats", prefUltureCode);
                    if (this.IncludeGlobalCategories == false)
                    {
                        badQueryMsg += ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.NoGlobals", prefUltureCode) + " ";
                    }
                    if (this.IncludeDisabledCategories == false)
                    {
                        badQueryMsg += ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.NoDisabled", prefUltureCode) + " ";
                    }
                    if (!String.IsNullOrEmpty(this.IncludeSites))
                    {
                        badQueryMsg += $"{ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.FromSites", prefUltureCode)} {this.IncludeSites}. ";
                    }
                    else
                    {
                        badQueryMsg += $"{ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.OnlySite", prefUltureCode)} {SiteContext.CurrentSiteName}";
                    }

                    baseValidationResults.Add(new ValidationResult(badQueryMsg));
                }

                var selectedCount = checkedCatgoryList.Count();


                if (MinimumSelectedCategoryNumber.HasValue && MaximumSelectedCategoryNumber.HasValue && MaximumSelectedCategoryNumber.Value == MinimumSelectedCategoryNumber.Value && selectedCount != MinimumSelectedCategoryNumber.Value)
                {
                    baseValidationResults.Add(new ValidationResult($"{ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.Exactly", prefUltureCode)} {MinimumSelectedCategoryNumber.Value} {ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.CatsRequired", prefUltureCode)}. "));
                }
                else
                {
                    if (MinimumSelectedCategoryNumber.HasValue && (selectedCount < MinimumSelectedCategoryNumber.Value))
                    {
                        baseValidationResults.Add(new ValidationResult($"{ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.AtLeast", prefUltureCode)} {MinimumSelectedCategoryNumber.Value} {ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.CatsRequired", prefUltureCode)}. "));
                    }

                    if (MaximumSelectedCategoryNumber.HasValue && (selectedCount > MaximumSelectedCategoryNumber.Value))
                    {
                        baseValidationResults.Add(new ValidationResult($"{ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.AtMost", prefUltureCode)} {MaximumSelectedCategoryNumber.Value} {ResHelper.GetString("VisualAntidote.FormComponent.CategorySelect.CatsAllowed", prefUltureCode)}. "));
                    }
                }
            }
            catch(Exception ex)
            {
                EventLogProvider.LogException("VisualAntidote.CategorySelectComponent", "Error", ex);
            }

            return baseValidationResults;
        }
    }
}
