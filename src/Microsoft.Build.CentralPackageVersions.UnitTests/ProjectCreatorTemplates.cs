using Microsoft.Build.Evaluation;
using Microsoft.Build.Utilities.ProjectCreation;
using System;
using System.Collections.Generic;

namespace Microsoft.Build.CentralPackageVersions.UnitTests
{
    public static class CustomProjectCreatorTemplates
    {
        public static ProjectCreator PackagesProps(
            this ProjectCreatorTemplates templates,
            Action<ProjectCreator> customAction = null,
            string path = null,
            string defaultTargets = null,
            string initialTargets = null,
            string sdk = null,
            string toolsVersion = null,
            string treatAsLocalProperty = null,
            ProjectCollection projectCollection = null,
            NewProjectFileOptions? projectFileOptions = NewProjectFileOptions.IncludeXmlDeclaration | NewProjectFileOptions.IncludeXmlNamespace,
            IReadOnlyDictionary<string, string> packageVersions = null,
            IReadOnlyCollection<string> globalPackageReferences = null)
        {
            return ProjectCreator.Create(
                        path,
                        defaultTargets,
                        initialTargets,
                        sdk,
                        toolsVersion,
                        treatAsLocalProperty,
                        projectCollection,
                        projectFileOptions)
                    .ForEach(packageVersions, (i, creator) => creator.ItemPackageVersion(i.Key, i.Value))
                    .ItemGroup()
                    .ForEach(globalPackageReferences, (i, creator) => creator.ItemGlobalPackageReference(i))
                    .CustomAction(customAction);
        }
    }
}