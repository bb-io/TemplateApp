﻿using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using TemplateApp.Connections.OAuth;

namespace TemplateApp;

public class Application : BaseInvocable,  IApplication
{
    /// <summary>
    /// Application name
    /// </summary>
    public string Name
    {
        get => "Application name";
        set { }
    }
    
    private readonly Dictionary<Type, object> _typesInstances;

    public Application(InvocationContext invocationContext) : base(invocationContext)
    {
        // Creating OAuth service instances
       // _typesInstances = CreateTypesInstances();
    }

    /// <summary>
    /// Returns authorization instance
    /// </summary>
    public T GetInstance<T>()
    {
        // Logic for OAuth auth
        // if (!_typesInstances.TryGetValue(typeof(T), out var value))
        // {
        //     throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");
        // }
        //
        // return (T)value;

        throw new NotImplementedException();
    }

    private Dictionary<Type, object> CreateTypesInstances()
    {
        return new Dictionary<Type, object>
        {
            { typeof(IOAuth2AuthorizeService), new OAuth2AuthorizeService(InvocationContext) },
            { typeof(IOAuth2TokenService), new OAuth2TokenService(InvocationContext) }
        };
    }
}