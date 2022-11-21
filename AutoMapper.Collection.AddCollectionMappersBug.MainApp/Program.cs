using AutoMapper.EquivalencyExpression;
using SomeExampleLibrary;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(
    (cfg) => {
        //If you comment this line it works
        cfg.AddCollectionMappers();
    },
    Assembly.GetExecutingAssembly()
);

builder.Services.AddSomeExampleLibrary();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    //This will never get resolved and cycle forever
    //unless you either comment the cfg.AddCollectionMappers() in this assembly or the one in SomeExampleLibrary
    var someService = scope.ServiceProvider.GetRequiredService<SomeService>();
}

app.MapGet("/", () => "Hello World!");

app.Run();
