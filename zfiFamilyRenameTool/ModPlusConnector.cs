namespace zfiFamilyRenameTool
{
    using System;
    using System.Collections.Generic;
    using ModPlusAPI.Interfaces;

    public class ModPlusConnector : IModPlusFunctionInterface
    {
        private static ModPlusConnector _instance;

        public static ModPlusConnector Instance => _instance ?? (_instance = new ModPlusConnector());

        public SupportedProduct SupportedProduct => SupportedProduct.Revit;

        public string Name => "zfiFamilyRenameTool";

#if R2015
        public string AvailProductExternalVersion => "2015";
#elif R2016
        public string AvailProductExternalVersion => "2016";
#elif R2017
        public string AvailProductExternalVersion => "2017";
#elif R2018
        public string AvailProductExternalVersion => "2018";
#elif R2019
        public string AvailProductExternalVersion => "2019";
#elif R2020
        public string AvailProductExternalVersion => "2020";
#elif R2021
        public string AvailProductExternalVersion => "2021";
#endif

        public string FullClassName => "zfiFamilyRenameTool.Revit.RenamerCommand";

        public string AppFullClassName => string.Empty;

        public Guid AddInId => Guid.Empty;

        public string LName => "Пакетное переименование в семействах";

        public string Description => "Пакетное переименование имен параметров, имен типов, значений параметров в указанных семействах";

        public string Author => "Жеков Фёдор";

        public string Price => "0";

        public bool CanAddToRibbon => true;

        public string FullDescription => "Плагин обрабатывает выбранные файлы или все файлы в выбранных папках. Можно добавлять префикс или суффикс, можно переименовывать с поиском и заменой. Присутствует несколько опций настройки поиска и замены. Присутствует возможность использовать регулярные выражения для поиска значений";

        public string ToolTipHelpImage => string.Empty;

        public List<string> SubFunctionsNames => new List<string>();

        public List<string> SubFunctionsLames => new List<string>();

        public List<string> SubDescriptions => new List<string>();

        public List<string> SubFullDescriptions => new List<string>();

        public List<string> SubHelpImages => new List<string>();

        public List<string> SubClassNames => new List<string>();
    }
}
