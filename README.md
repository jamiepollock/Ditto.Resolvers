# Ditto.Resolvers

[![Build status](https://ci.appveyor.com/api/projects/status/w545oncssfcafldq?svg=true)](https://ci.appveyor.com/project/MichaelLaw/ditto-resolvers)
[![NuGet release](https://img.shields.io/nuget/vpre/Ditto.Resolvers.Archetype.svg)](https://www.nuget.org/packages/Ditto.Resolvers.Archetype)

Ditto is awesome, we all know that, though it would be nice if we could hook third parties like Archetype and The Grid to the Ditto flow. This wee chunk of code is aiming to achieve just that.

Currently this only has support for Archetypes, but hopefully over the next couple of weeks we can add to that.

## Archetypes

Setup is easy. Below is a Ditto class which maps to my Home document type, this document type has an Archetype property called PriceList (alias: priceList).

```csharp
	public class Home : Page
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [ArchetypeResolver]
        public List<PriceList> PriceList { get; set; }
    }
```
PriceList is just a POCO which inherits from the Archetype class **ArchetypeFieldsetModel**.

```csharp
	public class PriceList : ArchetypeFieldsetModel
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }
    }
```

Now when we use Ditto to map the Home document type via the .As<Home>(); extension method, our Archetype will map to our POCO. Nice and easy, eh? This will provide almost all the functionality of Ditto e.g. TypeConverters, LazyLoading to use on your Archetype POCOs.

### Options

#### Alias

If your Archetype property on your Home document type doesn't match the Property name, you can set this in the **ArchetypeResolverAttribute**.

```csharp
	[ArchetypeResolver("HomeDocTypePriceListAlias")]
	public List<PriceList> PriceList { get; set; }
```

#### Single items

Your Archetype doesn't have to be a list! Simply use a single Type of your Archetype POCO and it will attempt to map to that.

```csharp
	[ArchetypeResolver]
	public PriceList PriceList { get; set; }
```

#### Multiple fieldsets

If you have enabled multiple fieldsets in your Archetype data type this would create a generic list including all of the different types of your POCOs which match an Archetype fieldset. The only caveat being your property on your Home document type would require to be a generic list with the base class **ArchetypeFieldsetModel** as its first generic property. This allows various different types inheriting from the base class to be assigned to the list.

```csharp
	[ArchetypeResolver]
	public List<ArchetypeFieldsetModel> PriceList { get; set; }
```

#### Nested Archetypes

Rejoice. Nested Archetypes should also work so long as you have decorated the child Archetype property on your POCO with the **ArchetypeResolverAttribute**.

```csharp
	public class PriceList : ArchetypeFieldsetModel
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }
        
        [ArchetypeResolver]
		public List<UrlPicker> Links { get; set; }
    }
```

### Archetypes Constraints

There are two contrainsts around POCO Archetypes currently. 

* Each POCO must inherit from the **ArchetypeFieldsetModel** in the Archetype assembly.
* The Name of your class must match the Archetypes alias on the fieldset alias.  

We can work around the second issue here in the coming weeks, but the first one is a deal breaker. Sorry if this offends you!
