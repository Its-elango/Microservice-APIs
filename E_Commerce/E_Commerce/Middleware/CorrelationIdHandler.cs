namespace E_Commerce.Middleware
{
public class CorrelationIdHandler : DelegatingHandler

    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdHandler(IHttpContextAccessor httpContextAccessor)

        {

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)

        {

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))

            {

                request.Headers.Add("X-Correlation-ID", correlationId.ToString());

            }

            return await base.SendAsync(request, cancellationToken);

        }

    }

}
