using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Rekognition.MediatR.Rules;
using Sample.Rekognition.MediatR.Rules.Requests;

namespace Sample.Rekognition.MediatR.Pipeline
{
    public class DocumentFrontHandler : IPipelineBehavior<Message, Result>
    {
        private readonly IDetectLabelsService _service;

        public DocumentFrontHandler(IDetectLabelsService service) { _service = service; }

        public async Task<Result> Handle(Message request, CancellationToken cancellationToken, RequestHandlerDelegate<Result> next)
        {
            var image = $"{request.Document}/{request.Images.FirstOrDefault(img => img.Equals("document-front.jpeg"))}";

            var result = await _service.AnalisyAsync(image);

            var document = new Document(Side.Front, result);

            var isValid = document.IsValidDocument(request.Confidence);

            if (isValid) return await next();

            return new Result(false, "Seu documento parece não ser válido");
        }
    }
}
