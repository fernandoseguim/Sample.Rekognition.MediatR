using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Rekognition.MediatR.Rules;
using Sample.Rekognition.MediatR.Rules.Requests;

namespace Sample.Rekognition.MediatR.Pipeline
{
    public class SelfieHandler : IPipelineBehavior<Message, Result>
    {
        private readonly IDetectLabelsService _service;

        public SelfieHandler(IDetectLabelsService service) { _service = service; }
        
        public async Task<Result> Handle(Message request, CancellationToken cancellationToken, RequestHandlerDelegate<Result> next)
        {
            var image = $"{request.Document}/{request.Images.FirstOrDefault(img => img.Equals("selfie.jpeg"))}";

            var result = await _service.AnalisyAsync(image);

            var self = new Selfie(result);

            var isValid = self.IsValidSelfie(request.Confidence);

            if (isValid) return await next();

            return new Result(false, "Sua selfie não passou no teste!");
        }
    }
}
