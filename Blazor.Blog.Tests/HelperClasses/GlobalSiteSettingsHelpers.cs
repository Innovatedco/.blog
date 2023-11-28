using AutoFixture;
using AutoFixture.Kernel;
using Blazor.Blog.Data;
using Blazor.Blog.Globals;
using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class GlobalSiteSettingsHelpers
    {
        /// <summary>
        /// Builds a base global site settings fixture
        /// </summary>
        /// <returns>Fixture</returns>
        public static Fixture BuildBaseGlobalSiteSettingsFixture()
        {
            var globalSiteSettingsFixtureBase = new Fixture();
            return globalSiteSettingsFixtureBase;
        }

        /// <summary>
        /// Builds global site settings from the base fixture
        /// </summary>
        /// <returns>GlobalSiteSettings</returns>
        public static GlobalSiteSettings BuildGlobalSiteSettingsFixture()
        {
            var baseFixture = BuildBaseGlobalSiteSettingsFixture();
            var globalSiteSettings = baseFixture.Build<GlobalSiteSettings>()
                .Create();
            return globalSiteSettings;
        }
    }
}
