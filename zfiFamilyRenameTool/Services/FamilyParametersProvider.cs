namespace zfiFamilyRenameTool.Services
{
    using System;
    using System.Collections.Generic;
    using Abstractions;
    using Autodesk.Revit.DB;
    using ModPlusAPI;

    public class FamilyParametersProvider : IRenameableProvider
    {
        // Имена параметров
        public string Name => Language.GetItem(ModPlusConnector.Instance.Name, "p1");

        /// <inheritdoc />
        public TabItemType TabItemType => TabItemType.SourceAndDestination;

        public IEnumerable<IRenameable> GetRenameables(Document doc)
        {
            if (!doc.IsFamilyDocument)
            {
                // Документ \"{doc.Title}\" не является семейством
                throw new ArgumentException(string.Format(Language.GetItem(ModPlusConnector.Instance.Name, "err1"), doc.Title));
            }

            var fm = doc.FamilyManager;

            foreach (FamilyParameter p in fm.Parameters)
            {
                if (p.IsShared || p.IsReadOnly || p.Id.IntegerValue < 0)
                {
                    continue;
                }

                yield return new FamilyParameterWrapper(p, doc);
            }
        }
    }
}