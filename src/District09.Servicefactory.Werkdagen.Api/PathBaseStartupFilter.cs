using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace District09.Servicefactory.Werkdagen.Api;

public class PathBaseStartupFilter : IStartupFilter
{
    private readonly string _pathBase;

    public PathBaseStartupFilter(string pathBase)
    {
        _pathBase = pathBase.StartsWith('/') ? pathBase : $"/{pathBase}";
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UsePathBase(_pathBase);
            next(app);
        };
    }
}