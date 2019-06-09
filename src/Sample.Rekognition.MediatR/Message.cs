using System.Collections.Generic;
using MediatR;

namespace Sample.Rekognition.MediatR
{
    public class Message : IRequest<Result>
    {
        public string Document { get; set; }
        public ICollection<string> Images { get; set; }

        public float Confidence { get; set; }
    }
}