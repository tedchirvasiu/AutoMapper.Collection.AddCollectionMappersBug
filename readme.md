### Description
I encountered the following weird behaviour which causes the app to **hang** when trying to resolve an IMapper dependency. No error is thrown, it just seems to run into an **infinite loop** somewhere internally.
So, according to https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/132 AutoMapper follows the Options Pattern and allows AddAutoMapper to be called multiple times.
Consider we have two projects, **ProjectA** and **ProjectB**. **ProjectA** references **ProjectB**. Both use AutoMapper.Collection and call the following code in their DI registration:

```C#
...services.AddAutoMapper(
    (cfg) => {
        cfg.AddCollectionMappers();
    },
    Assembly.GetExecutingAssembly()
);
```

Assume there exists a service called `SomeService` in **ProjectB** which requires an IMapper
```C#
public class SomeService {

    IMapper Mapper { get; set; }

    public SomeService(IMapper mapper) {
        Mapper = mapper;
    }
}
```


This setup works fine and the application runs normally. Now consider we add three objects of the form:
```C#
public class SomeEntity {
    public virtual ICollection<SomeElement> Elements { get; set; }
}

public class SomeElement {
    public string Id { get; set; }
    public string Name { get; set; }
}
```
```C#
public class SomeDto {
    public List<string> Ids { get; set; }
}
```
With a mapping profile containing the following configuration:
```C#
public class SomeMappingProfile : Profile {

    public SomeMappingProfile() {

        CreateMap<SomeEntity, SomeDto>()
            //This line seems to cause trouble
            .ForMember(dto => dto.Ids, m => m.MapFrom(entity => entity.Elements.Select(e => e.Id)));

    }
}
````

Now the application will **hang forever** when trying to resolve SomeService, specifically when trying to resolve IMapper. It's somewhere internally but not sure where.
Here's the weird part: commenting either the `AddCollectionMappers()` in **ProjectA** or **ProjectB** will resolve the issue. It hangs only when AddCollectionMappers() is called twice or more (even in succession in the same project). But the culprit seems to be the .ForMember expression in the mapping for the Elements collection. Any other ForMember expressions would not cause this weird behaviour.

### Example repo
https://github.com/tedchirvasiu/AutoMapper.Collection.AddCollectionMappersBug

### Versions
AutoMapper: 12.0.0
AutoMapper.Collection: 9.0.0

### Issue
[https://github.com/AutoMapper/AutoMapper.Collection/issues/173](https://github.com/AutoMapper/AutoMapper.Collection/issues/173)