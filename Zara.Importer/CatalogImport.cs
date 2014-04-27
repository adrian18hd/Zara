using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Dto;
using Mediachase.Commerce.Core;

namespace Zara.Importer
{
	public class CatalogImport
	{
		private readonly ICatalogSystem _catalogSystem;
		private readonly ILanguageBranchRepository _languageBranchRepository;

		public CatalogImport()
		{
			_catalogSystem = ServiceLocator.Current.GetInstance<ICatalogSystem>();
			_languageBranchRepository = ServiceLocator.Current.GetInstance<ILanguageBranchRepository>();
		}

		public void CreateZaraCatalog()
		{
			const string catalogName = "Zara";

			//get catalog dto
			CatalogDto catalogDto = _catalogSystem.GetCatalogDto();
			//check if we already created "Zara" catalog
			if (catalogDto.Catalog.Any(c => c.Name.Equals(catalogName, StringComparison.OrdinalIgnoreCase)))
				return;

			//get all enabled languages
			IList<LanguageBranch> availableLanguages = _languageBranchRepository.ListEnabled();

			//create new catalog row
			CatalogDto.CatalogRow catalogRow = catalogDto.Catalog.NewCatalogRow();
			catalogRow.Name = catalogName;
			catalogRow.IsPrimary = true;
			catalogRow.SortOrder = 1;
			catalogRow.StartDate = catalogRow.Created = catalogRow.Modified = DateTime.UtcNow;
			catalogRow.EndDate = DateTime.UtcNow.AddYears(20);
			catalogRow.IsActive = true;
			catalogRow.ApplicationId = AppContext.Current.ApplicationId;
			catalogRow.DefaultCurrency = SiteContext.Current.Currency.CurrencyCode;
			//set the first enabled language as default
			catalogRow.DefaultLanguage = availableLanguages.First().LanguageID;
			catalogRow.WeightBase = "lbs";
			//add new row to the catalog table
			catalogDto.Catalog.AddCatalogRow(catalogRow);
			//save catalog dto
			_catalogSystem.SaveCatalog(catalogDto);


			for (int i = 1; i < availableLanguages.Count; i++)
			{
				LanguageBranch languageBranch = availableLanguages[i];
				//create a language row for each language except first one and assign it to our catalog
				CatalogDto.CatalogLanguageRow languageRow = catalogDto.CatalogLanguage.NewCatalogLanguageRow();
				languageRow.CatalogId = catalogRow.CatalogId;
				languageRow.LanguageCode = languageBranch.LanguageID;
				catalogDto.CatalogLanguage.AddCatalogLanguageRow(languageRow);
			}
			//save the associated language and default language
			_catalogSystem.SaveCatalog(catalogDto);
		}
	}
}