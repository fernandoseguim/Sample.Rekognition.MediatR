using System.Collections.Generic;
using System.Linq;
using Amazon.Rekognition.Model;

namespace Sample.Rekognition.MediatR.Rules
{
    public class Document : Image
    {
        private static readonly string[] ValidDocumentFrontLabels = { "Human", "Person" };
        private static readonly string[] ValidDocumentBackLabels = { "Text" };

        private static readonly string[] ValidDocumentLabels = { "Document", "Id Cards", "License", "Driving License" };

        public Document(Side side, ICollection<Label> labels) : base(labels) => Side = side;

        public Side Side { get; }

        public bool IsValidDocument(float confidence) => Side is Side.Front ? HasDocumentFrontLabel(confidence) && HasDocumentLabel(confidence) : HasDocumentBackLabel(confidence) && HasDocumentLabel(confidence);

        private bool HasDocumentFrontLabel(float confidence) => Labels
                                                        .Where(label => label.Confidence > confidence)
                                                        .Any(label => ValidDocumentFrontLabels.Any(validLabel => validLabel.Equals(label.Name)));

        private bool HasDocumentBackLabel(float confidence) => Labels
                                                               .Where(label => label.Confidence > confidence)
                                                               .Any(label => ValidDocumentBackLabels.Any(validLabel => validLabel.Equals(label.Name)));

        private bool HasDocumentLabel(float confidence) => Labels
                                                               .Where(label => label.Confidence > confidence)
                                                               .Any(label => ValidDocumentLabels.Any(validLabel => validLabel.Equals(label.Name)));
    }

    public class Image
    {
        public Image(ICollection<Label> labels) { Labels = labels; }

        public ICollection<Label> Labels { get; }
    }

    public enum Side
    {
        Front,
        Back
    }
}
