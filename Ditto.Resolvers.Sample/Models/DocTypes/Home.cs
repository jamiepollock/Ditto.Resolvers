﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ditto.Resolvers.Sample.Models.Archetypes;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class Home : Page
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [ArchetypeResolver]
        public virtual List<PriceList> PriceList { get; set; }
    }
}