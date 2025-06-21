﻿using Kr.__PROJECT_NAME__.Api.Infra.Helpers;
using Kr.__PROJECT_NAME__.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;


namespace Kr.__PROJECT_NAME__.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    public static void HealthCheckEndpoints(this WebApplication app){
        app.MapGroup("/api/health/v1");
        
        app.MapGet("/verify",(CancellationToken token = default ) =>{
            return Results.Ok("alive version");              
        }).WithOpenApi(operation => 
        operation.GenerateOpenApiDoc(
                "v1 health check.",
                "verify the health of the application.",
                "Healthcheck",
                "Healthcheck for Document Api.", false
        ))
        .Produces(StatusCodes.Status200OK);
    }

}
