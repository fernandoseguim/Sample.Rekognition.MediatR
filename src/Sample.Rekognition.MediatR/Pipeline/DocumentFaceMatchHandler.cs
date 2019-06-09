using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Rekognition.MediatR.Rules;
using Sample.Rekognition.MediatR.Rules.Requests;

namespace Sample.Rekognition.MediatR.Pipeline
{
    public class DocumentFaceMatchHandler : IRequestHandler<Message, Result>
    {
        private readonly IFaceComparisonService _service;

        public DocumentFaceMatchHandler(IFaceComparisonService service) { _service = service; }

        public async Task<Result> Handle(Message request, CancellationToken cancellationToken)
        {
            var source = $"{request.Document}/{request.Images.FirstOrDefault(img => img.Equals("selfie.jpeg"))}";
            var target = $"{request.Document}/{request.Images.FirstOrDefault(img => img.Equals("document-front.jpeg"))}";

            var result = await _service.CompareAsync(request.Confidence, source, target);

            var faceMatch = new DocumentFaceMatch(result);

            var isValid = faceMatch.IsSameFaceAsync(request.Confidence);

            return isValid ? new Result(true, "Show! Sua selfie e seu documento são compatíveis.") 
                       : new Result(false, "Ops! Sua selfie e seu documento não são compatíveis.");
        }
    }
}
