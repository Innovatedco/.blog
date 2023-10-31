using Blazor.Blog.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;

namespace Blazor.Blog.Tests.HelperClasses
{
    using URI = Uri;
    public sealed class MockNavigationManager : NavigationManager
    {
        private readonly Stack<NavigationHistory> history = new();
        public IReadOnlyCollection<NavigationHistory> History => history;
        public MockNavigationManager()
        {
            Initialize("https://localhost/", "https://localhost/");
        }

        public MockNavigationManager(string uri)
        {
            Initialize(NavUrlHelpers.BaseUrl + "/", uri);
        }

        protected override void NavigateToCore(string uri, NavigationOptions options)
        {
            var absoluteUri = GetNewAbsoluteUri(uri);
            var changedBaseUri = HasDifferentBaseUri(absoluteUri);

            if (changedBaseUri)
            {
                BaseUri = GetBaseUri(absoluteUri);
            }

            Uri = ToAbsoluteUri(uri).OriginalString;

            if (options.ReplaceHistoryEntry && history.Count > 0)
                history.Pop();

            history.Push(new NavigationHistory(uri, options, NavigationState.Succeeded));
            if (!changedBaseUri)
            {
                NotifyLocationChanged(isInterceptedLink: false);
            }
            else
            {
                BaseUri = GetBaseUri(absoluteUri);
            }
        }

        protected override void SetNavigationLockState(bool value) { }

        protected override void HandleLocationChangingHandlerException(Exception ex, LocationChangingContext context)
            => throw ex;

        private URI GetNewAbsoluteUri(string uri) => new URI(uri, UriKind.RelativeOrAbsolute).IsAbsoluteUri
        ? new URI(uri, UriKind.RelativeOrAbsolute) : ToAbsoluteUri(uri);

        private bool HasDifferentBaseUri(URI absoluteUri)
            => URI.Compare(
                new URI(BaseUri, UriKind.Absolute),
                absoluteUri,
                UriComponents.SchemeAndServer,
                UriFormat.Unescaped,
                StringComparison.OrdinalIgnoreCase) != 0;

        private static string GetBaseUri(URI uri)
        {
            return uri.Scheme + "://" + uri.Authority + "/";
        }
    }
}
