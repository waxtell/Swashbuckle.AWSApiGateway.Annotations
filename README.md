# Swashbuckle.AWSApiGateway.Annotations
Extend your OAS generation to include AWS Api Gateway annotations

Getting started

Setting document centric configuration:

```csharp
            services.AddSwaggerGen(c =>
            {
                c.AddXAmazonApiGatewayAnnotations
                (
                    options =>
                    {
                        options
                            .WithKeySource
                            (
                                kso =>
                                {
                                    kso.ApiKeySource = ApiKeySource.Header;
                                }
                            );
                    }
                );
```

Setting default values for actions:

```csharp
                c.AddXAmazonApiGatewayOperationAnnotations
                (
                    op =>
                    {
                        op.WithIntegration
                        (
                            intOpt =>
                            {
                                intOpt.Type = IntegrationType.http_proxy;
                                intOpt.BaseUri = "https://your.domain.com";
                            }
                        );
                    }
                );
```

Setting default values for controllers:

```csharp
    [ApiController]
    [Route("[controller]")]
    [XAmazonApiGatewayIntegration(PassthroughBehavior = PassthroughBehavior.WHEN_NO_MATCH)]
    public class WeatherForecastController : ControllerBase
    {
```

Setting values for actions:

```csharp
        [HttpGet]
        [XAmazonApiGatewayIntegration(ConnectionType = ConnectionType.INTERNET)]
        public IEnumerable<WeatherForecast> Get()
        {
```